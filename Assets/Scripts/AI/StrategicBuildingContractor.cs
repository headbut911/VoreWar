using System;
using System.Collections.Generic;
using System.Linq;

class StrategicBuildingContractor
{
    Empire empire;
    int goldBank;

    BuildingUpgrade upgradeWanted;
    ConstructibleBuilding buildingWanted;
    ConstructibleBuilding activeBuilding;
    public StrategicBuildingContractor(Empire empire)
    {
        this.empire = empire;
        goldBank = 0;
        upgradeWanted = null;
        buildingWanted = null;
        activeBuilding = null;
    }

    internal bool AssessBuildStatus()
    {
        RunGoldStashing();
        RunAllBuildingActives();
        return true;
    }

    internal void RunGoldStashing()
    {
        // No need to bank, likely have other problems that require remaining gold
        if (empire.Income <= 0)
        {
            return;
        }
        // Stashes 0% to 30% of current gold
        BankGold((empire.Gold / 10) * State.Rand.Next(4));
    }


    internal List<Vec2i> LocateGoodLocation()
    {
        List<Vec2i > locs = new List<Vec2i>();

        return locs;
    }
    
    internal void RunAllBuildingActives()
    {
        foreach (var building in empire.Buildings)
        {
            activeBuilding = building;
            switch (building.buildingType)
            {
                case ConstructibleType.WorkCamp:
                    RunWorkCamp();
                    break;
                case ConstructibleType.LumberSite:
                    break;
                case ConstructibleType.Quarry:
                    break;
                case ConstructibleType.CasterTower:
                    break;
                case ConstructibleType.BarrierTower:
                    break;
                case ConstructibleType.DefEncampment:
                    break;
                case ConstructibleType.Academy:
                    break;
                case ConstructibleType.DarkMagicTower:
                    break;
                case ConstructibleType.TemporalTower:
                    break;
                case ConstructibleType.Laboratory:
                    break;
                case ConstructibleType.Teleporter:
                    break;
                case ConstructibleType.TownHall:
                    break;
                default:
                    break;
            }
        }
    }
    
    internal void RunUpgrades()
    {
        if (upgradeWanted != null)
        {
            if (State.Rand.Next(4) != 0)
            {
                return;
            }
            if (empire.constructionResources.CanBuildWithCurrentResources(upgradeWanted.ResourceToUpgrade))
            {

            }
        }
    }
    
    internal void RunWorkCamp()
    {
        WorkCamp workCamp = activeBuilding as WorkCamp;

        // If we want an upgrade, buy, if not and low on gold, sell
        if (upgradeWanted != null)
        {
            // Pull in a bit more gold to buy
            BankGold(empire.Gold / 10);
            
            //get count of needed resource
        }
        else 
        { 
            
        }
    }
    /// <summary>
    /// Easy way to simplify contractor reserving gold.
    /// Adds gold to bank from empire's stash.
    /// </summary>
    internal void BankGold(int gold) 
    {
        goldBank += gold;
        empire.RemoveGold(gold);
    }

    /// <summary>
    /// Easy way to simplify contractor gold spending.
    /// Adds gold from bank to empire, then spend it properly.
    /// </summary>
    internal void SpendGold(int gold) 
    {
        goldBank -= gold;
        empire.RemoveGold(gold * -1);
        empire.SpendGold(gold);
    }
}
