class WorkCamp : ConstructibleBuilding
{
    public BuildingUpgrade postUpgrade;
    public BuildingUpgrade merchantUpgrade;
    public BuildingUpgrade improveUpgrade;

    public ConstructionResources inStockItems;

    internal int currentGold;
    public WorkCamp(Vec2i location) : base(location)
    {
        Name = "Work Camp";
        Desc = "The work camp generates wood and stone every turn. It can be traded from when upgraded.";
        spriteID = 0;
        buildingType = ConstructibleType.WorkCamp;

        ApplyConfigStats(Config.BuildCon.WorkCamp);
        postUpgrade = AddUpgrade(postUpgrade, Config.BuildCon.WorkCampTradeUpgrade);
        merchantUpgrade = AddUpgrade(postUpgrade, Config.BuildCon.WorkCampMerchantUpgrade);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildCon.WorkCampImproveUpgrade);

        inStockItems = new ConstructionResources();
        inStockItems.SetResources(0, 0, 0, 0, 0, 0);
    }

    internal override void RunBuildingFunction()
    {
        ConstructionResources ownerResource = Owner.constructionResources;
        ownerResource.AddResource(ConstructionresourceType.wood, Config.BuildCon.WorkCampGenerationPerTurn);
        ownerResource.AddResource(ConstructionresourceType.stone, Config.BuildCon.WorkCampGenerationPerTurn);

        if (postUpgrade.built || merchantUpgrade.built) 
        {
            inStockItems.SetResources(10, 10, 5, 5, 3, 3);
        }

        if (improveUpgrade.built)
        {
            inStockItems.SetResources(Config.BuildCon.WorkCampTurnStock.Wood, Config.BuildCon.WorkCampTurnStock.Stone,Config.BuildCon.WorkCampTurnStock.NaturalMaterials, Config.BuildCon.WorkCampTurnStock.Ores, Config.BuildCon.WorkCampTurnStock.Prefabs, Config.BuildCon.WorkCampTurnStock.ManaStones);
            ownerResource.AddResource(ConstructionresourceType.wood, Config.BuildCon.WorkCampGenerationPerTurn);
            ownerResource.AddResource(ConstructionresourceType.stone, Config.BuildCon.WorkCampGenerationPerTurn);
        }
    }
}

