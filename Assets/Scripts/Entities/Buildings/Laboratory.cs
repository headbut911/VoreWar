using OdinSerializer;
using System;
using System.Collections.Generic;

class Laboratory : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade forceUpgrade;
    public BuildingUpgrade buffUpgrade;
    
    public Laboratory(Vec2i location) : base(location)
    {
        Name = "Laboratory";
        Desc = "The laboratory is used to purchase potions that can add or remove traits.";
        spriteID = 72;
        buildingType = ConstructibleType.Laboratory;

        ApplyConfigStats(Config.BuildCon.CasterTower);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildCon.CasterTowerImproveUpgrade);
        forceUpgrade = AddUpgrade(forceUpgrade, Config.BuildCon.CasterTowerForceUpgrade);
        buffUpgrade = AddUpgrade(buffUpgrade, Config.BuildCon.CasterTowerBuffUpgrade);
    }
    internal override void RunBuildingFunction()
    {
        
    }
}

