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
    internal string Name;
    protected ConstructibleBuilding(Vec2i location)
    {
        Position = location;
    }

    internal abstract void RunBuildingFunction();

}

