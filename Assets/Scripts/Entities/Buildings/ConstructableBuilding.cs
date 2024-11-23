using OdinSerializer;


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
    public bool constructing => turnsToUpgrade > 0;
    public bool upgrading => turnsToCompletion > 0;

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
    internal void ConstructBuilding(int buildTurns) 
    {
        turnsToCompletion = buildTurns;
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

