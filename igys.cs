
// Isy's Inventory Manager
// ===================
// Version: 2.9.5
// Date: 2023-12-10

// Guide: http://steamcommunity.com/sharedfiles/filedetails/?id=1226261795

//  =======================================================================================
//                                                                            --- Configuration ---
//  =======================================================================================

// --- Sorting ---
// =======================================================================================

// Define the keyword a cargo container has to contain in order to be recognized as a container of the given type.
const string oreContainerKeyword = "Ores";
const string ingotContainerKeyword = "Ingots";
const string componentContainerKeyword = "Components";
const string toolContainerKeyword = "Tools";
const string ammoContainerKeyword = "Ammo";
const string bottleContainerKeyword = "Bottles";

// Keyword a block name has to contain to be excluded from sorting (does mostly apply for cargo containers).
// This list is expandable - just separate the entries with a ",". But it's also language specific, so adjust it if needed.
// Default: string[] lockedContainerKeywords = { "Locked", "Control Station", "Control Seat", "Safe Zone" };
string[] lockedContainerKeywords = { "Locked", "Control Station", "Control Seat", "Safe Zone" };

// Keyword a block name has to contain to be excluded from item counting (used by autocrafting and inventory panels)
// This list is expandable - just separate the entries with a ",". But it's also language specific, so adjust it if needed.
// Default: string[] hiddenContainerKeywords = { "Hidden" };
string[] hiddenContainerKeywords = { "Hidden" };

// Treat inventories that are hidden via disabling the terminal option "Show block in Inventory Screen" as hidden containers
// just like adding the 'Hidden' keyword (see above)? (Note: It's not recommended to hide your main type containers!)
bool treatNotShownAsHidden = false;

// Keyword for connectors to disable sorting of a grid, that is docked to that connector.
// This will prevent IIM from pulling any items from that connected grid.
// Special containers, reactors and O2/H2 generators will still be filled.
string noSortingKeyword = "[No Sorting]";

// Keyword for connectors to disable IIM completely for another grid, that is docked to that connector.
string noIIMKeyword = "[No IIM]";

// Balance items between containers of the same type? This will result in an equal amount of all items in all containers of that type.
bool balanceTypeContainers = false;

// Show a fill level in the container's name?
bool showFillLevel = true;

// Fill bottles before storing them in the bottle container?
bool fillBottles = true;


// --- Automated container assignment ---
// =======================================================================================

// Master switch. If this is set to false, automated container un-/assignment is disabled entirely.
bool autoContainerAssignment = true;

// Assign switch. Assign new containers if a type is full or not present?
bool assignNewContainers = true;

// Which type should be assigned automatically? (only relevant if master and assign switch are true)
bool assignOres = true;
bool assignIngots = true;
bool assignComponents = true;
bool assignTools = true;
bool assignAmmo = true;
bool assignBottles = true;

// Unassign switch. Unassign empty type containers that aren't needed anymore (at least one of each type always remains).
// This doesn't touch containers with manual priority tokens, like [P1].
bool unassignEmptyContainers = true;

// Which type should be unassigned automatically? (only relevant if master and unassign switch are true)
bool unassignOres = true;
bool unassignIngots = true;
bool unassignComponents = true;
bool unassignTools = true;
bool unassignAmmo = true;
bool unassignBottles = true;

// Assign ores and ingots containers as one? (complies with type switches)
bool oresIngotsInOne = true;

// Assign tool, ammo and bottle containers as one? (complies with type switches)
bool toolsAmmoBottlesInOne = true;


// --- Autocrafting ---
// =======================================================================================

// Enable autocrafting or autodisassembling (disassembling will disassemble everything above the wanted amounts)
// All assemblers will be used. To use one manually, add the manualMachineKeyword to it (by default: "!manual")
bool enableAutocrafting = true;
bool enableAutodisassembling = false;

// A LCD with the keyword "Autocrafting" is required where you can set the wanted amount!
// This has multi LCD support. Just append numbers after the keyword, like: "LCD Autocrafting 1", "LCD Autocrafting 2", ..
string autocraftingKeyword = "Autocrafting";

// If you want an assembler to only assemble or only disassemble, use the following keywords in its name.
// A assembler without a keyword will do both tasks
string assembleKeyword = "!assemble-only";
string disassembleKeyword = "!disassemble-only";

// You can teach the script new crafting recipes, by adding one of the following tags to an assembler's name.
// There are two tag options to teach new blueprints:
// !learn will learn one item and then remove the tag so that the assembler is part of the autocrafting again.
// !learnMany will learn everything you queue in it and will never be part of the autorafting again until you remove the tag.
// To learn an item, queue it up about 100 times (Shift+Click) and wait until the script removes it from the queue.
string learnKeyword = "!learn";
string learnManyKeyword = "!learnMany";

// Default modifier that gets applied, when a new item is found. Modifiers can be one or more of these:
// 'A' (Assemble only), 'D' (Disassemble only), 'P' (Always queue first (priority)), 'H' (Hide and manage in background), 'I' (Hide and ignore)
string defaultModifier = "";

// Margins for assembling or disassembling items in percent based on the wanted amount (default: 0 = exact value).
// Examples:
// assembleMargin = 5 with a wanted amount of 100 items will only produce new items, if less than 95 are available.
// disassembleMargin = 10 with a wanted amount of 1000 items will only disassemble items if more than 1100 are available.
double assembleMargin = 0;
double disassembleMargin = 0;

// Show unlearned (mostly modded) items on the autocrafting screen? This adds the [NoBP] tag (no blueprint) like in the old days of IIM.
bool showUnlearnedItems = false;

// Use assemblers on docked grids?
bool useDockedAssemblers = false;

// Add the header to every screen when using multiple autocrafting LCDs?
bool headerOnEveryScreen = false;

// Show available modifiers help on the last screen?
bool showAutocraftingModifiers = true;

// Split assembler tasks (this is like cooperative mode but splits the whole queue between all assemblers equally)
bool splitAssemblerTasks = true;

// Sort the assembler queue based on the most needed components?
bool sortAssemblerQueue = true;

// Autocraft ingots from stone in survival kits until you have proper refineries?
bool enableBasicIngotCrafting = true;

// Disable autocrafting in survival kits when you have regular assemblers?
bool disableBasicAutocrafting = true;


// --- Special Loadout Containers ---
// =======================================================================================

// Keyword an inventory has to contain to be filled with a special loadout (see in it's custom data after you renamed it!)
// Special containers will be filled with your wanted amount of items and never be drained by the auto sorting!
const string specialContainerKeyword = "Special";

// Are special containers allowed to 'steal' items from other special containers with a lower priority?
bool allowSpecialSteal = true;


// --- Refinery handling ---
// =======================================================================================

// By enabling ore balancing, the script will balance the ores between all refinieres so that every refinery has the same amount of ore in it.
// To still use a refinery manually, add the manualMachineKeyword to it (by default: "!manual")
bool enableOreBalancing = true;

// Enable script assisted refinery filling? This will move in the most needed ore and will make room, if the refinery is already full
// Also, the script puts as many ores into the refinery as possible and will pull ores even from other refineries if needed.
bool enableScriptRefineryFilling = true;

// Sort the refinery queue based on the most needed ingots?
bool sortRefiningQueue = true;

// Use refineries on docked grids?
bool useDockedRefineries = false;

// If you want an ore to always be refined first, simply remove the two // in front of the ore name to enable it.
// Enabled ores are refined in order from top to bottom so if you removed several // you can change the order by
// copying and pasting them inside the list. Just be careful to keep the syntax correct: "OreName",
// By default stone is enabled and will always be refined first.
List<String> fixedRefiningList = new List<string> {
	"Stone",
	//"Iron",
	//"Nickel",
	//"Cobalt",
	//"Silicon",
	//"Uranium",
	//"Silver",
	//"Gold",
	//"Platinum",
	//"Magnesium",
	//"Scrap",
};


// --- O2/H2 generator handling ---
// =======================================================================================

// Enable balancing of ice in O2/H2 generators?
// All O2/H2 generators will be used. To use one manually, add the manualMachineKeyword to it (by default: "!manual")
bool enableIceBalancing = true;

// Put ice into O2/H2 generators that are turned off? (default: false)
bool fillOfflineGenerators = false;

// How much space should be left to fill bottles (aka how many bottles should fit in after it's filled with ice)?
// WARNING! O2/H2 generators automatically pull ice and bottles if their inventory volume drops below 30%.
// To avoid this, turn off "Use Conveyor" in the generator's terminal settings.
int spaceForBottles = 1;


// --- Reactor handling ---
// =======================================================================================

// Enable balancing of uranium in reactors? (Note: conveyors of reactors are turned off to stop them from pulling more)
// All reactors will be used. To use one manually, add the manualMachineKeyword to it (by default: "!manual")
bool enableUraniumBalancing = true;

// Put uranium into reactors that are turned off? (default: false)
bool fillOfflineReactors = false;

// Amount of uranium in each reactor? (default: 100 for large grid reactors, 25 for small grid reactors)
double uraniumAmountLargeGrid = 100;
double uraniumAmountSmallGrid = 25;


// --- Assembler Cleanup ---
// =======================================================================================

// This cleans up assemblers, if they have no queue and puts the contents back into a cargo container.
bool enableAssemblerCleanup = true;


// --- Internal item sorting ---
// =======================================================================================

// Sort the items inside all containers?
// Note, that this could cause inventory desync issues in multiplayer, so that items are invisible
// or can't be taken out. Use at your own risk!
bool enableInternalSorting = false;

// Internal sorting pattern. Always combine one of each category, e.g.: 'Ad' for descending item amount (from highest to lowest)
// 1. Quantifier:
// A = amount
// N = name
// T = type (alphabetical)
// X = type (number of items)

// 2. Direction:
// a = ascending
// d = descending

string sortingPattern = "Na";

// Internal sorting can also be set per inventory. Just use '(sort:PATTERN)' in the block's name.
// Example: Small Cargo Container 3 (sort:Ad)
// Note: Using this method, internal sorting will always be activated for this container, even if the main switch is turned off!


// --- LCD panels ---
// =======================================================================================

// To display the main script informations, add the following keyword to any LCD name (default: IIM-main).
// You can enable or disable specific informations on the LCD by editing its custom data.
string mainLCDKeyword = "IIM-main";

// To display current item amounts of different types, add the following keyword to any LCD name
// and follow the on screen instructions.
string inventoryLCDKeyword = "IIM-inventory";

// To display all current warnings and problems, add the following keyword to any LCD name (default: IIM-warnings).
string warningsLCDKeyword = "IIM-warnings";
bool showOwnerWarnings = true;

// To display all actions, the script did lately, add the following keyword to any LCD name (default: IIM-actions).
string actionsLCDKeyword = "IIM-actions";
bool showTimeStamp = true;
int maxEntries = 30;

// To display the script performance (PB terminal output), add the following keyword to any LCD name (default: IIM-performance).
string performanceLCDKeyword = "IIM-performance";

// Default screen font, fontsize and padding, when a screen is first initialized. Fonts: "Debug" or "Monospace"
string defaultFont = "Debug";
float defaultFontSize = 0.6f;
float defaultPadding = 2f;


// --- Settings for enthusiasts ---
// =======================================================================================

// Extra breaks between script methods in ticks (1 tick = 16.6ms).
double extraScriptTicks = 0;

// Use dynamic script speed? The script will slow down automatically if the current runtime exceeds a set value (default: 0.5ms)
bool useDynamicScriptSpeed = true;
double maxCurrentMs = 0.5;

// If you want to use a machine manually, append the keyword to it.
// This works for assemblers, refineries, reactors and O2/H2 generators
string[] manualMachineKeywords = { "!manual" };

// List of always excluded block types (not NAMES!). Mostly because they don't have conveyors or IIM shouldn't tinker with them.
string[] excludedBlocks = { "Parachute", "VendingMachine" };

// Exclude welders, grinders or drills from sorting? Set this to true, if you have huge welder or grinder walls!
bool excludeWelders = false;
bool excludeGrinders = false;
bool excludeDrills = false;

// Enable connection check for inventories (needed for [No Conveyor] info)?
bool connectionCheck = false;

// Tag inventories, that have no access to the main type containers with [No Conveyor]?
// This only works if the above setting connectionCheck is set to true!
bool showNoConveyorTag = true;

// Use connected grids as temporary storage for temporary storage?
// This only affects balancing methods or Special container unloading if no storage is available on your main grid.
bool useConnectedGridsTemporarily = true;

// Script mode: "ship", "station" or blank for autodetect
string scriptMode = "";

// Protect type containers when docking to another grid running the script?
bool protectTypeContainers = true;

// Enable name correction? This option will automtically correct capitalization, e.g.: iim-main -> IIM-main
bool enableNameCorrection = true;

// IIM considers an inventory with 98% fill level as full. For very large containers, this value would waste a lot of space.
// Large containers use the following static value as space left in liters (default: 500L).
// Calculation: IF maxVolume - maxVolume * 0.98 > inventoryFullBuffer USE inventoryFullBuffer (by default if max volume > 25000L).
double inventoryFullBuffer = 500;

// Format of the actionsLCD timestamp. See https://www.tutorialsteacher.com/articles/datetime-formats-in-csharp for more information.
string timeFormat = "HH:mm:ss";


//  =======================================================================================
//                                                                      --- End of Configuration ---
//                                                        Don't change anything beyond this point!
//  =======================================================================================


List<IMyTerminalBlock>ʪ=new List<IMyTerminalBlock>();List<IMyTerminalBlock>ʩ=new List<IMyTerminalBlock>();List<
IMyTerminalBlock>ʫ=new List<IMyTerminalBlock>();List<IMyTerminalBlock>ʬ=new List<IMyTerminalBlock>();List<IMyTerminalBlock>ʵ=new List<
IMyTerminalBlock>();List<IMyTerminalBlock>ʿ=new List<IMyTerminalBlock>();List<IMyTerminalBlock>ʽ=new List<IMyTerminalBlock>();List<
IMyTerminalBlock>ʼ=new List<IMyTerminalBlock>();List<IMyShipConnector>ʻ=new List<IMyShipConnector>();List<IMyRefinery>ʺ=new List<
IMyRefinery>();List<IMyRefinery>ʹ=new List<IMyRefinery>();List<IMyRefinery>ʸ=new List<IMyRefinery>();List<IMyRefinery>ʷ=new List<
IMyRefinery>();List<IMyAssembler>ʾ=new List<IMyAssembler>();List<IMyAssembler>ʶ=new List<IMyAssembler>();List<IMyAssembler>ʴ=new
List<IMyAssembler>();List<IMyAssembler>ʳ=new List<IMyAssembler>();List<IMyAssembler>ʲ=new List<IMyAssembler>();List<
IMyGasGenerator>ʱ=new List<IMyGasGenerator>();List<IMyGasTank>ʰ=new List<IMyGasTank>();List<IMyReactor>ʯ=new List<IMyReactor>();List<
IMyTextPanel>ʮ=new List<IMyTextPanel>();List<string>ʭ=new List<string>();HashSet<IMyCubeGrid>ʨ=new HashSet<IMyCubeGrid>();HashSet<
IMyCubeGrid>ʓ=new HashSet<IMyCubeGrid>();List<IMyTerminalBlock>ʝ=new List<IMyTerminalBlock>();List<IMyTerminalBlock>ʑ=new List<
IMyTerminalBlock>();List<IMyTerminalBlock>ʐ=new List<IMyTerminalBlock>();List<IMyTerminalBlock>ʏ=new List<IMyTerminalBlock>();List<
IMyTerminalBlock>ʎ=new List<IMyTerminalBlock>();List<IMyTerminalBlock>ʍ=new List<IMyTerminalBlock>();List<IMyTerminalBlock>ʌ=new List<
IMyTerminalBlock>();HashSet<string>ʋ=new HashSet<string>();IMyTerminalBlock ʊ;IMyInventory ʉ;IMyTerminalBlock ʈ;IMyTerminalBlock ʇ,ʆ;
bool ʅ=false;int ʄ=0;int ʃ=0;int ʒ=0;int ʂ=0;int ʔ=0;int ʧ=0;int ʥ=0;int ʤ=0;int ʣ=0;int ʢ=0;int ʡ=0;int ʠ=0;int ʟ=0;int ʦ=0
;string[]ʞ={"/","-","\\","|"};int ʜ=0;List<String>ʛ=new List<string>();string ʚ="";List<IMyTerminalBlock>ʙ=new List<
IMyTerminalBlock>();List<IMyTerminalBlock>ʘ=new List<IMyTerminalBlock>();List<IMyTerminalBlock>ʗ=new List<IMyTerminalBlock>();List<
IMyTerminalBlock>ʖ=new List<IMyTerminalBlock>();List<IMyTerminalBlock>ʕ=new List<IMyTerminalBlock>();StringBuilder ʁ=new StringBuilder()
;string[]Ƶ={"showHeading=true","showWarnings=true","showContainerStats=true","showManagedBlocks=true",
"showLastAction=true","scrollTextIfNeeded=true"};string[]Ș={"showHeading=true","scrollTextIfNeeded=true"};string[]ȹ={"showHeading=true",
"scrollTextIfNeeded=false"};string ŕ;int ȸ=0;bool ȷ=false;string ȶ="";bool ȵ=false;bool ȴ=false;bool Ⱥ=false;HashSet<string>ȳ=new HashSet<string>(
);HashSet<string>ȱ=new HashSet<string>();int Ȱ=0;int ȯ=0;int Ȯ=0;bool ȭ=false;bool Ȭ=true;bool ȫ=false;int Ȳ=0;string ȼ=
"itemID;blueprintID";Dictionary<string,string>Ʌ=new Dictionary<string,string>(){{"oreContainer",oreContainerKeyword},{"ingotContainer",
ingotContainerKeyword},{"componentContainer",componentContainerKeyword},{"toolContainer",toolContainerKeyword},{"ammoContainer",
ammoContainerKeyword},{"bottleContainer",bottleContainerKeyword},{"specialContainer",specialContainerKeyword},{"oreBalancing","true"},{
"iceBalancing","true"},{"uraniumBalancing","true"}};string ɏ="IIM Autocrafting";string ɍ="Remove a line to show this item on the LCD again!\nAdd an amount to manage the item without being on the LCD.\nExample: '-SteelPlate=1000'"
;char[]Ɍ={'=','>','<'};IMyAssembler ɋ;string Ɋ="";MyDefinitionId ɉ;int Ɉ=0;HashSet<string>ɇ=new HashSet<string>{"Uranium"
,"Silicon","Silver","Gold","Platinum","Magnesium","Iron","Nickel","Cobalt","Stone","Scrap"};List<MyItemType>Ɏ=new List<
MyItemType>();List<MyItemType>Ɇ=new List<MyItemType>();Dictionary<string,double>Ʉ=new Dictionary<string,double>(){{"Cobalt",0.3},{
"Gold",0.01},{"Iron",0.7},{"Magnesium",0.007},{"Nickel",0.4},{"Platinum",0.005},{"Silicon",0.7},{"Silver",0.1},{"Stone",0.014}
,{"Uranium",0.01}};const string Ƀ="MyObjectBuilder_";const string ɂ="Ore";const string Ɂ="Ingot";const string ɀ=
"Component";const string ȿ="AmmoMagazine";const string Ⱦ="OxygenContainerObject";const string Ƚ="GasContainerObject";const string Ȫ
="PhysicalGunObject";const string Ȗ="PhysicalObject";const string ȟ="ConsumableItem";const string Ȕ="Datapad";const
string ȓ=Ƀ+"BlueprintDefinition/";SortedSet<MyDefinitionId>Ȓ=new SortedSet<MyDefinitionId>(new ų());SortedSet<string>ȑ=new
SortedSet<string>();SortedSet<string>Ȑ=new SortedSet<string>();SortedSet<string>ȏ=new SortedSet<string>();SortedSet<string>Ȏ=new
SortedSet<string>();SortedSet<string>ȍ=new SortedSet<string>();SortedSet<string>Ȍ=new SortedSet<string>();SortedSet<string>ȋ=new
SortedSet<string>();SortedSet<string>Ȋ=new SortedSet<string>();SortedSet<string>ȉ=new SortedSet<string>();SortedSet<string>Ȉ=new
SortedSet<string>();Dictionary<MyDefinitionId,double>ȇ=new Dictionary<MyDefinitionId,double>();Dictionary<MyDefinitionId,double>Ȇ
=new Dictionary<MyDefinitionId,double>();Dictionary<MyDefinitionId,double>ȕ=new Dictionary<MyDefinitionId,double>();
Dictionary<MyDefinitionId,int>ȅ=new Dictionary<MyDefinitionId,int>();Dictionary<MyDefinitionId,MyDefinitionId>ȗ=new Dictionary<
MyDefinitionId,MyDefinitionId>();Dictionary<MyDefinitionId,MyDefinitionId>ȩ=new Dictionary<MyDefinitionId,MyDefinitionId>();Dictionary
<string,MyDefinitionId>ȧ=new Dictionary<string,MyDefinitionId>();Dictionary<string,string>Ȧ=new Dictionary<string,string>
();Dictionary<string,IMyTerminalBlock>ȥ=new Dictionary<string,IMyTerminalBlock>();bool Ȥ=false;string ȣ="station_mode;\n"
;string Ȣ="ship_mode;\n";string ȡ="[PROTECTED] ";string Ȩ="";string Ƞ="";string Ȟ="";DateTime ȝ;int Ȝ=0;string ț=
"Startup";string[]Ț={"Get inventory blocks","Find new items","Create item lists","Name correction","Assign containers",
"Fill special containers","Sort cargo","Container balancing","Internal sorting","Add fill level to names","Get global item amount",
"Get assembler queue","Autocrafting","Sort assembler queue","Clean up assemblers","Learn unknown blueprints","Fill refineries",
"Ore balancing","Ice balancing","Uranium balancing"};string[]ș={"Initializing script","","Getting inventory blocks",
"Loading saved items","Clearing assembler queues","Checking type containers","Setting script mode","Starting script",""};Program(){
inventoryFullBuffer/=1000;assembleMargin/=100;disassembleMargin/=100;Runtime.UpdateFrequency=UpdateFrequency.Update10;}void Main(string ɐ){
if(ȸ>=10){throw new Exception("Too many errors in script step "+Ȯ+":\n"+ț+"\n\nPlease recompile or try the reset argument!\nSupport: https://discord.gg/gY9aUUy\nScript stoppped!\n\nLast error:\n"
+ȶ+"\n");}try{if(Ȭ){ț=ș[Ȯ];ɲ();return;}if(ɐ!=""){Ȟ=ɐ;Ȯ=1;Ƞ="";ȝ=DateTime.Now;Ȝ=ȗ.Count;}if(useDynamicScriptSpeed){if(Ȱ>0)
{Ǧ("Dynamic script speed control");Í("..");Ȱ--;return;}}if(ȯ<extraScriptTicks){Runtime.UpdateFrequency=UpdateFrequency.
Update1;ȯ++;return;}else{Runtime.UpdateFrequency=UpdateFrequency.Update10;}if(ȫ){if(Ȳ==0)ő();if(Ȳ==1)Ŏ();if(Ȳ==2)ŗ();if(Ȳ==3)Ŀ(
);if(Ȳ==4)ļ();if(Ȳ>4)Ȳ=0;ȫ=false;return;}if(Ȯ==0||ȴ||ʅ){if(!Ⱥ)ɞ();if(ʅ)return;ȴ=false;Ⱥ=false;if(!Ǥ()){ʘ=ǣ(mainLCDKeyword
,Ƶ,defaultFont,defaultFontSize,defaultPadding);ʗ=ǣ(warningsLCDKeyword,Ș,defaultFont,defaultFontSize,defaultPadding);ʙ=ǣ(
actionsLCDKeyword,ȹ,defaultFont,defaultFontSize,defaultPadding);ʖ=ǣ(performanceLCDKeyword,Ș,defaultFont,defaultFontSize,defaultPadding);ʕ
=ǣ(inventoryLCDKeyword,null,defaultFont,defaultFontSize,defaultPadding);}else{ȴ=true;Ⱥ=true;return;}}if(!Ȥ)ɫ();if(ɰ(Ȟ))
return;ȯ=0;ȫ=true;if(showTimeStamp){ʚ=DateTime.Now.ToString(timeFormat)+":\n";}if(Ȯ==1){Ú();}if(Ȯ==2){ß();}if(Ȯ==3){if(
enableNameCorrection)ό();}if(Ȯ==4){if(autoContainerAssignment){if(unassignEmptyContainers)ϑ();if(assignNewContainers)ɛ();}}if(Ȯ==5){if(ʌ.
Count!=0)Ϯ();}if(Ȯ==6){if(!Ϛ())ȭ=true;}if(Ȯ==7){if(balanceTypeContainers)Β();}if(Ȯ==8){θ();}if(Ȯ==9){ϴ(ʿ);ϴ(ʌ);}if(Ȯ==10){ƺ()
;}if(Ȯ==11){if(enableAutocrafting||enableAutodisassembling)ǃ();}if(Ȯ==12){ː();}if(Ȯ==13){if(splitAssemblerTasks)Ο();if(
sortAssemblerQueue)β();}if(Ȯ==14){if(enableAssemblerCleanup)γ();if(enableBasicIngotCrafting){if(ʺ.Count>0){enableBasicIngotCrafting=false;
}else{Ε();}}}if(Ȯ==15){Û();}if(Ȯ==16){ˆ();}if(Ȯ==17){if(enableOreBalancing)ˁ();if(sortRefiningQueue){ˏ(ʸ,Ɏ);ˏ(ʷ,Ɇ);}}if(Ȯ
==18){if(enableIceBalancing)Ĩ();}if(Ȯ==19){if(enableUraniumBalancing){ç("uraniumBalancing","true");Ċ();}else if(!
enableUraniumBalancing&&é("uraniumBalancing")=="true"){ç("uraniumBalancing","false");foreach(IMyReactor ą in ʯ){ą.UseConveyorSystem=true;}}}Ǧ(
ț);Í();Ȱ=(int)Math.Floor((ǭ>20?20:ǭ)/maxCurrentMs);if(ȭ){ȭ=false;}else if(Ȯ>=19){Ȯ=0;ț=Ț[0];ȳ=new HashSet<string>(ȱ);ȱ.
Clear();if(ȸ>0)ȸ--;if(ȳ.Count==0)ŕ=null;}else{Ȯ++;ț=Ț[Ȯ];}}catch(Exception e){ɵ(e);Ƽ("Critical error in script step:\n"+ț+
" (ID: "+Ȯ+")\n\n"+e);}}void ɵ(Exception ɳ){ȸ++;ȴ=true;ȫ=false;ʅ=false;ȶ=ɳ.ToString();}void ɲ(){string ɱ="..";if(Ȯ>=0)Echo(ș[0]+
ɱ+" ("+(Ȯ+1)+"/10)\n");if(Ȯ>=2){Echo(ș[2]+ɱ);if(Ȯ==2)ɞ();if(ʅ)return;}if(Ȯ>=3){Echo(ș[3]+ɱ);if(Ȯ==3){if(!ä()){ȵ=true;
enableAutocrafting=false;enableAutodisassembling=false;}}Echo("-> Loaded "+Ȓ.Count+" items.");if(ȵ){Echo("-> No assemblers found!");Echo(
"-> Autocrafting deactivated!");}}if(Ȯ>=4){if(enableAutocrafting||enableAutodisassembling){Echo(ș[4]+ɱ);if(Ȯ==4){GridTerminalSystem.GetBlocksOfType<
IMyTextPanel>(ʮ,È=>È.IsSameConstructAs(Me)&&È.CustomName.Contains(autocraftingKeyword));if(ʮ.Count>0){foreach(var ǂ in ʾ){ǂ.Mode=
MyAssemblerMode.Disassembly;ǂ.ClearQueue();ǂ.Mode=MyAssemblerMode.Assembly;ǂ.ClearQueue();}}}}else{Echo(
"Skipped: Assembler queue clearing");}}if(Ȯ>=5){Echo(ș[5]+ɱ);if(Ȯ==5)ɤ();if(Ȯ==5)ɥ();}if(Ȯ>=6){if(scriptMode=="station"){Ȥ=true;}else if(Me.CubeGrid.
IsStatic&&scriptMode!="ship"){Ȥ=true;}Echo(ș[6]+" to: "+(Ȥ?"station..":"ship.."));if(Ȯ==6)Me.CustomData=(Ȥ?ȣ:Ȣ)+Me.CustomData.
Replace(ȣ,"").Replace(Ȣ,"");}if(Ȯ>=7){Echo("\n"+ș[7]);}if(Ȯ>=8){Ȯ=1;ț=Ț[8];Ȭ=false;return;}Ȯ++;return;}bool ɰ(string ɐ){if(ɐ.
Contains("pauseThisPB")){Echo("Script execution paused!\n");var ɴ=ɐ.Split(';');if(ɴ.Length==3){Echo("Found:");Echo("'"+ɴ[1]+"'")
;Echo("on grid:");Echo("'"+ɴ[2]+"'");Echo("also running the script.\n");Echo("Type container protection: "+(
protectTypeContainers?"ON":"OFF")+"\n");Echo("Everything else is managed by the other script.");}return true;}bool ɯ=true;bool ɮ=true;bool ɭ=
true;if(ɐ.EndsWith(" true")){ɭ=false;}else if(ɐ.EndsWith(" false")){ɮ=false;ɭ=false;}if(ɐ=="reset"){ƻ();return true;}else if
(ɐ=="findBlueprints"){if(!ǈ()){Echo("Checked "+Ɉ+" / "+Ȓ.Count+" saved items.");return true;}else{Ƞ="Checked all "+Ȓ.
Count+" saved items! Found "+(ȗ.Count-Ȝ)+" new autocrafting blueprints!";}}else if(ɐ=="showCountdown"){}else if(ɐ.StartsWith(
"balanceTypeContainers")){Ȩ="Balance type containers";if(ɭ)ɮ=!balanceTypeContainers;balanceTypeContainers=ɮ;}else if(ɐ.StartsWith(
"showFillLevel")){Ȩ="Show fill level";if(ɭ)ɮ=!showFillLevel;showFillLevel=ɮ;}else if(ɐ.StartsWith("autoContainerAssignment")){Ȩ=
"Auto assign containers";if(ɭ)ɮ=!autoContainerAssignment;autoContainerAssignment=ɮ;}else if(ɐ.StartsWith("assignNewContainers")){Ȩ=
"Assign new containers";if(ɭ)ɮ=!assignNewContainers;assignNewContainers=ɮ;}else if(ɐ.StartsWith("unassignEmptyContainers")){Ȩ=
"Unassign empty containers";if(ɭ)ɮ=!unassignEmptyContainers;unassignEmptyContainers=ɮ;}else if(ɐ.StartsWith("oresIngotsInOne")){Ȩ=
"Assign ores and ingots as one";if(ɭ)ɮ=!oresIngotsInOne;oresIngotsInOne=ɮ;}else if(ɐ.StartsWith("toolsAmmoBottlesInOne")){Ȩ=
"Assign tools, ammo and bottles as one";if(ɭ)ɮ=!toolsAmmoBottlesInOne;toolsAmmoBottlesInOne=ɮ;}else if(ɐ.StartsWith("fillBottles")){Ȩ="Fill bottles";if(ɭ)ɮ=!
fillBottles;fillBottles=ɮ;}else if(ɐ.StartsWith("enableAutocrafting")){Ȩ="Autocrafting";if(ɭ)ɮ=!enableAutocrafting;
enableAutocrafting=ɮ;}else if(ɐ.StartsWith("enableAutodisassembling")){Ȩ="Autodisassembling";if(ɭ)ɮ=!enableAutodisassembling;
enableAutodisassembling=ɮ;}else if(ɐ.StartsWith("showUnlearnedItems")){Ȩ="Show unlearned items";if(ɭ)ɮ=!showUnlearnedItems;showUnlearnedItems=ɮ
;}else if(ɐ.StartsWith("useDockedAssemblers")){Ȩ="Use docked assemblers";if(ɭ)ɮ=!useDockedAssemblers;useDockedAssemblers=
ɮ;}else if(ɐ.StartsWith("headerOnEveryScreen")){Ȩ="Show header on every autocrafting screen";if(ɭ)ɮ=!headerOnEveryScreen;
headerOnEveryScreen=ɮ;}else if(ɐ.StartsWith("sortAssemblerQueue")){Ȩ="Sort assembler queue";if(ɭ)ɮ=!sortAssemblerQueue;sortAssemblerQueue=ɮ
;}else if(ɐ.StartsWith("enableBasicIngotCrafting")){Ȩ="Basic ingot crafting";if(ɭ)ɮ=!enableBasicIngotCrafting;
enableBasicIngotCrafting=ɮ;}else if(ɐ.StartsWith("disableBasicAutocrafting")){Ȩ="Disable autocrafting in survival kits";if(ɭ)ɮ=!
disableBasicAutocrafting;disableBasicAutocrafting=ɮ;}else if(ɐ.StartsWith("allowSpecialSteal")){Ȩ="Allow special container steal";if(ɭ)ɮ=!
allowSpecialSteal;allowSpecialSteal=ɮ;}else if(ɐ.StartsWith("enableOreBalancing")){Ȩ="Ore balancing";if(ɭ)ɮ=!enableOreBalancing;
enableOreBalancing=ɮ;}else if(ɐ.StartsWith("enableScriptRefineryFilling")){Ȩ="Script assisted refinery filling";if(ɭ)ɮ=!
enableScriptRefineryFilling;enableScriptRefineryFilling=ɮ;}else if(ɐ.StartsWith("sortRefiningQueue")){Ȩ="Sort refinery queue";if(ɭ)ɮ=!
sortRefiningQueue;sortRefiningQueue=ɮ;}else if(ɐ.StartsWith("useDockedRefineries")){Ȩ="Use docked refineries";if(ɭ)ɮ=!useDockedRefineries
;useDockedRefineries=ɮ;}else if(ɐ.StartsWith("enableIceBalancing")){Ȩ="Ice balancing";if(ɭ)ɮ=!enableIceBalancing;
enableIceBalancing=ɮ;}else if(ɐ.StartsWith("fillOfflineGenerators")){Ȩ="Fill offline O2/H2 generators";if(ɭ)ɮ=!fillOfflineGenerators;
fillOfflineGenerators=ɮ;}else if(ɐ.StartsWith("enableUraniumBalancing")){Ȩ="Uranium balancing";if(ɭ)ɮ=!enableUraniumBalancing;
enableUraniumBalancing=ɮ;}else if(ɐ.StartsWith("fillOfflineReactors")){Ȩ="Fill offline reactors";if(ɭ)ɮ=!fillOfflineReactors;
fillOfflineReactors=ɮ;}else if(ɐ.StartsWith("enableAssemblerCleanup")){Ȩ="Assembler cleanup";if(ɭ)ɮ=!enableAssemblerCleanup;
enableAssemblerCleanup=ɮ;}else if(ɐ.StartsWith("enableInternalSorting")){Ȩ="Internal sorting";if(ɭ)ɮ=!enableInternalSorting;
enableInternalSorting=ɮ;}else if(ɐ.StartsWith("useDynamicScriptSpeed")){Ȩ="Dynamic script speed";if(ɭ)ɮ=!useDynamicScriptSpeed;
useDynamicScriptSpeed=ɮ;}else if(ɐ.StartsWith("excludeWelders")){Ȩ="Exclude welders";if(ɭ)ɮ=!excludeWelders;excludeWelders=ɮ;}else if(ɐ.
StartsWith("excludeGrinders")){Ȩ="Exclude grinders";if(ɭ)ɮ=!excludeGrinders;excludeGrinders=ɮ;}else if(ɐ.StartsWith(
"excludeDrills")){Ȩ="Exclude drills";if(ɭ)ɮ=!excludeDrills;excludeDrills=ɮ;}else if(ɐ.StartsWith("connectionCheck")){Ȩ=
"Connection check";if(ɭ)ɮ=!connectionCheck;connectionCheck=ɮ;ɥ();}else if(ɐ.StartsWith("showNoConveyorTag")){Ȩ="Show no conveyor access";
if(ɭ)ɮ=!showNoConveyorTag;showNoConveyorTag=ɮ;ɥ();}else if(ɐ.StartsWith("protectTypeContainers")){Ȩ=
"Protect type containers";if(ɭ)ɮ=!protectTypeContainers;protectTypeContainers=ɮ;}else if(ɐ.StartsWith("enableNameCorrection")){Ȩ=
"Name correction";if(ɭ)ɮ=!enableNameCorrection;enableNameCorrection=ɮ;}else{ɯ=false;}if(ɯ){TimeSpan ɬ=DateTime.Now-ȝ;if(Ƞ=="")Ƞ=Ȩ+
" temporarily "+(ɮ?"enabled":"disabled")+"!\n";Echo(Ƞ);Echo("Continuing in "+Math.Ceiling(3-ɬ.TotalSeconds)+" seconds..");Ȟ=
"showCountdown";if(ɬ.TotalSeconds>=3){Ǎ(Ƞ);Ȟ="";}}return ɯ;}void ɫ(){List<IMyProgrammableBlock>ɪ=new List<IMyProgrammableBlock>();
GridTerminalSystem.GetBlocksOfType(ɪ,ɩ=>ɩ!=Me);if(Ȟ.StartsWith("pauseThisPB")||Ȟ==""){Ȟ="";foreach(var ɨ in ɪ){if(ɨ.CustomData.Contains(ȣ)
||(ɨ.CustomData.Contains(Ȣ)&&í(ɨ)<í(Me))){Ȟ="pauseThisPB;"+ɨ.CustomName+";"+ɨ.CubeGrid.CustomName;foreach(var W in ʿ){if(
protectTypeContainers&&!W.CustomName.Contains(ȡ)&&W.IsSameConstructAs(Me))W.CustomName=ȡ+W.CustomName;}return;}}if(Ȟ==""){foreach(var W in ʽ)
{W.CustomName=W.CustomName.Replace(ȡ,"");}}}}void ɼ(){ʨ.Clear();ʓ.Clear();GridTerminalSystem.GetBlocksOfType(ʻ);foreach(
var ʀ in ʻ){if(ʀ.Status!=MyShipConnectorStatus.Connected)continue;try{if(ʀ.OtherConnector.CubeGrid.IsSameConstructAs(Me.
CubeGrid)){if(ʀ.CustomName.Contains(noSortingKeyword))ʨ.Add(ʀ.CubeGrid);if(ʀ.CustomName.Contains(noIIMKeyword))ʓ.Add(ʀ.CubeGrid)
;}else{if(ʀ.CustomName.Contains(noSortingKeyword))ʨ.Add(ʀ.OtherConnector.CubeGrid);if(ʀ.CustomName.Contains(noIIMKeyword)
)ʓ.Add(ʀ.OtherConnector.CubeGrid);}}catch(Exception){Ƽ("Error while checking connection status of:\n"+ʀ.Name);}}ʨ.Remove(
Me.CubeGrid);ʓ.Remove(Me.CubeGrid);}void ɿ(){if(ʉ!=null){try{ʉ=ʊ.GetInventory(0);}catch{ʉ=null;}}if(ʉ==null){try{foreach(
var W in ʿ){foreach(var d in ʩ){if(W==d)continue;if(W.GetInventory(0).IsConnectedTo(d.GetInventory(0))){ʊ=ʿ[0];ʉ=ʊ.
GetInventory(0);return;}}}}catch{ʉ=null;}}}void ɾ(IMyTerminalBlock d){foreach(var u in ʓ){if(u.IsSameConstructAs(Me.CubeGrid)){if(d.
CubeGrid==u)return;}else{if(d.CubeGrid.IsSameConstructAs(u))return;}}foreach(var C in excludedBlocks){if(d.BlockDefinition.
SubtypeId.Contains(C))return;}if(!ι(d))return;if(d is IMyShipWelder&&excludeWelders)return;if(d is IMyShipGrinder&&
excludeGrinders)return;if(d is IMyShipDrill&&excludeDrills)return;string Î=d.CustomName;if(Î.Contains(ȡ)){ʽ.Add(d);return;}bool ɽ=Î.
Contains(specialContainerKeyword),ɻ=false,ɺ=false,ɹ=false,ɸ=Î.Contains(learnKeyword)||Î.Contains(learnManyKeyword),ɷ=true,ɶ=
false;foreach(var ð in lockedContainerKeywords){if(Î.Contains(ð)){ɻ=true;break;}}foreach(var ð in manualMachineKeywords){if(Î
.Contains(ð)){ɺ=true;break;}}if(!d.ShowInInventory&&treatNotShownAsHidden){ɹ=true;}else{foreach(var ð in
hiddenContainerKeywords){if(Î.Contains(ð)){ɹ=true;break;}}}foreach(var u in ʨ){if(u.IsSameConstructAs(Me.CubeGrid)){if(d.CubeGrid==u)return;}
else{if(!ɽ&&!(d is IMyReactor)&&!(d is IMyGasGenerator)){if(d.CubeGrid.IsSameConstructAs(u))return;}}}if(!ɹ)ʩ.Add(d);if(
connectionCheck){if(ʉ!=null){if(!d.GetInventory(0).IsConnectedTo(ʉ)){ɷ=false;}}if(!ɷ){if(showNoConveyorTag)ɖ(d,"[No Conveyor]");return;
}else{ɖ(d,"[No Conveyor]",false);}}if(Î.Contains(oreContainerKeyword)){ʝ.Add(d);ɶ=true;}if(Î.Contains(
ingotContainerKeyword)){ʑ.Add(d);ɶ=true;}if(Î.Contains(componentContainerKeyword)){ʐ.Add(d);ɶ=true;}if(Î.Contains(toolContainerKeyword)){ʏ.
Add(d);ɶ=true;}if(Î.Contains(ammoContainerKeyword)){ʎ.Add(d);ɶ=true;}if(Î.Contains(bottleContainerKeyword)){ʍ.Add(d);ɶ=true
;}if(ɽ){ʌ.Add(d);if(d.CustomData.Length<200)ù(d);}if(ɶ)ʿ.Add(d);if(d.GetType().ToString().Contains("Weapon")&&!(d is
IMyShipDrill))return;if(d is IMyRefinery){if((useDockedRefineries||d.IsSameConstructAs(Me))&&!ɽ&&!ɺ&&d.IsWorking){(d as IMyRefinery)
.UseConveyorSystem=true;ʺ.Add(d as IMyRefinery);if(d.BlockDefinition.SubtypeId=="Blast Furnace"){ʷ.Add(d as IMyRefinery);
}else{ʸ.Add(d as IMyRefinery);}}if(!ɻ&&d.GetInventory(1).ItemCount>0)ʹ.Add(d as IMyRefinery);}else if(d is IMyAssembler){
if((useDockedAssemblers||d.IsSameConstructAs(Me))&&!ɺ&&!ɸ&&d.IsWorking){ʾ.Add(d as IMyAssembler);if(d.BlockDefinition.
SubtypeId.Contains("Survival"))ʲ.Add(d as IMyAssembler);}if(!ɻ&&!ɸ&&d.GetInventory(1).ItemCount>0)ʶ.Add(d as IMyAssembler);if(ɸ)ʴ
.Add(d as IMyAssembler);}else if(d is IMyGasGenerator){if(!ɽ&&!ɺ&&d.IsFunctional){if(fillOfflineGenerators&&!(d as
IMyGasGenerator).Enabled){ʱ.Add(d as IMyGasGenerator);}else if((d as IMyGasGenerator).Enabled){ʱ.Add(d as IMyGasGenerator);}}}else if(d
is IMyGasTank){if(!ɽ&&!ɺ&&!ɻ&&d.IsWorking&&d.IsSameConstructAs(Me)){ʰ.Add(d as IMyGasTank);}}else if(d is IMyReactor){if(!
ɽ&&!ɺ&&d.IsFunctional){if(fillOfflineReactors&&!(d as IMyReactor).Enabled){ʯ.Add(d as IMyReactor);}else if((d as
IMyReactor).Enabled){ʯ.Add(d as IMyReactor);}}}else if(d is IMyCargoContainer){if(d.IsSameConstructAs(Me)&&!ɶ&&!ɻ&&!ɽ)ʼ.Add(d);}if
(d.InventoryCount==1&&!ɽ&&!ɻ&&!(d is IMyReactor)){if(d.GetInventory(0).ItemCount>0)ʬ.Add(d);if(!d.BlockDefinition.
TypeIdString.Contains("Oxygen")&&!(d is IMyConveyorSorter)){if(d.IsSameConstructAs(Me)){ʵ.Insert(0,d);}else{if(
useConnectedGridsTemporarily)ʵ.Add(d);}}}}void ɞ(){if(!ʅ){ɼ();if(connectionCheck)ɿ();try{for(int S=0;S<ʌ.Count;S++){if(!ʌ[S].CustomName.Contains(
specialContainerKeyword))ʌ[S].CustomData="";}}catch{}ʿ.Clear();ʝ.Clear();ʑ.Clear();ʐ.Clear();ʏ.Clear();ʎ.Clear();ʍ.Clear();ʌ.Clear();ʼ.Clear();
ʽ.Clear();ʩ.Clear();ʬ.Clear();ʵ.Clear();ʺ.Clear();ʸ.Clear();ʷ.Clear();ʹ.Clear();ʾ.Clear();ʲ.Clear();ʶ.Clear();ʴ.Clear();ʱ
.Clear();ʰ.Clear();ʯ.Clear();ʈ=null;ʄ=0;GridTerminalSystem.GetBlocksOfType<IMyTerminalBlock>(ʪ,Ÿ=>Ÿ.HasInventory&&Ÿ.
OwnerId!=0);}Runtime.UpdateFrequency=UpdateFrequency.Update1;for(int S=ʄ;S<ʪ.Count;S++){if(ʪ[S].CubeGrid.CustomName.Contains(
noSortingKeyword))ʨ.Add(ʪ[S].CubeGrid);if(ʪ[S].CubeGrid.CustomName.Contains(noIIMKeyword))ʓ.Add(ʪ[S].CubeGrid);try{ɾ(ʪ[S]);}catch(
NullReferenceException){Ƽ("Error while indexing inventory blocks:\n"+ʪ[S].Name+"\nis no longer available..");}ʄ++;if(S%200==0){ʅ=true;return;}
}if(ʃ==0)ɓ(ʝ);if(ʃ==1)ɓ(ʑ);if(ʃ==2)ɓ(ʐ);if(ʃ==3)ɓ(ʏ);if(ʃ==4)ɓ(ʎ);if(ʃ==5)ɓ(ʌ);if(ʃ==6)ɓ(ʍ);if(ʃ==7)ʼ.Sort((ɑ,Ÿ)=>Ÿ.
GetInventory().MaxVolume.ToIntSafe().CompareTo(ɑ.GetInventory().MaxVolume.ToIntSafe()));ʃ++;if(ʃ>7){ʃ=0;}else{ʅ=true;return;}if(
disableBasicAutocrafting&&ʾ.Count!=ʲ.Count)ʾ.RemoveAll(ŉ=>ŉ.BlockDefinition.SubtypeId.Contains("Survival"));if(fillBottles){ʬ.Sort((ɑ,Ÿ)=>Ÿ.
BlockDefinition.TypeIdString.Contains("Oxygen").CompareTo(ɑ.BlockDefinition.TypeIdString.Contains("Oxygen")));}ʳ.Clear();bool ɕ;foreach
(var ǂ in ʾ){if(ʳ.Count==0){ʳ.Add(ǂ);continue;}ɕ=false;foreach(var ɔ in ʳ){if(ɔ.BlockDefinition.ToString()==ǂ.
BlockDefinition.ToString()){ɕ=true;}}if(!ɕ){ʳ.Add(ǂ);}}ʅ=false;Runtime.UpdateFrequency=UpdateFrequency.Update10;}void ɓ(List<
IMyTerminalBlock>ɒ){if(ɒ.Count>=2&&ɒ.Count<=500)ɒ.Sort((ɑ,Ÿ)=>í(ɑ).CompareTo(í(Ÿ)));if(!Ǥ())ʃ++;}void ɖ(IMyTerminalBlock d,string ɗ,bool
ɧ=true){if(ɧ){if(d.CustomName.Contains(ɗ))return;d.CustomName+=" "+ɗ;}else{if(!d.CustomName.Contains(ɗ))return;d.
CustomName=d.CustomName.Replace(" "+ɗ,"").Replace(ɗ,"").TrimEnd(' ');}}void ɥ(){for(int S=0;S<ʩ.Count;S++){ɖ(ʩ[S],"[No Conveyor]",
false);}}void ɤ(){bool ɣ=false;string ɢ=é("oreContainer");string ɡ=é("ingotContainer");string ɠ=é("componentContainer");
string ɦ=é("toolContainer");string ɟ=é("ammoContainer");string ɝ=é("bottleContainer");string ɜ=é("specialContainer");if(
oreContainerKeyword!=ɢ){ɣ=true;}else if(ingotContainerKeyword!=ɡ){ɣ=true;}else if(componentContainerKeyword!=ɠ){ɣ=true;}else if(
toolContainerKeyword!=ɦ){ɣ=true;}else if(ammoContainerKeyword!=ɟ){ɣ=true;}else if(bottleContainerKeyword!=ɝ){ɣ=true;}else if(
specialContainerKeyword!=ɜ){ɣ=true;}if(ɣ){for(int S=0;S<ʩ.Count;S++){if(ʩ[S].CustomName.Contains(ɢ)){ʩ[S].CustomName=ʩ[S].CustomName.Replace(ɢ,
oreContainerKeyword);}if(ʩ[S].CustomName.Contains(ɡ)){ʩ[S].CustomName=ʩ[S].CustomName.Replace(ɡ,ingotContainerKeyword);}if(ʩ[S].CustomName.
Contains(ɠ)){ʩ[S].CustomName=ʩ[S].CustomName.Replace(ɠ,componentContainerKeyword);}if(ʩ[S].CustomName.Contains(ɦ)){ʩ[S].
CustomName=ʩ[S].CustomName.Replace(ɦ,toolContainerKeyword);}if(ʩ[S].CustomName.Contains(ɟ)){ʩ[S].CustomName=ʩ[S].CustomName.
Replace(ɟ,ammoContainerKeyword);}if(ʩ[S].CustomName.Contains(ɝ)){ʩ[S].CustomName=ʩ[S].CustomName.Replace(ɝ,
bottleContainerKeyword);}if(ʩ[S].CustomName.Contains(ɜ)){ʩ[S].CustomName=ʩ[S].CustomName.Replace(ɜ,specialContainerKeyword);}}ç("oreContainer"
,oreContainerKeyword);ç("ingotContainer",ingotContainerKeyword);ç("componentContainer",componentContainerKeyword);ç(
"toolContainer",toolContainerKeyword);ç("ammoContainer",ammoContainerKeyword);ç("bottleContainer",bottleContainerKeyword);ç(
"specialContainer",specialContainerKeyword);}}void ɛ(){string ɚ="";foreach(var ə in ʋ){if(assignOres&&ə==ɂ){ɚ=ɂ;}else if(assignIngots&&ə==
Ɂ){ɚ=Ɂ;}else if(assignComponents&&ə==ɀ){ɚ=ɀ;}else if(assignTools&&(ə==Ȫ||ə==Ȗ||ə==ȟ||ə==Ȕ)){ɚ=Ȫ;}else if(assignAmmo&&ə==ȿ
){ɚ=ȿ;}else if(assignBottles&&(ə==Ⱦ||ə==Ƚ)){ɚ=Ⱦ;}if(ɚ!="")break;}for(int S=0;S<ʼ.Count;S++){bool Ȼ=false;bool ɘ=false;
string ˀ=ʼ[S].CustomName;string ϕ="";string ϓ=" and ";bool ϒ=false;if(assignOres&&(ʝ.Count==0||ɚ==ɂ)){if(oresIngotsInOne){ɘ=
true;}else{ʼ[S].CustomName+=" "+oreContainerKeyword;ʝ.Add(ʼ[S]);ϕ="Ores";}}else if(assignIngots&&(ʑ.Count==0||ɚ==Ɂ)){if(
oresIngotsInOne){ɘ=true;}else{ʼ[S].CustomName+=" "+ingotContainerKeyword;ʑ.Add(ʼ[S]);ϕ="Ingots";}}else if(assignComponents&&(ʐ.Count==0
||ɚ==ɀ)){ʼ[S].CustomName+=" "+componentContainerKeyword;ʐ.Add(ʼ[S]);ϕ="Components";}else if(assignTools&&(ʏ.Count==0||ɚ==Ȫ
)){if(toolsAmmoBottlesInOne){Ȼ=true;}else{ʼ[S].CustomName+=" "+toolContainerKeyword;ʏ.Add(ʼ[S]);ϕ="Tools";}}else if(
assignAmmo&&(ʎ.Count==0||ɚ==ȿ)){if(toolsAmmoBottlesInOne){Ȼ=true;}else{ʼ[S].CustomName+=" "+ammoContainerKeyword;ʎ.Add(ʼ[S]);ϕ=
"Ammo";}}else if(assignBottles&&(ʍ.Count==0||ɚ==Ⱦ)){if(toolsAmmoBottlesInOne){Ȼ=true;}else{ʼ[S].CustomName+=" "+
bottleContainerKeyword;ʍ.Add(ʼ[S]);ϕ="Bottles";}}if(ɘ){if(assignOres){ʼ[S].CustomName+=" "+oreContainerKeyword;ʝ.Add(ʼ[S]);ϕ="Ores";ϒ=true;}if
(assignIngots){ʼ[S].CustomName+=" "+ingotContainerKeyword;ʑ.Add(ʼ[S]);ϕ+=(ϒ?ϓ:"")+"Ingots";}}if(Ȼ){if(assignTools){ʼ[S].
CustomName+=" "+toolContainerKeyword;ʏ.Add(ʼ[S]);ϕ="Tools";ϒ=true;}if(assignAmmo){ʼ[S].CustomName+=" "+ammoContainerKeyword;ʎ.Add(
ʼ[S]);ϕ+=(ϒ?ϓ:"")+"Ammo";ϒ=true;}if(assignBottles){ʼ[S].CustomName+=" "+bottleContainerKeyword;ʍ.Add(ʼ[S]);ϕ+=(ϒ?ϓ:"")+
"Bottles";}}if(ϕ!=""){ɚ="";Ǎ("Assigned '"+ˀ+"' as a new container for type '"+ϕ+"'.");}}ʋ.Clear();}void ϑ(){if(unassignOres)ϐ(ʝ,
oreContainerKeyword);if(unassignIngots)ϐ(ʑ,ingotContainerKeyword);if(unassignComponents)ϐ(ʐ,componentContainerKeyword);if(unassignTools)ϐ(ʏ
,toolContainerKeyword);if(unassignAmmo)ϐ(ʎ,ammoContainerKeyword);if(unassignBottles)ϐ(ʍ,bottleContainerKeyword);}void ϐ(
List<IMyTerminalBlock>ď,string Ϗ){IMyTerminalBlock ώ=null;if(ȥ.TryGetValue(Ϗ,out ώ)){ȥ.Remove(Ϗ);if(ώ==null)return;if(ώ.
GetInventory(0).ItemCount==0){string ϔ=System.Text.RegularExpressions.Regex.Replace(ώ.CustomName,@"("+Ϗ+@")","");ϔ=System.Text.
RegularExpressions.Regex.Replace(ϔ,@"\(\d+\.?\d*\%\)","");ϔ=ϔ.Replace("  "," ");ώ.CustomName=ϔ.TrimEnd(' ');ʿ.Remove(ώ);Ǎ("Unassigned '"+ϔ
+"' from being a container for type '"+Ϗ+"'.");}return;}if(ď.Count>1){int ύ=0;foreach(var W in ď){if(W.CustomName.
Contains("[P"))continue;if(W.GetInventory(0).ItemCount==0){ώ=W;ύ++;}}if(ύ>1){ȥ[Ϗ]=ώ;}}}void ό(){string Î,ϋ;List<string>ϊ=new
List<string>();for(int S=0;S<ʩ.Count;S++){Î=ʩ[S].CustomName;ϋ=Î.ToLower();ϊ.Clear();if(ϋ.Contains(oreContainerKeyword.
ToLower())&&!Î.Contains(oreContainerKeyword))ϊ.Add(oreContainerKeyword);if(ϋ.Contains(ingotContainerKeyword.ToLower())&&!Î.
Contains(ingotContainerKeyword))ϊ.Add(ingotContainerKeyword);if(ϋ.Contains(componentContainerKeyword.ToLower())&&!Î.Contains(
componentContainerKeyword))ϊ.Add(componentContainerKeyword);if(ϋ.Contains(toolContainerKeyword.ToLower())&&!Î.Contains(toolContainerKeyword))ϊ.
Add(toolContainerKeyword);if(ϋ.Contains(ammoContainerKeyword.ToLower())&&!Î.Contains(ammoContainerKeyword))ϊ.Add(
ammoContainerKeyword);if(ϋ.Contains(bottleContainerKeyword.ToLower())&&!Î.Contains(bottleContainerKeyword))ϊ.Add(bottleContainerKeyword);
foreach(var ð in lockedContainerKeywords){if(ϋ.Contains(ð.ToLower())&&!Î.Contains(ð)){ϊ.Add(ð);break;}}foreach(var ð in
hiddenContainerKeywords){if(ϋ.Contains(ð.ToLower())&&!Î.Contains(ð)){ϊ.Add(ð);break;}}foreach(var ð in manualMachineKeywords){if(ϋ.Contains(ð.
ToLower())&&!Î.Contains(ð)){ϊ.Add(ð);break;}}if(ϋ.Contains(specialContainerKeyword.ToLower())&&!Î.Contains(
specialContainerKeyword))ϊ.Add(specialContainerKeyword);if(ϋ.Contains(noSortingKeyword.ToLower())&&!Î.Contains(noSortingKeyword))ϊ.Add(
noSortingKeyword);if(ϋ.Contains(noIIMKeyword.ToLower())&&!Î.Contains(noIIMKeyword))ϊ.Add(noIIMKeyword);if(ϋ.Contains(autocraftingKeyword
.ToLower())&&!Î.Contains(autocraftingKeyword))ϊ.Add(autocraftingKeyword);if(ϋ.Contains(assembleKeyword.ToLower())&&!Î.
Contains(assembleKeyword))ϊ.Add(assembleKeyword);if(ϋ.Contains(disassembleKeyword.ToLower())&&!Î.Contains(disassembleKeyword))ϊ.
Add(disassembleKeyword);if(ϋ.Contains(learnKeyword.ToLower())&&!Î.Contains(learnKeyword))ϊ.Add(learnKeyword);if(ϋ.Contains(
learnManyKeyword.ToLower())&&!Î.Contains(learnManyKeyword))ϊ.Add(learnManyKeyword);if(ϋ.Contains("[p")&&!Î.Contains("[P"))ϊ.Add("[P");if
(ϋ.Contains("[pmax]")&&!Î.Contains("[PMax]"))ϊ.Add("[PMax]");if(ϋ.Contains("[pmin]")&&!Î.Contains("[PMin]"))ϊ.Add(
"[PMin]");foreach(var Ð in ϊ){ʩ[S].CustomName=ʩ[S].CustomName.ƍ(Ð,Ð);Ǎ("Corrected name\nof: '"+Î+"'\nto: '"+ʩ[S].CustomName+"'")
;}}var ź=new List<IMyTerminalBlock>();GridTerminalSystem.GetBlocksOfType<IMyTextSurfaceProvider>(ź,Ÿ=>Ÿ.IsSameConstructAs
(Me));for(int S=0;S<ź.Count;S++){Î=ź[S].CustomName;ϋ=Î.ToLower();ϊ.Clear();if(ϋ.Contains(mainLCDKeyword.ToLower())&&!Î.
Contains(mainLCDKeyword))ϊ.Add(mainLCDKeyword);if(ϋ.Contains(warningsLCDKeyword.ToLower())&&!Î.Contains(warningsLCDKeyword))ϊ.
Add(warningsLCDKeyword);if(ϋ.Contains(actionsLCDKeyword.ToLower())&&!Î.Contains(actionsLCDKeyword))ϊ.Add(actionsLCDKeyword)
;if(ϋ.Contains(performanceLCDKeyword.ToLower())&&!Î.Contains(performanceLCDKeyword))ϊ.Add(performanceLCDKeyword);if(ϋ.
Contains(inventoryLCDKeyword.ToLower())&&!Î.Contains(inventoryLCDKeyword))ϊ.Add(inventoryLCDKeyword);foreach(var Ð in ϊ){ź[S].
CustomName=ź[S].CustomName.ƍ(Ð,Ð);Ǎ("Corrected name\nof: '"+Î+"'\nto: '"+ź[S].CustomName+"'");}}}bool Ϛ(){ț=Ț[6]+" stage "+(ʒ+1)+
"/10";if(ʒ==0)ϙ(ɂ,ʝ,oreContainerKeyword);if(ʒ==1)ϙ(Ɂ,ʑ,ingotContainerKeyword);if(ʒ==2)ϙ(ɀ,ʐ,componentContainerKeyword);if(ʒ==
3)ϙ(Ȫ,ʏ,toolContainerKeyword);if(ʒ==4)ϙ(ȿ,ʎ,ammoContainerKeyword);if(ʒ==5)ϙ(Ⱦ,ʍ,bottleContainerKeyword);if(ʒ==6)ϙ(Ƚ,ʍ,
bottleContainerKeyword);if(ʒ==7)ϙ(Ȗ,ʏ,toolContainerKeyword);if(ʒ==8)ϙ(ȟ,ʏ,toolContainerKeyword);if(ʒ==9)ϙ(Ȕ,ʏ,toolContainerKeyword);ʒ++;if(ʒ>9
){ʒ=0;return true;}else{return false;}}void ϙ(string κ,List<IMyTerminalBlock>Ϙ,string ϛ){if(Ϙ.Count==0){Ƽ(
"There are no containers for type '"+ϛ+"'!\nBuild new ones or add the tag to existing ones!");ʋ.Add(κ);return;}IMyTerminalBlock N=null;int ϗ=int.MaxValue;
for(int S=0;S<Ϙ.Count;S++){if(κ==Ⱦ&&Ϙ[S].BlockDefinition.TypeIdString.Contains("OxygenTank")&&Ϙ[S].BlockDefinition.
SubtypeId.Contains("Hydrogen")){continue;}else if(κ==Ƚ&&Ϙ[S].BlockDefinition.TypeIdString.Contains("OxygenTank")&&!Ϙ[S].
BlockDefinition.SubtypeId.Contains("Hydrogen")){continue;}var Ø=Ϙ[S].GetInventory(0);if(Ø.ư(inventoryFullBuffer)){N=Ϙ[S];ϗ=í(Ϙ[S]);
break;}}if(N==null){Ƽ("All containers for type '"+ϛ+"' are full!\nYou should build or tag new cargo containers!");ʋ.Add(κ);
return;}IMyTerminalBlock σ=null;if(fillBottles&&(κ==Ⱦ||κ==Ƚ)){σ=ϖ(κ);}for(int S=0;S<ʬ.Count;S++){if(ʬ[S]==N||(ʬ[S].CustomName.
Contains(ϛ)&&í(ʬ[S])<=ϗ)||(κ=="Ore"&&ʬ[S].GetType().ToString().Contains("MyGasGenerator"))){continue;}if(ʬ[S].CustomName.
Contains(ϛ)&&balanceTypeContainers&&!ʬ[S].BlockDefinition.TypeIdString.Contains("OxygenGenerator")&&!ʬ[S].BlockDefinition.
TypeIdString.Contains("OxygenTank"))continue;if(σ!=null){if(ʬ[S]!=σ){R(κ,ʬ[S],0,σ,0);continue;}}R(κ,ʬ[S],0,N,0);}for(int S=0;S<ʹ.
Count;S++){if(ʹ[S]==N||(ʹ[S].CustomName.Contains(ϛ)&&í(ʹ[S])<=ϗ)){continue;}R(κ,ʹ[S],1,N,0);}for(int S=0;S<ʶ.Count;S++){if((ʶ
[S].Mode==MyAssemblerMode.Disassembly&&ʶ[S].IsProducing)||ʶ[S]==N||(ʶ[S].CustomName.Contains(ϛ)&&í(ʶ[S])<=ϗ)){continue;}
if(σ!=null){R(κ,ʶ[S],1,σ,0);continue;}R(κ,ʶ[S],1,N,0);}}IMyTerminalBlock ϖ(string κ){IMyTerminalBlock σ;if(ʇ!=null&&κ==Ⱦ){
σ=ʇ;ʇ=null;return σ;}if(ʆ!=null&&κ==Ƚ){σ=ʆ;ʆ=null;return σ;}List<IMyGasTank>υ=new List<IMyGasTank>(ʰ);if(κ==Ⱦ)υ.RemoveAll
(ρ=>ρ.BlockDefinition.SubtypeId.Contains("Hydrogen"));if(κ==Ƚ)υ.RemoveAll(ρ=>!ρ.BlockDefinition.SubtypeId.Contains(
"Hydrogen"));foreach(var π in υ){if(π.FilledRatio>0){var ο=π.GetInventory();if((float)(ο.MaxVolume-ο.CurrentVolume)<0.120)continue
;π.AutoRefillBottles=true;λ(π,κ);return π;}}List<IMyGasGenerator>ξ=ʱ.Where(ν=>ν.IsSameConstructAs(Me)&&ν.Enabled==true).
ToList();MyDefinitionId Ġ=MyItemType.MakeOre("Ice");foreach(var μ in ξ){if(f(Ġ,μ)>100){μ.AutoRefill=true;λ(μ,κ);return μ;}}
return null;}void λ(IMyTerminalBlock W,string κ){if(κ==Ⱦ){ʇ=W;}else{ʆ=W;}}bool ι(IMyTerminalBlock d){if(d.GetOwnerFactionTag()
!=Me.GetOwnerFactionTag()){if(showOwnerWarnings)Ƽ("'"+d.CustomName+
"'\nhas a different owner/faction!\nIt won't be managed by the script!");return false;}return true;}void θ(){char η='0';char ς='0';char[]ζ={'A','N','T','X'};char[]τ={'a','d'};if(
sortingPattern.Length==2){η=sortingPattern[0];ς=sortingPattern[1];}ʫ=new List<IMyTerminalBlock>(ʬ);ʫ.AddRange(ʌ);if(
enableInternalSorting){if(η.ToString().IndexOfAny(ζ)<0||ς.ToString().IndexOfAny(τ)<0){Ƽ("You provided the invalid sorting pattern '"+
sortingPattern+"'!\nCan't sort the inventories!");return;}}else{ʫ=ʫ.FindAll(S=>S.CustomName.ToLower().Contains("(sort:"));}for(var ƭ=ʂ
;ƭ<ʫ.Count;ƭ++){if(Ǥ())return;if(ʂ>=ʫ.Count-1){ʂ=0;}else{ʂ++;}var Ø=ʫ[ƭ].GetInventory(0);var H=new List<MyInventoryItem>(
);Ø.GetItems(H);if(H.Count>200)continue;char ω=η;char ψ=ς;string χ=System.Text.RegularExpressions.Regex.Match(ʫ[ƭ].
CustomName,@"(\(sort:)(.{2})",System.Text.RegularExpressions.RegexOptions.IgnoreCase).Groups[2].Value;if(χ.Length==2){η=χ[0];ς=χ[1
];if(η.ToString().IndexOfAny(ζ)<0||ς.ToString().IndexOfAny(τ)<0){Ƽ("You provided an invalid sorting pattern in\n'"+ʫ[ƭ].
CustomName+"'!\nUsing global pattern!");η=ω;ς=ψ;}}var φ=new List<MyInventoryItem>();Ø.GetItems(φ);if(η=='A'){if(ς=='d'){φ.Sort((ɑ,
Ÿ)=>Ÿ.Amount.ToIntSafe().CompareTo(ɑ.Amount.ToIntSafe()));}else{φ.Sort((ɑ,Ÿ)=>ɑ.Amount.ToIntSafe().CompareTo(Ÿ.Amount.
ToIntSafe()));}}else if(η=='N'){if(ς=='d'){φ.Sort((ɑ,Ÿ)=>Ÿ.Type.SubtypeId.ToString().CompareTo(ɑ.Type.SubtypeId.ToString()));}
else{φ.Sort((ɑ,Ÿ)=>ɑ.Type.SubtypeId.ToString().CompareTo(Ÿ.Type.SubtypeId.ToString()));}}else if(η=='T'){if(ς=='d'){φ.Sort((
ɑ,Ÿ)=>Ÿ.Type.ToString().CompareTo(ɑ.Type.ToString()));}else{φ.Sort((ɑ,Ÿ)=>ɑ.Type.ToString().CompareTo(Ÿ.Type.ToString()))
;}}else if(η=='X'){if(ς=='d'){φ.Sort((ɑ,Ÿ)=>(Ÿ.Type.TypeId.ToString()+Ÿ.Amount.ToIntSafe().ToString(@"000000000")).
CompareTo((ɑ.Type.TypeId.ToString()+ɑ.Amount.ToIntSafe().ToString(@"000000000"))));}else{φ.Sort((ɑ,Ÿ)=>(ɑ.Type.TypeId.ToString()+
ɑ.Amount.ToIntSafe().ToString(@"000000000")).CompareTo((Ÿ.Type.TypeId.ToString()+Ÿ.Amount.ToIntSafe().ToString(
@"000000000"))));}}if(φ.SequenceEqual(H,new Ŷ()))continue;foreach(var Ð in φ){string ϯ=Ð.ToString();for(int S=0;S<H.Count;S++){if(H[
S].ToString()==ϯ){Ø.TransferItemTo(Ø,S,H.Count,false);H.Clear();Ø.GetItems(H);break;}}}η=ω;ς=ψ;}}void Ϯ(){for(int ƭ=ʔ;ƭ<ʌ
.Count;ƭ++){if(Ǥ())return;ʔ++;ù(ʌ[ƭ]);int q=0;if(ʌ[ƭ].BlockDefinition.SubtypeId.Contains("Assembler")){IMyAssembler ǂ=ʌ[ƭ
]as IMyAssembler;if(ǂ.Mode==MyAssemblerMode.Disassembly)q=1;}List<string>Ϭ=new List<string>();double ϫ,Ϫ,ˇ,ϩ;
MyDefinitionId F;string ϭ="",A="";foreach(var Ð in Ȧ){if(!MyDefinitionId.TryParse(Ƀ+Ð.Key,out F))continue;Ϫ=f(F,ʌ[ƭ],q);ϭ=Ð.Value.
ToLower();double.TryParse(System.Text.RegularExpressions.Regex.Match(ϭ,@"\d+").Value,out ϫ);ˇ=0;ϩ=0;if(ϭ.Contains("all")){A=
"all";ϫ=int.MaxValue;}else if(ϭ.Contains("m")){A="m";}else if(ϭ.Contains("l")||ϭ.Contains("-")){A="l";}ˇ=ϫ-Ϫ;if(ˇ>=1&&A!="l")
{var Ø=ʌ[ƭ].GetInventory(q);if(!Ø.ư(inventoryFullBuffer))break;IMyTerminalBlock P=null;if(allowSpecialSteal){P=Z(F,true,ʌ
[ƭ]);}else{P=Z(F);}if(P!=null){ϩ=R(F.ToString(),P,0,ʌ[ƭ],q,ˇ,true);}if(ˇ>ϩ&&A!="all"){Ϭ.Add(ˇ-ϩ+" "+F.SubtypeName);}}else
if(ˇ<0&&A!="m"){IMyTerminalBlock N=V(ʌ[ƭ],ʼ);if(N!=null)R(F.ToString(),ʌ[ƭ],q,N,0,Math.Abs(ˇ),true);}}if(Ϭ.Count>0){Ƽ(ʌ[ƭ]
.CustomName+"\nis missing the following items to match its quota:\n"+String.Join(", ",Ϭ));}}ʔ=0;}void ϴ(List<
IMyTerminalBlock>ď){foreach(var W in ď){string ϳ=W.CustomName;string ϔ;var ϵ=System.Text.RegularExpressions.Regex.Match(ϳ,
@"\(\d+\.?\d*\%\)").Value;if(ϵ!=""){ϔ=ϳ.Replace(ϵ,"").TrimEnd(' ');}else{ϔ=ϳ;}var Ø=W.GetInventory(0);string ǳ=((float)Ø.CurrentVolume).ƙ(
(float)Ø.MaxVolume);if(showFillLevel){ϔ+=" ("+ǳ+")";ϔ=ϔ.Replace("  "," ");}if(ϔ!=ϳ)W.CustomName=ϔ;}}StringBuilder ϲ(){if(
ʮ.Count>1){string ϱ=@"("+autocraftingKeyword+@" *)(\d*)";ʮ.Sort((ɑ,Ÿ)=>System.Text.RegularExpressions.Regex.Match(ɑ.
CustomName,ϱ).Groups[2].Value.CompareTo(System.Text.RegularExpressions.Regex.Match(Ÿ.CustomName,ϱ).Groups[2].Value));}
StringBuilder Ő=new StringBuilder();if(!ʮ[0].GetText().Contains(ɏ)){ʮ[0].Font=defaultFont;ʮ[0].FontSize=defaultFontSize;ʮ[0].
TextPadding=defaultPadding;}foreach(var È in ʮ){Ő.Append(È.GetText()+"\n");È.WritePublicTitle(
"Craft item manually once to show up here");È.Font=ʮ[0].Font;È.FontSize=ʮ[0].FontSize;È.TextPadding=ʮ[0].TextPadding;È.Alignment=TextAlignment.LEFT;È.ContentType=
ContentType.TEXT_AND_IMAGE;}var ϰ=new List<string>(Ő.ToString().Split('\n'));var Ά=new List<string>();var Ϩ=new HashSet<string>();
string Ϝ;foreach(var Ê in ϰ){if(Ê.IndexOfAny(Ɍ)<=0)continue;Ϝ=Ê.Remove(Ê.IndexOf(" "));if(!Ϩ.Contains(Ϝ)){Ά.Add(Ê);Ϩ.Add(Ϝ);}}
List<string>Ü=ʮ[0].CustomData.Split('\n').ToList();foreach(var C in ʭ){bool ϡ=false;if(Ϩ.Contains(C)){continue;}foreach(var
Ê in Ü){if(!Ê.StartsWith("-"))continue;string Ϡ="";try{if(Ê.Contains("=")){Ϡ=Ê.Substring(1,Ê.IndexOf("=")-1);}else{Ϡ=Ê.
Substring(1);}}catch{continue;}if(Ϡ==C){ϡ=true;break;}}if(!ϡ){MyDefinitionId F=Ƕ(C);bool ͼ;MyDefinitionId á=Ǹ(F,out ͼ);if(!ͼ&&!
showUnlearnedItems)continue;double Ȅ=Math.Ceiling(f(F));Ά.Add(C+" "+Ȅ+" = "+Ȅ+defaultModifier);}}foreach(var Ê in Ü){if(!Ê.StartsWith("-")
)continue;if(Ê.Contains("=")){Ά.Add(Ê);}}StringBuilder Ƨ=new StringBuilder();try{IOrderedEnumerable<string>ϟ;ϟ=Ά.OrderBy(
ɑ=>ɑ);bool Ϟ;string ϝ,C,Ϣ;foreach(var Ê in ϟ){Ϟ=false;if(Ê.StartsWith("-")){C=Ê.Remove(Ê.IndexOf("=")).TrimStart('-');ϝ=
"-";}else{C=Ê.Remove(Ê.IndexOf(" "));ϝ="";}Ϣ=Ê.Replace(ϝ+C,"");foreach(var Ð in ʭ){if(Ð==C){Ϟ=true;break;}}if(Ϟ)Ƨ.Append(ϝ+
C+Ϣ+"\n");}}catch{}return Ƨ;}void ϧ(StringBuilder Ő){if(Ő.Length==0){Ő.Append("Autocrafting error!\n\nNo items for crafting available!\n\nIf you hid all items, check the custom data of the first autocrafting panel and reenable some of them.\n\nOtherwise, store or build new items manually!"
);Ő=ʮ[0].Ū(Ő,2,false);ʮ[0].WriteText(Ő);return;}var ņ=Ő.ToString().TrimEnd('\n').Split('\n');int Ņ=ņ.Length;int ń=0;float
Ϧ=0;foreach(var È in ʮ){float Ɖ=È.Ś();int Ń=È.Ş();int ł=0;List<string>Ƨ=new List<string>();if(È==ʮ[0]||
headerOnEveryScreen){string ϥ=ɏ;if(headerOnEveryScreen&&ʮ.Count>1){ϥ+=" "+(ʮ.IndexOf(È)+1)+"/"+ʮ.Count;try{ϥ+=" ["+ņ[ń][0]+"-#]";}catch{ϥ+=
" [Empty]";}}Ƨ.Add(ϥ);Ƨ.Add(È.Ř('=',È.Ţ(ϥ)).ToString()+"\n");string Ϥ="Component ";string ϣ="Current | Wanted ";Ϧ=È.Ţ("Wanted ");
string ǟ=È.Ř(' ',Ɖ-È.Ţ(Ϥ)-È.Ţ(ϣ)).ToString();Ƨ.Add(Ϥ+ǟ+ϣ+"\n");ł=5;}while((ń<Ņ&&ł<Ń)||(È==ʮ[ʮ.Count-1]&&ń<Ņ)){var Ê=ņ[ń].Split
(' ');Ê[0]+=" ";Ê[1]=Ê[1].Replace('$',' ');string ǟ=È.Ř(' ',Ɖ-È.Ţ(Ê[0])-È.Ţ(Ê[1])-Ϧ).ToString();string ε=Ê[0]+ǟ+Ê[1]+Ê[2]
;Ƨ.Add(ε);ń++;ł++;}if(headerOnEveryScreen&&ʮ.Count>1){Ƨ[0]=Ƨ[0].Replace('#',ņ[ń-1][0]);}È.WriteText(String.Join("\n",Ƨ));
}if(showAutocraftingModifiers){string Ή="\n\n---\n\nModifiers (append after wanted amount):\n"+"'A' - Assemble only\n"+
"'D' - Disassemble only\n"+"'P' - Priority (always craft first)\n"+"'H' - Hide (manage in custom data)\n"+"'I' - Ignore (don't manage and hide)\n"
+"'Y#' - Yield modifier. Set # to the itemamount, one craft yields";ʮ[ʮ.Count-1].WriteText(Ή,true);}}void ː(){ʮ.Clear();
GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(ʮ,È=>È.IsSameConstructAs(Me)&&È.CustomName.Contains(autocraftingKeyword));if(ʮ.Count==0)
return;if(ʾ.Count==0){Ƽ("No usable assemblers found!\nBuild or enable assemblers to enable autocrafting!");return;}if(!
enableAutocrafting&&!enableAutodisassembling)return;Π();List<MyDefinitionId>Έ=new List<MyDefinitionId>();var Ά=ϲ().ToString().TrimEnd('\n'
).Split('\n');StringBuilder Ƨ=new StringBuilder();foreach(var Ê in Ά){string C="";bool ͽ=true;if(Ê.StartsWith("-")){ͽ=
false;try{C=Ê.Substring(1,Ê.IndexOf("=")-1);}catch{continue;}}else{try{C=Ê.Substring(0,Ê.IndexOf(" "));}catch{continue;}}
MyDefinitionId F=Ƕ(C);if(F==null)continue;bool ͼ;MyDefinitionId á=Ǹ(F,out ͼ);if(!ͼ&&!showUnlearnedItems)continue;double ͺ=Math.Ceiling
(f(F));string ͷ=Ê.Substring(Ê.IndexOfAny(Ɍ)+1).ToLower().Replace(" ","");double Ͷ=0;int ʹ=1;double.TryParse(System.Text.
RegularExpressions.Regex.Match(ͷ,@"\d+").Value,out Ͷ);string ͳ=ͺ.ToString();string Ͳ=Ͷ.ToString();string ͻ="";bool ï=false;if(ͷ.Contains(
"h")&&ͽ){if(!ʮ[0].CustomData.StartsWith(ɍ))ʮ[0].CustomData=ɍ;ʮ[0].CustomData+="\n-"+C+"="+ͷ.Replace("h","").Replace(" ","")
.ToUpper();continue;}else if(ͷ.Contains("i")&&ͽ){if(!ʮ[0].CustomData.StartsWith(ɍ))ʮ[0].CustomData=ɍ;ʮ[0].CustomData+=
"\n-"+C;continue;}if(ͷ.Contains("a")){if(ͺ>Ͷ)Ͷ=ͺ;ͻ+="A";}if(ͷ.Contains("d")){if(ͺ<Ͷ)Ͷ=ͺ;ͻ+="D";}if(ͷ.Contains("p")){ï=true;ͻ
+="P";}if(ͷ.Contains("y")){int.TryParse(System.Text.RegularExpressions.Regex.Match(ͷ,@"y\d+").Value.Replace("y",""),out ʹ)
;if(ʹ==0)ʹ=1;ͻ+="Y"+ʹ;}ǀ(F,Ͷ);double Α=Math.Abs((Ͷ-ͺ)/ʹ);if(ʹ!=1)Α=Math.Floor(Α);double Θ=ǁ(á);if(ͺ>=Ͷ+Ͷ*assembleMargin&&
Θ>0&&ƿ(á)>0){Σ(á);ƾ(á,0);Θ=0;Ǎ("Removed '"+F.SubtypeId.ToString()+"' from the assembling queue.");}if(ͺ<=Ͷ-Ͷ*
disassembleMargin&&Θ>0&&ƿ(á)<0){Σ(á);ƾ(á,0);Θ=0;Ǎ("Removed '"+F.SubtypeId.ToString()+"' from the disassembling queue.");}string ĥ="";if(Θ
>0||Α>0){if((enableAutodisassembling||ͷ.Contains("d"))&&ͺ>Ͷ+Ͷ*disassembleMargin){ƾ(á,-1);ĥ="$[D:";}else if(
enableAutocrafting&&ͺ<Ͷ-Ͷ*assembleMargin){ƾ(á,1);ĥ="$[A:";}if(ĥ!=""){if(Θ==0){ĥ+="Wait]";}else{ĥ+=Math.Round(Θ)+"]";}}}else{ƾ(á,0);}if(
showUnlearnedItems&&!ͼ)ĥ="$[NoBP]";if(ï){Έ.Add(á);}string Ζ="$=$ ";if(ͺ>Ͷ)Ζ="$>$ ";if(ͺ<Ͷ)Ζ="$<$ ";if(ͽ)Ƨ.Append(C+" "+ͳ+ĥ+Ζ+Ͳ+ͻ+"\n");if(
ĥ.Contains("[D:Wait]")){Ι(á,Α);}else if(ĥ.Contains("[A:Wait]")){Λ(á,Α,ï);Ǎ("Queued "+Α+" '"+F.SubtypeId.ToString()+
"' in the assemblers.");}}Ρ(Έ);ϧ(Ƨ);}void Ε(){if(ʺ.Count>0)return;MyDefinitionId ˠ=MyItemType.MakeOre("Stone");MyDefinitionId á=MyDefinitionId
.Parse(ȓ+"Position0010_StoneOreToIngotBasic");double Δ=f(ˠ);if(Δ>0){double Γ=Math.Floor(Δ/100/ʲ.Count);if(Γ<1)return;
foreach(var Η in ʲ){if(Η.CanUseBlueprint(á)&&Η.IsQueueEmpty){Η.AddQueueItem(á,Γ);Ǎ("Queued "+Γ+" ingot crafts in "+Η.CustomName
+".");}}}}void Β(){if(ʧ==0)ʧ+=ΐ(ʝ,ɂ,true,true);if(ʧ==1)ʧ+=ΐ(ʑ,Ɂ,true,true);if(ʧ==2)ʧ+=ΐ(ʐ,ɀ,true,true);if(ʧ==3)ʧ+=ΐ(ʏ,Ȫ,
true,true);if(ʧ==4)ʧ+=ΐ(ʎ,ȿ,true,true);if(ʧ==5)ʧ+=ΐ(ʍ,"ContainerObject",true,true);ʧ++;if(ʧ>5)ʧ=0;}int ΐ(List<
IMyTerminalBlock>ɒ,string Ώ="",bool Ύ=false,bool Ό=false){if(Ύ)ɒ.RemoveAll(ſ=>ſ.InventoryCount==2||ſ.BlockDefinition.TypeIdString.
Contains("OxygenGenerator")||ſ.BlockDefinition.TypeIdString.Contains("OxygenTank"));if(Ό)ɒ.RemoveAll(S=>!S.CubeGrid.
IsSameConstructAs(Me.CubeGrid));if(ɒ.Count<2){return 1;}Dictionary<MyItemType,double>Ί=new Dictionary<MyItemType,double>();for(int S=0;S<
ɒ.Count;S++){var H=new List<MyInventoryItem>();ɒ[S].GetInventory(0).GetItems(H);foreach(var Ð in H){if(!Ð.Type.TypeId.
ToString().Contains(Ώ))continue;MyItemType F=Ð.Type;if(Ί.ContainsKey(F)){Ί[F]+=(double)Ð.Amount;}else{Ί[F]=(double)Ð.Amount;}}}
Dictionary<MyItemType,double>ˍ=new Dictionary<MyItemType,double>();foreach(var Ð in Ί){ˍ[Ð.Key]=(int)(Ð.Value/ɒ.Count);}for(int ˌ=
0;ˌ<ɒ.Count;ˌ++){if(Ǥ())return 0;var ˋ=new List<MyInventoryItem>();ɒ[ˌ].GetInventory(0).GetItems(ˋ);Dictionary<MyItemType
,double>ˊ=new Dictionary<MyItemType,double>();foreach(var Ð in ˋ){MyItemType F=Ð.Type;if(ˊ.ContainsKey(F)){ˊ[F]+=(double)
Ð.Amount;}else{ˊ[F]=(double)Ð.Amount;}}double ı=0;foreach(var Ð in Ί){ˊ.TryGetValue(Ð.Key,out ı);double ˉ=ˍ[Ð.Key];if(ı<=
ˉ+1)continue;for(int ˈ=0;ˈ<ɒ.Count;ˈ++){if(ɒ[ˌ]==ɒ[ˈ])continue;double ĭ=f(Ð.Key,ɒ[ˈ]);if(ĭ>=ˉ-1)continue;double ˇ=ˉ-ĭ;if(
ˇ>ı-ˉ)ˇ=ı-ˉ;if(ˇ>0){ı-=R(Ð.Key.ToString(),ɒ[ˌ],0,ɒ[ˈ],0,ˇ,true);if(ı.ƚ(ˉ-1,ˉ+1))break;}}}}return Ǥ()?0:1;}void ˆ(){if(ʺ.
Count==0)return;if(ʟ==0)Ɏ=Φ(ʸ);if(ʟ==1)Ɇ=Φ(ʷ);if(enableScriptRefineryFilling){if(ʟ==2)ˤ(ʸ,Ɏ);if(ʟ==3)ˤ(ʷ,Ɇ);if(ʟ==4)ά(ʸ,Ɏ);if
(ʟ==5)ά(ʷ,Ɇ);if(ʟ==6&&ʸ.Count>0&&ʷ.Count>0){bool ˎ=false;ˎ=Ω(ʸ,ʷ,Ɏ);if(!ˎ)Ω(ʷ,ʸ,Ɇ);}}else{if(ʟ>1)ʟ=6;}ʟ++;if(ʟ>6)ʟ=0;}
void ˁ(){if(ʦ==0)ʦ+=ΐ(ʸ.ToList<IMyTerminalBlock>());if(ʦ==1)ʦ+=ΐ(ʷ.ToList<IMyTerminalBlock>());ʦ++;if(ʦ>1)ʦ=0;}void ˏ(List<
IMyRefinery>ͱ,List<MyItemType>ˢ){foreach(IMyRefinery Þ in ͱ){var Ø=Þ.GetInventory(0);var H=new List<MyInventoryItem>();Ø.GetItems(H
);if(H.Count<2)continue;bool Ͱ=false;int ˮ=0;string ˬ="";foreach(var ˑ in ˢ){for(int S=0;S<H.Count;S++){if(H[S].Type==ˑ){
ˮ=S;ˬ=ˑ.SubtypeId;Ͱ=true;break;}}if(Ͱ)break;}if(ˮ!=0){Ø.TransferItemTo(Ø,ˮ,0,true);Ǎ("Sorted the refining queue.\n'"+ˬ+
"' is now at the front of the queue.");}}}void ˤ(List<IMyRefinery>ˣ,List<MyItemType>ˢ){if(ˣ.Count==0){ʟ++;return;}MyItemType ˡ=new MyItemType();MyItemType ˠ=
MyItemType.MakeOre("Stone");foreach(var ˑ in ˢ){if(f(ˑ)>100){ˡ=ˑ;break;}}if(!ˡ.ToString().Contains(ɂ))return;for(int S=0;S<ˣ.Count
;S++){if(Ǥ())return;var Ø=ˣ[S].GetInventory(0);if(!Ø.ư(inventoryFullBuffer)){var H=new List<MyInventoryItem>();Ø.GetItems
(H);foreach(var Ð in H){if(Ð.Type==ˡ)return;}IMyTerminalBlock N=V(ˣ[S],ʝ);if(N!=null){R("",ˣ[S],0,N,0);}}}if(!Ǥ())ʟ++;}
void ά(List<IMyRefinery>ˣ,List<MyItemType>ˢ){if(ˣ.Count==0){ʟ++;return;}double Ϋ;foreach(var ˑ in ˢ){if(f(ˑ)==0)continue;
IMyTerminalBlock Ϊ=Z(ˑ,true);if(Ϊ==null)continue;for(int S=0;S<ˣ.Count;S++){if(Ǥ())return;var Ø=ˣ[S].GetInventory(0);if(!Ø.ư(
inventoryFullBuffer))continue;Ϋ=R(ˑ.ToString(),Ϊ,0,ˣ[S],0);if(Ϋ==0){Ϊ=Z(ˑ,true);if(Ϊ==null)break;}}}if(!Ǥ())ʟ++;}bool Ω(List<IMyRefinery>Ψ,
List<IMyRefinery>Χ,List<MyItemType>ˢ){for(int S=0;S<Ψ.Count;S++){if((float)Ψ[S].GetInventory(0).CurrentVolume>0.05f)continue
;for(int ģ=0;ģ<Χ.Count;ģ++){if((float)Χ[ģ].GetInventory(0).CurrentVolume>0){foreach(var ˑ in ˢ){R(ˑ.ToString(),Χ[ģ],0,Ψ[S
],0,-0.5);}return true;}}}return false;}List<MyItemType>Φ(List<IMyRefinery>ˣ){if(ˣ.Count==0){ʟ++;return null;}List<string
>έ=new List<string>(ɇ);έ.Sort((ɑ,Ÿ)=>(f(MyItemType.MakeIngot(ɑ))/ǅ(ɑ)).CompareTo((f(MyItemType.MakeIngot(Ÿ))/ǅ(Ÿ))));έ.
InsertRange(0,fixedRefiningList);List<MyItemType>δ=new List<MyItemType>();MyItemType F;foreach(var Ð in έ){F=MyItemType.MakeOre(Ð);
foreach(var Þ in ˣ){if(Þ.GetInventory(0).CanItemsBeAdded(1,F)){δ.Add(F);break;}}}if(!Ǥ())ʟ++;return δ;}void γ(){foreach(var ǂ
in ʾ){var Ø=ǂ.GetInventory(0);if((float)Ø.CurrentVolume==0)continue;if(ǂ.IsQueueEmpty||ǂ.Mode==MyAssemblerMode.Disassembly
||!Ø.ư(inventoryFullBuffer)){IMyTerminalBlock N=V(ǂ,ʑ);if(N!=null)R("",ǂ,0,N,0);}}}void β(){foreach(IMyAssembler ǂ in ʾ){
if(ǂ.Mode==MyAssemblerMode.Disassembly)continue;if(ǂ.CustomData.Contains("skipQueueSorting")){ǂ.CustomData="";continue;}
var ĥ=new List<MyProductionItem>();ǂ.GetQueue(ĥ);if(ĥ.Count<2)continue;int ˮ=0;string ˬ="";double α=Double.MaxValue;double
ΰ=Double.MinValue;double ί,ή;for(int S=0;S<ĥ.Count;S++){MyDefinitionId F=Ƿ(ĥ[S].BlueprintId);ή=f(F);ί=(double)ĥ[S].Amount
;if(ή<100&&ή<α){α=ή;ˮ=S;ˬ=F.SubtypeId.ToString();continue;}if(α==Double.MaxValue&&ί>ΰ){ΰ=ί;ˮ=S;ˬ=F.SubtypeId.ToString();}
}if(ˮ!=0){ǂ.MoveQueueItemRequest(ĥ[ˮ].ItemId,0);Ǎ("Sorted the assembling queue.\n'"+ˬ+
"' is now at the front of the queue.");}}}void Ρ(List<MyDefinitionId>Μ){if(Μ.Count==0)return;if(Μ.Count>1)Μ.Sort((ɑ,Ÿ)=>f(Ƿ(ɑ)).CompareTo(f(Ƿ(Ÿ))));foreach(
var ǂ in ʾ){var ĥ=new List<MyProductionItem>();ǂ.GetQueue(ĥ);if(ĥ.Count<2)continue;foreach(var á in Μ){int ƭ=ĥ.FindIndex(S
=>S.BlueprintId==á);if(ƭ==-1)continue;if(ƭ==0){ǂ.CustomData="skipQueueSorting";break;}ǂ.MoveQueueItemRequest(ĥ[ƭ].ItemId,0
);ǂ.CustomData="skipQueueSorting";Ǎ("Sorted the assembler queue by priority.\n'"+Ƿ(á).SubtypeId.ToString()+
"' is now at the front of the queue.");break;}}}void Λ(MyDefinitionId á,double L,bool ï){List<IMyAssembler>Κ=new List<IMyAssembler>();foreach(IMyAssembler ǂ
in ʾ){if(ǂ.CustomName.Contains(disassembleKeyword))continue;if(ï==false&&ǂ.Mode==MyAssemblerMode.Disassembly&&!ǂ.
IsQueueEmpty)continue;if(ǂ.Mode==MyAssemblerMode.Disassembly){ǂ.ClearQueue();ǂ.Mode=MyAssemblerMode.Assembly;}if(ǂ.CanUseBlueprint(á
)){Κ.Add(ǂ);}}if(Κ.Count==0)Ƽ("There's no assembler available to produce '"+á.SubtypeName+
"'. Make sure, that you have at least one assembler with no tags or the !assemble-only tag!");Υ(Κ,á,L);}void Ι(MyDefinitionId á,double L){List<IMyAssembler>Κ=new List<IMyAssembler>();foreach(IMyAssembler ǂ in ʾ){
if(ǂ.CustomName.Contains(assembleKeyword))continue;if(ǂ.Mode==MyAssemblerMode.Assembly&&ǂ.IsProducing)continue;if(ǂ.Mode==
MyAssemblerMode.Assembly){ǂ.ClearQueue();ǂ.Mode=MyAssemblerMode.Disassembly;}if(ǂ.Mode==MyAssemblerMode.Assembly)continue;if(ǂ.
CanUseBlueprint(á)){Κ.Add(ǂ);}}if(Κ.Count==0)Ƽ("There's no assembler available to dismantle '"+á.SubtypeName+
"'. Make sure, that you have at least one assembler with no tags or the !disassemble-only tag!");Υ(Κ,á,L);}void Υ(List<IMyAssembler>Κ,MyDefinitionId á,double L){if(Κ.Count==0)return;double Τ=Math.Ceiling(L/Κ.Count);
foreach(IMyAssembler ǂ in Κ){if(Τ>L)Τ=Math.Ceiling(L);if(L>0){ǂ.InsertQueueItem(0,á,Τ);L-=Τ;}else{break;}}}void Σ(
MyDefinitionId á){foreach(IMyAssembler ǂ in ʾ){var ĥ=new List<MyProductionItem>();ǂ.GetQueue(ĥ);for(int S=0;S<ĥ.Count;S++){if(ĥ[S].
BlueprintId==á)ǂ.RemoveQueueItem(S,ĥ[S].Amount);}}}void Π(){foreach(IMyAssembler ǂ in ʾ){ǂ.UseConveyorSystem=true;ǂ.CooperativeMode
=false;ǂ.Repeating=false;}}void Ο(){List<IMyAssembler>Ξ=new List<IMyAssembler>(ʾ);Ξ.RemoveAll(ɑ=>ɑ.IsQueueEmpty);if(Ξ.
Count==0)return;List<IMyAssembler>Ν=new List<IMyAssembler>(ʾ);Ν.RemoveAll(ɑ=>!ɑ.IsQueueEmpty);if(Ν.Count==0)return;double Ȅ,ǡ
=0,ħ;IMyAssembler Ħ=null;List<MyProductionItem>ĥ=new List<MyProductionItem>();foreach(var Ĥ in Ξ){ĥ.Clear();Ĥ.GetQueue(ĥ)
;Ȅ=0;foreach(var Ð in ĥ){Ȅ+=(double)Ð.Amount;}if(Ȅ>ǡ){ǡ=Ȅ;Ħ=Ĥ;}}if(Ħ==null)return;ĥ.Clear();Ħ.GetQueue(ĥ);for(int S=0;S<ĥ
.Count;S++){Ȅ=(double)ĥ[S].Amount;if(Ȅ<10)continue;ħ=Math.Floor(Ȅ/(Ν.Count+1));if(ħ<1)ħ=1;for(int ģ=0;ģ<Ν.Count;ģ++){if(!
Ν[ģ].CanUseBlueprint(ĥ[0].BlueprintId))continue;if(Ħ.Mode==MyAssemblerMode.Assembly&&Ν[ģ].CustomName.Contains(
disassembleKeyword))continue;if(Ħ.Mode==MyAssemblerMode.Disassembly&&Ν[ģ].CustomName.Contains(assembleKeyword))continue;Ν[ģ].Mode=Ħ.Mode;
if(Ν[ģ].Mode!=Ħ.Mode)continue;Ν[ģ].AddQueueItem(ĥ[S].BlueprintId,ħ);Ħ.RemoveQueueItem(S,ħ);Ȅ-=ħ;if(Ȅ<=1)break;}}}void Ĩ(){
if(ʱ.Count==0)return;double Ģ=spaceForBottles*0.12;MyDefinitionId Ġ=MyItemType.MakeOre("Ice");string ğ=Ġ.ToString();double
Ğ=0.00037;foreach(IMyGasGenerator ĝ in ʱ){var Ø=ĝ.GetInventory(0);double Ĝ=f(Ġ,ĝ);double ě=Ĝ*Ğ;double ġ=(double)Ø.
MaxVolume-Ģ-Ğ;if(ě>ġ+Ğ){IMyTerminalBlock N=V(ĝ,ʝ);if(N!=null){double Ă=(ě-ġ)/Ğ;R(ğ,ĝ,0,N,0,Ă);}}else if(ě<ġ-Ğ){IMyTerminalBlock P
=Z(Ġ,true);if(P!=null){double Ă=(ġ-ě)/Ğ;R(ğ,P,0,ĝ,0,Ă);}}}double ĵ=0;double Ĵ=0;foreach(var ĝ in ʱ){ĵ+=f(Ġ,ĝ);var Ø=ĝ.
GetInventory(0);Ĵ+=(double)Ø.MaxVolume;}double ĳ=(ĵ*Ğ)/Ĵ;foreach(var Ĳ in ʱ){var J=Ĳ.GetInventory(0);double ı=f(Ġ,Ĳ);double İ=ı*Ğ;
double į=(double)J.MaxVolume;if(İ>į*(ĳ+0.001)){foreach(var Į in ʱ){if(Ĳ==Į)continue;var I=Į.GetInventory(0);double ĭ=f(Ġ,Į);
double Ĭ=ĭ*Ğ;double ī=(double)I.MaxVolume;if(Ĭ<ī*(ĳ-0.001)){double Ī=((ī*ĳ)-Ĭ)/Ğ;if((ı-Ī)*Ğ>=į*ĳ&&Ī>5){ı-=R(ğ,Ĳ,0,Į,0,Ī);
continue;}if((ı-Ī)*Ğ<į*ĳ&&Ī>5){double Ě=(ı*Ğ-į*ĳ)/Ğ;R(ğ,Ĳ,0,Į,0,Ě);break;}}}}}}void Ċ(){if(ʯ.Count==0)return;MyDefinitionId đ=
MyItemType.MakeIngot("Uranium");string Ĉ=đ.ToString();double ć=0;double Ć=0;foreach(IMyReactor ą in ʯ){ą.UseConveyorSystem=false;
double Ą=f(đ,ą);double ă=uraniumAmountLargeGrid;if(ą.CubeGrid.GridSize==0.5f)ă=uraniumAmountSmallGrid;Ć+=ă;if(Ą>ă+0.05){
IMyTerminalBlock N=V(ą,ʑ);if(N!=null){double Ă=Ą-ă;R(Ĉ,ą,0,N,0,Ă);}}else if(Ą<ă-0.05){IMyTerminalBlock P=Z(đ,true);if(P!=null){double Ă=
ă-Ą;R(Ĉ,P,0,ą,0,Ă);}}ć+=f(đ,ą);}double ā=ć/Ć;foreach(var Ā in ʯ){double ÿ=f(đ,Ā);double þ=ā*uraniumAmountLargeGrid;if(Ā.
CubeGrid.GridSize==0.5f)þ=ā*uraniumAmountSmallGrid;if(ÿ>þ+0.05){foreach(var ý in ʯ){if(Ā==ý)continue;double ĉ=f(đ,ý);double ü=ā*
uraniumAmountLargeGrid;if(ý.CubeGrid.GridSize==0.5f)ü=ā*uraniumAmountSmallGrid;if(ĉ<ü-0.05){ÿ=f(đ,Ā);double ċ=ü-ĉ;if(ÿ-ċ>=þ){R(Ĉ,Ā,0,ý,0,ċ);
continue;}if(ÿ-ċ<þ){ċ=ÿ-þ;R(Ĉ,Ā,0,ý,0,ċ);break;}}}}}}StringBuilder ę(IMyTextSurface È,bool Ę=true,bool ė=true,bool Ė=true,bool ĕ
=true,bool Ĕ=true){bool ē=false;StringBuilder T=new StringBuilder();if(Ę){T.Append("Isy's Inventory Manager\n");T.Append(
È.Ř('=',È.Ţ(T))).Append("\n\n");}if(ė&&ŕ!=null){T.Append("Warning!\n"+ŕ+"\n\n");ē=true;}if(Ė){T.Append(Đ(È,ʝ,"Ores"));T.
Append(Đ(È,ʑ,"Ingots"));T.Append(Đ(È,ʐ,"Components"));T.Append(Đ(È,ʏ,"Tools"));T.Append(Đ(È,ʎ,"Ammo"));T.Append(Đ(È,ʍ,
"Bottles"));T.Append("=> "+ʿ.Count+" type containers: Balancing "+(balanceTypeContainers?"ON":"OFF")+"\n\n");ē=true;}if(ĕ){T.
Append("Managed blocks:\n");float Ē=È.Ţ(ʩ.Count.ToString());T.Append(ʩ.Count+" Inventories (total) / "+ʬ.Count+
" inventories contain items\n");if(ʌ.Count>0){T.Append(È.Ř(' ',Ē-È.Ţ(ʌ.Count.ToString())).ToString()+ʌ.Count+" Special Containers\n");}if(ʺ.Count>0){T
.Append(È.Ř(' ',Ē-È.Ţ(ʺ.Count.ToString())).ToString()+ʺ.Count+" Refineries: ");T.Append("Ore Balancing "+(
enableOreBalancing?"ON":"OFF")+"\n");}if(ʱ.Count>0){T.Append(È.Ř(' ',Ē-È.Ţ(ʱ.Count.ToString())).ToString()+ʱ.Count+" O2/H2 Generators: ");
T.Append("Ice Balancing "+(enableIceBalancing?"ON":"OFF")+"\n");}if(ʯ.Count>0){T.Append(È.Ř(' ',Ē-È.Ţ(ʯ.Count.ToString())
).ToString()+ʯ.Count+" Reactors: ");T.Append("Uranium Balancing "+(enableUraniumBalancing?"ON":"OFF")+"\n");}if(ʾ.Count>0
){T.Append(È.Ř(' ',Ē-È.Ţ(ʾ.Count.ToString())).ToString()+ʾ.Count+" Assemblers: ");T.Append("Craft "+(enableAutocrafting?
"ON":"OFF")+" | ");T.Append("Uncraft "+(enableAutodisassembling?"ON":"OFF")+" | ");T.Append("Cleanup "+(
enableAssemblerCleanup?"ON":"OFF")+"\n");}if(ʲ.Count>0){T.Append(È.Ř(' ',Ē-È.Ţ(ʲ.Count.ToString())).ToString()+ʲ.Count+" Survival Kits: ");T.
Append("Ingot Crafting "+(enableBasicIngotCrafting?"ON":"OFF")+(ʺ.Count>0?" (Auto OFF - refineries exist)":"")+"\n");}T.Append
("\n");ē=true;}if(Ĕ&&ʛ.Count!=0){T.Append("Last Action:\n"+ʛ[0]);ē=true;}if(!ē){T.Append("-- No informations to show --")
;}return T;}StringBuilder Đ(IMyTextSurface È,List<IMyTerminalBlock>ď,string D){double Ď=0,č=0;foreach(var W in ď){var Ø=W
.GetInventory(0);Ď+=(double)Ø.CurrentVolume;č+=(double)Ø.MaxVolume;}string Ï=ď.Count+"x "+D+":";string ō=Ď.ƞ();string œ=č
.ƞ();StringBuilder Œ=ȃ(È,Ï,Ď,č,ō,œ);return Œ;}void ő(string Ő=null){if(ʘ.Count==0){Ȳ++;return;}for(int S=ʥ;S<ʘ.Count;S++)
{if(Ǥ())return;ʥ++;var ķ=ʘ[S].ƥ(mainLCDKeyword);foreach(var Ķ in ķ){var Č=Ķ.Key;var Ç=Ķ.Value;bool Ę=Ç.Ʊ("showHeading");
bool ė=Ç.Ʊ("showWarnings");bool Ė=Ç.Ʊ("showContainerStats");bool ĕ=Ç.Ʊ("showManagedBlocks");bool Ĕ=Ç.Ʊ("showLastAction");
bool Ľ=Ç.Ʊ("scrollTextIfNeeded");StringBuilder T=new StringBuilder();if(Ő!=null){T.Append(Ő);}else{T=ę(Č,Ę,ė,Ė,ĕ,Ĕ);}T=Č.Ū(T
,Ę?3:0,Ľ);Č.WriteText(T);}}Ȳ++;ʥ=0;}void Ŏ(){if(ʗ.Count==0){Ȳ++;return;}StringBuilder ŏ=new StringBuilder();if(ȳ.Count==0
){ŏ.Append("- No problems detected -");}else{int Ŕ=1;foreach(var ŕ in ȳ){ŏ.Append(Ŕ+". "+ŕ.Replace("\n"," ")+"\n");Ŕ++;}}
for(int S=ʤ;S<ʗ.Count;S++){if(Ǥ())return;ʤ++;var ķ=ʗ[S].ƥ(warningsLCDKeyword);foreach(var Ķ in ķ){var Č=Ķ.Key;var Ç=Ķ.Value
;bool Ę=Ç.Ʊ("showHeading");bool Ľ=Ç.Ʊ("scrollTextIfNeeded");StringBuilder T=new StringBuilder();if(Ę){T.Append(
"Isy's Inventory Manager Warnings\n");T.Append(Č.Ř('=',Č.Ţ(T))).Append("\n\n");}T.Append(ŏ);T=Č.Ū(T,Ę?3:0,Ľ);Č.WriteText(T);}}Ȳ++;ʤ=0;}void ŗ(){if(ʙ.Count==
0){Ȳ++;return;}StringBuilder Ŗ=new StringBuilder();if(ʛ.Count==0){Ŗ.Append("- Nothing to show yet -");}else{foreach(var ð
in ʛ){Ŗ.Append(ð.Replace(": "," ").Replace("\n"," ")+"\n");}}for(int S=ʣ;S<ʙ.Count;S++){if(Ǥ())return;ʣ++;var ķ=ʙ[S].ƥ(
actionsLCDKeyword);foreach(var Ķ in ķ){var Č=Ķ.Key;var Ç=Ķ.Value;bool Ę=Ç.Ʊ("showHeading");bool Ľ=Ç.Ʊ("scrollTextIfNeeded");StringBuilder
T=new StringBuilder();if(Ę){T.Append("Isy's Inventory Manager Actions\n");T.Append(Č.Ř('=',Č.Ţ(T))).Append("\n\n");}T.
Append(Ŗ);T=Č.Ū(T,Ę?3:0,Ľ);Č.WriteText(T);}}Ȳ++;ʣ=0;}void Ŀ(){if(ʖ.Count==0){Ȳ++;return;}for(int S=ʢ;S<ʖ.Count;S++){if(Ǥ())
return;ʢ++;var ķ=ʖ[S].ƥ(performanceLCDKeyword);foreach(var Ķ in ķ){var Č=Ķ.Key;var Ç=Ķ.Value;bool Ę=Ç.Ʊ("showHeading");bool Ľ=
Ç.Ʊ("scrollTextIfNeeded");StringBuilder T=new StringBuilder();if(Ę){T.Append("Isy's Inventory Manager Performance\n");T.
Append(Č.Ř('=',Č.Ţ(T))).Append("\n\n");}T.Append(ʁ);T=Č.Ū(T,Ę?3:0,Ľ);Č.WriteText(T);}}Ȳ++;ʢ=0;}void ļ(){if(ʕ.Count==0){Ȳ++;
return;}Dictionary<IMyTextSurface,string>Ļ=new Dictionary<IMyTextSurface,string>();Dictionary<IMyTextSurface,string>ĺ=new
Dictionary<IMyTextSurface,string>();List<IMyTextSurface>Ĺ=new List<IMyTextSurface>();List<IMyTextSurface>ĸ=new List<IMyTextSurface
>();foreach(var d in ʕ){var ķ=d.ƥ(inventoryLCDKeyword);foreach(var Ķ in ķ){if(Ķ.Value.Contains(inventoryLCDKeyword+":")){
Ļ[Ķ.Key]=Ķ.Value;Ĺ.Add(Ķ.Key);}else{ĺ[Ķ.Key]=Ķ.Value;ĸ.Add(Ķ.Key);}}}HashSet<string>ľ=new HashSet<string>();foreach(var Č
in Ļ){ľ.Add(System.Text.RegularExpressions.Regex.Match(Č.Value,inventoryLCDKeyword+@":[A-Za-z]+").Value);}ľ.RemoveWhere(ŀ
=>ŀ=="");List<string>Ō=ľ.ToList();for(int S=ʡ;S<Ō.Count;S++){if(Ǥ())return;ʡ++;var Ŋ=Ļ.Where(ŉ=>ŉ.Value.Contains(Ō[S]));
var ň=from pair in Ŋ orderby System.Text.RegularExpressions.Regex.Match(pair.Value,inventoryLCDKeyword+@":\w+").Value
ascending select pair;IMyTextSurface Ň=ň.ElementAt(0).Key;string Ç=ň.ElementAt(0).Value;StringBuilder T=É(Ň,Ç);if(!Ç.ToLower().
Contains("noscroll")){int ŋ=0;foreach(var Ł in ň){ŋ+=Ł.Key.Ş();}T=Ň.Ū(T,0,true,ŋ);}var ņ=T.ToString().Split('\n');int Ņ=ņ.Length
;int ń=0;int Ń,ł;foreach(var Ł in ň){IMyTextSurface Č=Ł.Key;Č.FontSize=Ň.TextureSize.Y/Č.TextureSize.Y*Ň.FontSize;Č.Font=
Ň.Font;Č.TextPadding=Ň.TextPadding;Č.Alignment=Ň.Alignment;Č.ContentType=ContentType.TEXT_AND_IMAGE;Ń=Č.Ş();ł=0;T.Clear()
;while(ń<Ņ&&ł<Ń){T.Append(ņ[ń]+"\n");ń++;ł++;}Č.WriteText(T);}}for(int S=ʠ;S<ĸ.Count;S++){if(Ǥ())return;ʠ++;
IMyTextSurface Č=ĸ[S];string Ç=ĺ[Č];StringBuilder T=É(Č,Ç);if(!Ç.ToLower().Contains("noscroll")){T=Č.Ū(T,0);}Č.WriteText(T);Č.
Alignment=TextAlignment.LEFT;Č.ContentType=ContentType.TEXT_AND_IMAGE;}Ȳ++;ʡ=0;ʠ=0;}StringBuilder É(IMyTextSurface È,string Ç){
StringBuilder T=new StringBuilder();var Æ=Ç.Split('\n').ToList();Æ.RemoveAll(Å=>Å.StartsWith("@")||Å.Length<1);bool Ä=true;try{if(Æ[0
].Length<1)Ä=false;}catch{Ä=false;}if(!Ä){T.Append("This screen supports (partial) item or type names, regex and Echo commands. All settings are done in the custom data.\n"
+"Examples:\n  @0 IIM-inventory\n  Component\n  SteelPlate\n"+@"  Iron\W"+"\n  Echo My cool text\n\n"+
"Optionally, add a max amount for the bars as a 2nd parameter.\n"+"Example:\n  @0 IIM-inventory\n  Ingot 100000\n\n"+"At last, add any of these 6 modifiers (optional):\n"+
"  'noHeading' to hide the heading\n"+"  'singleLine' to force one line per item\n"+"  'noBar' to hide the bars\n"+
"  'noScroll' to prevent the screen from scrolling\n"+"  'hideEmpty' to hide items that have an amount of 0\n"+"  'hideType' to hide the type behind the item name\n\n"+
"Example:\n  @0 IIM-inventory\n  Component 100000 noBar noHeading hideEmpty hideType\n\n"+"Full guide: https://steamcommunity.com/sharedfiles/filedetails/?id=1226261795");}else{foreach(var Ê in Æ){var Ã=Ê.
Split(' ');double Á=-1;bool À=false;bool º=false;bool µ=false;bool ª=false;bool z=false;if(Ã.Length>=2){try{Á=Convert.
ToDouble(Ã[1]);}catch{Á=-1;}}string w=Ê.ToLower();if(w.Contains("noheading"))À=true;if(w.Contains("nobar"))º=true;if(w.Contains(
"hideempty"))µ=true;if(w.Contains("hidetype"))ª=true;if(w.Contains("singleline"))z=true;if(w.StartsWith("echoc")){string Â=Ê.ƍ(
"echoc ","").ƍ("echoc","");T.Append(È.Ř(' ',(È.Ś()-È.Ţ(Â))/2)).Append(Â+"\n");}else if(w.StartsWith("echor")){string Â=Ê.ƍ(
"echor ","").ƍ("echor","");T.Append(È.Ř(' ',È.Ś()-È.Ţ(Â))).Append(Â+"\n");}else if(w.StartsWith("echo")){T.Append(Ê.ƍ("echo ",""
).ƍ("echo","")+"\n");}else{T.Append(Ñ(È,Ã[0],Á,À,º,µ,ª,z));}}}if(T.Length>2){return T.Replace("\n","",0,2);}else{return
new StringBuilder("Nothing to show at the moment...");}}StringBuilder Ñ(IMyTextSurface È,string Ó,double Á,bool À=false,
bool º=false,bool µ=false,bool ª=false,bool z=false){StringBuilder T=new StringBuilder();bool Ò=Á==-1?true:false;foreach(var
Ð in Ȓ){if(System.Text.RegularExpressions.Regex.IsMatch(Ð.ToString().ToLower(),Ó.ToLower())){if(T.Length==0&&!À){string Ï
="Itemfilter: '"+Ó+"'";T.Append("\n"+È.Ř(' ',(È.Ś()-È.Ţ(Ï))/2)).Append(Ï+"\n\n");}double L=f(Ð);if(L==0&&µ)continue;if(Ò)
{Á=Ǻ(Ð,true);if(Á==0)continue;}string Î=Ð.SubtypeName;if(Ð.ToString().EndsWith("Ingot/Stone"))Î="Gravel";if(!ª)Î+=" ("+Ð.
TypeId.ToString().Replace(Ƀ,"").Substring(0,2)+")";T.Append(ȃ(È,Î,L,Á,L.Ɯ(),Á.Ɯ(),º,z));}}if(T.Length==0&&!µ){T.Append(
"Error!\n\n");T.Append("No items containing '"+Ó+
"' found!\n\nCheck the custom data of this LCD and enter a valid type, item name or regex expression!\n");}return T;}void Í(string Ì=""){ʜ=ʜ>=3?0:ʜ+1;Echo("Isy's Inventory Manager "+ʞ[ʜ]+"\n====================\n");if(ŕ!=
null){Echo("Warning!\n"+ŕ+"\n");}StringBuilder T=new StringBuilder();T.Append("Script is running in "+(Ȥ?"station":"ship")+
" mode\n\n");T.Append("Task: "+ț+Ì+"\n");T.Append("Script step: "+Ȯ+" / "+(Ț.Length-1)+"\n\n");T.Append(ǧ);if(ʨ.Count>0){T.Append(
"[No Sorting] grids:\n==============\n");foreach(var u in ʨ){T.Append(u.CustomName+"\n");}T.Append("\n");}if(ʓ.Count>0){T.Append(
"[No IIM] grids:\n===========\n");foreach(var u in ʓ){T.Append(u.CustomName+"\n");}}ʁ=T;Echo(T.ToString());if(ʘ.Count==0){Echo(
"Hint:\nBuild a LCD and add the main LCD\nkeyword '"+mainLCDKeyword+"' to its name to get\nmore informations about your base\nand the current script actions.\n");}}double R
(string Y,IMyTerminalBlock P,int O,IMyTerminalBlock N,int M,double L=-1,bool K=false){var J=P.GetInventory(O);var I=N.
GetInventory(M);if(I.IsFull||L==0){return 0;}var H=new List<MyInventoryItem>();J.GetItems(H);if(H.Count==0)return 0;double G=0;
MyDefinitionId F=new MyDefinitionId();MyDefinitionId E=new MyDefinitionId();string D,C,B="";bool Q=false;double A=L;for(int S=H.Count-
1;S>=0;S--){F=H[S].Type;if(K?F.ToString()==Y:F.ToString().Contains(Y)){if(A<0&&F!=E)G=0;E=F;D=F.TypeId.ToString().Replace
(Ƀ,"");C=F.SubtypeId.ToString();Q=true;if(!J.CanTransferItemTo(I,F)){Ƽ("'"+C+"' couldn't be transferred\nfrom '"+P.
CustomName+"'\nto '"+N.CustomName+"'\nThere's no valid conveyor path!");return 0;}double r=(double)H[S].Amount;double o=0;bool m=
false;if(A==-1){m=J.TransferItemTo(I,S,null,true);}else if(A==-0.5){double k=Math.Ceiling((double)H[S].Amount/2);m=J.
TransferItemTo(I,S,null,true,(VRage.MyFixedPoint)k);}else{if(!D.Contains(Ɂ))L=Math.Ceiling(L);m=J.TransferItemTo(I,S,null,true,(VRage.
MyFixedPoint)L);}if(!m){Ƽ("'"+C+"' couldn't be transferred\nfrom '"+P.CustomName+"'\nto '"+N.CustomName+"'\nThe inventory is full!")
;return 0;}H.Clear();J.GetItems(H);try{if((MyDefinitionId)H[S].Type==F){o=(double)H[S].Amount;}}catch{o=0;}double h=r-o;G
+=h;L-=h;if(h>0){if(B!="")B+=", ";B+=Math.Round(h,2)+" "+C+" "+D;}if(L<=0&&A>=0)break;if(!I.ư(inventoryFullBuffer))break;}
}if(!Q)return 0;if(G>0){Ǎ("Moved: "+B+"\nfrom: '"+P.CustomName+"'\nto: '"+N.CustomName+"'");}else{B=Math.Round(L,2)+" "+Y
.Replace(Ƀ,"");if(A==-1)B="all items";if(A==-0.5)B="half of the items";Ƽ("Couldn't transfer '"+B+"'\nfrom '"+P.CustomName
+"'\nto '"+N.CustomName+"'\nCheck conveyor connection!");}return G;}double f(MyDefinitionId F,IMyTerminalBlock d,int q=0)
{return(double)d.GetInventory(q).GetItemAmount(F);;}IMyTerminalBlock Z(MyDefinitionId F,bool X=false,IMyTerminalBlock N=
null){try{if(ʈ.GetInventory(0).FindItem(F)!=null&&ʈ!=N){return ʈ;}}catch{}foreach(var W in ʬ){if(F.SubtypeId.ToString()==
"Ice"&&W.GetType().ToString().Contains("MyGasGenerator"))continue;if(W.GetInventory(0).FindItem(F)!=null){ʈ=W;return W;}}if(X
){foreach(var W in ʌ){if(N!=null){if(í(W)<=í(N))continue;}if(W.GetInventory(0).FindItem(F)!=null){return W;}}}return null
;}IMyTerminalBlock V(IMyTerminalBlock U,List<IMyTerminalBlock>v){IMyTerminalBlock Ô;Ô=å(U,v);if(Ô!=null)return Ô;Ô=å(U,ʵ)
;if(Ô==null)Ƽ("'"+U.CustomName+"'\nhas no empty containers to move its items!");return Ô;}IMyTerminalBlock å(
IMyTerminalBlock U,List<IMyTerminalBlock>v){var î=U.GetInventory(0);foreach(var W in v){if(W==U)continue;var Ø=W.GetInventory(0);if(Ø.ư(
inventoryFullBuffer)){if(!W.GetInventory(0).IsConnectedTo(î))continue;return W;}}return null;}int í(IMyTerminalBlock d){string ì=System.
Text.RegularExpressions.Regex.Match(d.CustomName,@"\[p(\d+|max|min)\]",System.Text.RegularExpressions.RegexOptions.
IgnoreCase).Groups[1].Value.ToLower();int ï=0;bool ë=true;if(ì=="max"){ï=int.MinValue;}else if(ì=="min"){ï=int.MaxValue;}else{ë=
Int32.TryParse(ì,out ï);}if(!ë){string u=d.IsSameConstructAs(Me)?"":"1";Int32.TryParse(u+d.EntityId.ToString().Substring(0,4)
,out ï);}return ï;}string é(string æ){ú();var è=Storage.Split('\n');foreach(var Ê in è){if(Ê.Contains(æ)){return Ê.
Replace(æ+"=","");}}return"!404notfound";}void ç(string æ,string ê=""){ú();var è=Storage.Split('\n');string õ="";foreach(var Ê
in è){if(Ê.Contains(æ)){õ+=æ+"="+ê+"\n";}else{õ+=Ê+"\n";}}Storage=õ.TrimEnd('\n');}void ú(){var è=Storage.Split('\n');if(è
.Length!=Ʌ.Count){string õ="";foreach(var Ð in Ʌ){õ+=Ð.Key+"="+Ð.Value+"\n";}Storage=õ.TrimEnd('\n');}}void ù(
IMyTerminalBlock W){foreach(var ø in Ȧ.Keys.ToList()){Ȧ[ø]="0";}string û="Special Container modes:";List<string>ö=new List<string>();
List<string>ô=W.CustomData.Replace("\n\n\n","\n\n").Split('\n').ToList();bool Õ=false,ó=false,ò=false;int ñ=-1;for(int S=0;S
<ô.Count;S++){if(ñ==-1){if(ô[S].Contains(û)){ñ=S;continue;}else{ö.Add(ô[S]);continue;}}if(ô[S].StartsWith("- "))continue;
var ð=ô[S].Split('=');if(!ó&&ð.Length!=2){continue;}else if(ó&&ð.Length!=2){ò=true;}if(ò){ö.Add(ô[S]);continue;}ó=true;if(!
Ȧ.ContainsKey(ð[0])){MyDefinitionId F;if(MyDefinitionId.TryParse(Ƀ+ð[0],out F)){Ý(F);Õ=true;}}Ȧ[ð[0]]=ð[1];}if(Õ)ä();for(
int S=ö.Count-1;S>=0;S--){if(ö[S]==string.Empty){ö.RemoveAt(S);}else{break;}}try{while(ö[0]==string.Empty){ö.RemoveAt(0);}}
catch{}if(ñ>0)ö.Add("");ö.Add("@"+û);ö.Add("");ö.Add("- Normal: stores wanted amount, removes excess. Usage: item=100");ö.Add
("- Minimum: stores wanted amount, ignores excess. Usage: item=100M");ö.Add(
"- Limiter: doesn't store items, only removes excess. Usage: item=100L");ö.Add("- All: stores all items it can get until it's full. Usage: item=All");ö.Add("");foreach(var Ð in Ȧ){ö.Add(Ð.Key
+"="+Ð.Value);}W.CustomData=string.Join("\n",ö);}void ß(){ʭ.Clear();ʭ.AddRange(ȏ);ʭ.AddRange(ȑ);ʭ.AddRange(Ȑ);ʭ.AddRange(
Ȏ);ʭ.AddRange(ȍ);ʭ.AddRange(Ȍ);ʭ.AddRange(ȋ);ʭ.AddRange(Ȋ);ʭ.AddRange(ȉ);ʭ.AddRange(Ȉ);Ȧ.Clear();foreach(var Ð in ȏ){Ȧ[ɀ+
"/"+Ð]="0";}foreach(var Ð in ȑ){Ȧ[ɂ+"/"+Ð]="0";}foreach(var Ð in Ȑ){Ȧ[Ɂ+"/"+Ð]="0";}foreach(var Ð in Ȏ){Ȧ[ȿ+"/"+Ð]="0";}
foreach(var Ð in ȍ){Ȧ[Ⱦ+"/"+Ð]="0";}foreach(var Ð in Ȍ){Ȧ[Ƚ+"/"+Ð]="0";}foreach(var Ð in ȋ){Ȧ[Ȫ+"/"+Ð]="0";}foreach(var Ð in Ȋ)
{Ȧ[Ȗ+"/"+Ð]="0";}foreach(var Ð in ȉ){Ȧ[ȟ+"/"+Ð]="0";}foreach(var Ð in Ȉ){Ȧ[Ȕ+"/"+Ð]="0";}}void Ú(){var H=new List<
MyInventoryItem>();var Ù=new HashSet<MyInventoryItem>();foreach(var Ø in ʩ){H.Clear();Ø.GetInventory(0).GetItems(H);Ù.UnionWith(H);}var
Ö=new List<MyDefinitionId>();foreach(var Ð in Ù){Ö.Add(Ð.Type);}var Õ=Ö.Except(Ȓ);foreach(var Ð in Õ){Ǎ(
"Found new item:\n"+Ð.SubtypeId.ToString()+" ("+Ð.TypeId.ToString().Replace(Ƀ,"")+")");à(Ð);Ý(Ð);Ǉ(Ð);if(Ǥ())return;}}bool ä(){Ë();var Ü=Me
.CustomData.Split('\n');bool ã=false;foreach(var Ê in Ü){var â=Ê.Split(';');if(â.Length<2)continue;MyDefinitionId F;if(!
MyDefinitionId.TryParse(â[0],out F))continue;if(ʾ.Count==0){ã=true;}else{MyDefinitionId á;if(MyDefinitionId.TryParse(â[1],out á)){if(ǋ
(á)){ǵ(F,á);}else{ǎ(F);continue;}}}à(F);Ǵ(F);}if(ã)return false;return true;}void à(MyDefinitionId F){string D=F.TypeId.
ToString().Replace(Ƀ,"");string C=F.SubtypeId.ToString();if(D==ɂ){ȑ.Add(C);ƽ(C);if(!C.Contains("Ice")){foreach(var Þ in ʺ){if(Þ.
GetInventory(0).CanItemsBeAdded(1,F)){ɇ.Add(C);break;}}}}else if(D==Ɂ){Ȑ.Add(C);}else if(D==ɀ){ȏ.Add(C);}else if(D==ȿ){Ȏ.Add(C);}
else if(D==Ⱦ){ȍ.Add(C);}else if(D==Ƚ){Ȍ.Add(C);}else if(D==Ȫ){ȋ.Add(C);}else if(D==Ȗ){Ȋ.Add(C);}else if(D==ȟ){ȉ.Add(C);}else
if(D==Ȕ){Ȉ.Add(C);}}void Ý(MyDefinitionId F){Ë();var Ü=Me.CustomData.Split('\n').ToList();foreach(var Ê in Ü){try{if(Ê.
Substring(0,Ê.IndexOf(";"))==F.ToString())return;}catch{}}for(int S=Ü.Count-1;S>=0;S--){if(Ü[S].Contains(";")){Ü.Insert(S+1,F+
";noBP");break;}}Me.CustomData=String.Join("\n",Ü);Ǵ(F);}void Ë(){if(!Me.CustomData.Contains(ȼ)){Me.CustomData=(Ȥ?ȣ:Ȣ)+ȼ;}}void
Û(){if(ɋ!=null){MyDefinitionId á,F;var H=new List<MyInventoryItem>();ɋ.GetInventory(1).GetItems(H);var ĥ=new List<
MyProductionItem>();ɋ.GetQueue(ĥ);try{á=ĥ[0].BlueprintId;}catch{ɋ.CustomName=Ɋ;ɋ=null;return;}try{F=H[0].Type;}catch{ȭ=true;return;}ɋ.
CustomName=Ɋ;if(ɋ.Mode==MyAssemblerMode.Assembly&&á==ɉ){if(!Ɋ.Contains(learnManyKeyword))ɋ.CustomName=Ɋ.Replace(" "+learnKeyword,
"").Replace(learnKeyword+" ","");ɋ.ClearQueue();ɉ=new MyDefinitionId();Ǎ(
"Learned and saved crafting blueprint for item:\n"+F.SubtypeId.ToString()+" ("+F.TypeId.ToString().Replace(Ƀ,"")+")");Ǵ(F);à(F);ǵ(F,á);Ý(F);Ǐ(F,á);Ǌ(ɋ);ɋ=null;return;}
else if(ĥ.Count!=1){Ƽ("Blueprint learning aborted!\nExactly 1 itemstack in the queue and output is needed to learn new recipes! Also, did you queue up at least 100 items?"
);Ǌ(ɋ);ȭ=true;return;}}ɋ=null;ɉ=new MyDefinitionId();foreach(var ǂ in ʴ){var ĥ=new List<MyProductionItem>();ǂ.GetQueue(ĥ)
;if(ĥ.Count==1&&ǂ.Mode==MyAssemblerMode.Assembly){if(!Ǌ(ǂ))return;ɋ=ǂ;ɉ=ĥ[0].BlueprintId;Ɋ=ǂ.CustomName;ǂ.CustomName=
"Learning "+ɉ.SubtypeName+" in: "+ǂ.CustomName;ȭ=true;return;}}}bool Ǌ(IMyAssembler ǂ){if(ǂ.GetInventory(1).ItemCount!=0){
IMyTerminalBlock N=V(ǂ,ʐ);if(N!=null){R("",ǂ,1,N,0);return true;}else{Ƽ(
"Can't learn blueprint!\nNo free containers to clear the output inventory found!");return false;}}return true;}bool ǋ(MyDefinitionId á){try{foreach(var ǂ in ʳ){if(ǂ.CanUseBlueprint(á))return true;}}
catch{return false;}return false;}bool ǈ(){if(Ȓ.Count==0)return true;for(int S=0;S<10;S++){if(Ɉ>=Ȓ.Count){Ɉ=0;return true;}Ǉ(
Ȓ.ElementAt(Ɉ));Ɉ++;}return false;}void Ǉ(MyDefinitionId F){if(ʾ.Count==0)return;ȷ=false;MyDefinitionId á;bool ǆ=ȗ.
TryGetValue(F,out á);if(ǆ)ǆ=ǋ(á);if(!ǆ){var ǉ=new List<string>{"","Component","Magazine","AutoPistol"};foreach(var ǌ in ǉ){if(ǒ(F,ǌ
))return;if(ȷ)break;}for(int S=10;S<=200;S+=10){if(ȷ)break;foreach(var ǌ in ǉ){if(ǒ(F,ǌ,"Position"+S.ToString("0000")+"_"
))return;}}Ǎ("The crafting blueprint of\n"+F+"\ncould not be guessed automatically! You have to teach it manually (see guide topic: 'Autocrafting unknown / modded items')."
);}}bool ǒ(MyDefinitionId F,string ǌ,string Ǒ=""){MyDefinitionId á;string ǐ=ȓ+Ǒ+F.SubtypeId.ToString().Replace(
"AutoPistolItem","").Replace("PistolItem","").Replace("Item","")+ǌ;try{if(!MyDefinitionId.TryParse(ǐ,out á))return false;}catch(
ArgumentException){ȷ=true;return false;}if(ǋ(á)){ǵ(F,á);Ǐ(F,á);Ǎ("Automatically learned and saved crafting blueprint for item:\n"+F.
SubtypeId.ToString()+" ("+F.TypeId.ToString().Replace(Ƀ,"")+")");return true;}return false;}void Ǐ(MyDefinitionId F,
MyDefinitionId á){Ë();var Ü=Me.CustomData.Split('\n');for(var S=0;S<Ü.Length;S++){var â=Ü[S].Split(';');if(â[0]!=F.ToString())continue
;Ü[S]=â[0]+";"+á.ToString();Me.CustomData=String.Join("\n",Ü);return;}}void ǎ(MyDefinitionId F){Ë();var Ü=Me.CustomData.
Split('\n').ToList();Ü.RemoveAll(S=>S.Contains(F.ToString()+";"));Me.CustomData=String.Join("\n",Ü);}void Ǎ(string Ő){ʛ.
Insert(0,ʚ+Ő);if(ʛ.Count>=maxEntries)ʛ.RemoveAt(maxEntries-1);}void Ƽ(string Ő){ȳ.Add(Ő);ȱ.Add(Ő);ŕ=ȳ.ElementAt(0);}void ƻ(){
Me.CustomData="";foreach(var W in ʌ){List<string>Ü=W.CustomData.Replace(" ","").TrimEnd('\n').Split('\n').ToList();Ü.
RemoveAll(Ê=>!Ê.Contains("=")||Ê.Contains("=0"));W.CustomData=string.Join("\n",Ü);}Echo("Stored items deleted!\n");if(ʌ.Count>0)
Echo("Also deleted itemlists of "+ʌ.Count+" Special containers!\n");Echo("Please hit 'Recompile'!\n\nScript stopped!");}void
ƺ(){ȇ.Clear();List<IMyTerminalBlock>ƹ=ʶ.ToList<IMyTerminalBlock>();List<IMyTerminalBlock>Ƹ=ʹ.ToList<IMyTerminalBlock>();Ʒ
(ʩ,0);Ʒ(ƹ,1);Ʒ(Ƹ,1);}void Ʒ(List<IMyTerminalBlock>ƶ,int q){for(int S=0;S<ƶ.Count;S++){var H=new List<MyInventoryItem>();ƶ
[S].GetInventory(q).GetItems(H);for(int ģ=0;ģ<H.Count;ģ++){MyDefinitionId F=H[ģ].Type;if(ȇ.ContainsKey(F)){ȇ[F]+=(double)
H[ģ].Amount;}else{ȇ[F]=(double)H[ģ].Amount;}}}}double f(MyDefinitionId F){double Ɗ;ȇ.TryGetValue(F,out Ɗ);return Ɗ;}void
ƽ(string Ǆ){if(!Ʉ.ContainsKey(Ǆ)){Ʉ[Ǆ]=0.5;}}double ǅ(string Ǆ){double Ɗ;Ǆ=Ǆ.Replace(Ƀ+ɂ+"/","");Ʉ.TryGetValue(Ǆ,out Ɗ);
return Ɗ!=0?Ɗ:0.5;}void ǃ(){Ȇ.Clear();foreach(IMyAssembler ǂ in ʾ){var ĥ=new List<MyProductionItem>();ǂ.GetQueue(ĥ);if(ĥ.Count
>0&&!ǂ.IsProducing){if(ǂ.Mode==MyAssemblerMode.Assembly)Ƽ("'"+ǂ.CustomName+
"' has a queue but is currently not assembling!\nAre there enough ingots for the craft?");if(ǂ.Mode==MyAssemblerMode.Disassembly)Ƽ("'"+ǂ.CustomName+
"' has a queue but is currently not disassembling!\nAre the items to disassemble missing?");}foreach(var Ð in ĥ){MyDefinitionId á=Ð.BlueprintId;if(Ȇ.ContainsKey(á)){Ȇ[á]+=(double)Ð.Amount;}else{Ȇ[á]=(double)Ð.
Amount;}}}}double ǁ(MyDefinitionId á){double Ɗ;Ȇ.TryGetValue(á,out Ɗ);return Ɗ;}void ǀ(MyDefinitionId F,double L){ȕ[F]=L;}
double ƿ(MyDefinitionId á){int Ɗ;if(!ȅ.TryGetValue(á,out Ɗ))Ɗ=0;return Ɗ;}void ƾ(MyDefinitionId F,int ê){ȅ[F]=ê;}double Ǻ(
MyDefinitionId F,bool ǹ=false){double Ɗ;if(!ȕ.TryGetValue(F,out Ɗ)&&ǹ)Ɗ=10000;return Ɗ;}MyDefinitionId Ǹ(MyDefinitionId F,out bool ǆ){
MyDefinitionId á;ǆ=ȗ.TryGetValue(F,out á);return á;}MyDefinitionId Ƿ(MyDefinitionId á){MyDefinitionId F;ȩ.TryGetValue(á,out F);return
F;}void ǵ(MyDefinitionId F,MyDefinitionId á){ȗ[F]=á;ȩ[á]=F;}void Ǵ(MyDefinitionId F){Ȓ.Add(F);ȧ[F.SubtypeId.ToString()]=F
;}MyDefinitionId Ƕ(string C){MyDefinitionId F=new MyDefinitionId();ȧ.TryGetValue(C,out F);return F;}StringBuilder ȃ(
IMyTextSurface È,string Ï,double ê,double ȁ,string Ȁ=null,string ǿ=null,bool º=false,bool Ȃ=false,string Ē=""){string ō=ê.ToString();
string œ=ȁ.ToString();if(Ȁ!=null){ō=Ȁ;}if(ǿ!=null){œ=ǿ;}float ƀ=È.FontSize;float Ǿ=0.61f;float ǽ=1.01f;if(È.Font=="Monospace")
{Ǿ=0.41f;ǽ=0.81f;}float Ɖ=È.Ś();char Ǽ=' ';float ǻ=È.š(Ǽ);StringBuilder ǳ=new StringBuilder(" "+ê.ƙ(ȁ));ǳ=È.Ř(Ǽ,È.Ţ(
"9999.9%")-È.Ţ(ǳ)).Append(ǳ);StringBuilder ǲ=new StringBuilder(ō+" / "+œ);StringBuilder Ǔ=new StringBuilder();StringBuilder Ǡ=new
StringBuilder();StringBuilder ǟ;if(ȁ==0){Ǔ.Append(Ē+Ï+" ");ǟ=È.Ř(Ǽ,Ɖ-È.Ţ(Ǔ)-È.Ţ(ō));Ǔ.Append(ǟ).Append(ō);return Ǔ.Append("\n");}
double ǝ=0;if(ȁ>0)ǝ=ê/ȁ>=1?1:ê/ȁ;if(Ȃ&&!º){if(ƀ<Ǿ||(ƀ<ǽ&&Ɖ>512)){Ǔ.Append(Ǟ(È,Ɖ*0.25f,ǝ,Ē)+" "+Ï+" ");ǟ=È.Ř(Ǽ,Ɖ*0.75-È.Ţ(Ǔ)-È.
Ţ(ō+" /"));Ǔ.Append(ǟ).Append(ǲ);ǟ=È.Ř(Ǽ,Ɖ-È.Ţ(Ǔ)-È.Ţ(ǳ));Ǔ.Append(ǟ);Ǔ.Append(ǳ);}else{Ǔ.Append(Ǟ(È,Ɖ*0.3f,ǝ,Ē)+" "+Ï+
" ");ǟ=È.Ř(Ǽ,Ɖ-È.Ţ(Ǔ)-È.Ţ(ǳ));Ǔ.Append(ǟ);Ǔ.Append(ǳ);}}else{Ǔ.Append(Ē+Ï+" ");if(ƀ<Ǿ||(ƀ<ǽ&&Ɖ>512)){ǟ=È.Ř(Ǽ,Ɖ*0.5-È.Ţ(Ǔ)-È
.Ţ(ō+" /"));Ǔ.Append(ǟ).Append(ǲ);ǟ=È.Ř(Ǽ,Ɖ-È.Ţ(Ǔ)-È.Ţ(ǳ));Ǔ.Append(ǟ).Append(ǳ);if(!º){Ǡ=Ǟ(È,Ɖ,ǝ,Ē).Append("\n");}}else{
ǟ=È.Ř(Ǽ,Ɖ-È.Ţ(Ǔ)-È.Ţ(ǲ));Ǔ.Append(ǟ).Append(ǲ);if(!º){Ǡ=Ǟ(È,Ɖ-È.Ţ(ǳ),ǝ,Ē);Ǡ.Append(ǳ).Append("\n");}}}return Ǔ.Append(
"\n").Append(Ǡ);}StringBuilder Ǟ(IMyTextSurface È,float Ɓ,double ǝ,string Ē){StringBuilder ǜ,ǚ;char Ǚ='[';char ǘ=']';char Ǘ=
'I';char ǖ='∙';float Ǖ=È.š(Ǚ);float ǔ=È.š(ǘ);float Ǜ=0;if(Ē!="")Ǜ=È.Ţ(Ē);float Ǣ=Ɓ-Ǖ-ǔ-Ǜ;ǜ=È.Ř(Ǘ,Ǣ*ǝ);ǚ=È.Ř(ǖ,Ǣ-È.Ţ(ǜ));
return new StringBuilder().Append(Ē).Append(Ǚ).Append(ǜ).Append(ǚ).Append(ǘ);}StringBuilder ǧ=new StringBuilder(
"No performance Information available!");Dictionary<string,int>Ǳ=new Dictionary<string,int>();List<int>ǯ=new List<int>(new int[600]);List<double>Ǯ=new List<
double>(new double[600]);double ǭ,Ǭ,ǫ,Ǫ,ǩ;int ǰ,Ǩ=0;void Ǧ(string ǥ){Ǩ=Ǩ>=599?0:Ǩ+1;ǰ=Runtime.CurrentInstructionCount;if(ǰ>Ǭ)Ǭ
=ǰ;ǯ[Ǩ]=ǰ;Ǫ=ǯ.Sum()/ǯ.Count;ǧ.Clear();ǧ.Append("Instructions: "+ǰ+" / "+Runtime.MaxInstructionCount+"\n");ǧ.Append(
"Max. Instructions: "+Ǭ+" / "+Runtime.MaxInstructionCount+"\n");ǧ.Append("Avg. Instructions: "+Math.Floor(Ǫ)+" / "+Runtime.
MaxInstructionCount+"\n\n");ǭ=Runtime.LastRunTimeMs;if(ǭ>ǫ&&Ǳ.ContainsKey(ǥ))ǫ=ǭ;Ǯ[Ǩ]=ǭ;ǩ=Ǯ.Sum()/Ǯ.Count;ǧ.Append("Last runtime: "+Math.
Round(ǭ,4)+" ms\n");ǧ.Append("Max. runtime: "+Math.Round(ǫ,4)+" ms\n");ǧ.Append("Avg. runtime: "+Math.Round(ǩ,4)+" ms\n\n");ǧ
.Append("Instructions per Method:\n");Ǳ[ǥ]=ǰ;foreach(var Ð in Ǳ.OrderByDescending(S=>S.Value)){ǧ.Append("- "+Ð.Key+": "+Ð
.Value+"\n");}ǧ.Append("\n");}bool Ǥ(double ê=10){return Runtime.CurrentInstructionCount>ê*1000;}List<IMyTerminalBlock>ǣ(
string Ƥ,string[]Ƶ=null,string ž="Debug",float ţ=0.6f,float ż=2f){string Ż="[IsyLCD]";var ź=new List<IMyTerminalBlock>();
GridTerminalSystem.GetBlocksOfType<IMyTextSurfaceProvider>(ź,Ÿ=>Ÿ.IsSameConstructAs(Me)&&(Ÿ.CustomName.Contains(Ƥ)||(Ÿ.CustomName.Contains
(Ż)&&Ÿ.CustomData.Contains(Ƥ))));var Ź=ź.FindAll(Ÿ=>Ÿ.CustomName.Contains(Ƥ));foreach(var È in Ź){È.CustomName=È.
CustomName.Replace(Ƥ,"").Replace(" "+Ƥ,"").TrimEnd(' ');bool Ž=false;bool ŷ=false;int ŵ=0;if(È is IMyTextSurface){if(!È.CustomName
.Contains(Ż))Ž=true;if(!È.CustomData.Contains(Ƥ)){ŷ=true;È.CustomData="@0 "+Ƥ+(Ƶ!=null?"\n"+String.Join("\n",Ƶ):"");}}
else if(È is IMyTextSurfaceProvider){if(!È.CustomName.Contains(Ż))Ž=true;int Ŵ=(È as IMyTextSurfaceProvider).SurfaceCount;
for(int S=0;S<Ŵ;S++){if(!È.CustomData.Contains("@"+S)){ŷ=true;ŵ=S;È.CustomData+=(È.CustomData==""?"":"\n\n")+"@"+S+" "+Ƥ+(Ƶ
!=null?"\n"+String.Join("\n",Ƶ):"");break;}}}else{ź.Remove(È);}if(Ž)È.CustomName+=" "+Ż;if(ŷ){var Č=(È as
IMyTextSurfaceProvider).GetSurface(ŵ);Č.Font=ž;Č.FontSize=ţ;Č.TextPadding=ż;Č.Alignment=TextAlignment.LEFT;Č.ContentType=ContentType.
TEXT_AND_IMAGE;}}return ź;}
}class ų:IComparer<MyDefinitionId>{public int Compare(MyDefinitionId Ų,MyDefinitionId ű){return Ų.ToString().CompareTo(ű.
ToString());}}class Ŷ:IEqualityComparer<MyInventoryItem>{public bool Equals(MyInventoryItem Ų,MyInventoryItem ű){return Ų.
ToString()==ű.ToString();}public int GetHashCode(MyInventoryItem Ð){return Ð.ToString().GetHashCode();}}public static partial
class Ƈ{private static Dictionary<char,float>Ɔ=new Dictionary<char,float>();public static void ƅ(string Ƅ,float ƃ){foreach(
char ſ in Ƅ){Ɔ[ſ]=ƃ;}}public static void Ƃ(){if(Ɔ.Count>0)return;ƅ(
"3FKTabdeghknopqsuy£µÝàáâãäåèéêëðñòóôõöøùúûüýþÿāăąďđēĕėęěĝğġģĥħĶķńņňŉōŏőśŝşšŢŤŦũūŭůűųŶŷŸșȚЎЗКЛбдекруцяёђћўџ",18);ƅ("ABDNOQRSÀÁÂÃÄÅÐÑÒÓÔÕÖØĂĄĎĐŃŅŇŌŎŐŔŖŘŚŜŞŠȘЅЊЖф□",22);ƅ("#0245689CXZ¤¥ÇßĆĈĊČŹŻŽƒЁЌАБВДИЙПРСТУХЬ€",20);ƅ(
"￥$&GHPUVY§ÙÚÛÜÞĀĜĞĠĢĤĦŨŪŬŮŰŲОФЦЪЯжы†‡",21);ƅ("！ !I`ijl ¡¨¯´¸ÌÍÎÏìíîïĨĩĪīĮįİıĵĺļľłˆˇ˘˙˚˛˜˝ІЇії‹›∙",9);ƅ("？7?Jcz¢¿çćĉċčĴźżžЃЈЧавийнопсъьѓѕќ",17);ƅ(
"（）：《》，。、；【】(),.1:;[]ft{}·ţťŧț",10);ƅ("+<=>E^~¬±¶ÈÉÊË×÷ĒĔĖĘĚЄЏЕНЭ−",19);ƅ("L_vx«»ĹĻĽĿŁГгзлхчҐ–•",16);ƅ("\"-rª­ºŀŕŗř",11);ƅ("WÆŒŴ—…‰",32);ƅ("'|¦ˉ‘’‚",7)
;ƅ("@©®мшњ",26);ƅ("mw¼ŵЮщ",28);ƅ("/ĳтэє",15);ƅ("\\°“”„",13);ƅ("*²³¹",12);ƅ("¾æœЉ",29);ƅ("%ĲЫ",25);ƅ("MМШ",27);ƅ("½Щ",30);
ƅ("ю",24);ƅ("ј",8);ƅ("љ",23);ƅ("ґ",14);ƅ("™",31);}public static Vector2 ƈ(this IMyTextSurface Č,StringBuilder Ő){Ƃ();
Vector2 Ɓ=new Vector2();if(Č.Font=="Monospace"){float ƀ=Č.FontSize;Ɓ.X=(float)(Ő.Length*19.4*ƀ);Ɓ.Y=(float)(28.8*ƀ);return Ɓ;}
else{float ƀ=(float)(Č.FontSize*0.779);foreach(char ſ in Ő.ToString()){try{Ɓ.X+=Ɔ[ſ]*ƀ;}catch{}}Ɓ.Y=(float)(28.8*Č.FontSize)
;return Ɓ;}}public static float Ţ(this IMyTextSurface È,StringBuilder Ő){Vector2 ś=È.ƈ(Ő);return ś.X;}public static float
Ţ(this IMyTextSurface È,string Ő){Vector2 ś=È.ƈ(new StringBuilder(Ő));return ś.X;}public static float š(this
IMyTextSurface È,char Š){float ş=Ţ(È,new string(Š,1));return ş;}public static int Ş(this IMyTextSurface È){Vector2 ř=È.SurfaceSize;
float ŝ=È.TextureSize.Y;if(ř.X<512||ŝ!=ř.Y)ř.Y*=512/ŝ;float Ŝ=ř.Y*(100-È.TextPadding*2)/100;Vector2 ś=È.ƈ(new StringBuilder(
"T"));return(int)(Ŝ/ś.Y);}public static float Ś(this IMyTextSurface È){Vector2 ř=È.SurfaceSize;float ŝ=È.TextureSize.Y;if(ř
.X<512||ŝ!=ř.Y)ř.X*=512/ŝ;return ř.X*(100-È.TextPadding*2)/100;}public static StringBuilder Ř(this IMyTextSurface È,char
Ű,double Ů){int ŭ=(int)(Ů/š(È,Ű));if(ŭ<0)ŭ=0;return new StringBuilder().Append(Ű,ŭ);}private static DateTime Ŭ=DateTime.
Now;private static Dictionary<int,List<int>>ū=new Dictionary<int,List<int>>();public static StringBuilder Ū(this
IMyTextSurface È,StringBuilder Ő,int ů=3,bool Ľ=true,int Ń=0){int ũ=È.GetHashCode();if(!ū.ContainsKey(ũ)){ū[ũ]=new List<int>{1,3,ů,0};
}int Ũ=ū[ũ][0];int ŧ=ū[ũ][1];int Ŧ=ū[ũ][2];int ť=ū[ũ][3];var Ť=Ő.ToString().TrimEnd('\n').Split('\n');List<string>ņ=new
List<string>();if(Ń==0)Ń=È.Ş();float Ɖ=È.Ś();StringBuilder Ē,Ã=new StringBuilder();for(int S=0;S<Ť.Length;S++){if(S<ů||S<Ŧ||
ņ.Count-Ŧ>Ń||È.Ţ(Ť[S])<=Ɖ){ņ.Add(Ť[S]);}else{try{Ã.Clear();float Ƭ,ƫ;var ƪ=Ť[S].Split(' ');string Ʃ=System.Text.
RegularExpressions.Regex.Match(Ť[S],@"\d+(\.|\:)\ ").Value;Ē=È.Ř(' ',È.Ţ(Ʃ));foreach(var ƨ in ƪ){Ƭ=È.Ţ(Ã);ƫ=È.Ţ(ƨ);if(Ƭ+ƫ>Ɖ){ņ.Add(Ã.
ToString());Ã=new StringBuilder(Ē+ƨ+" ");}else{Ã.Append(ƨ+" ");}}ņ.Add(Ã.ToString());}catch{ņ.Add(Ť[S]);}}}if(Ľ){if(ņ.Count>Ń){
if(DateTime.Now.Second!=ť){ť=DateTime.Now.Second;if(ŧ>0)ŧ--;if(ŧ<=0)Ŧ+=Ũ;if(Ŧ+Ń-ů>=ņ.Count&&ŧ<=0){Ũ=-1;ŧ=3;}if(Ŧ<=ů&&ŧ<=0)
{Ũ=1;ŧ=3;}}}else{Ŧ=ů;Ũ=1;ŧ=3;}ū[ũ][0]=Ũ;ū[ũ][1]=ŧ;ū[ũ][2]=Ŧ;ū[ũ][3]=ť;}else{Ŧ=ů;}StringBuilder Ƨ=new StringBuilder();for(
var Ê=0;Ê<ů;Ê++){Ƨ.Append(ņ[Ê]+"\n");}try{for(var Ê=Ŧ;Ê<ņ.Count;Ê++){Ƨ.Append(ņ[Ê]+"\n");}}catch{}return Ƨ;}public static
Dictionary<IMyTextSurface,string>ƥ(this IMyTerminalBlock d,string Ƥ,Dictionary<string,string>ƣ=null){var Ƣ=new Dictionary<
IMyTextSurface,string>();if(d is IMyTextSurface){Ƣ[d as IMyTextSurface]=d.CustomData;}else if(d is IMyTextSurfaceProvider){var ơ=
System.Text.RegularExpressions.Regex.Matches(d.CustomData,@"@(\d).*("+Ƥ+@")");int Ơ=(d as IMyTextSurfaceProvider).SurfaceCount
;foreach(System.Text.RegularExpressions.Match Ʀ in ơ){int ƭ=-1;if(int.TryParse(Ʀ.Groups[1].Value,out ƭ)){if(ƭ>=Ơ)continue
;string Ü=d.CustomData;int ƴ=Ü.IndexOf("@"+ƭ);int Ʋ=Ü.IndexOf("@",ƴ+1)-ƴ;string Ç=Ʋ<=0?Ü.Substring(ƴ):Ü.Substring(ƴ,Ʋ);Ƣ[
(d as IMyTextSurfaceProvider).GetSurface(ƭ)]=Ç;}}}return Ƣ;}public static bool Ʊ(this string Ç,string æ){var Ü=Ç.Replace(
" ","").Split('\n');foreach(var Ê in Ü){if(Ê.StartsWith(æ+"=")){try{return Convert.ToBoolean(Ê.Replace(æ+"=",""));}catch{
return true;}}}return true;}public static string Ƴ(this string Ç,string æ){var Ü=Ç.Replace(" ","").Split('\n');foreach(var Ê
in Ü){if(Ê.StartsWith(æ+"=")){return Ê.Replace(æ+"=","");}}return"";}}public static partial class Ƈ{public static bool ư(
this IMyInventory Ø,double Ư=0.1){float Ʈ=(float)Ø.CurrentVolume;float Ɵ=(float)Ø.MaxVolume;if(Ɵ*0.02<Ư){if(Ʈ<Ɵ*0.98){return
true;}}else{if(Ʈ<Ɵ-Ư){return true;}}return false;}}public static partial class Ƈ{public static bool ƚ(this double ê,double ƕ
,double œ,bool Ɣ=false,bool Ɠ=false){bool ƒ=ê>=ƕ;bool Ƒ=ê<=œ;if(Ɠ)ƒ=ê>ƕ;if(Ɣ)Ƒ=ê<œ;return ƒ&&Ƒ;}}public static partial
class Ƈ{public static string Ɛ(this char Ə,int Ǝ){if(Ǝ<=0){return"";}return new string(Ə,Ǝ);}}public static partial class Ƈ{
public static string ƍ(this string ƌ,string Ƌ,string Ɩ){string Ɗ=System.Text.RegularExpressions.Regex.Replace(ƌ,System.Text.
RegularExpressions.Regex.Escape(Ƌ),Ɩ,System.Text.RegularExpressions.RegexOptions.IgnoreCase);return Ɗ;}}public static partial class Ƈ{
public static string ƞ(this float ê){string ƛ="kL";if(ê<1){ê*=1000;ƛ="L";}else if(ê>=1000&&ê<1000000){ê/=1000;ƛ="ML";}else if(
ê>=1000000&&ê<1000000000){ê/=1000000;ƛ="BL";}else if(ê>=1000000000){ê/=1000000000;ƛ="TL";}return Math.Round(ê,1)+" "+ƛ;}
public static string ƞ(this double ê){float Ɲ=(float)ê;return Ɲ.ƞ();}}public static partial class Ƈ{public static string Ɯ(
this double ê){string ƛ="";if(ê>=1000&&ê<1000000){ê/=1000;ƛ=" k";}else if(ê>=1000000&&ê<1000000000){ê/=1000000;ƛ=" M";}else
if(ê>=1000000000){ê/=1000000000;ƛ=" B";}return Math.Round(ê,1)+ƛ;}}public static partial class Ƈ{public static string ƙ(
this double Ƙ,double ĩ){double Ɨ=Math.Round(Ƙ/ĩ*100,1);if(ĩ==0){return"0%";}else{return Ɨ+"%";}}public static string ƙ(this
float Ƙ,float ĩ){double Ɨ=Math.Round(Ƙ/ĩ*100,1);if(ĩ==0){return"0%";}else{return Ɨ+"%";}}