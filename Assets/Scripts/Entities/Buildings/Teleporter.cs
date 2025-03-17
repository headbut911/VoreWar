using OdinSerializer;
using System;
using System.Collections.Generic;

class Teleporter : ConstructibleBuilding
{
    public BuildingUpgrade stoneUpgrade;
    public BuildingUpgrade capacityUpgrade;
    public BuildingUpgrade ancientUpgrade;

    [OdinSerialize]
    internal float TeleportCapacity;
    public Teleporter(Vec2i location) : base(location)
    {
        Name = "Teleporter";
        Desc = "The teleporter helps move armies across the map quickly.";
        spriteID = 72;
        buildingType = ConstructibleType.Teleporter;

        ApplyConfigStats(Config.BuildCon.Teleporter);
        stoneUpgrade = AddUpgrade(stoneUpgrade, Config.BuildCon.TeleporterStoneUpgrade);
        capacityUpgrade = AddUpgrade(capacityUpgrade, Config.BuildCon.TeleporterCapacityUpgrade);
        ancientUpgrade = AddUpgrade(ancientUpgrade, Config.BuildCon.TeleporterAncientUpgrade);
        TeleportCapacity = Config.BuildCon.TeleporterMaxCapacity;
    }
    internal override void RunBuildingFunction()
    {
        TeleportCapacity += Config.BuildCon.TeleporterCapacityRegen;
        if (TeleportCapacity > Config.BuildCon.TeleporterMaxCapacity && !capacityUpgrade.built)
        {
            TeleportCapacity = Config.BuildCon.TeleporterMaxCapacity;
        }
    }
}

