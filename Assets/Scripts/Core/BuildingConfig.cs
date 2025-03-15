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
    [OdinSerialize]
    internal int BuildingPassiveRange = 3;

    //Specific Building Settings
    //Work Camp
    [OdinSerialize]
    internal GeneralBuildingConfig WorkCamp = new GeneralBuildingConfig(15, 2, -1);
    [OdinSerialize]
    internal int WorkCampGoldPerTurn = 100;
    [OdinSerialize]
    internal int WorkCampGenerationPerTurn = 1;
    [OdinSerialize]
    internal ConstructionResources WorkCampTurnStock = new ConstructionResources(10, 10, 5, 5, 3, 3);
    [OdinSerialize]
    internal ConstructionResources WorkCampItemPrice = new ConstructionResources(10, 10, 25, 25, 50, 50); //Used as a price counter, not inventory
    [OdinSerialize]
    internal BuildingUpgrade WorkCampTradeUpgrade = new BuildingUpgrade(75, 2, new ConstructionResources(10, 10, 0, 0, 0, 0), "Trade Post", "Open a trade post, allowing the purchase and sale of basic materials. Wares are restocked every turn.");
    [OdinSerialize]
    internal BuildingUpgrade WorkCampMerchantUpgrade = new BuildingUpgrade(300, 3, new ConstructionResources(15, 15, 10, 10, 0, 0), "Merchant Guild Branch", "Work camp can now be used to purchase and sell Ores, Natural Materials, Prefabs, and Mana Stones.");
    [OdinSerialize]
    internal BuildingUpgrade WorkCampImproveUpgrade = new BuildingUpgrade(150, 2, new ConstructionResources(30, 25, 10, 5, 10, 0), "Improve Camp", "Improve the work camp, triples the max stock and doubles the stone and wood produced every turn.");

    //Lumber Site
    [OdinSerialize]
    internal GeneralBuildingConfig LumberSite = new GeneralBuildingConfig(50, 2, -1, 5, 5);
    [OdinSerialize]
    internal int LumberSiteWorkerCap = 2;
    [OdinSerialize]
    internal BuildingUpgrade LumberSiteLodgeUpgrade = new BuildingUpgrade(250, 3, new ConstructionResources(20, 10, 0, 0, 0, 0), "Improve Lodge", "Construct better living spaces, doubles the worker cap.");
    [OdinSerialize]
    internal BuildingUpgrade LumberSiteCarpenterUpgrade = new BuildingUpgrade(300, 3, new ConstructionResources(20, 10, 5, 25, 0, 0), "Carpentry", "Construct a workshop, allowing 2 workers to be assigned to produce a Prefab.");
    [OdinSerialize]
    internal BuildingUpgrade LumberSiteGreenHouseUpgrade = new BuildingUpgrade(150, 2, new ConstructionResources(30, 15, 0, 0, 0, 0), "Greenhouse", "Construct a greenhouse, enabling workers to be assinged to cultivating natural materials.");
    [OdinSerialize]

    //Lumber Site
    internal GeneralBuildingConfig Quarry = new GeneralBuildingConfig(50, 2, -1, 5, 5);
    [OdinSerialize]
    internal int QuarryStoneMin = 1;
    [OdinSerialize]
    internal int QuarryStoneMax = 3;
    [OdinSerialize]
    internal int QuarryOreMin = 1;
    [OdinSerialize]
    internal int QuarryOreMax = 3;
    [OdinSerialize]
    internal int QuarryMSMin = 1;
    [OdinSerialize]
    internal int QuarryMSMax = 3;
    [OdinSerialize]
    internal int QuarryGoldMin = 0;
    [OdinSerialize]
    internal int QuarryGoldMax = 20;
    [OdinSerialize]
    internal BuildingUpgrade QuarryImproveUpgrade = new BuildingUpgrade(150, 2, new ConstructionResources(10, 20, 0, 0, 0, 0), "Improve Infrastructure", "Improve all aspects of the quarry, unlocking new action plans, boosting old ones, and improving min and max generations by 1.");
    [OdinSerialize]
    internal BuildingUpgrade QuarryDeepUpgrade = new BuildingUpgrade(300, 3, new ConstructionResources(20, 10, 15, 0, 0, 0), "Deep Mining", "Preform additional tunneling, allowing ores to be collected.");
    [OdinSerialize]
    internal BuildingUpgrade QuarryLeyLineUpgrade = new BuildingUpgrade(150, 2, new ConstructionResources(30, 30, 15, 15, 0, 0), "Leyline Tap", "Cosntruct proper protective measures, allowing mana stones to be collected.");

    //Caster Tower
    [OdinSerialize]
    internal GeneralBuildingConfig CasterTower = new GeneralBuildingConfig(250, 2, -1, 0, 0);
    [OdinSerialize]
    internal int CasterTowerManaChargesMax = 20;
    [OdinSerialize]
    internal int CasterTowerManaChargesRegen = 4;
    [OdinSerialize]
    internal int CasterTowerBaseChargeCost = 1;
    [OdinSerialize]
    internal int CasterTowerBetterTierChargeCost = 3;
    [OdinSerialize]
    internal int CasterTowerBuffChargeCost = 2;
    [OdinSerialize]
    internal BuildingUpgrade CasterTowerImproveUpgrade = new BuildingUpgrade(250, 2, new ConstructionResources(25, 15, 0, 5, 0, 10), "Improve Tower", "Improve capacity and throughput by installing mana stones. Max mana charges and mana charge regeneration is doubled.");
    [OdinSerialize] 
    internal BuildingUpgrade CasterTowerForceUpgrade = new BuildingUpgrade(300, 3, new ConstructionResources(10, 10, 0, 15, 0, 5), "Forceful Focus", "Install better casting foci, adds higher tier spells to the spell pool.");
    [OdinSerialize] 
    internal BuildingUpgrade CasterTowerBuffUpgrade = new BuildingUpgrade(300, 2, new ConstructionResources(50, 20, 15, 5, 5, 0), "Spell Library", "Construct a tome library, adds buffing spells to the spell pool and allows count of spells to be set.");

    //Barrier Tower
    [OdinSerialize]
    internal GeneralBuildingConfig BarrierTower = new GeneralBuildingConfig(250, 2, -1, 0, 0);
    [OdinSerialize]
    internal int BarrierTowerBaseBarrierStrength = 10;
    [OdinSerialize]
    internal bool BarrierTowerIgnoreDowntime = false;
    [OdinSerialize]
    internal BuildingUpgrade BarrierTowerImproveUpgrade = new BuildingUpgrade(250, 2, new ConstructionResources(25, 25, 0, 0, 0, 10), "Improve Tower", "Boosts tower funciton by installing mana stones. Adds two parallel casts, both with individual downtimes.");
    [OdinSerialize]
    internal BuildingUpgrade BarrierTowerHealUpgrade = new BuildingUpgrade(300, 3, new ConstructionResources(30, 30, 15, 25, 0, 0), "Soothing Barrier", "Barriers are infused with resotrative properties, tower applies a long lasting mending status to all ally units.");
    [OdinSerialize]
    internal BuildingUpgrade BarrierTowerBuffUpgrade = new BuildingUpgrade(300, 2, new ConstructionResources(50, 20, 15, 5, 5, 0), "Invigorating Barrier", "Barriers are infused with empowering properties, tower applies an empowered buff to all ally units.");

    //Defense Encampment
    [OdinSerialize]
    internal GeneralBuildingConfig DefenseEncampment = new GeneralBuildingConfig(250, 2, -1, 0, 0);
    [OdinSerialize]
    internal float DefenseEncampmentArmyPercentage = 0.2f;
    [OdinSerialize]
    internal float DefenseEncampmentUnitScale = 0.5f;
    [OdinSerialize]
    internal float DefenseEncampmentMaxGarrisonSizeScale = 0.5f;
    [OdinSerialize]
    internal int DefenseEncampmentTrainTime = 4;
    internal BuildingUpgrade DefenseEncampmentImproveUpgrade = new BuildingUpgrade(250, 2, new ConstructionResources(40, 10, 15, 35, 0, 0), "Improve Equipment", "Invest in better gear. Units will now come with one random equipment. They also have a higher chance to use a heavy weapon.");
    [OdinSerialize]
    internal BuildingUpgrade DefenseEncampmentUnitsUpgrade = new BuildingUpgrade(300, 3, new ConstructionResources(10, 50, 0, 15, 25, 0), "Bastion", "The amount of units that reinforce the battle is increased by 50%. Increase maximum reinforcemets by 50%");
    [OdinSerialize]
    internal BuildingUpgrade DefenseEncampmentLevelUpgrade = new BuildingUpgrade(350, 2, new ConstructionResources(10, 10, 0, 15, 15, 0), "Tactical Training", "Construct training grounds. Increases how well units scale with leader's level by 50%. Halve unit training time.");

    //Academy Tower
    [OdinSerialize]
    internal GeneralBuildingConfig Academy = new GeneralBuildingConfig(250, 2, -1, 0, 0);
    [OdinSerialize]
    internal int AcademyEXPPerGold = 5;
    [OdinSerialize]
    internal int AcademyMaximumUpgrades = 4;
    [OdinSerialize]
    internal int AcademyUpgradeCost = 1000;
    [OdinSerialize]
    internal float AcademyCostIncreaseMultPerUpgrade = 1.5f;
    internal BuildingUpgrade AcademyImproveUpgrade = new BuildingUpgrade(250, 2, new ConstructionResources(40, 10, 15, 35, 0, 0), "", "Allows for additional funds to be allocated.");
    [OdinSerialize]
    internal BuildingUpgrade AcademyResearchUpgrade = new BuildingUpgrade(300, 3, new ConstructionResources(10, 50, 0, 15, 25, 0), "", "Unlock research that can apply buffs to the whole empire.");
    [OdinSerialize]
    internal BuildingUpgrade AcademyEfficiencyUpgrade = new BuildingUpgrade(350, 2, new ConstructionResources(10, 10, 0, 15, 15, 0), "", "Allow a higher ammount of Stored EXP to be distributed per turn");

    //Dark Magic Tower
    [OdinSerialize]
    internal GeneralBuildingConfig DarkMagicTower = new GeneralBuildingConfig(250, 2, -1, 0, 0);
    [OdinSerialize]
    internal int DarkMagicTowerDurationImprovement = 5;
    [OdinSerialize]
    internal int DarkMagicTowerAccImprovement = 10;
    [OdinSerialize]
    internal int DarkMagicTowerSoulPointBase = 100;
    [OdinSerialize]
    internal float DarkMagicTowerSoulPointMult = 1.5f;
    [OdinSerialize]
    internal BuildingUpgrade DarkMagicTowerImproveUpgrade = new BuildingUpgrade(250, 2, new ConstructionResources(40, 10, 15, 35, 0, 0), "", "Increased Maximum Pact level to 10");
    [OdinSerialize]
    internal BuildingUpgrade DarkMagicTowerSoulUpgrade = new BuildingUpgrade(300, 3, new ConstructionResources(10, 50, 0, 15, 25, 0), "", "Soul points gained are doubled.");
    [OdinSerialize]
    internal BuildingUpgrade DarkMagicTowerAfflictionUpgrade = new BuildingUpgrade(350, 2, new ConstructionResources(10, 10, 0, 15, 15, 0), "", "Pact level increases effect of afflictions");

    //Temporal Tower
    [OdinSerialize]
    internal GeneralBuildingConfig TemporalTower = new GeneralBuildingConfig(250, 2, -1, 0, 0);
    [OdinSerialize]
    internal BuildingUpgrade TemporalTowerImproveUpgrade = new BuildingUpgrade(250, 2, new ConstructionResources(40, 10, 15, 35, 0, 0), "", "Tower increases ally empire MP by 1");
    [OdinSerialize]
    internal BuildingUpgrade TemporalTowerTuneUpgrade = new BuildingUpgrade(300, 3, new ConstructionResources(10, 50, 0, 15, 25, 0), "", "Enemy Empire armies now also have their MP reduced by 1.");
    [OdinSerialize]
    internal BuildingUpgrade TemporalTowerDisruptUpgrade = new BuildingUpgrade(350, 2, new ConstructionResources(10, 10, 0, 15, 15, 0), "", "Monster armies now have their MP reduced to 1 while within range");

    //Temporal Tower
    [OdinSerialize]
    internal GeneralBuildingConfig Laboratory = new GeneralBuildingConfig(250, 2, -1, 0, 0);
    [OdinSerialize]
    internal int LaboratoryUpfrontCost = 100;
    [OdinSerialize]
    internal int LaboratoryBaseUnitPrice= 100;
    [OdinSerialize]
    internal float LaboratoryBulkDiscount = 0.5f;
    [OdinSerialize]
    internal int LaboratoryBulkMin = 5;
    [OdinSerialize]
    internal int LaboratoryBulkMax = 50;
    [OdinSerialize]
    internal int LaboratoryBaseRollCount = 4;
    [OdinSerialize]
    internal float LaboratoryBaseTraitChance = 0.6f;
    [OdinSerialize]
    internal BuildingUpgrade LaboratoryImproveUpgrade = new BuildingUpgrade(250, 2, new ConstructionResources(40, 10, 15, 35, 0, 0), "", "Increases max rolls per potion.");
    [OdinSerialize]
    internal BuildingUpgrade LaboratoryIngredientUpgrade = new BuildingUpgrade(300, 3, new ConstructionResources(10, 50, 0, 15, 25, 0), "", "Expands types of availible ingredients.");
    [OdinSerialize]
    internal BuildingUpgrade LaboratoryBoostUpgrade = new BuildingUpgrade(350, 2, new ConstructionResources(10, 10, 0, 15, 15, 0), "", "Chance of boosting effect of low quality ingredients.");

}

public class GeneralBuildingConfig
{
    [OdinSerialize]
    public int Gold = 0;
    [OdinSerialize]
    public int BuildTime = 0;
    [OdinSerialize]
    public int BuildLimit = -1;
    [OdinSerialize]
    public bool AICanBuild = true;
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

