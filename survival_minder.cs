IMyCockpit controller;
IMyTextPanel lcdTop;
IMyTextPanel lcdBottom;
IMyMotorStator motor;
IMyMotorAdvancedStator drillInnerRotor;
IMyMotorAdvancedStator drillOuterRotor;
IMyShipDrill drill;
// Spirograph for the win!!!
float drillRotorVelocity = 1.3f;
float drillRatio = 2f;
List<string> bottomLcdText = new List<string>();
List<string> topLcdText = new List<string>();
List<string> controller1Text = new List<string>();

enum LanderState
{
    Deploying,
    Deployed,
    Retracting,
    Retracted
}

LanderState landerState = LanderState.Retracted;
Dictionary<int, LanderGroup> landerGroups = new Dictionary<int, LanderGroup>();

enum DrillState
{
    Drilling,
    Idle,
    Retracting,
    Retracted
}

DrillState drillState = DrillState.Retracted;

List<IMyPistonBase> drillPistons = new List<IMyPistonBase>();

// Going to be using merge blocks to provide user with 1,2,3,4,5,6,7,8,9 key inputs
IMyShipMergeBlock mergeDrillExecute; // key 4
IMyShipMergeBlock mergeDrillRetract; // key 5
IMyShipMergeBlock mergeLanders; // key 7 - toggle land or "not land"

public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update10; // Run every 10 ticks

    controller = GridTerminalSystem.GetBlockWithName("Control Seat") as IMyCockpit;
    lcdTop = GridTerminalSystem.GetBlockWithName("LCD_Top") as IMyTextPanel;
    lcdBottom = GridTerminalSystem.GetBlockWithName("LCD_Bottom") as IMyTextPanel;
    lcdTop.WriteText("");
    lcdBottom.WriteText("");
    mergeDrillExecute = GridTerminalSystem.GetBlockWithName("MERGE_DrillExecute") as IMyShipMergeBlock;
    mergeDrillRetract = GridTerminalSystem.GetBlockWithName("MERGE_DrillRetract") as IMyShipMergeBlock;
    mergeLanders = GridTerminalSystem.GetBlockWithName("MERGE_Landers") as IMyShipMergeBlock;
    drillInnerRotor = GridTerminalSystem.GetBlockWithName("DRILL_InnerRotor") as IMyMotorAdvancedStator;
    drillOuterRotor = GridTerminalSystem.GetBlockWithName("DRILL_OuterRotor") as IMyMotorAdvancedStator;
    drill = GridTerminalSystem.GetBlockWithName("Drill") as IMyShipDrill;

    // get drill pistons from group name "DRILL_Pistons"
    IMyBlockGroup drillPistonsGroup = GridTerminalSystem.GetBlockGroupWithName("DRILL_PISTONS");
    if (drillPistonsGroup != null)
    {
        drillPistonsGroup.GetBlocksOfType<IMyPistonBase>(drillPistons);
    }

    // Write instructions for user
    controller.GetSurface(1).WriteText(
        "INSTRUCTIONS:\n"
        + "4) Start Drilling - Toggle (Pause/Start)\n"
        + "5) Retract Drill\n"
        + "7) Toggle Landers - (Deploy/Retract)\n"
    );

    InitLanders();
}

// Initialize lander groups
private void InitLanders()
{
    IMyBlockGroup foundLanders = GridTerminalSystem.GetBlockGroupWithName("LANDERS");
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

public void Main(string argument, UpdateType updateSource)
{
    // When the remote control is activated, the motor will start spinning
    bottomLcdText.Clear();
    bottomLcdText.Add("Machine Stats:");
    topLcdText.Clear();
    controller1Text.Clear();
    controller1Text.Add("Controll Status:");

    // What are the states??

    if (mergeLanders.Enabled)
    {
        landerState = mergeLanders.Enabled ? (landerState == LanderState.Retracted ? LanderState.Deploying : LanderState.Retracting) : LanderState.Retracted;
    }

    // --- DRILL ROTOR CODE ---

    bottomLcdText.Add("Drill:");
    // calculate target velocity for outer rotor
    float drillOuterRotorTargetVelocity = (float)(drillInnerRotor.TargetVelocityRPM * drillRatio);
    bottomLcdText.Add("   Outer Target: " + drillOuterRotorTargetVelocity.ToString("F2"));

    if (mergeDrillExecute.Enabled)
    {
        // drillOuterRotor.RotateToAngle(MyRotationDirection.AUTO, -1f, drillOuterRotorTargetVelocity);
        drillOuterRotor.TargetVelocityRPM = drillOuterRotorTargetVelocity;
        drillInnerRotor.TargetVelocityRPM = drillRotorVelocity;
        drill.Enabled = true;
        drillState = DrillState.Drilling;
        // diasable so we don' mess up the ship right now.
        foreach (var piston in drillPistons)
        {
            piston.Velocity = 0.0005f;
            topLcdText.Add("  Pistons: " + piston.CustomName + " " + piston.CurrentPosition.ToString("F2"));
        }
    }
    else
    {
        drillInnerRotor.TargetVelocityRPM = 0f;
        // drillOuterRotor.RotateToAngle(MyRotationDirection.AUTO, 180f, 2f);
        drill.Enabled = false;
        drillState = mergeDrillRetract.Enabled ? DrillState.Retracting : DrillState.Idle;
        foreach (var piston in drillPistons)
        {
            piston.Velocity = mergeDrillRetract.Enabled ? -0.6f : 0f;
            topLcdText.Add("Piston: " + piston.CustomName + " " + piston.CurrentPosition.ToString("F2"));
        }
    }

    // print rotor angles
    bottomLcdText.Add("Rotor Angles: "
        + " Inner: " + drillInnerRotor.Angle.ToString("F2")
        + " / Outer: " + drillOuterRotor.Angle.ToString("F2")
    );

    bottomLcdText.Add(" Rotor Velocities: "
        + " Inner: " + drillInnerRotor.TargetVelocityRPM.ToString("F2")
        + " / Outer: " + drillOuterRotor.TargetVelocityRPM.ToString("F2")
    );
    controller1Text.Add("   Drill: " + drillState.ToString());



    // --- LANDERS CODE ---

    // iterate through landerGroups and update their piston velocities
    bool allLandersLocked = true;
    foreach (var landerGroup in landerGroups)
    {
        topLcdText.Add("Lander "
            + landerGroup.Key.ToString() + ": "
            + landerGroup.Value.piston.CustomName + " "
            + landerGroup.Value.gear.IsLocked.ToString()
        );

        landerGroup.Value.handleState(mergeLanders);
        if (!landerGroup.Value.gear.IsLocked)
        {
            allLandersLocked = false;
        }
    }   

    if (allLandersLocked)
    {
        landerState = LanderState.Deployed;
    }

    // --- REPORT TILT ---
    Vector3D gravity = Vector3D.Normalize(controller.GetNaturalGravity());
    Vector3D up = controller.CubeGrid.WorldMatrix.Up;

    // Calculate dot product between grid up and inverse gravity
    // This gives us how aligned our up vector is with the opposite of gravity
    double levelness = Vector3D.Dot(up, -gravity);

    // Calculate forward and right tilt
    double forwardTilt = Math.Asin(Vector3D.Dot(controller.CubeGrid.WorldMatrix.Forward, -gravity)) * 180 / Math.PI;
    double rightTilt = Math.Asin(Vector3D.Dot(controller.CubeGrid.WorldMatrix.Right, -gravity)) * 180 / Math.PI;

    bottomLcdText.Add("Tilt (degrees):"
        + "\n   Forward: " + forwardTilt.ToString("F2")
        + "\n   Right: " + rightTilt.ToString("F2")
    );


    // -- WRITE TO LCDS ---
    lcdBottom.WriteText("");
    lcdBottom.WriteText(string.Join("\n", bottomLcdText));

    lcdTop.WriteText("");
    lcdTop.WriteText(string.Join("\n", topLcdText));

    controller.GetSurface(0).WriteText("");
    controller1Text.Add("    Lander State: " + landerState.ToString());
    controller.GetSurface(0).WriteText(string.Join("\n", controller1Text));

    controller.GetSurface(2).WriteText("--index 2--");
    controller.GetSurface(3).WriteText("--index 3--");
    controller.GetSurface(4).WriteText("--index 4--");
    Me.GetSurface(0).WriteText("THIS IS THE PROGRAMMABLE BLOCK SCREEN");
    Me.GetSurface(1).WriteText("THIS IS THE PROGRAMMABLE BLOCK SCREEN 2");

    SetStates();
}

// Set the states based on real world conditions, like React useEffect
private void SetStates()
{
}

public class LanderGroup
{
    public IMyPistonBase piston;
    public IMyLandingGear gear;

    public void handleState(IMyShipMergeBlock mergeLanders)
    {
        if (mergeLanders.Enabled)
        {
            if (gear.IsLocked)
            {
                piston.Velocity = 0f;
            }
            else
            {
                piston.Velocity = 0.6f;
            }
        }
        else
        {
            // UNLOCK THE GEAR FIRST
            gear.Unlock();
            piston.Velocity = -0.6f;
        }
    }
}
