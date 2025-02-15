using OdinSerializer;
using System.Collections.Generic;
using System.Linq;


public abstract class ConstructibleBuilding
{
    [OdinSerialize]
    internal Empire Owner;

    [OdinSerialize]
    internal Vec2i Position;

    [OdinSerialize]
    internal List<BuildingUpgrade> Upgrades; 

    [OdinSerialize]
    internal int GoldCost; 
    [OdinSerialize]
    internal ConstructionResources ResourceToBuild;

    [OdinSerialize]
    internal int turnsToCompletion;
    [OdinSerialize]
    internal int turnsToUpgrade;
    [OdinSerialize]
    internal int baseBuildTurns;
    public int upgradeStage => Upgrades.Where(u=> u.built).Count();
    public bool constructing => turnsToCompletion > 0;
    public bool upgrading => turnsToUpgrade > 0;
    public bool active => !constructing && !upgrading;

    [OdinSerialize]
    internal bool enabled = true;

    [OdinSerialize]
    internal string Name;

    [OdinSerialize]
    internal string Desc;

    [OdinSerialize]
    internal int spriteID;

    [OdinSerialize]
    internal ConstructibleType buildingType;
    protected ConstructibleBuilding(Vec2i location)
    {
        Position = location;
        GoldCost = 0;
        ResourceToBuild = new ConstructionResources();
        baseBuildTurns = -1;
        Upgrades = new List<BuildingUpgrade>();
    }

    internal abstract void RunBuildingFunction();
    internal void ConstructBuilding() 
    {
        Owner.Buildings.Add(this);
        turnsToCompletion = baseBuildTurns;
        Owner.constructionResources.SpendProvidedResources(ResourceToBuild);
        Owner.SpendGold(GoldCost);
        var contstruct = State.World.Constructibles.ToList();
        contstruct.Add(this);
        State.World.Constructibles = contstruct.ToArray();
        State.GameManager.StrategyMode.RedrawVillages();
        State.GameManager.StrategyMode.StatusBarUI.Gold.text = "Gold:" + Owner.Gold;
        
    }

    public void RunBuildingTurn()
    {
        if (constructing)
        {
            turnsToCompletion--;
            return;
        }
        if (upgrading)
        {
            turnsToUpgrade--;
            return;
        }
        if (Owner != null)
        {
            RunBuildingFunction();
        }
    }

    internal void ApplyConfigStats(GeneralBuildingConfig buildingConfig)
    {
        ResourceToBuild = buildingConfig.Resources;
        GoldCost = buildingConfig.Gold;
        baseBuildTurns = buildingConfig.BuildTime;
    }

    internal BuildingUpgrade AddUpgrade(BuildingUpgrade upgrade, BuildingUpgrade configUpgrade)
    {
        upgrade = new BuildingUpgrade();
        upgrade.Name = configUpgrade.Name;
        upgrade.Desc = configUpgrade.Desc;
        upgrade.GoldCost = configUpgrade.GoldCost;
        upgrade.ResourceToUpgrade = configUpgrade.ResourceToUpgrade;
        upgrade.upgradeTime = configUpgrade.upgradeTime;
        Upgrades.Add(upgrade);
        return upgrade;
    }

}

