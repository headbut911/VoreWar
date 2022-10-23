﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

static class TacticalUtilities
{
    static Army[] armies;
    static Village village;
    static List<Actor_Unit> garrison;
    static TacticalTileType[,] tiles;
    static bool[,] blockedTiles;
    static bool[,] blockedClimberTiles;
    static HirePanel UnitPickerUI;

    static internal TacticalMessageLog Log => State.GameManager.TacticalMode.Log;

    static internal List<Actor_Unit> Units { get; private set; }

    static internal void ResetData()
    {
        armies = null;
        village = null;
        Units = null;
        tiles = null;
        garrison = null;
        blockedTiles = null;
        blockedClimberTiles = null;
    }

    static internal void ResetData(Army[] larmies, Village lvillage, List<Actor_Unit> lunits, List<Actor_Unit> lgarrison, TacticalTileType[,] ltiles, bool[,] lblockedTiles, bool[,] lblockedClimberTiles)
    {
        armies = larmies;
        village = lvillage;
        Units = lunits;
        tiles = ltiles;
        garrison = lgarrison;
        blockedTiles = lblockedTiles;
        blockedClimberTiles = lblockedClimberTiles;
        UnitPickerUI = State.GameManager.TacticalMode.UnitPickerUI;
    }

    static internal void ProcessTravelingUnits(List<Unit> travelingUnits)
    {
        if (State.World.Villages == null)
        {
            return;
        }
        if (travelingUnits[0].Side < 60)
        {
            if (Config.TroopScatter)
            {
                foreach (var unit in travelingUnits.ToList())
                {
                    var villageList = State.World.Villages.Where(s => travelingUnits[0].Side == s.Side && s != village).ToList();
                    Village friendlyVillage;
                    if (villageList.Count() == 0)
                        continue;
                    if (villageList.Count() == 1)
                        friendlyVillage = villageList[0];
                    else
                        friendlyVillage = villageList[State.Rand.Next(villageList.Count())];
                    var loc = StrategyPathfinder.GetPath(null, armies[0].Position, friendlyVillage.Position, 3, false, 999);
                    int turns = 9999;
                    int flightTurns = 9999;
                    Vec2i destination = null;
                    bool flyersExist = unit.HasTrait(Traits.Pathfinder);
                    if (loc != null && loc.Count > 0)
                    {
                        destination = new Vec2i(loc.Last().X, loc.Last().Y);
                        turns = StrategyPathfinder.TurnsToReach(null, armies[0].Position, destination, Config.ArmyMP, false);
                        if (flyersExist)
                            flightTurns = StrategyPathfinder.TurnsToReach(null, armies[0].Position, destination, Config.ArmyMP, true);
                    }
                    if (turns < 999)
                    {
                        if (flyersExist)
                            StrategicUtilities.CreateInvisibleTravelingArmy(unit, StrategicUtilities.GetVillageAt(destination), flightTurns);
                        else
                            StrategicUtilities.CreateInvisibleTravelingArmy(unit, StrategicUtilities.GetVillageAt(destination), turns);
                    }
                    travelingUnits.Remove(unit);
                }
            }
            else
            {
                var loc = StrategyPathfinder.GetPathToClosestObject(null, armies[0].Position, State.World.Villages.Where(s => travelingUnits[0].Side == s.Side && s != village).Select(s => s.Position).ToArray(), 3, 999, false);
                int turns = 9999;
                int flightTurns = 9999;
                Vec2i destination = null;
                bool flyersExist = travelingUnits.Where(s => s.HasTrait(Traits.Pathfinder)).Count() > 0;
                if (loc != null && loc.Count > 0)
                {
                    destination = new Vec2i(loc.Last().X, loc.Last().Y);
                    turns = StrategyPathfinder.TurnsToReach(null, armies[0].Position, destination, Config.ArmyMP, false);
                    if (flyersExist)
                        flightTurns = StrategyPathfinder.TurnsToReach(null, armies[0].Position, destination, Config.ArmyMP, true);
                }
                if (turns < 999)
                {
                    if (flyersExist)
                        StrategicUtilities.CreateInvisibleTravelingArmy(travelingUnits.Where(s => s.HasTrait(Traits.Pathfinder)).ToList(), StrategicUtilities.GetVillageAt(destination), flightTurns);
                    StrategicUtilities.CreateInvisibleTravelingArmy(travelingUnits.Where(s => s.HasTrait(Traits.Pathfinder) == false).ToList(), StrategicUtilities.GetVillageAt(destination), turns);
                }
            }
            

        }
        else if (travelingUnits[0].Side > 500)
        {
            //Bandits and rebels that flee simply vanish
        }
        else
        {
            GenerateFleeingArmy(travelingUnits);
        }

    }



    static void GenerateFleeingArmy(List<Unit> fleeingUnits)
    {
        if (fleeingUnits.Any() == false)
            return;
        if (Config.MonstersCanReform == false)
            return;
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                Vec2i loc = new Vec2i(armies[0].Position.x + x, armies[0].Position.y + y);
                if (loc.x < 0 || loc.y < 0 || loc.x >= Config.StrategicWorldSizeX || loc.y >= Config.StrategicWorldSizeY)
                    continue;
                if (StrategicUtilities.IsTileClear(loc))
                {
                    MonsterEmpire monsterEmp = (MonsterEmpire)State.World.GetEmpireOfSide(fleeingUnits[0].Side);
                    if (monsterEmp == null)
                        return;
                    var army = new Army(monsterEmp, loc, fleeingUnits[0].Side);
                    army.RemainingMP = 0;
                    monsterEmp.Armies.Add(army);
                    army.Units.AddRange(fleeingUnits);
                    return;
                }
            }
        }
    }

    static internal void CleanVillage(int remainingAttackers)
    {
        bool MonsterAttacker = armies[0].Side >= 100;
        SpawnerInfo spawner = Config.SpawnerInfo((Race)armies[0].Side);
        Config.MonsterConquestType spawnerType;
        if (spawner != null)
            spawnerType = spawner.GetConquestType();
        else
            spawnerType = Config.MonsterConquest;
        //clean up missing garrison units
        if (garrison != null)
        {
            foreach (Actor_Unit garrison in garrison)
            {
                if (garrison.Unit.IsDead || garrison.Fled || garrison.Unit.Side == armies[0].Side)
                {
                    village.VillagePopulation.RemoveHireable(garrison.Unit);
                }
            }
        }
        if (village.Side != armies[0].Side && remainingAttackers > 0 && (MonsterAttacker == false || spawnerType != Config.MonsterConquestType.DevourAndDisperse))
        {            
            village.ChangeOwner(armies[0].Side);
        }
        else if (remainingAttackers > 0 && MonsterAttacker && spawnerType != Config.MonsterConquestType.DevourAndDisperse)
        {
            if (State.World.GetEmpireOfRace(village.Race)?.IsAlly(armies[0].Empire) ?? false)
                village.ChangeOwner(armies[0].Side);
        }

      
       


        if (MonsterAttacker && remainingAttackers > 0 && village.Empire.IsEnemy(armies[0].Empire))
        {
            if (spawnerType == Config.MonsterConquestType.DevourAndDisperse)
            {
                armies[0].RemainingMP = 1;
                State.GameManager.StrategyMode.Devour(armies[0], Mathf.Min(2 * armies[0].Units.Count, village.Population - 2));
                armies[0].Units = new List<Unit>();
            }
            else if (spawnerType == Config.MonsterConquestType.DevourAndHold)
            {
                if (village.GetTotalPop() > village.Maxpop / 2)
                {
                    armies[0].RemainingMP = 1;
                    State.GameManager.StrategyMode.Devour(armies[0], village.GetTotalPop() - village.Maxpop / 2);
                }
            }
            else //if (Config.MonsterConquest == Config.MonsterConquestType.CompleteDevourAndHold || Config.MonsterConquest == Config.MonsterConquestType.CompleteDevourAndMoveOn)
            {
                if (village.GetTotalPop() > 0)
                {                    
                    armies[0].RemainingMP = 1;
                    if (Config.MonsterConquestTurns > 1)
                    {
                        armies[0].MonsterTurnsRemaining = Config.MonsterConquestTurns;
                    }
                    else
                        State.GameManager.StrategyMode.Devour(armies[0], village.GetTotalPop() / Config.MonsterConquestTurns);
                }

            }


        }

    }


    internal static bool IsUnitControlledByAIEnemy(Unit unit)
    {
        int defenderSide = State.GameManager.TacticalMode.GetDefenderSide();
        bool aiDefender = State.GameManager.TacticalMode.AIDefender;
        bool aiAttacker = State.GameManager.TacticalMode.AIAttacker;
        if (aiAttacker && aiDefender)
            return false;
        return (unit.Side == defenderSide ? aiDefender : aiAttacker);
    }
    internal static bool IsUnitControlledByPlayer(Unit unit)
    {
        int defenderSide = State.GameManager.TacticalMode.GetDefenderSide();
        bool aiDefender = State.GameManager.TacticalMode.AIDefender;
        bool aiAttacker = State.GameManager.TacticalMode.AIAttacker;
        if (aiAttacker && aiDefender)
            return false;
        return (unit.Side == defenderSide ? !aiDefender : !aiAttacker);
    }

    static internal bool AppropriateVoreTarget (Actor_Unit pred, Actor_Unit prey)
    {
        if (pred == prey)
            return false;
        if (pred.Unit.Side == prey.Unit.Side)
        {
            if (prey.Surrendered || pred.Unit.HasTrait(Traits.Cruel) || Config.AllowInfighting || pred.Unit.HasTrait(Traits.Endosoma))
                return true;
            return false;
        }
        return true;
    }

    static public bool OpenTile(Vec2i vec, Actor_Unit actor) => OpenTile(vec.x, vec.y, actor);

    static public bool FreeSpaceAroundTarget(Vec2i targetLocation, Actor_Unit actor)
    {
        for (int x = targetLocation.x - 1; x < targetLocation.x + 2; x++)
        {
            for (int y = targetLocation.y - 1; y < targetLocation.y + 2; y++)
            {
                if (x == targetLocation.x && y == targetLocation.y)
                    continue;
                if (OpenTile(x, y, actor))
                {
                    return true;
                }
            }
        }
        return false;
    }

    static public bool OpenTile(int x, int y, Actor_Unit actor)
    {
        if (x < 0 || y < 0 || x > tiles.GetUpperBound(0) || y > tiles.GetUpperBound(1))
            return false;
        if (blockedTiles != null)
        {
            if (actor?.Unit.HasTrait(Traits.NimbleClimber) ?? false)
            {
                if (x <= blockedClimberTiles.GetUpperBound(0) || y <= blockedClimberTiles.GetUpperBound(1))
                {
                    if (blockedClimberTiles[x, y])
                        return false;
                }
            }
            else
            {
                if (x <= blockedTiles.GetUpperBound(0) || y <= blockedTiles.GetUpperBound(1))
                {
                    if (blockedTiles[x, y])
                        return false;
                }
            }
            
        }
       
        if (TacticalTileInfo.CanWalkInto(tiles[x, y], actor))
        {
            for (int i = 0; i < Units.Count; i++)
            {
                if (Units[i].Targetable == true)
                {
                    if (Units[i].Position.x == x && Units[i].Position.y == y)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        return false;
    }

    static public bool TileContainsMoreThanOneUnit(int x, int y)
    {
        if (x < 0 || y < 0 || x > tiles.GetUpperBound(0) || y > tiles.GetUpperBound(1))
            return false;
        if (Units == null)
        {
            Debug.Log("This shouldn't have happened");
            return false;
        }
        int count = 0;
        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].Targetable == true)
            {
                if (Units[i].Position.x == x && Units[i].Position.y == y)
                {
                    count++;
                }
            }
        }

        return count > 1;
    }


    //public bool IsWalkable(int x, int y, Actor_Unit actor)
    //{
    //    return TacticalTileInfo.CanWalkInto(tiles[x, y], actor);
    //}


    static public bool FlyableTile(int x, int y)
    {
        if (x < 0 || y < 0 || x > tiles.GetUpperBound(0) || y > tiles.GetUpperBound(1))
            return false;
        return true;
    }


    static internal void CheckKnockBack(Actor_Unit Attacker, Actor_Unit Target, ref float damage)
    {
        int xDiff = Target.Position.x - Attacker.Position.x;
        int yDiff = Target.Position.y - Attacker.Position.y;
        int direction = Attacker.DiffToDirection(xDiff, yDiff);
        if (OpenTile(Attacker.GetTile(Target.Position, direction), Target))
            return;
        if (OpenTile(Attacker.GetTile(Target.Position, (direction + 1) % 8), Target))
            return;
        if (OpenTile(Attacker.GetTile(Target.Position, (direction + 7) % 8), Target))
            return;
        damage *= 1.2f;
        return;
    }

    static internal void KnockBack(Actor_Unit Attacker, Actor_Unit Target)
    {
        int xDiff = Target.Position.x - Attacker.Position.x;
        int yDiff = Target.Position.y - Attacker.Position.y;
        int direction = Attacker.DiffToDirection(xDiff, yDiff);

        Target.Movement += 1;
        if (Target.Move(direction, tiles))
            return;
        else if (Target.Move((direction + 1) % 8, tiles))
            return;
        else if (Target.Move((direction + 7) % 8, tiles))
            return;
        Target.Movement -= 1;

        return;
    }

    static internal PredatorComponent GetPredatorComponentOfUnit(Unit unit)
    {
        foreach (Actor_Unit actor in Units)
        {
            if (actor.Unit == unit)
                return actor.PredatorComponent;
        }
        return null;
    }

    static internal Actor_Unit FindPredator(Actor_Unit searcher)
    {
        foreach (Actor_Unit Unit in Units)
        {
            if (Unit.PredatorComponent?.IsActorInPrey(searcher) ?? false)
                return Unit;
        }
        return null;
    }

    static internal void UpdateActorLocations()
    {
        foreach (Actor_Unit unit in Units)
        {
            if (unit.UnitSprite == null)
                continue;
            Vec2i vec = unit.Position;
            Vector2 vector2 = new Vector2(vec.x, vec.y);
            unit.UnitSprite.transform.position = vector2;
        }
    }


    static internal void RefreshUnitGraphicType()
    {
        if (Units == null)
            return;
        foreach (Actor_Unit actor in Units)
        {
            if (actor.Unit.Race != Race.Imps && actor.Unit.Race != Race.Lamia && actor.Unit.Race != Race.Tigers)
            {
                Races.GetRace(actor.Unit).RandomCustom(actor.Unit);
            }
        }
    }

    static internal void UpdateVersion()
    {
        foreach (Actor_Unit actor in Units)
        {
            actor.PredatorComponent?.UpdateVersion();
        }
    }

    static internal List<Actor_Unit> UnitsWithinTiles(Vec2 target, int tiles)
    {
        List<Actor_Unit> unitList = new List<Actor_Unit>();

        foreach (Actor_Unit actor in Units)
        {
            if (actor.Visible && actor.Targetable)
            {
                if (actor.Position.GetNumberOfMovesDistance(target) <= tiles)
                {
                    unitList.Add(actor);
                }
            }            
        }
        return unitList;
    }

    static internal Actor_Unit FindUnitToResurrect(Actor_Unit caster)
    {
        Actor_Unit actor = Units.Where(s => s.Unit.Side == caster.Unit.Side && s.Unit.IsDead && s.Unit.Type != UnitType.Summon).OrderByDescending(s => s.Unit.Experience).FirstOrDefault();
        return actor;
    }
    

    internal static void CreateResurrectionPanel(Vec2i loc, int side)
    {
        int children = UnitPickerUI.ActorFolder.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            UnityEngine.Object.Destroy(UnitPickerUI.ActorFolder.transform.GetChild(i).gameObject);
        }
        Actor_Unit[] list = Units.Where(s => s.Unit.Side == side && s.Unit.IsDead).OrderByDescending(s => s.Unit.Experience).ToArray();
        foreach (Actor_Unit actor in list)
        {
            GameObject obj = UnityEngine.Object.Instantiate(UnitPickerUI.HiringUnitPanel, UnitPickerUI.ActorFolder);
            UIUnitSprite sprite = obj.GetComponentInChildren<UIUnitSprite>();
            Text text = obj.transform.GetChild(3).GetComponent<Text>();
            text.text = $"Level: {actor.Unit.Level} Exp: {(int)actor.Unit.Experience}\n" +
                $"Health : {100 * actor.Unit.HealthPct}%\n" +
                $"Items: {actor.Unit.GetItem(0)?.Name} {actor.Unit.GetItem(1)?.Name}\n" +
                $"Str: {actor.Unit.GetStatBase(Stat.Strength)} Dex: { actor.Unit.GetStatBase(Stat.Dexterity)} Agility: {actor.Unit.GetStatBase(Stat.Agility)}\n" +
                $"Mind: {actor.Unit.GetStatBase(Stat.Mind)} Will: { actor.Unit.GetStatBase(Stat.Will)} Endurance: {actor.Unit.GetStatBase(Stat.Endurance)}\n";
            if (actor.PredatorComponent != null)
                text.text += $"Vore: {actor.Unit.GetStatBase(Stat.Voracity)} Stomach: { actor.Unit.GetStatBase(Stat.Stomach)}";
            actor.UpdateBestWeapons();
            sprite.UpdateSprites(actor);
            sprite.Name.text = actor.Unit.Name;
            Button button = obj.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => Resurrect(loc, actor));
            button.onClick.AddListener(() => UnitPickerUI.gameObject.SetActive(false));
        }
        UnitPickerUI.ActorFolder.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 250 * (1 + (list.Length / 3)));
        UnitPickerUI.gameObject.SetActive(true);
    }

    internal static void Resurrect(Vec2i loc, Actor_Unit target)
    {
        var pred = FindPredator(target);
        if (pred != null)
            pred.PredatorComponent.FreeUnit(target, true);
        target.Position.x = loc.x;
        target.Position.y = loc.y;
        target.Unit.Health = target.Unit.MaxHealth * 3 / 4;
        target.Visible = true;
        target.Targetable = true;
        target.SelfPrey = null;
        target.Surrendered = false;
        UpdateActorLocations();
        if (target.UnitSprite != null)
        {
            target.UnitSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            target.UnitSprite.LevelText.gameObject.SetActive(true);
            target.UnitSprite.FlexibleSquare.gameObject.SetActive(true);
            target.UnitSprite.HealthBar.gameObject.SetActive(true);
        }

       
    }

    static internal bool MeetsQualifier(List<AbilityTargets> targets, Actor_Unit actor, Actor_Unit target)
    {
        if (actor.Unit.Side != target.Unit.Side && targets.Contains(AbilityTargets.Enemy))
            return true;
        if (actor.Unit.Side == target.Unit.Side && targets.Contains(AbilityTargets.Ally))
            return true;
        if (actor.Unit.Side == target.Unit.Side && target.Surrendered && targets.Contains(AbilityTargets.SurrenderedAlly))
            return true;
        if (actor.Unit.Side == target.Unit.Side && targets.Contains(AbilityTargets.Enemy) && Config.AllowInfighting)
            return true;
        return false;

    }
    
    static internal Actor_Unit GetActorAt(Vec2 location)
    {
        foreach (Actor_Unit actor in Units)
        {
            if (actor == null)
                continue;
            if (actor.Position.x == location.x && actor.Position.y == location.y)
                return actor;
        }
        return null;
    }

    static internal void CreateEffect(Vec2i location, TileEffectType type, int areaOfEffect, float strength, int duration)
    {
        for (int x = location.x - areaOfEffect; x <= location.x + areaOfEffect; x++)
        {
            for (int y = location.y - areaOfEffect; y <= location.y + areaOfEffect; y++)
            {
                if (x < 0 || y < 0 || x > tiles.GetUpperBound(0) || y > tiles.GetUpperBound(1))
                    continue;
                Vec2 position = new Vec2(x, y);
                TileEffect effect = new TileEffect(duration, strength, type);
                State.GameManager.TacticalMode.ActiveEffects[position] = effect;
                switch (type)
                {
                    case TileEffectType.Fire:
                        State.GameManager.TacticalMode.EffectTileMap.SetTile(new Vector3Int(position.x, position.y, 0), State.GameManager.TacticalMode.Pyre);
                        break;
                    case TileEffectType.IcePatch:
                        State.GameManager.TacticalMode.EffectTileMap.SetTile(new Vector3Int(position.x, position.y, 0), State.GameManager.TacticalMode.Ice);
                        break;
                }
            }
        }

    }




}
