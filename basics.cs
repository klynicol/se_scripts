IMyCockpit controller;
IMyRemoteControl remoteControl;
IMyTextPanel screen;
IMyMotorStator motor;
// Going to be using merge blocks to provide user with 1,2,3,4,5,6,7,8,9 key inputs
IMyShipMergeBlock mergeBlock;

LanderManager landerManager;

public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update10; // Run every 10 ticks
    remoteControl = GridTerminalSystem.GetBlockWithName("Remote Control") as IMyRemoteControl;
    if(remoteControl == null){
        Echo("Remote Control not found");
        return;
    }
    controller = GridTerminalSystem.GetBlockWithName("Control Seat") as IMyCockpit;
    screen = GridTerminalSystem.GetBlockWithName("LCD Screen") as IMyTextPanel;
    motor = GridTerminalSystem.GetBlockWithName("Rotor") as IMyMotorStator;
    mergeBlock = GridTerminalSystem.GetBlockWithName("Merge Block") as IMyShipMergeBlock;

    landerManager = new LanderManager(this);
}

public void Main(string argument, UpdateType updateSource)
{
    // print something to the control screen
    // screen.WriteText("Hello, world!");

    // IMyBlockGroup blockGroup = GridTerminalSystem.GetBlockGroupWithName("Group");
    // if (blockGroup != null)
    // {
    //     // Create a list to store blocks
    //     List<IMyTextSurface> blocks = new List<IMyTextSurface>();
    //     // Get both types of blocks
    //     blockGroup.GetBlocksOfType<IMyTextSurface>(blocks);
    //     List<IMyTextSurfaceProvider> surfaceProviders = new List<IMyTextSurfaceProvider>();
    //     blockGroup.GetBlocksOfType<IMyTextSurfaceProvider>(surfaceProviders);

    //     // Process both types
    //     foreach (var block in blocks)
    //     {
    //         Echo($"Block: {block.Name}");
    //         block.WriteText("Hello, world! ");
    //     }

    //     foreach (var provider in surfaceProviders)
    //     {
    //         var surface = provider.GetSurface(0); // Get the first surface
    //         surface.WriteText("Hello, provider! ");
    //     }
    // }

    // When the remote control is activated, the motor will start spinning
    if (mergeBlock.Enabled)
    {
        motor.TargetVelocityRPM = 100;
    } else {
        // stop the motor
        motor.TargetVelocityRPM = 0;
    }

    screen.WriteText("THIS IS THE LCD SCREEEN");
    // controller.GetSurface(0).WriteText("THIS IS THE CONTROLLER SCREEN");
    Me.GetSurface(0).WriteText("THIS IS THE PROGRAMMABLE BLOCK SCREEN");
    Me.GetSurface(1).WriteText("THIS IS THE PROGRAMMABLE BLOCK SCREEN 2");

    landerManager.Test();
}

public class LanderManager
{

    private readonly Program program;

    public LanderManager(Program program)
    {
        this.program = program;
    }

    public void Test()
    {
        program.controller.GetSurface(0).WriteText("TEST");
    }   
}

