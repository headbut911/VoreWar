using OdinSerializer;
using System;
using System.Collections.Generic;

class TownHall : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade forceUpgrade;
    public BuildingUpgrade buffUpgrade;
    
    public TownHall(Vec2i location) : base(location)
    {
        Name = "Town Hall";
        Desc = "The town hall is used to construct a new village.";
        spriteID = 88;
        buildingType = ConstructibleType.TownHall;

        ApplyConfigStats(Config.BuildCon.CasterTower);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildCon.CasterTowerImproveUpgrade);
        forceUpgrade = AddUpgrade(forceUpgrade, Config.BuildCon.CasterTowerForceUpgrade);
        buffUpgrade = AddUpgrade(buffUpgrade, Config.BuildCon.CasterTowerBuffUpgrade);
    }
    internal override void RunBuildingFunction()
    {
        
    }
}

