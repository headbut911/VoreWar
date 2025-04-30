using OdinSerializer;
using System;
using System.Collections.Generic;

public enum EquipmentActivator
{
    OnTacticalBattleStart, // Unit, Army, null
    OnTacticalBattleEnd, // Unit, Army, null
    OnTacticalTurnStart, // Unit, army, null
    OnStrategicTurnStart, // Unit, army, null
    OnMeleeAttack, // Unit, target, damage
    OnMeleeHit, // Unit, target, damage
    OnMeleeMiss, // Unit, target, damage
    WhenMeleeAttacked, // Unit, attacker, damage
    WhenMeleeHit, // Unit, attacker, damage
    WhenMeleeMissed, // Unit, attacker, damage
    OnRangedAttack, // Unit, target, damage
    OnRangedHit, // Unit, target, damage
    OnRangedMiss, // Unit, target, damage
    WhenRangedAttacked, // Unit, attacker, damage
    WhenRangedHit, // Unit, attacker, damage
    WhenRangedMissed, // Unit, attacker, damage
    OnSpellCast, // Unit, Target (Can be Null), damage
    WhenTargetedBySpellDamage, // Unit, attacker, damage
    WhenMissedBySpellDamage,  // Unit, attacker, damage
    WhenHitBySpellDamage,  // Unit, attacker, damage
    WhenTargetedBySpellStatus,  // Unit, attacker, spell
    WhenMissedBySpellStatus, // Unit, attacker, spell
    WhenHitBySpellStatus, // Unit, attacker, spell
}

public class Equipment : Item
{
    [OdinSerialize]
    internal Dictionary<EquipmentActivator, Action<object, object, object>> EquipmentFunction;

    public Equipment(string name, string description, int cost, Dictionary<EquipmentActivator, Action<object, object, object>> func)
    {
        Name = name;
        Description = description;
        Cost = cost;
        EquipmentFunction = func;
    }
}
