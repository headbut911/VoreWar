using OdinSerializer;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public static class EquipmentFunctions
{
    public static void CheckEquipment(Unit unit, EquipmentActivator activator, object[] args)
    {
        for (int i = 0; i < unit.Items.Length; i++)
        {
            Item item = unit.Items[i];
            if (item is Equipment)
            {
                Equipment equipment = item as Equipment;
                if (equipment.EquipmentFunction.ContainsKey(activator))
                {
                    equipment.InvokeFunction(activator, args, unit, i);
                }
            }
        }
    }

    public static void TickCoolDown(Unit unit, EquipmentType type, bool reset = false)
    {
        for (int i = 0; i < unit.Items.Length; i++)
        {
            Item item = unit.Items[i];
            if (item is Equipment)
            {
                Equipment equipment = item as Equipment;
                if (equipment.useType >= type && equipment.useType < type + 10)
                {
                    if (unit.ItemCooldowns[i] > 0)
                    {
                        unit.ItemCooldowns[i] = reset ? 0 : unit.ItemCooldowns[i] - 1;

                        if (unit.ItemCooldowns[i] <= 0)
                            unit.ItemCooldowns[i] = 0;

                        if (unit.ItemCooldowns[i] <= 0)
                        {
                            unit.ItemUses[i] = equipment.ItemUses;
                        }
                    }
                }
            }
        }
    }

    public static int GetEquipmentSlot(Unit unit, ItemType type)
    {
        for (int i = 0; i < unit.Items.Length; i++)
        {
            if (unit.Items[i] == null)
                continue;
            if (State.World.ItemRepository.GetItemType(unit.Items[i]) == type)
                return i;
        }
        return -1;
    }

    internal static void UseEquipmentHealthPotion(Unit unit)
    {
        if (unit.HealthPct <= 0.5f)
        {
            unit.Heal(20);
        }
    }   
    internal static void UseEquipmentManaPotion(Unit unit)
    {
        if (unit.HealthPct <= 0.5f)
        {
            unit.Heal(20);
            unit.SetItem(null, GetEquipmentSlot(unit, ItemType.HealthPotion));            
        }
    }
}
