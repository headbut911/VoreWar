using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class StrategicBuildingContractor
{
    Empire empire;
    int goldBank;

    BuildingUpgrade upgradeWanted;
    ConstructibleBuilding upgradeBuilding;
    ConstructibleType buildingWanted;
    Dictionary<ConstructibleType, GeneralBuildingConfig> buildingInfo;
    ConstructionResources resourceWanted;
    ConstructibleBuilding activeBuilding;

    int switchTimer;
    float switchChance => switchTimer * (-0.1f);
    int activeConstructions => empire.Buildings.Where(b => b.active).Count();

    public StrategicBuildingContractor(Empire empire)
    {
        this.empire = empire;
        goldBank = 0;

        buildingInfo = new Dictionary<ConstructibleType, GeneralBuildingConfig>
        {
            [ConstructibleType.WorkCamp] = Config.BuildConfig.WorkCamp,
            [ConstructibleType.LumberSite] = Config.BuildConfig.LumberSite,
            [ConstructibleType.Quarry] = Config.BuildConfig.Quarry,
            [ConstructibleType.CasterTower] = Config.BuildConfig.CasterTower,
            [ConstructibleType.BarrierTower] = Config.BuildConfig.BarrierTower,
            [ConstructibleType.DefEncampment] = Config.BuildConfig.DefenseEncampment,
            [ConstructibleType.Academy] = Config.BuildConfig.Academy,
            [ConstructibleType.DarkMagicTower] = Config.BuildConfig.DarkMagicTower,
            [ConstructibleType.TemporalTower] = Config.BuildConfig.TemporalTower,
            [ConstructibleType.Laboratory] = Config.BuildConfig.Laboratory,
            [ConstructibleType.Teleporter] = Config.BuildConfig.Teleporter,
            [ConstructibleType.TownHall] = Config.BuildConfig.TownHall,
        };

        GetNewProject();
    }

    internal bool AssessBuildStatus()
    {
        Debug.Log(goldBank);

        RunGoldStashing();
        RunAllBuildingActives();
        if (activeConstructions >= 3)
        {
            return true;
        }
        if (upgradeWanted != null)
        {

        }
        if (buildingWanted >= 0)
        {
            Vec2i loc = LocateGoodLocation();
            if (loc != null)
            {
                ConstructBuilding(loc, buildingWanted);
            }
        }

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
        int transact = (empire.Gold / 10) * State.Rand.Next(4);
        //Debug.Log($"Adding {transact} to bank.");
        TransactGold(transact);
    }


    internal Vec2i LocateGoodLocation()
    {
        if (empire.OwnedTiles == null || empire.OwnedTiles.Count <= 0)
        {
            return null;
        }
        List<Vec2i> locs = empire.OwnedTiles;

        // player can't build on following, so nor should AI
        foreach (var vil in State.World.Villages)
        {
            locs.Remove(vil.Position);
        }
        foreach (var con in State.World.Constructibles)
        {
            locs.Remove(con.Position);
        }
        foreach (var cla in State.World.Claimables)
        {
            locs.Remove(cla.Position);
        }
        foreach (var tele in State.World.AncientTeleporters)
        {
            locs.Remove(tele.Position);
        }
        foreach (var mer in State.World.MercenaryHouses)
        {
            locs.Remove(mer.Position);
        }

        return locs[State.Rand.Next(locs.Count)];
    }

    internal void DecideBuilding()
    {
        Dictionary<ConstructibleType, GeneralBuildingConfig> AIBuilding = new Dictionary<ConstructibleType, GeneralBuildingConfig>();
        foreach (var item in buildingInfo)
        {
            if (item.Value.AICanBuild && empire.WithinBuildLimit(item.Key))
                AIBuilding.Add(item.Key, item.Value);
        }

        if (AIBuilding.Count <= 0)
        {
            // All buildings are off, or constructed,
            TransactGold(goldBank * -1);
            return;
        }
        // Ensure generation buildings are constructed first
        if (!empire.Buildings.Where(b => b is WorkCamp).Any() && AIBuilding.Keys.Contains(ConstructibleType.WorkCamp))
        {
            buildingWanted = ConstructibleType.WorkCamp;
            return;
        }
        if (!empire.Buildings.Where(b => b is LumberSite).Any() && AIBuilding.Keys.Contains(ConstructibleType.LumberSite))
        {
            buildingWanted = ConstructibleType.LumberSite;
            return;

        }
        if (!empire.Buildings.Where(b => b is Quarry).Any() && AIBuilding.Keys.Contains(ConstructibleType.Quarry))
        {
            buildingWanted = ConstructibleType.Quarry;
            return;
        }
        //If producion is created, allow Any kind of building to be constructed
        buildingWanted = AIBuilding.ElementAt(State.Rand.Next(AIBuilding.Count)).Key;
    }

    internal void DecideUpgrade()
    {
        Dictionary<BuildingUpgrade, ConstructibleBuilding> upgradeDict = new Dictionary<BuildingUpgrade, ConstructibleBuilding>();
        foreach (var building in empire.Buildings)
        {
            foreach (var upgrade in building.Upgrades.Where(u => !u.built))
            {
                upgradeDict.Add(upgrade, building);
            }
        }
        int index = State.Rand.Next(0, upgradeDict.Count);
        upgradeWanted = upgradeDict.ElementAt(index).Key;
        upgradeBuilding = upgradeDict.ElementAt(index).Value;
        resourceWanted = upgradeWanted.ResourceToUpgrade;
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

    internal void RunWorkCamp()
    {
        WorkCamp workCamp = activeBuilding as WorkCamp;

        // If we want an upgrade, buy resources
        if (resourceWanted != null)
        {
            // Bank a bit more gold to buy resources
            TransactGold(empire.Gold / 10);
            //get count of needed resource
            Dictionary<ConstructionResourceType, int> neededResource = empire.constructionResources.NeededResources(resourceWanted);
            // get how many we generate per turn
            Dictionary<ConstructionResourceType, int> generatedResource = GetGeneration();
            foreach (var item in neededResource)
            {
                // skip buying if we reach goal with two turns of generation
                if (generatedResource[item.Key] * 2 >= neededResource[item.Key])
                {
                    continue;
                }
                int count = item.Value;
                // While we have enough gold, and item is in stock, and we still need the item
                while (goldBank > Config.BuildConfig.WorkCampItemPrice.ResourceCountFromType(item.Key) && AIBuildingUtilityFunctions.WorkCampUtility.PurchaseResource(item.Key, workCamp) && count >= 1)
                {
                    count--;
                }
            }
        }
    }

    /// <summary>
    /// Easy way to simplify contractor reserving gold.
    /// Adds gold to bank from empire's stash.
    /// Removes from stash and adds back to empire in some cases.
    /// </summary>
    internal void TransactGold(int gold)
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

    internal void CheckForSwitch()
    {
        switchTimer--;
        if (switchTimer > 0)
        {
            return;
        }
        if (ShouldSwitch())
        {
            GetNewProject();
        }
    }
    /// <summary>
    /// Switches the desired project if it gets stuck.
    /// </summary>
    internal bool ShouldSwitch()
    {
        if (State.Rand.NextDouble() > switchChance)
        {
            return false;
        }
        switchTimer = 5;
        return true;
    }

    internal void GetNewProject()
    {
        upgradeWanted = null;
        upgradeBuilding = null;
        buildingWanted = (ConstructibleType)(-1);
        activeBuilding = null;
        resourceWanted = null;
        switchTimer = 0;
        int buidlingsConstructed = empire.Buildings.Count;
        int upgradesNeeded = 0;
        foreach (var item in empire.Buildings)
        {
            upgradesNeeded += item.Upgrades.Where(u => !u.built).Count();
        }
        // chance upgrade is chosen increases the more upgrades there are to buildings
        double chanceForUpgrade = (upgradesNeeded / 10) / Math.Pow(buidlingsConstructed, 0.5f);
        if (chanceForUpgrade > State.Rand.NextDouble())
        {
            Debug.Log("Chose Upgrade");
            DecideUpgrade();
        }
        else
        {
            Debug.Log("Chose Building");
            DecideBuilding();          
        }
    }
    /// <summary>
    /// Performs all required generation calculations
    /// </summary>
    internal Dictionary<ConstructionResourceType, int> GetGeneration()
    {
        Dictionary<ConstructionResourceType, int> perTurnGen = new Dictionary<ConstructionResourceType, int>
        {
            [ConstructionResourceType.wood] = 0,
            [ConstructionResourceType.stone] = 0,
            [ConstructionResourceType.naturalmaterials] = 0,
            [ConstructionResourceType.ores] = 0,
            [ConstructionResourceType.prefabs] = 0,
            [ConstructionResourceType.manastones] = 0,
        };

        foreach (var building in empire.Buildings)
        {
            switch (building.buildingType)
            {
                case ConstructibleType.WorkCamp:
                    WorkCamp camp = (WorkCamp)building;
                    perTurnGen[ConstructionResourceType.wood] += Config.BuildConfig.WorkCampGenerationPerTurn;
                    perTurnGen[ConstructionResourceType.stone] += Config.BuildConfig.WorkCampGenerationPerTurn;
                    break;
                case ConstructibleType.LumberSite:
                    LumberSite site = (LumberSite)building;
                    perTurnGen[ConstructionResourceType.wood] += site.woodWorkers;
                    perTurnGen[ConstructionResourceType.naturalmaterials] += site.natureWorkers;
                    perTurnGen[ConstructionResourceType.prefabs] += site.carpenterWorkers;
                    break;
                case ConstructibleType.Quarry:
                    Quarry quarry = (Quarry)building;
                    if (quarry.ActionPlan == 0)
                    {
                        perTurnGen[ConstructionResourceType.stone] += ((Config.BuildConfig.QuarryStoneMin + Config.BuildConfig.QuarryStoneMax) / 2) + (quarry.improveUpgrade.built ? 1 : 0);
                        perTurnGen[ConstructionResourceType.ores] += ((Config.BuildConfig.QuarryOreMin + Config.BuildConfig.QuarryOreMax) / 2) + (quarry.improveUpgrade.built ? 1 : 0) * (quarry.deepUpgrade.built ? 1 : 0);
                        perTurnGen[ConstructionResourceType.manastones] += ((Config.BuildConfig.QuarryMSMin + Config.BuildConfig.QuarryMSMax) / 2) + (quarry.improveUpgrade.built ? 1 : 0) * (quarry.leyUpgrade.built ? 1 : 0);
                    }
                    else if (quarry.ActionPlan == 1)
                    {
                        perTurnGen[ConstructionResourceType.stone] += ((Config.BuildConfig.QuarryStoneMin / 2 + (int)(Config.BuildConfig.QuarryStoneMax * (quarry.improveUpgrade.built ? 1.75f : 1.5f))) / 2);
                        perTurnGen[ConstructionResourceType.ores] += ((Config.BuildConfig.QuarryOreMin / 2 + (int)(Config.BuildConfig.QuarryOreMax * (quarry.improveUpgrade.built ? 1.75f : 1.5f))) / 2) * (quarry.deepUpgrade.built ? 1 : 0);
                        perTurnGen[ConstructionResourceType.manastones] += ((Config.BuildConfig.QuarryMSMin / 2 + (int)(Config.BuildConfig.QuarryMSMax * (quarry.improveUpgrade.built ? 1.75f : 1.5f))) / 2) * (quarry.leyUpgrade.built ? 1 : 0);
                    }
                    else if (quarry.ActionPlan == 2)
                    {
                        perTurnGen[ConstructionResourceType.stone] += Config.BuildConfig.QuarryStoneMax / (quarry.improveUpgrade.built ? 2 : 4);
                        perTurnGen[ConstructionResourceType.ores] += Config.BuildConfig.QuarryOreMax / (quarry.improveUpgrade.built ? 2 : 4);
                        perTurnGen[ConstructionResourceType.manastones] += Config.BuildConfig.QuarryMSMax / (quarry.improveUpgrade.built ? 2 : 4);
                    }
                    else if (quarry.ActionPlan == 3)
                    {
                        perTurnGen[ConstructionResourceType.stone] += ((Config.BuildConfig.QuarryStoneMin + Config.BuildConfig.QuarryStoneMax) / 2);
                        perTurnGen[ConstructionResourceType.ores] += ((Config.BuildConfig.QuarryOreMin + Config.BuildConfig.QuarryOreMax) / 2) * (quarry.deepUpgrade.built ? 1 : 0);
                        perTurnGen[ConstructionResourceType.manastones] += ((Config.BuildConfig.QuarryMSMin + Config.BuildConfig.QuarryMSMax) / 2) * (quarry.leyUpgrade.built ? 1 : 0);
                    }
                    else if (quarry.ActionPlan == 4)
                    {
                        perTurnGen[ConstructionResourceType.ores] += (int)Math.Round((Config.BuildConfig.QuarryOreMin + Config.BuildConfig.QuarryOreMax) * 0.6f) * (quarry.deepUpgrade.built ? 1 : 0);
                        perTurnGen[ConstructionResourceType.manastones] += (int)Math.Round((Config.BuildConfig.QuarryMSMin + Config.BuildConfig.QuarryMSMax) * 0.4f) * (quarry.leyUpgrade.built ? 1 : 0);
                    }
                    break;
                default:
                    break;
            }
        }
        return perTurnGen;
    }
    void ConstructBuilding(Vec2i position, ConstructibleType SelectedConstruction)
    {
        // If not enough resource, don't build
        if (!empire.constructionResources.CanBuildWithCurrentResources(buildingInfo[buildingWanted].Resources) || !(goldBank >= buildingInfo[buildingWanted].Gold))
        {
            return;
        }
        ConstructibleBuilding newBuilding;
        switch (SelectedConstruction)
        {
            case ConstructibleType.WorkCamp:
                TransactGold(Config.BuildConfig.WorkCamp.Gold * -1);
                newBuilding = new WorkCamp(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.LumberSite:
                TransactGold(Config.BuildConfig.LumberSite.Gold * -1);
                newBuilding = new LumberSite(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.Quarry:
                TransactGold(Config.BuildConfig.Quarry.Gold * -1);
                newBuilding = new Quarry(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.CasterTower:
                TransactGold(Config.BuildConfig.CasterTower.Gold * -1);
                newBuilding = new CasterTower(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.BarrierTower:
                TransactGold(Config.BuildConfig.BarrierTower.Gold * -1);
                newBuilding = new BarrierTower(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.DefEncampment:
                TransactGold(Config.BuildConfig.DefenseEncampment.Gold * -1);
                newBuilding = new DefenseEncampment(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.Academy:
                TransactGold(Config.BuildConfig.Academy.Gold * -1);
                newBuilding = new Academy(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.DarkMagicTower:
                TransactGold(Config.BuildConfig.DarkMagicTower.Gold * -1);
                newBuilding = new BlackMagicTower(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.TemporalTower:
                TransactGold(Config.BuildConfig.Teleporter.Gold * -1);
                newBuilding = new TemporalTower(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.Laboratory:
                TransactGold(Config.BuildConfig.Laboratory.Gold * -1);
                newBuilding = new Laboratory(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.Teleporter:
                TransactGold(Config.BuildConfig.Teleporter.Gold * -1);
                newBuilding = new Teleporter(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
            case ConstructibleType.TownHall:
                TransactGold(Config.BuildConfig.TownHall.Gold * -1);
                newBuilding = new TownHall(position);
                newBuilding.Owner = empire;
                newBuilding.ConstructBuilding();
                break;
        }
        buildingWanted = (ConstructibleType)(-1);
        resourceWanted = null;
    }

    internal void BuildUpgrades()
    {
        if (upgradeWanted != null)
        {
            // 20% chance to skip upgrade and save
            if (State.Rand.Next(5) == 0)
            {
                return;
            }
            // If all resource ready, build upgrade
            if (empire.constructionResources.CanBuildWithCurrentResources(upgradeWanted.ResourceToUpgrade) && goldBank >= upgradeWanted.GoldCost)
            {
                upgradeBuilding.turnsToUpgrade += upgradeWanted.upgradeTime;
                empire.constructionResources.SpendProvidedResources(upgradeWanted.ResourceToUpgrade);
                SpendGold(upgradeWanted.GoldCost);
                State.GameManager.StrategyMode.RedrawVillages();
            }
        }
    }
}
