using System.Security.Cryptography;

class WorkCamp : ConstructibleBuilding
{
    public BuildingUpgrade postUpgrade;
    public BuildingUpgrade merchantUpgrade;
    public BuildingUpgrade improveUpgrade;

    public ConstructionResources inStockItems;

    internal int currentGold;
    public WorkCamp(Vec2i location, int buildtime) : base(location, buildtime)
    {
        Name = "Work Camp";
        Desc = "The work camp generates wood and stone every turn. It can be traded from when upgraded.";
        ResourceToBuild = Config.BuildCon.WorkCampResources;
        GoldCost = Config.BuildCon.WorkCampGold;

        postUpgrade = new BuildingUpgrade();
        postUpgrade.Name = "Trade Post";
        postUpgrade.Desc = "Open a trade post, allowing the purchase and sale of basic materials. Wares are restocked every turn.";
        postUpgrade.GoldCost = Config.BuildCon.WorkCampTradeUpgrade.GoldCost;
        postUpgrade.ResourceToUpgrade = Config.BuildCon.WorkCampTradeUpgrade.ResourceToUpgrade;
        postUpgrade.upgradeTime = Config.BuildCon.WorkCampTradeUpgrade.upgradeTime;
        Upgrades.Add(postUpgrade);

        merchantUpgrade = new BuildingUpgrade();
        merchantUpgrade.Name = "Merchant Guild Branch";
        merchantUpgrade.Desc = "Work camp can now be used to purchase and sell Ores, Natural Materials, Prefabs, and Mana Stones.";
        merchantUpgrade.GoldCost = Config.BuildCon.WorkCampMerchantUpgrade.GoldCost;
        merchantUpgrade.ResourceToUpgrade = Config.BuildCon.WorkCampMerchantUpgrade.ResourceToUpgrade;
        merchantUpgrade.upgradeTime = Config.BuildCon.WorkCampMerchantUpgrade.upgradeTime;
        Upgrades.Add(merchantUpgrade);

        improveUpgrade = new BuildingUpgrade();
        improveUpgrade.Name = "Improve Camp";
        improveUpgrade.Desc = "Improve the work camp, triples the max stock and doubles the stone and wood produced every turn.";
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
        ownerResource.AddResource(ConstructionresourceType.wood, 1);
        ownerResource.AddResource(ConstructionresourceType.stone, 1);

        if (postUpgrade.built || merchantUpgrade.built) 
        {
            inStockItems.SetResources(10, 10, 5, 5, 3, 3);
        }

        if (improveUpgrade.built)
        {
            inStockItems.SetResources(Config.BuildCon.WorkCampTurnStock.Wood, Config.BuildCon.WorkCampTurnStock.Stone, Config.BuildCon.WorkCampTurnStock.Ores, Config.BuildCon.WorkCampTurnStock.NaturalMaterials, Config.BuildCon.WorkCampTurnStock.Prefabs, Config.BuildCon.WorkCampTurnStock.ManaStones);
            ownerResource.AddResource(ConstructionresourceType.wood, 1);
            ownerResource.AddResource(ConstructionresourceType.stone, 1);
        }
    }
}

