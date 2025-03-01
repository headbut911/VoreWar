using OdinSerializer;
using System;
using System.Collections.Generic;

class BlackMagicTower : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade soulUpgrade;
    public BuildingUpgrade afflictUpgrade;
    
    [OdinSerialize]
    internal Dictionary<Unit,int> storedUnits;
    [OdinSerialize]
    internal int PactLevel;
    [OdinSerialize]
    internal int SoulPower;
    [OdinSerialize]
    internal StatusEffectType Affliction;

    public BlackMagicTower(Vec2i location) : base(location)
    {
        Name = "Dark Magic Tower";
        Desc = "The dark magic tower can preserve and restore unimprinted units.";
        spriteID = 56;
        buildingType = ConstructibleType.DarkMagicTower;

        ApplyConfigStats(Config.BuildCon.CasterTower);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildCon.DarkMagicTowerImproveUpgrade);
        soulUpgrade = AddUpgrade(soulUpgrade, Config.BuildCon.DarkMagicTowerSoulUpgrade);
        afflictUpgrade = AddUpgrade(afflictUpgrade, Config.BuildCon.DarkMagicTowerAfflictionUpgrade);

        storedUnits = new Dictionary<Unit, int>();
        Unit newUnit = new NPC_unit(10, false, 2, 0, Race.Fairies, 0, false);
        storedUnits.Add(newUnit, 5);
        SoulPower = 0;
        PactLevel = 0;
        Affliction = StatusEffectType.Necrosis;
    }
    internal override void RunBuildingFunction()
    {

    }
}

