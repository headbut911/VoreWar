using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;

public class BuildingConfig
{
    //Building System Setting
    [OdinSerialize]
    public bool BuildingSystemEnabled = false;
    [OdinSerialize]
    public int BuildingSystemTurnLockout = 0;


    //Specific Building Settings
    //Work Camp
    internal GeneralBuildingConfig WorkCamp = new GeneralBuildingConfig(15, 2, -1);
    [OdinSerialize]
    internal int WorkCampGoldPerTurn = 100;
    [OdinSerialize]
    internal int WorkCampGenerationPerTurn = 1;
    internal ConstructionResources WorkCampTurnStock = new ConstructionResources(10, 10, 5, 5, 3, 3);
    internal ConstructionResources WorkCampItemPrice = new ConstructionResources(10, 10, 25, 25, 50, 50); //Used as a price counter, not inventory
    internal BuildingUpgrade WorkCampTradeUpgrade = new BuildingUpgrade(75, 2, new ConstructionResources(10, 10, 0, 0, 0, 0), "Trade Post", "Open a trade post, allowing the purchase and sale of basic materials. Wares are restocked every turn.");
    internal BuildingUpgrade WorkCampMerchantUpgrade = new BuildingUpgrade(300, 3, new ConstructionResources(15, 15, 10, 10, 0, 0), "Merchant Guild Branch", "Work camp can now be used to purchase and sell Ores, Natural Materials, Prefabs, and Mana Stones.");
    internal BuildingUpgrade WorkCampImproveUpgrade = new BuildingUpgrade(150, 2, new ConstructionResources(30, 25, 10, 5, 10, 0), "Improve Camp", "Improve the work camp, triples the max stock and doubles the stone and wood produced every turn.");

    //Lumber Site
    internal GeneralBuildingConfig LumberSite = new GeneralBuildingConfig(50, 2, -1, 5, 5);
    [OdinSerialize]
    internal int LumberSiteWorkerCap = 2;
    internal BuildingUpgrade LumberSiteLodgeUpgrade = new BuildingUpgrade(250, 3, new ConstructionResources(10, 10, 0, 0, 0, 0), "Lumber Lodge", "Construct better living spaces, doubles the worker cap.");
    internal BuildingUpgrade LumberSiteCarpenterUpgrade = new BuildingUpgrade(300, 3, new ConstructionResources(20, 10, 5, 25, 0, 0), "Carpentry", "Construct a workshop, allowing 2 workers to be assigned to produce a Prefab.");
    internal BuildingUpgrade LumberSiteGreenHouseUpgrade = new BuildingUpgrade(150, 2, new ConstructionResources(30, 15, 0, 0, 0, 0), "Greenhouse", "Construct a greenhouse, enabling workers to be assinged to cultivating natural materials.");

    public class GeneralBuildingConfig
    {
        [OdinSerialize]
        public int Gold = 0;
        [OdinSerialize]
        public int BuildTime = 0;
        [OdinSerialize]
        public int BuildLimit = -1;
        public ConstructionResources Resources = new ConstructionResources();

        public GeneralBuildingConfig()
        {

        }
        public GeneralBuildingConfig(int gold, int time, int limit, int wood = 0, int stones = 0, int nm = 0, int ores = 0, int prefabs = 0, int ms = 0)
        {
            Gold = gold;
            BuildTime = time;
            BuildLimit = limit;
            Resources.SetResources(wood, stones, nm, ores, prefabs, ores);
        }

    }
}

