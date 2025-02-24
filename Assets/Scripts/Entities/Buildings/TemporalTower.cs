using OdinSerializer;
using System;
using System.Collections.Generic;

class TemporalTower : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade forceUpgrade;
    public BuildingUpgrade buffUpgrade;
    
    public TemporalTower(Vec2i location) : base(location)
    {
        Name = "Temporal Tower";
        Desc = "The temporal tower reduces the MP of monster armies in range. This can be extended to empires as well.";
        spriteID = 64;
        buildingType = ConstructibleType.TemporalTower;

        ApplyConfigStats(Config.BuildCon.CasterTower);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildCon.CasterTowerImproveUpgrade);
        forceUpgrade = AddUpgrade(forceUpgrade, Config.BuildCon.CasterTowerForceUpgrade);
        buffUpgrade = AddUpgrade(buffUpgrade, Config.BuildCon.CasterTowerBuffUpgrade);
    }
    internal override void RunBuildingFunction()
    {
        
    }
}

