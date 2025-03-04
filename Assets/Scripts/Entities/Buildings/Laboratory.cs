using OdinSerializer;
using System;
using System.Collections.Generic;

class Laboratory : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade pay2winUpgrade;
    public BuildingUpgrade buffUpgrade;
    
    public Laboratory(Vec2i location) : base(location)
    {
        Name = "Laboratory";
        Desc = "The laboratory is used to purchase potions that can modify a unit.";
        spriteID = 72;
        buildingType = ConstructibleType.Laboratory;

        ApplyConfigStats(Config.BuildCon.Laboratory);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildCon.LaboratoryImproveUpgrade);
        pay2winUpgrade = AddUpgrade(pay2winUpgrade, Config.BuildCon.CasterTowerForceUpgrade);
        buffUpgrade = AddUpgrade(buffUpgrade, Config.BuildCon.CasterTowerBuffUpgrade);
    }
    internal override void RunBuildingFunction()
    {
        
    }
}

