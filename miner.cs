
// When in the side drill mode, we limit the angle of the drill rotor
// to drill a hole within a given set of angles
const bool LIMIT_DRILL_ROTOR_ANGLE = true;
const float DRILL_ROTOR_UPPER = 240f; //10
const float DRILL_ROTOR_LOWER = 140f;
const float SIDE_DRILL_PISTON_VELOCITY = 0.0035f;


// Merge block that controls all landers
const string MERGE_LANDERS = "MERGE_Landers";
// Merge block that controls the drill
const string MERGE_DRILL_EXECUTE = "MERGE_DrillExecute";
// Merge block that controls the drill
const string MERGE_DRILL_RETRACT = "MERGE_DrillRetract";
// Merge block that controls the drill mode true = down, false = side
const string MERGE_DRILL_MODE = "MERGE_DrillMode";
// Inner rotor of the drill
const string DRILL_INNER_ROTOR = "DRILL_InnerRotor";
// Outer rotor of the drill
const string DRILL_OUTER_ROTOR = "DRILL_OuterRotor";
// The drill itself
const string DRILL = "DRILL_Bottom";
// The control seat
const string CONTROL_SEAT = "CONTROL_Main";
// The top LCD
const string LCD_TOP = "LCD_Top";
// The bottom LCD
const string LCD_BOTTOM = "LCD_Bottom";
// Depth camera -> to track and report on the distance of the drill head to the surface
const string DEPTH_CAMERA = "CAMERA_Depth";


// +++ GROUP NAMES +++
// group that contains all the drill pistons
const string DRILL_PISTONS = "DRILL_PISTONS";
// Group that contains all the lander blocks, including all pistons and landing gears
const string LANDERS = "LANDERS";
// group that contains all the cargo containers for drill ore
const string CARGO = "CARGO";
// group that contains all the side drills
const string SIDE_DRILLS = "SIDE_DRILLS";
// group that contains all the side drill pistons
const string SIDE_DRILL_PISTONS = "SIDE_DRILL_PISTONS";

// +++ CONSTANTS +++ Only change these if you know what you're doing
const string SHIP_NAME = "Gondor_01";
const float DRILL_PISTON_VELOCITY = 0.0005f;
// Spirograph for the win!!!
const float DRILL_ROTOR_VELOCITY = 0.25f;
const float DRILL_RATIO = 8f;
const float MAX_SCAN_DISTANCE = 50f; // Maximum distance to scan in meters
const float DRILL_QUICK_LOWER_DISTANCE = 10f; // We can quickly lower the drill to the ground if we're close

IMyCockpit controller;
IMyTextPanel lcdTop;
IMyTextPanel lcdBottom;
IMyCameraBlock depthCamera;
IMyBeacon beacon;
List<IMyCargoContainer> cargoContainers = new List<IMyCargoContainer>();

public enum DrillState
{
    Drilling,
    Idle,
    Retracting,
    Retracted,
    Error
}
public enum LanderState
{
    Deploying,
    Deployed,
    Retracting,
    Retracted,
    Error
}

DrillManager drillManager;

// globals
double depthCameraDistanceToGround = 0f;
bool isCargoFull = true;

List<string> bottomLcdText = new List<string>();
List<string> topLcdText = new List<string>();
List<string> controller1Text = new List<string>();

public class LanderManager
{
    private readonly Program program;
    private Dictionary<int, LanderGroup> landerGroups = new Dictionary<int, LanderGroup>();
    public LanderState currentState = LanderState.Retracted;
    public IMyShipMergeBlock mergeLanders;
    bool allLandersLocked = true;
    bool allPistonsRetracted = true;
    public string stateMessage = "";

    public LanderManager(Program program)
    {
        this.program = program;
        mergeLanders = program.GridTerminalSystem.GetBlockWithName(MERGE_LANDERS) as IMyShipMergeBlock;
        InitLanderBlocks();
    }

    // Initialize lander groups
    private void InitLanderBlocks()
    {
        IMyBlockGroup foundLanders = program.GridTerminalSystem.GetBlockGroupWithName(LANDERS);
        if (foundLanders != null)
        {
            foundLanders.GetBlocksOfType<IMyCubeBlock>(null, (block) =>
            {
                string blockName = block.CustomName;
                int landerNumber = int.Parse(blockName.Substring(blockName.Length - 1));

                IMyLandingGear landingGear = block as IMyLandingGear;
                if (landingGear != null)
                {
                    if (!landerGroups.ContainsKey(landerNumber))
                    {
                        landerGroups.Add(landerNumber, new LanderGroup());
                    }
                    landerGroups[landerNumber].gear = landingGear;
                }

                IMyPistonBase piston = block as IMyPistonBase;
                if (piston != null)
                {
                    if (!landerGroups.ContainsKey(landerNumber))
                    {
                        landerGroups.Add(landerNumber, new LanderGroup());
                    }
                    landerGroups[landerNumber].piston = piston;
                }
                return true;
            });
        }
    }

    public void Main()
    {
        program.bottomLcdText.Add("Landing Gear:");
        allLandersLocked = true;
        allPistonsRetracted = true;
        foreach (var landerGroup in landerGroups)
        {
            program.bottomLcdText.Add(
                landerGroup.Value.piston.CustomName + " "
                + " Locked = " + landerGroup.Value.gear.IsLocked.ToString()
            );

            landerGroup.Value.handleState(program, mergeLanders);
            if (!landerGroup.Value.gear.IsLocked)
            {
                allLandersLocked = false;
            }
            if (landerGroup.Value.piston.CurrentPosition > 0f)
            {
                allPistonsRetracted = false;
            }
        }

        checkState();
    }

    public void checkState()
    {
        stateMessage = "";
        if (allLandersLocked)
        {
            currentState = LanderState.Deployed;
            stateMessage = "Ready to drill";
        }
        else if (allPistonsRetracted)
        {
            currentState = LanderState.Retracted;
        }
        else if (landerGroups.Values.Any(group => group.currentState == "retracting"))
        {
            currentState = LanderState.Retracting;
        }
        else if (landerGroups.Values.Any(group => group.currentState == "deploying"))
        {
            currentState = LanderState.Deploying;

        }
        else
        {
            if (landerGroups.Values.Any(group => group.currentState == "drilling"))
            {
                stateMessage = "Cannot change lander state while drilling";
            }
            else
            {
                stateMessage = "Unknown error";
            }
            currentState = LanderState.Error;
        }
    }
}

public class DrillManager
{
    private readonly Program program;
    IMyMotorAdvancedStator drillInnerRotor;
    IMyMotorAdvancedStator drillOuterRotor;
    IMyShipMergeBlock mergeDrillExecute; // key 4
    IMyShipMergeBlock mergeDrillRetract; // key 5
    IMyShipMergeBlock mergeDrillMode; // key 6
    IMyShipDrill drill;
    List<IMyShipDrill> sideDrills = new List<IMyShipDrill>();
    List<IMyPistonBase> sideDrillPistons = new List<IMyPistonBase>();
    public DrillState currentState = DrillState.Retracted;
    List<IMyPistonBase> drillPistons = new List<IMyPistonBase>();
    bool pistonsRetracted = true;
    float drillOuterRotorTargetVelocity;
    public string stateMessage = "";

    private bool lastMergeEnabled = false;
    private bool lastCargoFull = false;
    private float calculatedSideDrillPistonVelocity;
    private bool sideDrillDirectionSwapAction = false;

    public DrillManager(Program program)
    {
        this.program = program;
        mergeDrillExecute = program.GridTerminalSystem.GetBlockWithName(MERGE_DRILL_EXECUTE) as IMyShipMergeBlock;
        if (mergeDrillExecute == null)
        {
            throw new Exception($"Block: {MERGE_DRILL_EXECUTE} not found");
        }
        mergeDrillRetract = program.GridTerminalSystem.GetBlockWithName(MERGE_DRILL_RETRACT) as IMyShipMergeBlock;
        if (mergeDrillRetract == null)
        {
            throw new Exception($"Block: {MERGE_DRILL_RETRACT} not found");
        }

        mergeDrillMode = program.GridTerminalSystem.GetBlockWithName(MERGE_DRILL_MODE) as IMyShipMergeBlock;
        if (mergeDrillMode == null)
        {
            throw new Exception($"Block: {MERGE_DRILL_MODE} not found");
        }

        drillInnerRotor = program.GridTerminalSystem.GetBlockWithName(DRILL_INNER_ROTOR) as IMyMotorAdvancedStator;
        if (drillInnerRotor == null)
        {
            throw new Exception($"Block: {DRILL_INNER_ROTOR} not found");
        }
        drillOuterRotor = program.GridTerminalSystem.GetBlockWithName(DRILL_OUTER_ROTOR) as IMyMotorAdvancedStator;
        if (drillOuterRotor == null)
        {
            throw new Exception($"Block: {DRILL_OUTER_ROTOR} not found");
        }

        drill = program.GridTerminalSystem.GetBlockWithName(DRILL) as IMyShipDrill;
        if (drill == null)
        {
            throw new Exception($"Block {DRILL} not found");
        }
        // get drill pistons from group name "DRILL_Pistons"
        IMyBlockGroup drillPistonsGroup = program.GridTerminalSystem.GetBlockGroupWithName(DRILL_PISTONS);
        if (drillPistonsGroup == null)
        {
            throw new Exception($"Block group: {DRILL_PISTONS} not found");
        }
        drillPistonsGroup.GetBlocksOfType<IMyPistonBase>(drillPistons);

        IMyBlockGroup sideDrillPistonsGroup = program.GridTerminalSystem.GetBlockGroupWithName(SIDE_DRILL_PISTONS);
        if (sideDrillPistonsGroup == null)
        {
            throw new Exception($"Block group: {SIDE_DRILL_PISTONS} not found");
        }
        sideDrillPistonsGroup.GetBlocksOfType<IMyPistonBase>(sideDrillPistons);


        IMyBlockGroup sideDrillsGroup = program.GridTerminalSystem.GetBlockGroupWithName(SIDE_DRILLS);
        if (sideDrillsGroup == null)
        {
            throw new Exception($"Block group: {SIDE_DRILLS} not found");
        }
        sideDrillsGroup.GetBlocksOfType<IMyShipDrill>(sideDrills);

        drillOuterRotorTargetVelocity = (float)(DRILL_ROTOR_VELOCITY * DRILL_RATIO);
        /**
        const float DRILL_ROTOR_UPPER = 240f; //10
        const float DRILL_ROTOR_LOWER = 180f;
        The side drill velocity assumes that the drill rotor is at 360 degrees
        so we need to scale the velocity based on the angle of the drill rotor
        */
        float angleDifference = DRILL_ROTOR_UPPER - DRILL_ROTOR_LOWER;
        if (DRILL_ROTOR_UPPER < DRILL_ROTOR_LOWER)
        {
            angleDifference = (360f + DRILL_ROTOR_UPPER) - DRILL_ROTOR_LOWER;
        }
        calculatedSideDrillPistonVelocity = SIDE_DRILL_PISTON_VELOCITY + (SIDE_DRILL_PISTON_VELOCITY * (1 - (angleDifference / 360f)));

        program.Echo("Calculated Side Drill Piston Velocity: " + calculatedSideDrillPistonVelocity.ToString("F5"));

        program.Echo("Drill Outer Rotor Target Velocity: " + drillOuterRotorTargetVelocity.ToString("F2"));
    }

    public void Main()
    {

        double outerRotorAngle = (drillOuterRotor.Angle * 180 / Math.PI) % 360f;
        double innerRotorAngle = (drillInnerRotor.Angle * 180 / Math.PI);

        // good to drill
        if (lastMergeEnabled != mergeDrillExecute.Enabled)
        {
            if (mergeDrillExecute.Enabled && !program.isCargoFull)
            {
                // If we are drilling then turn the retract merge off
                stateMessage = "Starting drill, merge drill action";
                startDrill();
            }
            else
            {
                stateMessage = "Cannot start drill while cargo is full";
                stopDrill();
            }
            lastMergeEnabled = mergeDrillExecute.Enabled;
        }

        // putting cargo check here to prevent the drill from running when the cargo is full
        if (lastCargoFull != program.isCargoFull)
        {
            if (program.isCargoFull && mergeDrillExecute.Enabled)
            {
                startDrill();
                stateMessage = "Cargo is full, stopping drill";
            }
            else
            {
                stopDrill();
                stateMessage = "Cargo is empty, starting drill";
            }
            lastCargoFull = program.isCargoFull;
        }


        if (LIMIT_DRILL_ROTOR_ANGLE && mergeDrillExecute.Enabled && !mergeDrillMode.Enabled && !sideDrillDirectionSwapAction)
        {
            sideDrillDirectionSwapAction = true;
            // rotate the dril back and forth betwen the upper and lower angles
            if (innerRotorAngle >= DRILL_ROTOR_UPPER)
            {
                // WE ARE CURRENTLY RUNNING IN A POSITIVE ROTOR VELOCITY
                // WE NEED TO CHANGE THE DIRECTION OF THE ROTOR VELOCITY
                // wait 1 seconds before changing the direction
                wait(1000);
                drillInnerRotor.TargetVelocityRPM = -DRILL_ROTOR_VELOCITY;
            }
            else if (innerRotorAngle <= DRILL_ROTOR_LOWER)
            {
                // WE ARE CURRENTLY RUNNING IN A NEGATIVE ROTOR VELOCITY
                // WE NEED TO CHANGE THE DIRECTION OF THE ROTOR VELOCITY
                wait(1000);
                drillInnerRotor.TargetVelocityRPM = DRILL_ROTOR_VELOCITY;
            }
            sideDrillDirectionSwapAction = false;
        }

        foreach (var piston in drillPistons)
        {
            program.topLcdText.Add(piston.CustomName + " " + (100f * (piston.CurrentPosition / 10f)).ToString("F2") + "%");
        }

        foreach (var piston in sideDrillPistons)
        {
            program.topLcdText.Add(piston.CustomName + " " + (100f * (piston.CurrentPosition / 10f)).ToString("F2") + "%");
        }

        // print rotor angles
        program.topLcdText.Add("Rotor Angles: "
            + " Inner: " + innerRotorAngle.ToString("F2")
            + " / Outer: " + outerRotorAngle.ToString("F2")
        );
    }

    public void wait(int seconds)
    {
        long iterations = seconds * 100000000;
        for (long i = 0; i < iterations; i++)
        {
            // waiting
        }
    }

    private void startDrill()
    {
        mergeDrillRetract.Enabled = false;
        drill.Enabled = true;

        if (mergeDrillMode.Enabled)
        {
            drillInnerRotor.TargetVelocityRPM = DRILL_ROTOR_VELOCITY;
            // drill down
            drillOuterRotor.TargetVelocityRPM = drillOuterRotorTargetVelocity;
            foreach (var piston in drillPistons)
            {
                piston.Velocity = DRILL_PISTON_VELOCITY;
            }
        }
        else
        {
            drillInnerRotor.TargetVelocityRPM = 0.4f;
            // drill side
            foreach (var sideDrill in sideDrills)
            {
                sideDrill.Enabled = true;
            }
            foreach (var piston in sideDrillPistons)
            {
                piston.Velocity = calculatedSideDrillPistonVelocity;
            }
        }
    }

    private void stopDrill()
    {
        drillInnerRotor.TargetVelocityRPM = 0f;
        drillOuterRotor.TargetVelocityRPM = 0f;
        drill.Enabled = false;
        foreach (var piston in drillPistons)
        {
            piston.Velocity = 0f;
        }
        foreach (var sideDrill in sideDrills)
        {
            sideDrill.Enabled = false;
        }
        foreach (var piston in sideDrillPistons)
        {
            piston.Velocity = 0f;
        }
    }

    private void retractDrill(double outerRotorAngle)
    {
        drillInnerRotor.TargetVelocityRPM = 0f;
        if (outerRotorAngle > 185f || outerRotorAngle < 175f)
        {
            drillOuterRotor.TargetVelocityRPM = 0.4f;
        }
        else
        {
            drillOuterRotor.TargetVelocityRPM = 0f;
        }
        drill.Enabled = false;
        foreach (var piston in drillPistons)
        {
            piston.Velocity = -0.6f;
        }
    }
}

public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update10; // Run every 10 ticks

    controller = GridTerminalSystem.GetBlockWithName("Control Seat") as IMyCockpit;
    if (controller == null)
    {
        throw new Exception($"Block {CONTROL_SEAT} not found");
    }
    lcdTop = GridTerminalSystem.GetBlockWithName("LCD_Top") as IMyTextPanel;
    if (lcdTop == null)
    {
        throw new Exception($"Block {LCD_TOP} not found");
    }
    lcdBottom = GridTerminalSystem.GetBlockWithName("LCD_Bottom") as IMyTextPanel;
    if (lcdBottom == null)
    {
        throw new Exception($"Block {LCD_BOTTOM} not found");
    }

    // get the first beacon in the grid
    List<IMyBeacon> beacons = new List<IMyBeacon>();
    GridTerminalSystem.GetBlocksOfType<IMyBeacon>(beacons);
    if (beacons.Count > 0)
    {
        beacon = beacons[0];
    }
    else
    {
        throw new Exception($"No beacons found in grid");
    }

    IMyBlockGroup cargoGroup = GridTerminalSystem.GetBlockGroupWithName("CARGO");
    if (cargoGroup != null)
    {
        cargoGroup.GetBlocksOfType<IMyCargoContainer>(cargoContainers);
    }
    else
    {
        throw new Exception($"Block group {CARGO} not found");
    }

    depthCamera = GridTerminalSystem.GetBlockWithName(DEPTH_CAMERA) as IMyCameraBlock;
    if (depthCamera == null)
    {
        throw new Exception($"Block {DEPTH_CAMERA} not found");
    }
    depthCamera.EnableRaycast = true;

    // Write instructions for user
    controller.GetSurface(1).WriteText(
        "INSTRUCTIONS:\n"
        + "4) Start Drilling - Toggle (Pause/Start)\n"
        + "5) Retract Drill\n"
        + "7) Toggle Landers - (Deploy/Retract)\n"
    );

    // landerManager = new LanderManager(this);
    drillManager = new DrillManager(this);
}

// Report distance to the ground using the depth camera
private void handleDepthCamera()
{
    string depthReadout = "Camera distance to ground: N/A";
    depthCameraDistanceToGround = 99999999f; // prevent crashing
    if (depthCamera.CanScan(MAX_SCAN_DISTANCE))
    {
        MyDetectedEntityInfo hit = depthCamera.Raycast(MAX_SCAN_DISTANCE, 0, 0);
        if (!hit.IsEmpty())
        {
            depthCameraDistanceToGround = Vector3D.Distance(depthCamera.GetPosition(), hit.HitPosition.Value);
            depthReadout = $"Camera distance to ground: {depthCameraDistanceToGround:F2}m";
        }
    }
    bottomLcdText.Add(depthReadout);
}

private void reportTilt()
{
    Vector3D gravity = Vector3D.Normalize(controller.GetNaturalGravity());
    Vector3D up = controller.CubeGrid.WorldMatrix.Up;

    // Calculate dot product between grid up and inverse gravity
    // This gives us how aligned our up vector is with the opposite of gravity
    double levelness = Vector3D.Dot(up, -gravity);

    // Calculate forward and right tilt
    double forwardTilt = Math.Asin(Vector3D.Dot(controller.CubeGrid.WorldMatrix.Forward, -gravity)) * 180 / Math.PI;
    double rightTilt = Math.Asin(Vector3D.Dot(controller.CubeGrid.WorldMatrix.Right, -gravity)) * 180 / Math.PI;

    bottomLcdText.Add("Tilt (degrees):"
        + "\n   Forward/Back: " + forwardTilt.ToString("F2")
        + "\n   Right/Left: " + rightTilt.ToString("F2")
    );
}

private void checkCargo()
{
    float accumulatedPercentageFull = 0f;
    isCargoFull = true;
    foreach (var cargoContainer in cargoContainers)
    {
        IMyInventory inventory = cargoContainer.GetInventory();
        MyFixedPoint totalVolume = inventory.CurrentVolume;
        float percentageFull = 100f * (float)totalVolume.ToIntSafe() / (float)inventory.MaxVolume.ToIntSafe();
        accumulatedPercentageFull += percentageFull;
        if (percentageFull < 99.5f)
        {
            isCargoFull = false;
        }
    }

    float averagePercentageFull = accumulatedPercentageFull / cargoContainers.Count;
    bottomLcdText.Add("-----------\nCargo Volume: " + averagePercentageFull.ToString("F2") + "%\n-----------");

    // if (isCargoFull)
    // {
    //     beacon.CustomName = SHIP_NAME + "_Cargo_Full";
    // }
    // else
    // {
    //     beacon.CustomName = SHIP_NAME + "_Cargo_Empty";
    // }

    beacon.CustomName = SHIP_NAME + "_Cargo_" + averagePercentageFull.ToString("F2") + "%";
}

public void Main(string argument, UpdateType updateSource)
{
    // When the remote control is activated, the motor will start spinning
    controller1Text.Clear();
    controller.GetSurface(0).WriteText("");
    bottomLcdText.Clear();
    bottomLcdText.Add("LANDER STATS:");
    topLcdText.Clear();
    topLcdText.Add("DRILL STATS:");

    reportTilt();
    checkCargo();
    handleDepthCamera();
    // landerManager.Main();
    drillManager.Main();

    lcdBottom.WriteText("");
    lcdBottom.WriteText(string.Join("\n", bottomLcdText));

    lcdTop.WriteText("");
    lcdTop.WriteText(string.Join("\n", topLcdText));

    // report on all functional/controllable states
    controller1Text.Add("FUNCTIONAL STATES: \n");
    controller1Text.Add("Drill: " + drillManager.currentState.ToString() + " " + drillManager.stateMessage);
    // controller1Text.Add("Lander: " + landerManager.currentState.ToString() + " " + landerManager.stateMessage);
    controller.GetSurface(0).WriteText(string.Join("\n", controller1Text));
}

public class LanderGroup
{
    public IMyPistonBase piston;
    public IMyLandingGear gear;
    public string currentState = "idle";
    // returns the state of the lander group
    public void handleState(Program program, IMyShipMergeBlock mergeLanders)
    {
        if (program.drillManager.currentState != DrillState.Retracted)
        {
            // cant do anything while drilling
            piston.Velocity = 0f;
            currentState = "drilling";
            return;
        }

        if (mergeLanders.Enabled)
        {
            if (gear.IsLocked)
            {
                piston.Velocity = 0f;
                currentState = "idle";
            }
            else
            {
                piston.Velocity = 0.6f;
                currentState = "deploying";
            }
        }
        else
        {
            gear.Unlock();
            piston.Velocity = -0.6f;
            currentState = "retracting";
        }
    }
}
