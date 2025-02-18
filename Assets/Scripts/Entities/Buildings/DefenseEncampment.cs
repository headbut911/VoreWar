using OdinSerializer;
using System;

class DefenseEncampment : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade healUpgrade;
    public BuildingUpgrade buffUpgrade;

    public DefenseEncampment(Vec2i location) : base(location)
    {
        Name = "Defense Encampment";
        Desc = "The defense encampment sends reinforcements when an army is attacked.";
        spriteID = 40;
        buildingType = ConstructibleType.DefEncampment;

        ApplyConfigStats(Config.BuildCon.DefenseEncampment);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildCon.BarrierTowerImproveUpgrade);
        healUpgrade = AddUpgrade(healUpgrade, Config.BuildCon.BarrierTowerHealUpgrade);
        buffUpgrade = AddUpgrade(buffUpgrade, Config.BuildCon.BarrierTowerBuffUpgrade);

    }
    internal override void RunBuildingFunction()
    {

    }
}

