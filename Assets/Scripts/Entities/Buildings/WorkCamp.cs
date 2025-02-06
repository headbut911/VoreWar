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
        ResourceToBuild = Config.BuildCon.WorkCamp.Resources;
        GoldCost = Config.BuildCon.WorkCamp.Gold;
        baseBuildTurns = Config.BuildCon.WorkCamp.BuildTime;

        postUpgrade = new BuildingUpgrade();
        postUpgrade.Name = Config.BuildCon.WorkCampTradeUpgrade.Name;
        postUpgrade.Desc = Config.BuildCon.WorkCampTradeUpgrade.Desc;
        postUpgrade.GoldCost = Config.BuildCon.WorkCampTradeUpgrade.GoldCost;
        postUpgrade.ResourceToUpgrade = Config.BuildCon.WorkCampTradeUpgrade.ResourceToUpgrade;
        postUpgrade.upgradeTime = Config.BuildCon.WorkCampTradeUpgrade.upgradeTime;
        Upgrades.Add(postUpgrade);

        merchantUpgrade = new BuildingUpgrade();
        merchantUpgrade.Name = Config.BuildCon.WorkCampMerchantUpgrade.Name;
        merchantUpgrade.Desc = Config.BuildCon.WorkCampMerchantUpgrade.Desc;
        merchantUpgrade.GoldCost = Config.BuildCon.WorkCampMerchantUpgrade.GoldCost;
        merchantUpgrade.ResourceToUpgrade = Config.BuildCon.WorkCampMerchantUpgrade.ResourceToUpgrade;
        merchantUpgrade.upgradeTime = Config.BuildCon.WorkCampMerchantUpgrade.upgradeTime;
        Upgrades.Add(merchantUpgrade);

        improveUpgrade = new BuildingUpgrade();
        improveUpgrade.Name = Config.BuildCon.WorkCampImproveUpgrade.Name;
        improveUpgrade.Desc = Config.BuildCon.WorkCampImproveUpgrade.Desc;
        improveUpgrade.GoldCost = Config.BuildCon.WorkCampImproveUpgrade.GoldCost;
        improveUpgrade.ResourceToUpgrade = Config.BuildCon.WorkCampImproveUpgrade.ResourceToUpgrade;
        improveUpgrade.upgradeTime = Config.BuildCon.WorkCampImproveUpgrade.upgradeTime;
        Upgrades.Add(improveUpgrade);

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

