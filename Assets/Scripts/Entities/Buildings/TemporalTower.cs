using OdinSerializer;
using System;
using System.Collections.Generic;

class TemporalTower : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade tuneUpgrade;
    public BuildingUpgrade disruptUpgrade;
    
    public TemporalTower(Vec2i location) : base(location)
    {
        Name = "Temporal Tower";
        Desc = "The temporal tower reduces the MP of monster armies in range. This can be extended to empires as well.";
        spriteID = 64;
        buildingType = ConstructibleType.TemporalTower;

        ApplyConfigStats(Config.BuildCon.TemporalTower);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildCon.TemporalTowerImproveUpgrade);
        tuneUpgrade = AddUpgrade(tuneUpgrade, Config.BuildCon.TemporalTowerTuneUpgrade);
        disruptUpgrade = AddUpgrade(disruptUpgrade, Config.BuildCon.TemporalTowerDisruptUpgrade);
    }
    internal override void RunBuildingFunction()
    {
        
    }
}

