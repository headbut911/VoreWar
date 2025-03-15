using OdinSerializer;
using System;
using System.Collections.Generic;

class Laboratory : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade ingredientUpgrade;
    public BuildingUpgrade boostUpgrade;
    
    public Laboratory(Vec2i location) : base(location)
    {
        Name = "Laboratory";
        Desc = "The laboratory is used to purchase potions that can modify a unit.";
        spriteID = 80;
        buildingType = ConstructibleType.Laboratory;

        ApplyConfigStats(Config.BuildCon.Laboratory);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildCon.LaboratoryImproveUpgrade);
        ingredientUpgrade = AddUpgrade(ingredientUpgrade, Config.BuildCon.LaboratoryIngredientUpgrade);
        boostUpgrade = AddUpgrade(boostUpgrade, Config.BuildCon.LaboratoryBoostUpgrade);
    }
    internal override void RunBuildingFunction()
    {
        
    }
}

