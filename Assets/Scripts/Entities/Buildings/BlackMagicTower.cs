using OdinSerializer;
using System;
using System.Collections.Generic;

class BlackMagicTower : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade forceUpgrade;
    public BuildingUpgrade buffUpgrade;
    
    public BlackMagicTower(Vec2i location) : base(location)
    {
        Name = "Dark Magic Tower";
        Desc = "The dark magic tower can preserve and restore unimprinted units.";
        spriteID = 56;
        buildingType = ConstructibleType.DarkMagicTower;

        ApplyConfigStats(Config.BuildCon.CasterTower);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildCon.CasterTowerImproveUpgrade);
        forceUpgrade = AddUpgrade(forceUpgrade, Config.BuildCon.CasterTowerForceUpgrade);
        buffUpgrade = AddUpgrade(buffUpgrade, Config.BuildCon.CasterTowerBuffUpgrade);
    }
    internal override void RunBuildingFunction()
    {
        
    }
}

