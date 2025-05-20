using OdinSerializer;
using System;
using System.Collections.Generic;

public class Potion: Item
{
    [OdinSerialize]
    internal Action<object, object> PotionFunction; //Target, Unit
    [OdinSerialize]
    internal int Tier { get; private set; }
    [OdinSerialize]
    internal bool NegativeEffect;
    [OdinSerialize]
    internal int ItemUses;

    public Potion(string name, string description, int cost, int tier, bool negative, Action<object, object> func)
    {
        Name = name;
        Description = description;
        Cost = cost;
        Tier = tier;
        NegativeEffect = negative;
        PotionFunction = func;
    }
}
