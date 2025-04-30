using OdinSerializer;
using System;
using System.Collections.Generic;
using static UnityEngine.UI.CanvasScaler;

public static class EquipmentFunctions
{
    public static void CheckEquipment(Unit unit, EquipmentActivator activator, object[] args)
    {
        foreach (var item in unit.Items)
        {
            if (item is Equipment)
            {
                Equipment equipment = item as Equipment;
                if (equipment.EquipmentFunction.ContainsKey(activator))
                {
                    equipment.EquipmentFunction[activator].Invoke(args[0], args[1], args[2]);
                }
            }
        }
    }
}
