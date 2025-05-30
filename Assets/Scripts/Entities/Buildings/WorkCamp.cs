﻿using OdinSerializer;

public class WorkCamp : ConstructibleBuilding
{
    public BuildingUpgrade postUpgrade;
    public BuildingUpgrade merchantUpgrade;
    public BuildingUpgrade improveUpgrade;

    public ConstructionResources inStockItems;

    [OdinSerialize]
    internal int currentGold;
    public WorkCamp(Vec2i location) : base(location)
    {
        Name = "Work Camp";
        Desc = "The work camp generates wood and stone every turn. It can be traded from when upgraded.";
        spriteID = 0;
        buildingType = ConstructibleType.WorkCamp;

        ApplyConfigStats(Config.BuildConfig.WorkCamp);
        postUpgrade = AddUpgrade(postUpgrade, Config.BuildConfig.WorkCampTradeUpgrade);
        merchantUpgrade = AddUpgrade(postUpgrade, Config.BuildConfig.WorkCampMerchantUpgrade);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildConfig.WorkCampImproveUpgrade);

        inStockItems = new ConstructionResources();
        inStockItems.SetResources(0, 0, 0, 0, 0, 0);
    }

    internal override void RunBuildingFunction()
    {
        ConstructionResources ownerResource = Owner.constructionResources;
        ownerResource.AddResource(ConstructionResourceType.wood, Config.BuildConfig.WorkCampGenerationPerTurn);
        ownerResource.AddResource(ConstructionResourceType.stone, Config.BuildConfig.WorkCampGenerationPerTurn);

        if (postUpgrade.built || merchantUpgrade.built) 
        {
            currentGold += Config.BuildConfig.WorkCampGoldPerTurn;
            inStockItems.SetResources(Config.BuildConfig.WorkCampTurnStock);
        }

        if (improveUpgrade.built)
        {
            ownerResource.AddResource(ConstructionResourceType.wood, Config.BuildConfig.WorkCampGenerationPerTurn);
            ownerResource.AddResource(ConstructionResourceType.stone, Config.BuildConfig.WorkCampGenerationPerTurn);
        }
    }
}

