using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;

public class LaboratoryPotion
{
    [OdinSerialize]
    public List<Traits> PositiveTraits;
    [OdinSerialize]
    public List<Traits> NegativeTraits;
    [OdinSerialize]
    public Dictionary<Stat, int> StatModifiers;

    public LaboratoryPotion()
    {
        PositiveTraits = new List<Traits>();
        NegativeTraits = new List<Traits>();
        StatModifiers = new Dictionary<Stat, int>();
    }

    internal void AddPositiveTrait(Traits trait)
    {
        if (!PositiveTraits.Contains(trait))
        {
            PositiveTraits.Add(trait);
        }
    }
    internal void AddNegativeTrait(Traits trait)
    {
        if (!NegativeTraits.Contains(trait))
        {
            NegativeTraits.Add(trait);
        }
    }
    internal void AddStatMod(Stat stat, int mod)
    {
        if (StatModifiers.Keys.Contains(stat))
            StatModifiers[stat] = StatModifiers[stat] + mod;
        else
            StatModifiers.Add(stat, mod);

    }
}

