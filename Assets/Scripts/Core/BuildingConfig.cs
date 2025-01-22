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
    [OdinSerialize]
    internal int WorkCampGold = 15;
    [OdinSerialize]
    internal int WorkCampBuildTime = 2;
    [OdinSerialize]
    internal int WorkCampBuildLimit = -1;
    [OdinSerialize]
    internal int WorkCampGoldPerTurn = 100;
    internal ConstructionResources WorkCampResources = new ConstructionResources();
    internal ConstructionResources WorkCampTurnStock = new ConstructionResources(10, 10, 5, 5, 3, 3);
    internal ConstructionResources WorkCampItemPrice = new ConstructionResources(10, 10, 25, 25, 50, 50); //Used as a price counter, not inventory
    internal BuildingUpgrade WorkCampTradeUpgrade = new BuildingUpgrade(50, 2, new ConstructionResources(10, 10, 5, 5, 0, 0), "Trade Post", "Open a trade post, allowing the purchase and sale of basic materials. Wares are restocked every turn.");
    internal BuildingUpgrade WorkCampMerchantUpgrade = new BuildingUpgrade(300, 3, new ConstructionResources(10, 10, 5, 5, 0, 0), "Merchant Guild Branch", "Work camp can now be used to purchase and sell Ores, Natural Materials, Prefabs, and Mana Stones.");
    internal BuildingUpgrade WorkCampImproveUpgrade = new BuildingUpgrade(150, 2, new ConstructionResources(30, 25, 10, 5, 10, 0), "Improve Camp", "Improve the work camp, triples the max stock and doubles the stone and wood produced every turn.");
}

