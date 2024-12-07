IMyCockpit controller;
IMyTextPanel lcdTop;
IMyTextPanel lcdBottom;
IMyMotorStator motor;
IMyMotorAdvancedStator drillInnerRotor;
IMyMotorAdvancedStator drillOuterRotor;
IMyShipDrill drill;
float drillRotorVelocity = 0.8f;
float drillRatio = 2f;
List<string> lcdText = new List<string>();

List<IMyPistonBase> drillPistons = new List<IMyPistonBase>();

// Going to be using merge blocks to provide user with 1,2,3,4,5,6,7,8,9 key inputs
IMyShipMergeBlock mergeDrillExecute; // key 2

public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update10; // Run every 10 ticks

    // clear both lcds

    controller = GridTerminalSystem.GetBlockWithName("Control Seat") as IMyCockpit;
    lcdTop = GridTerminalSystem.GetBlockWithName("LCD_Top") as IMyTextPanel;
    lcdBottom = GridTerminalSystem.GetBlockWithName("LCD_Bottom") as IMyTextPanel;
    lcdTop.WriteText("");
    lcdBottom.WriteText("");
    mergeDrillExecute = GridTerminalSystem.GetBlockWithName("MERGE_DrillExecute") as IMyShipMergeBlock;
    drillInnerRotor = GridTerminalSystem.GetBlockWithName("DRILL_InnerRotor") as IMyMotorAdvancedStator;
    drillOuterRotor = GridTerminalSystem.GetBlockWithName("DRILL_OuterRotor") as IMyMotorAdvancedStator;
    drill = GridTerminalSystem.GetBlockWithName("Drill") as IMyShipDrill;
    // get drill pistons from group name "DRILL_Pistons"
    IMyBlockGroup drillPistonsGroup = GridTerminalSystem.GetBlockGroupWithName("DRILL_PISTONS");
    if (drillPistonsGroup != null)
    {
        drillPistonsGroup.GetBlocksOfType<IMyPistonBase>(drillPistons);
    }
}

public void Main(string argument, UpdateType updateSource)
{
    // When the remote control is activated, the motor will start spinning
    lcdText.Clear();


    // --- DRILL ROTOR CODE ---
    // calculate target velocity for outer rotor
    float drillOuterRotorTargetVelocity = (float)(drillInnerRotor.TargetVelocityRPM * drillRatio);
    lcdText.Add("Target Velocity: " + drillOuterRotorTargetVelocity.ToString());
    lcdText.Add("Outer Rotor Angle: " + (drillOuterRotor.Angle * 180 / Math.PI).ToString());
    if (mergeDrillExecute.Enabled)
    {
        drillOuterRotor.RotateToAngle(MyRotationDirection.AUTO, -1f, drillOuterRotorTargetVelocity);
        drillInnerRotor.TargetVelocityRPM = drillRotorVelocity;
        drillOuterRotor.TargetVelocityRPM = drillOuterRotorTargetVelocity;
        drill.Enabled = true;
    }
    else
    {
        drillInnerRotor.TargetVelocityRPM = 0f;
        drillOuterRotor.RotateToAngle(MyRotationDirection.AUTO, 180f, 2f);
        drill.Enabled = false;
    }


    // --- DRILL PISTON CODE ---


    // --- REPORT TILT ---
    Vector3D gravity = Vector3D.Normalize(controller.GetNaturalGravity());
    Vector3D up = controller.CubeGrid.WorldMatrix.Up;

    // Calculate dot product between grid up and inverse gravity
    // This gives us how aligned our up vector is with the opposite of gravity
    double levelness = Vector3D.Dot(up, -gravity);
    
    // Calculate forward and right tilt
    double forwardTilt = Math.Asin(Vector3D.Dot(controller.CubeGrid.WorldMatrix.Forward, -gravity)) * 180 / Math.PI;
    double rightTilt = Math.Asin(Vector3D.Dot(controller.CubeGrid.WorldMatrix.Right, -gravity)) * 180 / Math.PI;

    lcdText.Add("Tilt (degrees):" 
        + "\nForward: " + forwardTilt.ToString("F2")
        + "\nRight: " + rightTilt.ToString("F2")
    );

    

    lcdBottom.WriteText("");
    lcdBottom.WriteText(string.Join("\n", lcdText));

    // lcdTop.WriteText("THIS IS THE TOP LCD SCREEN");
    // lcdBottom.WriteText("THIS IS THE BOTTOM LCD SCREEN");   
    controller.GetSurface(0).WriteText("--1--");
    controller.GetSurface(1).WriteText("--2--");
    controller.GetSurface(2).WriteText("--3--");
    controller.GetSurface(3).WriteText("--4--");
    controller.GetSurface(4).WriteText("--5--");
    Me.GetSurface(0).WriteText("THIS IS THE PROGRAMMABLE BLOCK SCREEN");
    Me.GetSurface(1).WriteText("THIS IS THE PROGRAMMABLE BLOCK SCREEN 2");
}
