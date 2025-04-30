using OdinSerializer;
using System;
using System.Collections.Generic;

public enum EquipmentActivator
{
    OnTacticalBattleStart,
    OnTacticalBattleEnd,
    OnTacticalTurnStart,
    OnStrategicTurnStart,
    OnAttack,
    OnHit,
    OnMiss,
    WhenAttacked,
    WhenHit,
    WhenMissed,
}

public class Equipment : Item
{
    [OdinSerialize]
    internal Action<object, object, object> EquipmentFunction; // Owner, Target
    [OdinSerialize]
    internal List<EquipmentActivator> Activators;

    public Equipment(string name, string description, int cost, Action<object, object, object> func, List<EquipmentActivator> activators)
    {
        Name = name;
        Description = description;
        Cost = cost;
        EquipmentFunction = func;
        Activators = activators;
    }
}
