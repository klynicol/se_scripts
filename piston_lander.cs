IMyCockpit controller;
IMyShipMergeBlock mergeBlock;
// IMyPistonBase piston;
// IMyLandingGear gear;
IMyTextPanel lcdScreen;

List<string> lcdText = new List<string>();

LanderGroup landerGroup;

public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update10; // Run every 10 ticks

    controller = GridTerminalSystem.GetBlockWithName("Cockpit") as IMyCockpit;
    mergeBlock = GridTerminalSystem.GetBlockWithName("MERGE_BLOCK") as IMyShipMergeBlock;
    // piston = GridTerminalSystem.GetBlockWithName("PISTON_Lander") as IMyPistonBase;
    // gear = GridTerminalSystem.GetBlockWithName("LANDING_GEAR") as IMyLandingGear;

    IMyBlockGroup testGroup = GridTerminalSystem.GetBlockGroupWithName("GROUP");
    List<IMyPistonBase> pistonGroup = new List<IMyPistonBase>();
    List<IMyLandingGear> gearGroup = new List<IMyLandingGear>();
    testGroup.GetBlocksOfType<IMyPistonBase>(pistonGroup);
    testGroup.GetBlocksOfType<IMyLandingGear>(gearGroup);
    IMyPistonBase piston = pistonGroup.First();
    IMyLandingGear landingGear = gearGroup.First();
    landerGroup = new LanderGroup(piston, landingGear);

    lcdScreen = GridTerminalSystem.GetBlockWithName("LCD_Panel") as IMyTextPanel;
}

public void Main(string argument, UpdateType updateSource)
{
    lcdText.Clear();

    if(mergeBlock.Enabled)
    {
        lcdText.Add("here 1");
        if(landerGroup.gear.IsLocked){
            lcdText.Add("here 3");
            landerGroup.piston.Velocity = 0f;
        } else {
            lcdText.Add("here 4");
            landerGroup.piston.Velocity = 0.8f;
        }
    } else {
        lcdText.Add("here 2");
        landerGroup.piston.Velocity = 0f;
    }

    lcdText.Add("Piston Velocity: " + landerGroup.piston.Velocity.ToString());

    lcdScreen.WriteText("");
    lcdScreen.WriteText(string.Join("\n", lcdText));
}


public class LanderGroup
{
    public IMyPistonBase piston;
    public IMyLandingGear gear;

    public LanderGroup(IMyPistonBase piston, IMyLandingGear gear)
    {
        this.piston = piston;
        this.gear = gear;
    }
}
