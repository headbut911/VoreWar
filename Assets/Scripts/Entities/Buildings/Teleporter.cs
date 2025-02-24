using OdinSerializer;
using System;
using System.Collections.Generic;

class Teleporter : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade forceUpgrade;
    public BuildingUpgrade buffUpgrade;
    
    public Teleporter(Vec2i location) : base(location)
    {
        Name = "Teleporter";
        Desc = "The teleporter helps move armies across the map quickly.";
        spriteID = 80;
        buildingType = ConstructibleType.Teleporter;

        ApplyConfigStats(Config.BuildCon.CasterTower);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildCon.CasterTowerImproveUpgrade);
        forceUpgrade = AddUpgrade(forceUpgrade, Config.BuildCon.CasterTowerForceUpgrade);
        buffUpgrade = AddUpgrade(buffUpgrade, Config.BuildCon.CasterTowerBuffUpgrade);
    }
    internal override void RunBuildingFunction()
    {
        
    }
}

