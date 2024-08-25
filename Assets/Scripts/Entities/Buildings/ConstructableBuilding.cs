using OdinSerializer;


abstract class ConstructableBuilding
{
    [OdinSerialize]
    internal Empire Owner;

    [OdinSerialize]
    internal Vec2i Position;

    [OdinSerialize]
    internal int UpgradeStage;

    protected ConstructableBuilding(Vec2i location)
    {
        Position = location;
    }

    internal abstract void RunBuildingFunction();

}

