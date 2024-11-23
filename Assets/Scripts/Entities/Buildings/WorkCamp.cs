class WorkCamp : ConstructibleBuilding
{

    public WorkCamp(Vec2i location, int buildtime, int upgradetime) : base(location, buildtime, upgradetime)
    {
        Name = "Work Camp";
        ResourceToBuild.SetResources(0,0,0,0,0,0);
        GoldCost = 10;
    }

    internal override void RunBuildingFunction()
    {
        ConstructionResources ownerResource = Owner.constructionResources;
        ownerResource.AddResource(ConstructionresourceType.wood, 1);
        ownerResource.AddResource(ConstructionresourceType.stone, 1);
    }
}

