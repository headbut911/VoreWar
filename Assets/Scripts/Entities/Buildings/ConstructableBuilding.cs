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
    internal bool AIenabled;


    [OdinSerialize]
    internal string Name;

    [OdinSerialize]
    internal string Desc;

    [OdinSerialize]
    internal int spriteID;
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
        turnsToCompletion = baseBuildTurns;
        Owner.constructionResources.SpendProvidedResources(ResourceToBuild);
        Owner.SpendGold(GoldCost);
        var contstruct = State.World.Constructibles.ToList();
        contstruct.Add(this);
        State.World.Constructibles = contstruct.ToArray();
        State.GameManager.StrategyMode.RedrawVillages();

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
        RunBuildingFunction();
    }

}

