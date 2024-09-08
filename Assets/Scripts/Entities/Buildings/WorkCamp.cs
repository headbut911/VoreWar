class WorkCamp : ConstructibleBuilding
{

    public WorkCamp(Vec2i location, int upgradestage, int buildtime, int upgradetime) : base(location, 0, 4, 2)
    {
    }

    internal override void RunBuildingFunction()
    {
        ConstructionResources ownerResource = Owner.constructionResources;
        ownerResource.AddResource(ConstructionresourceType.wood, 1);
        ownerResource.AddResource(ConstructionresourceType.stone, 1);
    }
}

