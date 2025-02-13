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
        buildingType = ConstructibleType.LumberSite;

        ApplyConfigStats(Config.BuildCon.LumberSite);
        lodgeUpgrade = AddUpgrade(lodgeUpgrade, Config.BuildCon.LumberSiteLodgeUpgrade);
        carpenterUpgrade = AddUpgrade(carpenterUpgrade, Config.BuildCon.LumberSiteCarpenterUpgrade);
        greenHouseUpgrade = AddUpgrade(greenHouseUpgrade, Config.BuildCon.LumberSiteGreenHouseUpgrade);


        IdleWorkers = 0;
        woodWorkers = Config.BuildCon.LumberSiteWorkerCap;
        natureWorkers = 0;
        carpenterWorkers = 0;
        
    }

    internal override void RunBuildingFunction()
    {
        ConstructionResources ownerResource = Owner.constructionResources;
        ownerResource.AddResource(ConstructionresourceType.wood, woodWorkers + IdleWorkers);

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

