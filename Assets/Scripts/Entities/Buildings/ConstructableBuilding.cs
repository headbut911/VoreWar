using OdinSerializer;
using System.Linq;


abstract class ConstructibleBuilding
{
    [OdinSerialize]
    internal Empire Owner;

    [OdinSerialize]
    internal Vec2i Position;

    [OdinSerialize]
    internal int UpgradeStage; 

    [OdinSerialize]
    internal int GoldCost; 
    [OdinSerialize]
    internal ConstructionResources ResourceToBuild;

    [OdinSerialize]
    internal int turnsToCompletion;
    [OdinSerialize]
    internal int turnsToUpgrade;
    [OdinSerialize]
    internal int baseBuildTurns;
    [OdinSerialize]
    internal int baseUpgradeTurns;
    public bool constructing => turnsToCompletion > 0;
    public bool upgrading => turnsToUpgrade > 0;

    [OdinSerialize]
    internal bool enabled = true;
    [OdinSerialize]
    internal bool AIenabled;


    [OdinSerialize]
    internal string Name;
    protected ConstructibleBuilding(Vec2i location, int buildtime, int upgradetime)
    {
        Position = location;
        UpgradeStage = 0;
        GoldCost = 0;
        ResourceToBuild = new ConstructionResources();
        baseBuildTurns = buildtime;
        baseUpgradeTurns = upgradetime;
    }

    internal abstract void RunBuildingFunction();
    internal void ConstructBuilding() 
    {
        turnsToCompletion = baseBuildTurns;
        var contstruct = State.World.Constructibles.ToList();
        contstruct.Add(this);
        State.World.Constructibles = contstruct.ToArray();
        State.GameManager.StrategyMode.RedrawVillages();

    }

    internal void UpgradeBuilding(int upgradeTurns)
    {
        turnsToUpgrade = upgradeTurns;
    }

    public void RunBuildingTurn()
    {
        if (constructing)
        {
            turnsToCompletion--;
            return;
        }
        if (upgrading)
        {
            turnsToUpgrade--;
            return;
        }
        RunBuildingFunction();
    }

}

