IMyCockpit controller;
IMyRemoteControl remoteControl;
IMyTextPanel screen;

public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update10; // Run every 10 
    
    remoteControl = GridTerminalSystem.GetBlockWithName("Remote Control") as IMyRemoteControl;
    controller = GridTerminalSystem.GetBlockWithName("Control Seat") as IMyCockpit;
    screen = GridTerminalSystem.GetBlockWithName("LCD Screen") as IMyTextPanel;
}

public void Main(string argument, UpdateType updateSource)
{
    screen.WriteText("THIS IS THE LCD SCREEEN");
    controller.GetSurface(0).WriteText("THIS IS THE CONTROLLER SCREEN");
    Me.GetSurface(0).WriteText("THIS IS THE PROGRAMMABLE BLOCK SCREEN");
    Me.GetSurface(1).WriteText("THIS IS THE PROGRAMMABLE BLOCK SCREEN 2");
}
