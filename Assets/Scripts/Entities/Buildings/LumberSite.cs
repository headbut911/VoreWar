using OdinSerializer;

class LumberSite : ConstructibleBuilding
{
    public BuildingUpgrade lodgeUpgrade;
    public BuildingUpgrade carpenterUpgrade;
    public BuildingUpgrade greenHouseUpgrade;

    [OdinSerialize]
    internal int IdleWorkers;
    [OdinSerialize]
    internal int woodWorkers;
    [OdinSerialize]
    internal int natureWorkers;
    [OdinSerialize] 
    internal int carpenterWorkers;
    public LumberSite(Vec2i location) : base(location)
    {
        Name = "Lumber Site";
        Desc = "The lumber site generates wood each turn using workers. It can be upgraded to produce natural materials and prefabs each turn.";
        spriteID = 8;
        ResourceToBuild = Config.BuildCon.LumberSite.Resources;
        GoldCost = Config.BuildCon.LumberSite.Gold;
        baseBuildTurns = Config.BuildCon.LumberSite.BuildTime;

        lodgeUpgrade = new BuildingUpgrade();
        lodgeUpgrade.Name = Config.BuildCon.LumberSiteLodgeUpgrade.Name;
        lodgeUpgrade.Desc = Config.BuildCon.LumberSiteLodgeUpgrade.Desc;
        lodgeUpgrade.GoldCost = Config.BuildCon.LumberSiteLodgeUpgrade.GoldCost;
        lodgeUpgrade.ResourceToUpgrade = Config.BuildCon.LumberSiteLodgeUpgrade.ResourceToUpgrade;
        lodgeUpgrade.upgradeTime = Config.BuildCon.LumberSiteLodgeUpgrade.upgradeTime;
        Upgrades.Add(lodgeUpgrade);

        carpenterUpgrade = new BuildingUpgrade();
        carpenterUpgrade.Name = Config.BuildCon.LumberSiteCarpenterUpgrade.Name;
        carpenterUpgrade.Desc = Config.BuildCon.LumberSiteCarpenterUpgrade.Desc;
        carpenterUpgrade.GoldCost = Config.BuildCon.LumberSiteCarpenterUpgrade.GoldCost;
        carpenterUpgrade.ResourceToUpgrade = Config.BuildCon.LumberSiteCarpenterUpgrade.ResourceToUpgrade;
        carpenterUpgrade.upgradeTime = Config.BuildCon.LumberSiteCarpenterUpgrade.upgradeTime;
        Upgrades.Add(carpenterUpgrade);

        greenHouseUpgrade = new BuildingUpgrade();
        greenHouseUpgrade.Name = Config.BuildCon.LumberSiteGreenHouseUpgrade.Name;
        greenHouseUpgrade.Desc = Config.BuildCon.LumberSiteGreenHouseUpgrade.Desc;
        greenHouseUpgrade.GoldCost = Config.BuildCon.LumberSiteGreenHouseUpgrade.GoldCost;
        greenHouseUpgrade.ResourceToUpgrade = Config.BuildCon.LumberSiteGreenHouseUpgrade.ResourceToUpgrade;
        greenHouseUpgrade.upgradeTime = Config.BuildCon.LumberSiteGreenHouseUpgrade.upgradeTime;
        Upgrades.Add(greenHouseUpgrade);

        IdleWorkers = 0;
        woodWorkers = Config.BuildCon.LumberSiteWorkerCap;
        natureWorkers = 0;
        carpenterWorkers = 0;
        
    }

    internal override void RunBuildingFunction()
    {
        ConstructionResources ownerResource = Owner.constructionResources;
        int looseWorkers = IdleWorkers - woodWorkers - natureWorkers - carpenterWorkers;
        ownerResource.AddResource(ConstructionresourceType.wood, looseWorkers);

        if (lodgeUpgrade.built)
        {
            IdleWorkers = Config.BuildCon.LumberSiteWorkerCap * 2;
        }

        if (carpenterUpgrade.built)
        {
            ownerResource.AddResource(ConstructionresourceType.naturalmaterials, natureWorkers);
        }

        if (greenHouseUpgrade.built)
        {
            ownerResource.AddResource(ConstructionresourceType.prefabs, carpenterWorkers);

        }
    }
}

