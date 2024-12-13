using System.Security.Cryptography;

class WorkCamp : ConstructibleBuilding
{
    public BuildingUpgrade postUpgrade;
    public BuildingUpgrade merchantUpgrade;
    public BuildingUpgrade improveUpgrade;

    public ConstructionResources inStockItems;
    public WorkCamp(Vec2i location, int buildtime) : base(location, buildtime)
    {
        Name = "Work Camp";
        ResourceToBuild.SetResources(0,0,0,0,0,0);
        GoldCost = 15;

        postUpgrade = new BuildingUpgrade();
        postUpgrade.Name = "Trade Post";
        postUpgrade.Desc = "Open a trade post, allowing the purchase and sale of basic materials. Wares are restocked every turn.";
        postUpgrade.GoldCost = 50;
        postUpgrade.ResourceToUpgrade.SetResources(10, 10, 5, 5, 0, 0);
        postUpgrade.upgradeTime = 2;
        Upgrades.Add(postUpgrade);

        merchantUpgrade = new BuildingUpgrade();
        merchantUpgrade.Name = "Merchant Guild Branch";
        merchantUpgrade.Desc = "Work camp can now be used to purchase and sell Ores, Natural Materials, Prefabs, and Mana Stones.";
        merchantUpgrade.GoldCost = 300;
        merchantUpgrade.ResourceToUpgrade.SetResources(10, 10, 5, 5, 0, 0);
        merchantUpgrade.upgradeTime = 3;
        Upgrades.Add(merchantUpgrade);

        improveUpgrade = new BuildingUpgrade();
        improveUpgrade.Name = "Improve Camp";
        improveUpgrade.Desc = "Improve the work camp, increasing max stock and materials produced every turn.";
        improveUpgrade.GoldCost = 150;
        improveUpgrade.ResourceToUpgrade.SetResources(30, 25, 10, 5, 10, 0);
        improveUpgrade.upgradeTime = 2;
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
            inStockItems.SetResources(20, 20, 10, 10, 5, 5);
        }

        if (improveUpgrade.built)
        {
            inStockItems.SetResources(30, 30, 15, 15, 7, 7);
            ownerResource.AddResource(ConstructionresourceType.wood, 1);
            ownerResource.AddResource(ConstructionresourceType.stone, 1);
        }
    }
}

