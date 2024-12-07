
/*
PISTON AND ROTOR KEYBINDS
By Trekker
*
Contributors:
Maril (Mouse movement controls)
*/
const string VER = "v1.2.9";

/*
        Instructions for use:
        {
        1) Create a group called "PARK" containing all the ship controllers, motors, pistons, and suspensions you want this script to control.
        You can change the name of the group at the beginning of the script.

        2) Compile this script.
        It may give some warnings if the ship controllers control thrusters or tires, which will not prevent the script from running,
        but could be risky depending on your build.

        3) Go into the customData of the rotors and pistons. You will see this or something similar:

        ;Provide the RPM of the part when the corresponding key is pressed.
        [PARK:Main]
        Forward=0
        Backward=0
        Left=0
        Right=0
        Up=0
        Down=0
        Roll Left=0
        Roll Right=0
        Pitch Up=0
        Pitch Down=0
        Yaw Left=0
        Yaw Right=0
        Return Position=0
        Return Speed=0
        Return Order Priority=0

        Each value under the section under "[PARK:Main]" is a direction input and a corresponding velocity value.
        For example, if you want your part to move forward and back when you press the forward and back keys,
        use "Forward=5" and "Backward=-5".
        Keep in mind the sign you should use is going to depend on your build.
        5 and -5 represent 5RPM and 5m/s on their respective motor and piston.
        The system is additive. If you have multiple keys pressed down which effect the part in different ways,
        the effect is summed up and it uses total velocity.

        By default, the script ramps up in speed until it hits the defined speed over 3 ticks. This should make fine adjustments easier.
        You can change this at the beginning of the script.

        Thanks to an addition added in by Maril, you can also use mouse controls to move components run by PARK.
        The last four settings correspond to that.

        Recently, I added in Return Angle, Return Speed, and Return Order Priority.
        If you run the command "Return", it will return to the angle set, at a set speed, prioritized lowest first.
        So if you want a part to return to 15 degrees at a speed of 2 RPM after the first set of the controlled parts moved, then you can do:
        "Return Angle=15"
        "Return Speed=2"
        "Return Order Priority=1" (0 goes first, 1 is second.)
        Setting Return Order Priority to -1 causes the part to return to neutral positon immediately after control is released.

        4) Set the blocks to the values you want, and recompile to get the script to recognize changes.

        5) Hop into your ship controller, test it out, and tweak as neccessary.
        }

        Advanced: Profiles.
        {
        Look in the customData of the PB. You will find "Main" on one line by itself. This is the automatically generated profile it
        refers to when first compiled. A profile defines which set of instructions to use. You can set up multiple profiles to control
        multiple sets of joints on a crane arm, or use finer controls, etc.

        To set up a profile:

        1) go in the PB's customData and enter the name of the new profile on a newline and recompile.
        The PB will automatically take the new profile and generate a new entry similar to the one shown above.

        2) Set up as neccessary.
        }

        Commands:
        {
        Commands are not case sensitive (for now) and control how the script operates.

        "enable" enables the script to take commands from the ship controller.
        "disable" disables the script from taking commands from the ship controller.
        "toggle" toggles the enabled state on and off.
        "return" Returns the parts to the defined zero position provided they have a return speed.
        "mult <ratio>" sets the multiplier that effects the speed of configured parts. "mult 2" would double speed, for example.

        Anything else will be regarded as a command to switch profiles. For example, if you have a profile called "Wrist",
        you can tell the script to respond according to that profile with "wrist".

        Note: These settings are saved and the script will not reset after a load.
        }
        */

readonly string groupName = "PARK";// Change this to change the name of the group.

readonly int maxRampTicks = 3;// The number of ticks it will take to ramp up to full speed. Set this to a minimum of 1 to ignore.

readonly string commTag = "";// Put something between the quotation marks to transmit/recieve on that channel.

readonly float returnDeadZone = 0.0025f;// The level of precision the parts use before stopping and are considered zeroed in.

readonly bool togglePistonPower = false;// This can reduce the level of noise made by pistons by turning them off when not moving.

// Do not edit below unless you know what you are doing!
readonly List<IMyShipController> controllers = new List<IMyShipController>();
readonly List<Motor> motors = new List<Motor>();
readonly List<LinearMotor> linearMotors = new List<LinearMotor>();
readonly List<Suspension> suspensions = new List<Suspension>();
readonly List<string> profiles = new List<string>();
int profile = 0;
readonly List<string> sections = new List<string>();

MoveFlag flag = MoveFlag.None;// The flag indicating the binary movement values.
ushort prev = 0;// The previous flag state. Used to prevent the same button press from activating multiple times.
ushort flagMask = 0;

float multiplier = 1;
int rampTicks = 1;

readonly List<Motor> returningMotors = new List<Motor>();
readonly List<LinearMotor> returningLinearMotors = new List<LinearMotor>();

readonly List<Motor> autoReturningMotors = new List<Motor>();
readonly List<LinearMotor> autoReturningLinearMotors = new List<LinearMotor>();
//IMyBroadcastListener listener = null;
public Program()
{
    string tag = Me.GetOwnerFactionTag();
    if (tag == "")
        Echo("Warning: PB not part of a faction.");
    else
        Echo($"Initializing PARK ({tag})...");
    IMyBlockGroup group = GridTerminalSystem.GetBlockGroupWithName(groupName);
    if (group == null)
        throw new NullReferenceException($"There is no group with the name \"{groupName}\".\nEnsure you have all blocks you wish to control under this group and recompile.");
    if (Me.CustomData == "")
        Me.CustomData = "Main";
    string[] temp = Me.CustomData.Split('\n');
    foreach (string value in temp)
    {
        profiles.Add(value);
        sections.Add($"PARK:{value}");
    }
    MyIni ini = new MyIni();
    group.GetBlocksOfType<IMyTerminalBlock>(null, b => InitBlock(b, ini));
    if (controllers.Count == 0)
        throw new NullReferenceException($"There are no controllers in \"{groupName}\".\nEnsure you have at least one controller under this group and recompile.");
    if (motors.Count == 0 && linearMotors.Count == 0 && suspensions.Count == 0)
        throw new NullReferenceException($"There are no rotors or pistons in \"{groupName}\".\nEnsure you have at least one rotor or piston under this group and recompile.");
    if (Storage.Length > 0)
    {
        ini.Clear();
        ini.TryParse(Storage);
        enabled = ini.Get("PARK", "Enabled").ToBoolean();
        TryGetProfileInt(ini.Get("PARK", "Current Profile").ToString(), ref profile);
        if (ini.ContainsKey("PARK", "Multiplier"))
            multiplier = (float)ini.Get("PARK", "Multiplier").ToDouble();
    }
    if (Me.SurfaceCount > 0)
        Me.GetSurface(0).ContentType = ContentType.TEXT_AND_IMAGE;
    else
        Echo($"There is no display on this PB. Text output disabled.");
    if (commTag != "")
    {
        //listener = IGC.RegisterBroadcastListener(commTag);
        //listener.SetMessageCallback();

        //Echo($"Transmitting on channel \"{commTag}\"");
    }
    Runtime.UpdateFrequency = UpdateFrequency.Update10;// This can be changed to Update1, however, it will take up more resources.
}
bool InitBlock<T>(T block, MyIni ini) where T : IMyTerminalBlock
{
    IMyMotorStator motor = block as IMyMotorStator;
    if (motor != null)
    {
        motors.Add(new Motor(motor, ini, sections, ref flagMask));
        return false;
    }
    IMyPistonBase piston = block as IMyPistonBase;
    if (piston != null)
    {
        linearMotors.Add(new LinearMotor(piston, ini, sections, ref flagMask));
        return false;
    }
    IMyMotorSuspension suspension = block as IMyMotorSuspension;
    if (suspension != null)
    {
        suspensions.Add(new Suspension(suspension, ini, sections, ref flagMask));
        return false;
    }
    IMyShipController controller = block as IMyShipController;
    if (controller != null)
    {
        controllers.Add(controller);
        if (!controller.CanControlShip)
            Echo($"Warning, controller \"{controller.CustomName}\" cannot control ship.");
        if (controller.IsUnderControl)
            Echo($"I see you are already inside {controller.CustomName}.");
        if (controller.ControlThrusters)
            Echo($"Warning, controller \"{controller.CustomName}\" is set to control thrusters.");
        if (controller.ControlWheels && controller.HasWheels)
            Echo($"Warning, controller \"{controller.CustomName}\" is set to control wheels.");
        return false;
    }
    return false;
}


bool TryGetProfileInt(string profileName, ref int profileInt)
{
    for (int i = 0; i < profiles.Count; i++)
    {
        if (profiles[i].Equals(profileName, StringComparison.OrdinalIgnoreCase))
        {
            profileInt = i;
            return true;
        }
    }
    return false;
}

public void Save()
{
    Storage = "";
    MyIni ini = new MyIni();
    ini.Set("PARK", "Enabled", enabled);
    ini.Set("PARK", "Current Profile", profiles[profile]);
    ini.Set("PARK", "Multiplier", multiplier);
    Storage = ini.ToString();
}

int counter = 0;
double runtimeMS;
bool enabled = true;
string enableStr = "";
int returnSeq = -1;
public void Main(string argument, UpdateType updateSource)
{
    // Accept commands.
    if (argument.Length > 0)
    {
        argument = argument.ToLower();
        bool acceptedCommand = false;
        switch (argument)
        {
            case "enable":
                enabled = true;
                acceptedCommand = true;
                break;
            case "disable":
                enabled = false;
                acceptedCommand = true;
                break;
            case "toggle":
                enabled = !enabled;
                acceptedCommand = true;
                break;
            case "return":
                returnSeq = 0;
                returningMotors.Clear();
                returningLinearMotors.Clear();
                acceptedCommand = true;
                break;

        }
        if (acceptedCommand == false)
        {
            if (argument.StartsWith("mult "))
            {
                float m;
                if (float.TryParse(argument.Substring(5), out m))
                    multiplier = m;
            }
            else if (!TryGetProfileInt(argument, ref profile))
                Echo($"Warning, there is no profile named \"{argument}\"");
        }
    }
    // Read inputs from controller.
    flag = MoveFlag.None;
    if (enabled)// Do not read cockpit information if the script is disabled. This will cause the script to stop movement.
    {
        foreach (IMyShipController controller in controllers)
        {
            if (controller.IsWorking && controller.IsUnderControl)
            {
                Read(controller); break;
            }
        }
    }

    ushort moveFlagShort = MoveFlagToUShort(flag);

    if ((updateSource & UpdateType.IGC) != 0)
    {// Alter the MoveFlag according to the ushort.

    }

    moveFlagShort &= flagMask;// Filter to prevent unused keys from interfering with movement.

    if (moveFlagShort != prev)
    {
        rampTicks = maxRampTicks;
    }


    if (moveFlagShort != 0)
        returnSeq = -1;
    // Return to center handling.
    if (returnSeq != -1)
    {
        if (returningMotors.Count == 0 && returningLinearMotors.Count == 0)
        {
            foreach (Motor m in motors)
            {
                if (m.GetReturnOrder(profile) == returnSeq)
                    returningMotors.Add(m);
            }
            foreach (LinearMotor l in linearMotors)
            {
                if (l.GetReturnOrder(profile) == returnSeq)
                    returningLinearMotors.Add(l);
            }
            if (returningMotors.Count == 0 && returningLinearMotors.Count == 0)
            {
                returnSeq = -1;
                RenderScreen("Returned to zero position.");
                return;
            }
        }
        bool isReturning = false;
        foreach (Motor m in returningMotors)
        {
            isReturning |= !m.MotorReturn(multiplier, profile, returnDeadZone);
            //Echo($"{m.motor.CustomName} {isReturning}");
        }
        foreach (LinearMotor l in returningLinearMotors)
        {
            isReturning |= !l.LinearMotorReturn(multiplier, profile, returnDeadZone, togglePistonPower);
            //Echo($"{l.linearMotor.CustomName} {isReturning}");
        }
        if (!isReturning)
        {
            returningMotors.Clear();
            returningLinearMotors.Clear();
            returnSeq++;
        }
        RenderScreen("Returning to zero position...");
        return;
    }
    // Input execution handling.
    if (rampTicks != 0)// Filter to ensure the script only sends updates to the blocks when the input changes.
    {
        if (motors.Count > 0)
        {
            foreach (Motor motor in motors)
            {
                if (motor.Actuate(moveFlagShort, profile, multiplier / rampTicks, returnDeadZone))
                {
                    if (!autoReturningMotors.Contains(motor))
                        autoReturningMotors.Add(motor);
                }
                else
                    autoReturningMotors.Remove(motor);
            }
        }
        if (linearMotors.Count > 0)
        {
            foreach (LinearMotor linearMotor in linearMotors)
            {
                if (linearMotor.Actuate(moveFlagShort, profile, multiplier / rampTicks, returnDeadZone, togglePistonPower))
                {
                    if (!autoReturningLinearMotors.Contains(linearMotor))
                        autoReturningLinearMotors.Add(linearMotor);
                }
                else
                    autoReturningLinearMotors.Remove(linearMotor);
            }
        }
        if (suspensions.Count > 0)
        {
            foreach (Suspension suspension in suspensions)
                suspension.Actuate(moveFlagShort, profile, multiplier);
        }
        prev = moveFlagShort;
        rampTicks--;
    }
    // Autoreturn handling.
    bool autoReturn = false;
    foreach (Motor m in autoReturningMotors)
    {
        autoReturn |= !m.MotorReturn(multiplier, profile, returnDeadZone);
    }
    if (!autoReturn)
        autoReturningMotors.Clear();
    autoReturn = false;
    foreach (LinearMotor l in autoReturningLinearMotors)
    {
        autoReturn |= !l.LinearMotorReturn(multiplier, profile, returnDeadZone, togglePistonPower);
    }
    if (!autoReturn)
        autoReturningLinearMotors.Clear();
    RenderScreen();
}

string lastDebug = "";
private void RenderScreen(string DebugStr = "")
{
    runtimeMS = AvgRuntimeMs();
    if (Me.SurfaceCount > 0 && counter++ >= 6 || lastDebug != DebugStr)
    {
        lastDebug = DebugStr;
        counter = 0;
        enableStr = enabled ? "Enabled" : "Disabled";
        Me.GetSurface(0).WriteText($"Piston And Rotor Keybinds {VER}\nRuntime: {runtimeMS:0.000}ms\nCurrent Profile: {profiles[profile]}\n{enableStr}\nKeyPresses: {flag}\n{DebugStr}");
    }
}

int rI = -1;
readonly double[] reading = new double[30];
double AvgRuntimeMs()
{
    if (++rI >= reading.Length)
        rI = 0;
    reading[rI] = Runtime.LastRunTimeMs;
    return reading.Average();
}

const float SENS = 0.35355f;// The dead zone to help seperate controller inputs.
void Read(IMyShipController controller)
{
    Vector3D vector = Vector3D.Normalize(controller.MoveIndicator);
    Vector2 rotation = Vector2.Normalize(controller.RotationIndicator);
    flag = MoveFlag.None;// Nothing has been pressed.
    if (vector.Z < -SENS)// Forward
        flag |= MoveFlag.Forward;
    else if (vector.Z > SENS)// Backward
        flag |= MoveFlag.Backward;
    if (vector.X < -SENS)// Left
        flag |= MoveFlag.StrafeLeft;
    else if (vector.X > SENS)// Right
        flag |= MoveFlag.StrafeRight;
    if (vector.Y > SENS)// Jump
        flag |= MoveFlag.Up;
    else if (vector.Y < -SENS)// Crouch
        flag |= MoveFlag.Down;
    if (controller.RollIndicator < -SENS)// Roll Left
        flag |= MoveFlag.RollLeft;
    else if (controller.RollIndicator > SENS)// Roll Right
        flag |= MoveFlag.RollRight;
    if (rotation.X < -SENS)// Pitch Down
        flag |= MoveFlag.PitchDown;
    else if (rotation.X > SENS)// Pitch Up
        flag |= MoveFlag.PitchUp;
    if (rotation.Y < -SENS)// Yaw Left
        flag |= MoveFlag.YawLeft;
    else if (rotation.Y > SENS)// Yaw Right
        flag |= MoveFlag.YawRight;
}
public ushort MoveFlagToUShort(MoveFlag moveFlag)
{
    ushort flagInt = 0;
    if ((moveFlag & MoveFlag.Forward) != 0)// Forward
        flagInt = 1;
    else if ((moveFlag & MoveFlag.Backward) != 0)// Backward
        flagInt |= 1 << 1;
    if ((moveFlag & MoveFlag.StrafeLeft) != 0)// Left
        flagInt |= 1 << 2;
    else if ((moveFlag & MoveFlag.StrafeRight) != 0)// Right
        flagInt |= 1 << 3;
    if ((moveFlag & MoveFlag.Up) != 0)// Jump
        flagInt |= 1 << 4;
    else if ((moveFlag & MoveFlag.Down) != 0)// Crouch
        flagInt |= 1 << 5;
    if ((moveFlag & MoveFlag.RollLeft) != 0)// Roll Left
        flagInt |= 1 << 6;
    else if ((moveFlag & MoveFlag.RollRight) != 0)// Roll Right
        flagInt |= 1 << 7;
    if ((moveFlag & MoveFlag.PitchUp) != 0)// Pitch Up
        flagInt |= 1 << 8;
    else if ((moveFlag & MoveFlag.PitchDown) != 0)// Pitch Down
        flagInt |= 1 << 9;
    if ((moveFlag & MoveFlag.YawLeft) != 0)// Yaw Left
        flagInt |= 1 << 10;
    else if ((moveFlag & MoveFlag.YawRight) != 0)// Yaw Right
        flagInt |= 1 << 11;
    return flagInt;
}
public enum MoveFlag : ushort
{
    None = 0,

    Forward = 1,
    Backward = 1 << 1,
    StrafeLeft = 1 << 2,
    StrafeRight = 1 << 3,
    Up = 1 << 4,
    Down = 1 << 5,
    RollLeft = 1 << 6,
    RollRight = 1 << 7,

    PitchUp = 1 << 8,// Added in mouse controls thanks to Maril.
    PitchDown = 1 << 9,
    YawLeft = 1 << 10,
    YawRight = 1 << 11
}

public class Controllable
{
    public List<Dictionary<ushort, float>> commands = new List<Dictionary<ushort, float>>();
    // TODO: Test multiple return positions across multiple profiles to ensure this fix worked.
    public float GetReturnPosition(int profile) { return commands[profile][1 << 12]; }
    public float GetReturnSpeed(int profile) { return commands[profile][1 << 13]; }
    public int GetReturnOrder(int profile) { return (int)commands[profile][1 << 14]; }

    public void LoadValues(MyIni ini, IMyTerminalBlock block, List<string> sections, ref ushort mask, string heading = "")
    {
        commands.Clear();// = new List<Dictionary<ushort, float>>();
        ini.Clear();
        bool update = false;
        if (block.CustomData.Length > 0 && ini.TryParse(block.CustomData))
        {
            foreach (string section in sections)
            {
                if (ini.ContainsSection(section))
                {

                    float f = (float)ini.Get(section, "Forward").ToDouble();
                    float b = (float)ini.Get(section, "Backward").ToDouble();
                    float l = (float)ini.Get(section, "Left").ToDouble();
                    float r = (float)ini.Get(section, "Right").ToDouble();
                    float u = (float)ini.Get(section, "Up").ToDouble();
                    float d = (float)ini.Get(section, "Down").ToDouble();

                    float rL = (float)ini.Get(section, "Roll Left").ToDouble();
                    float rR = (float)ini.Get(section, "Roll Right").ToDouble();

                    float pU = (float)ini.Get(section, "Pitch Up").ToDouble();
                    float pD = (float)ini.Get(section, "Pitch Down").ToDouble();
                    float yL = (float)ini.Get(section, "Yaw Left").ToDouble();
                    float yR = (float)ini.Get(section, "Yaw Right").ToDouble();

                    mask |= (ushort)(f != 0 ? 1 : 0);
                    mask |= (ushort)(b != 0 ? 1 << 1 : 0);
                    mask |= (ushort)(l != 0 ? 1 << 2 : 0);
                    mask |= (ushort)(r != 0 ? 1 << 3 : 0);
                    mask |= (ushort)(u != 0 ? 1 << 4 : 0);
                    mask |= (ushort)(d != 0 ? 1 << 5 : 0);

                    mask |= (ushort)(rL != 0 ? 1 << 6 : 0);
                    mask |= (ushort)(rR != 0 ? 1 << 7 : 0);

                    mask |= (ushort)(pU != 0 ? 1 << 8 : 0);
                    mask |= (ushort)(pD != 0 ? 1 << 9 : 0);
                    mask |= (ushort)(yL != 0 ? 1 << 10 : 0);
                    mask |= (ushort)(yR != 0 ? 1 << 11 : 0);

                    float rP = (float)ini.Get(section, "Return Position").ToDouble();
                    float rS = (float)ini.Get(section, "Return Speed").ToDouble();
                    float rO = ini.Get(section, "Return Order Priority").ToInt32();

                    Dictionary<ushort, float> cmds = new Dictionary<ushort, float>
                    {
                        { 1, f },
                        { 1 << 1, b },
                        { 1 << 2, l },
                        { 1 << 3, r },
                        { 1 << 4, u },
                        { 1 << 5, d },

                        { 1 << 6, rL },
                        { 1 << 7, rR },

                        { 1 << 8, pU },
                        { 1 << 9, pD },
                        { 1 << 10, yL },
                        { 1 << 11, yR },

                        { 1 << 12, rP },
                        { 1 << 13, rS },
                        { 1 << 14, rO }
                    };

                    //GetReturnPosition = (float)ini.Get(section, "Return Position").ToDouble();
                    //GetReturnSpeed = (float)ini.Get(section, "Return Speed").ToDouble();
                    //GetReturnOrder = ini.Get(section, "Return Order Priority").ToInt32();

                    commands.Add(cmds);
                    if (!ini.ContainsKey(section, "Pitch Up"))// For reverse compatability.
                    {
                        ini.Set(section, "Pitch Up", 0);
                        ini.Set(section, "Pitch Down", 0);
                        ini.Set(section, "Yaw Left", 0);
                        ini.Set(section, "Yaw Right", 0);
                        update = true;
                    }
                    if (!(this is Suspension) && !ini.ContainsKey(section, "Return Position"))
                    {
                        ini.Set(section, "Return Position", 0);
                        ini.Set(section, "Return Speed", 0);
                        ini.Set(section, "Return Order Priority", 0);
                    }
                    continue;
                }
                ini.Set(section, "Forward", 0);
                ini.SetSectionComment(section, heading);
                ini.Set(section, "Backward", 0);
                ini.Set(section, "Left", 0);
                ini.Set(section, "Right", 0);
                ini.Set(section, "Up", 0);
                ini.Set(section, "Down", 0);
                ini.Set(section, "Roll Left", 0);
                ini.Set(section, "Roll Right", 0);
                ini.Set(section, "Pitch Up", 0);
                ini.Set(section, "Pitch Down", 0);
                ini.Set(section, "Yaw Left", 0);
                ini.Set(section, "Yaw Right", 0);
                if (!(this is Suspension))
                {
                    ini.Set(section, "Return Position", 0);
                    ini.Set(section, "Return Speed", 0);
                    ini.Set(section, "Return Order Priority", 0);
                }
                update = true;
            }
            if (update)
                block.CustomData = ini.ToString();
            return;
        }
        foreach (string section in sections)
        {
            ini.Set(section, "Forward", 0);
            ini.SetSectionComment(section, heading);
            ini.Set(section, "Backward", 0);
            ini.Set(section, "Left", 0);
            ini.Set(section, "Right", 0);
            ini.Set(section, "Up", 0);
            ini.Set(section, "Down", 0);
            ini.Set(section, "Roll Left", 0);
            ini.Set(section, "Roll Right", 0);
            ini.Set(section, "Pitch Up", 0);
            ini.Set(section, "Pitch Down", 0);
            ini.Set(section, "Yaw Left", 0);
            ini.Set(section, "Yaw Right", 0);
            if (!(this is Suspension))
            {
                ini.Set(section, "Return Position", 0);
                ini.Set(section, "Return Speed", 0);
                ini.Set(section, "Return Order Priority", 0);
            }
        }
        block.CustomData = ini.ToString();
    }
}

public class Motor : Controllable
{
    public IMyMotorStator motor;
    readonly float maxVel;
    public Motor(IMyMotorStator motor, MyIni ini, List<string> sections, ref ushort mask)
    {
        this.motor = motor;
        maxVel = motor.GetMaximum<float>("Velocity") * MathHelper.RadiansPerSecondToRPM;
        LoadValues(ini, motor, sections, ref mask, "Provide the RPM of the part when the corresponding key is pressed.");
    }
    public bool Actuate(ushort flag, int profile, float multiplier, float deadZone)
    {
        int returnOrder = GetReturnOrder(profile);
        float impulse = 0;// The movement impulse of the motor. It is additive.
        if (flag == 0)
        {
            if (returnOrder == -1)
            {
                MotorReturn(multiplier, profile, deadZone);
                return true;
            }
            motor.TargetVelocityRPM = 0; return false;
        }
        bool hasCommand = false;
        for (ushort i = 0; i <= 11; i += 2)// Decompose the flag.
        {
            ushort mask = (ushort)(3 << i);//3 == 00000011b
            if ((flag & mask) != 0 && commands[profile].ContainsKey((ushort)(flag & mask)))
            {
                impulse += commands[profile].GetValueOrDefault((ushort)(flag & mask));
                hasCommand |= true;// impulse != 0;
            }
        }
        if (returnOrder == -1 && impulse == 0)
        {
            MotorReturn(multiplier, profile, deadZone);
            return true;
        }
        if (hasCommand)
            motor.TargetVelocityRPM = MathHelper.Clamp(impulse * multiplier, -maxVel, maxVel);
        return false;
    }
    /// <summary>
            /// Returns true when within deadzone.
            /// </summary>
    public bool MotorReturn(float multiplier, int profile, float deadZone)
    {
        float returnSpeed = GetReturnSpeed(profile);
        if (returnSpeed == 0)
            return true;
        float diffDeg = MathHelper.ToDegrees(MathHelper.WrapAngle(MathHelper.ToRadians(GetReturnPosition(profile)) - motor.Angle));
        if (Math.Abs(diffDeg) < deadZone)
        {
            motor.TargetVelocityRad = 0;
            return true;
        }
        float impulse = diffDeg - (motor.TargetVelocityRPM / 6);
        motor.TargetVelocityRPM = MathHelper.Clamp(impulse, -returnSpeed * multiplier, returnSpeed * multiplier);
        return false;
    }
}

public class LinearMotor : Controllable
{
    public IMyPistonBase linearMotor;
    public LinearMotor(IMyPistonBase linearMotor, MyIni ini, List<string> sections, ref ushort mask)
    {
        this.linearMotor = linearMotor;
        LoadValues(ini, linearMotor, sections, ref mask, "Provide the M/s of the part when the corresponding key is pressed.");
    }
    public bool Actuate(ushort flag, int profile, float multiplier, float deadZone, bool controlPower)
    {
        int returnOrder = GetReturnOrder(profile);
        float impulse = 0;// The movement impulse of the motor. It is additive.
        if (flag == 0)// No point deciphering an empty flag.
        {
            if (returnOrder == -1)
            {
                LinearMotorReturn(multiplier, profile, deadZone, controlPower);
                return true;
            }
            if (controlPower)
                linearMotor.Enabled = false;
            linearMotor.Velocity = 0;
            return false;
        }
        bool hasCommand = false;
        for (ushort i = 0; i <= 11; i += 2)// Decompose the flag.
        {
            ushort mask = (ushort)(3 << i);//3 == 00000011b
            if ((flag & mask) != 0 && commands[profile].ContainsKey((ushort)(flag & mask)))
            {
                impulse += commands[profile].GetValueOrDefault((ushort)(flag & mask));
                hasCommand |= true;//impulse != 0;
            }
        }
        if (returnOrder == -1 && impulse == 0)
        {
            LinearMotorReturn(multiplier, profile, deadZone, controlPower);
            return true;
        }
        if (controlPower)
            linearMotor.Enabled = hasCommand;
        if (hasCommand)
        {
            linearMotor.Velocity = MathHelper.Clamp(impulse * multiplier, -linearMotor.MaxVelocity, linearMotor.MaxVelocity);
        }
        return false;
    }
    /// <summary>
            /// Returns true when within deadzone.
            /// </summary>
    public bool LinearMotorReturn(float multiplier, int profile, float deadZone, bool controlPower)
    {
        float returnSpeed = GetReturnSpeed(profile);
        if (returnSpeed == 0)
        {
            if (controlPower)
                linearMotor.Enabled = false;
            return true;
        }
        float diff = GetReturnPosition(profile) - linearMotor.CurrentPosition;
        if (Math.Abs(diff) < deadZone)
        {
            linearMotor.Velocity = 0;
            if (controlPower)
                linearMotor.Enabled = false;
            return true;
        }
        if (controlPower)
            linearMotor.Enabled = true;
        linearMotor.Velocity = MathHelper.Clamp(diff * 6, -returnSpeed * multiplier, returnSpeed * multiplier);
        return false;
    }
}

public class Suspension : Controllable
{
    public IMyMotorSuspension suspension;
    public Suspension(IMyMotorSuspension suspension, MyIni ini, List<string> sections, ref ushort mask)
    {
        this.suspension = suspension;
        LoadValues(ini, suspension, sections, ref mask, "Provide the throttle of the part when the corresponding key is pressed.");

    }
    public void Actuate(ushort flag, int profile, float multiplier)
    {
        float impulse = 0;// The movement impulse of the motor. It is additive.
        if (flag == 0)
        {
            suspension.PropulsionOverride = 0; return;
        }
        for (byte i = 0; i <= 11; i += 2)// Decompose the flag.
        {
            ushort mask = (ushort)(3 << i);//3 == 00000011b
            if ((flag & mask) != 0 && commands[profile].ContainsKey((ushort)(flag & mask)))
                impulse += commands[profile].GetValueOrDefault((ushort)(flag & mask));
        }
        suspension.PropulsionOverride = MathHelper.Clamp(impulse * multiplier, -1, 1);
    }
}