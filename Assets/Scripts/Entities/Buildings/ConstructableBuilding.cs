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
    internal int turnsToCompletion;
    [OdinSerialize]
    internal int turnsToUpgrade;
    [OdinSerialize]
    internal int baseBuildTurns;
    [OdinSerialize]
    internal int baseUpgraderTurns;
    public bool constructing => turnsToUpgrade > 0;
    public bool upgrading => turnsToCompletion > 0;

    [OdinSerialize]
    internal string Name;
    protected ConstructibleBuilding(Vec2i location, int upgradestage, int buildtime, int upgradetime)
    {
        Position = location;
        UpgradeStage = upgradestage;
        baseBuildTurns = buildtime;
        baseUpgraderTurns = upgradetime;
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

