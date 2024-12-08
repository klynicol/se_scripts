const string SHIP_NAME = "Gondor_01";
const float DRILL_PISTON_VELOCITY = 0.0005f;
// Spirograph for the win!!!
const float DRILL_ROTOR_VELOCITY = 0.25f;
const float DRILL_RATIO = 8f;

bool isCargoFull = true;

IMyCockpit controller;
IMyTextPanel lcdTop;
IMyTextPanel lcdBottom;
IMyMotorStator motor;
IMyMotorAdvancedStator drillInnerRotor;
IMyMotorAdvancedStator drillOuterRotor;
IMyShipDrill drill;

List<string> bottomLcdText = new List<string>();
List<string> topLcdText = new List<string>();
List<string> controller1Text = new List<string>();

List<IMyCargoContainer> cargoContainers = new List<IMyCargoContainer>();

IMyBeacon beacon;

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

/**
Deploy drill if:
- mergeDrillExecute is enabled
- landerState is Deployed
- cargo is not full
Retract drill if:
- mergeDrillRetract is enabled
- landerState is Retracted
*/

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
    drill = GridTerminalSystem.GetBlockWithName("DRILL") as IMyShipDrill;

    // get drill pistons from group name "DRILL_Pistons"
    IMyBlockGroup drillPistonsGroup = GridTerminalSystem.GetBlockGroupWithName("DRILL_PISTONS");
    if (drillPistonsGroup != null)
    {
        drillPistonsGroup.GetBlocksOfType<IMyPistonBase>(drillPistons);
    }

    IMyBlockGroup cargoGroup = GridTerminalSystem.GetBlockGroupWithName("CARGO");
    if (cargoGroup != null)
    {
        cargoGroup.GetBlocksOfType<IMyCargoContainer>(cargoContainers);
    }

    // get the first beacon in the grid
    List<IMyBeacon> beacons = new List<IMyBeacon>();
    GridTerminalSystem.GetBlocksOfType<IMyBeacon>(beacons);
    if (beacons.Count > 0)
    {
        beacon = beacons[0];
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
    controller1Text.Clear();
    controller.GetSurface(0).WriteText("");
    bottomLcdText.Clear();
    bottomLcdText.Add("LANDER STATS:");
    topLcdText.Clear();
    topLcdText.Add("DRILL STATS:");


    // --- LANDERS CODE ---

    // iterate through landerGroups and update their piston velocities
    bool allLandersLocked = true;
    bottomLcdText.Add("Landing Gear:");
    foreach (var landerGroup in landerGroups)
    {
        bottomLcdText.Add(
            +landerGroup.Value.piston.CustomName + " "
            + " Locked = " + landerGroup.Value.gear.IsLocked.ToString()
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
        + "\n   Forward/Back: " + forwardTilt.ToString("F2")
        + "\n   Right/Left: " + rightTilt.ToString("F2")
    );

    // --- CARGO CONTAINER CODE ---

    float accumulatedPercentageFull = 0f;
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

    if (isCargoFull)
    {
        drillState = DrillState.Idle;
        // light the beacons of Gondor
        beacon.CustomName = SHIP_NAME + "_Cargo_Full";
        // stop the drill!!
        drillInnerRotor.TargetVelocityRPM = 0f;
        drillOuterRotor.TargetVelocityRPM = 0f;
        drill.Enabled = false;
    }
    else
    {
        beacon.CustomName = SHIP_NAME + "_Cargo_Empty";
    }

    // --- DRILL CODE ---

    topLcdText.Add("Piston Positions:");
    bool pistonsRetracted = true;
    foreach (var piston in drillPistons)
    {
        topLcdText.Add(piston.CustomName + " " + (100f * (piston.CurrentPosition / 10f)).ToString("F2") + "%");
        if (piston.CurrentPosition > 0f)
        {
            pistonsRetracted = false;
        }
    }
    if (pistonsRetracted)
    {
        drillState = DrillState.Retracted;
    }


    // calculate target velocity for outer rotor
    float drillOuterRotorTargetVelocity = (float)(drillInnerRotor.TargetVelocityRPM * DRILL_RATIO);

    // good to drill
    if (drillState == DrillState.Drilling)
    {
        drillOuterRotor.TargetVelocityRPM = drillOuterRotorTargetVelocity;
        drillInnerRotor.TargetVelocityRPM = DRILL_ROTOR_VELOCITY;
        drill.Enabled = true;
        foreach (var piston in drillPistons)
        {
            piston.Velocity = DRILL_PISTON_VELOCITY;
        }
    }
    // idle drill
    else if (drillState == DrillState.Idle)
    {
        drillInnerRotor.TargetVelocityRPM = 0f;
        drillOuterRotor.TargetVelocityRPM = 0f;
        drill.Enabled = false;
        foreach (var piston in drillPistons)
        {
            piston.Velocity = 0f;
        }
    }
    // retract drill
    else if (drillState == DrillState.Retracting)
    {
        drillInnerRotor.TargetVelocityRPM = 0f;
        drillOuterRotor.TargetVelocityRPM = 0f;
        // drillOuterRotor.RotateToAngle(MyRotationDirection.AUTO, 1.8f ) // TODO
        drill.Enabled = false;
        foreach (var piston in drillPistons)
        {
            piston.Velocity = -0.6f
        }
    }

    // print rotor angles
    topLcdText.Add("Rotor Angles: "
        + " Inner: " + (drillInnerRotor.Angle * 180 / Math.PI).ToString("F2")
        + " / Outer: " + ((drillOuterRotor.Angle * 180 / Math.PI) % 360).ToString("F2")
    );

    // -- WRITE TO LCDS ---

    lcdBottom.WriteText("");
    lcdBottom.WriteText(string.Join("\n", bottomLcdText));

    lcdTop.WriteText("");
    lcdTop.WriteText(string.Join("\n", topLcdText));

    // report on all functional/controllable states
    controller1Text.Add("FUNCTIONAL STATES: \n");
    controller1Text.Add("   Drill: " + drillState.ToString());
    controller1Text.Add("   Lander: " + landerState.ToString());
    controller.GetSurface(0).WriteText(string.Join("\n", controller1Text));

    // controller.GetSurface(2).WriteText("--index 2--");
    // controller.GetSurface(3).WriteText("--index 3--");
    // controller.GetSurface(4).WriteText("--index 4--");
    // Me.GetSurface(0).WriteText("THIS IS THE PROGRAMMABLE BLOCK SCREEN");
    // Me.GetSurface(1).WriteText("THIS IS THE PROGRAMMABLE BLOCK SCREEN 2");

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
