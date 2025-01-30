using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEditorInternal.VR;
using UnityEngine;

public class ConditionalTraitConditionChecker
{
    public static bool TraitConditionActive(Actor_Unit unit, ConditionalTraitContainer conditionalTrait)
    {
        bool condition = false;
        TraitConditionLogicalOperator op = TraitConditionLogicalOperator.none;
        foreach (var item in conditionalTrait.OperationBlocks)
        {
            switch (op)
            {
                case TraitConditionLogicalOperator.and:
                    condition = condition && CheckCondition(unit, item.Key);
                    break;
                case TraitConditionLogicalOperator.or:
                    condition = condition || CheckCondition(unit, item.Key);
                    break;
                case TraitConditionLogicalOperator.not:
                    condition = condition && !CheckCondition(unit, item.Key);
                    break;
                default:
                    condition = CheckCondition(unit, item.Key);
                    break;
            }            
            op = item.Value;
            if (op == TraitConditionLogicalOperator.none)
            {
                return condition;

            }
        }
        return condition;
    }

    public static bool CheckCondition(Actor_Unit unit, ConditionalTraitOperationBlock opBlock)
    {
        TraitCondition leadCondition = opBlock.conditionVariable.First();
        if (leadCondition >= TraitCondition.Male)
        {
            switch (leadCondition)
            {
                case TraitCondition.Male:
                    if (unit.Unit.GetGender() == Gender.Male || unit.Unit.GetGender() == Gender.Gynomorph)
                        return true;
                    break;
                case TraitCondition.Female:
                    if (unit.Unit.GetGender() == Gender.Female || unit.Unit.GetGender() == Gender.Andromorph)
                        return true;
                    break;
                case TraitCondition.NonBinary:
                    if (unit.Unit.GetGender() == Gender.Male || unit.Unit.GetGender() == Gender.Gynomorph || unit.Unit.GetGender() == Gender.Female || unit.Unit.GetGender() == Gender.Andromorph)
                        return false;
                    return true;
                case TraitCondition.Predator:
                    if (unit.PredatorComponent.PreyCount >= 1)
                        return true;
                    break;
                case TraitCondition.Digesting:
                    if (unit.PredatorComponent.AlivePrey >= 1)
                        return true;
                    break;
                case TraitCondition.Absorbing:
                    if (unit.PredatorComponent.PreyCount > unit.PredatorComponent.AlivePrey)
                        return true;
                    break;
                case TraitCondition.Prey:
                    if (unit.SelfPrey != null)
                        return true;
                    break;
                default:
                    break;
            }
            return false;
        }

        float total = 0;
        int counter = 0;
        foreach (var cond in opBlock.conditionVariable)
        {
            float value = 0;
            switch (cond)
            {
                case TraitCondition.Level:
                    value = unit.Unit.Level;
                    break;
                case TraitCondition.Health:
                    value = unit.Unit.Health;
                    break;
                case TraitCondition.HealthPercent:
                    value = unit.Unit.HealthPct;
                    break;
                case TraitCondition.Mana:
                    value = unit.Unit.Mana;
                    break;
                case TraitCondition.ManaPercent:
                    value = unit.Unit.ManaPct;
                    break;
                case TraitCondition.Strength:
                    value = unit.Unit.GetStat(Stat.Strength);
                    break;
                case TraitCondition.Dexterity:
                    value = unit.Unit.GetStat(Stat.Dexterity);
                    break;
                case TraitCondition.Endurance:
                    value = unit.Unit.GetStat(Stat.Endurance);
                    break;
                case TraitCondition.Mind:
                    value = unit.Unit.GetStat(Stat.Mind);
                    break;
                case TraitCondition.Will:
                    value = unit.Unit.GetStat(Stat.Will);
                    break;
                case TraitCondition.Agility:
                    value = unit.Unit.GetStat(Stat.Agility);
                    break;
                case TraitCondition.Voracity:
                    value = unit.Unit.GetStat(Stat.Voracity);
                    break;
                case TraitCondition.Stomach:
                    value = unit.Unit.GetStat(Stat.Stomach);
                    break;
                case TraitCondition.StatTotal:
                    value = unit.Unit.GetStatTotal();
                    break;
                case TraitCondition.TotalDigestions:
                    value = unit.Unit.DigestedUnits;
                    break;
                case TraitCondition.TotalKills:
                    value = unit.Unit.KilledUnits;
                    break;
                case TraitCondition.TimesKilled:
                    value = unit.Unit.TimesKilled;
                    break;
                case TraitCondition.TacticalTurn:
                    value = State.GameManager.TacticalMode.currentTurn;
                    break;
                case TraitCondition.StrategicTurn:
                    value = State.World.Turn;
                    break;
                case TraitCondition.AliveAllies:
                    value = State.GameManager.TacticalMode.ExposeRemainingUnits(unit.Unit.GetApparentSide() == State.GameManager.TacticalMode.GetDefenderSide());
                    break;
                case TraitCondition.AliveEnemies:
                    value = State.GameManager.TacticalMode.ExposeRemainingUnits(!(unit.Unit.GetApparentSide() == State.GameManager.TacticalMode.GetDefenderSide()));
                    break;
                case TraitCondition.Growth:
                    value = (float)unit.Unit.BaseScale;
                    break;
                case TraitCondition.Fullness:
                    value = unit.PredatorComponent.Fullness;
                    break;
                default:
                    break;
            }

            if (counter <= 0)
            {
                counter++;
                total += value;
                continue;
            }

            switch (opBlock.arithmeticOperator[counter-1])
            {
                case TraitConditionArithmeticOperator.none:
                    break;
                case TraitConditionArithmeticOperator.add:
                    total += value;
                    break;
                case TraitConditionArithmeticOperator.sub:
                    total -= value;
                    break;
                case TraitConditionArithmeticOperator.mul:
                    total *= value;
                    break;
                case TraitConditionArithmeticOperator.div:
                    total /= value;
                    break;
                default:
                    break;
            }
        }

        switch (opBlock.compareOp)
        {
            case TraitConditionCompareOperator.eq:
                return total == opBlock.compareValue;
            case TraitConditionCompareOperator.geq:
                return total >= opBlock.compareValue;
            case TraitConditionCompareOperator.leq:
                return total <= opBlock.compareValue;
            case TraitConditionCompareOperator.none:
                break;
            default:
                return false;
        }
        return false;
    }
}