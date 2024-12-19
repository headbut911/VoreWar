using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class CustomTraitBoost
{
    public int id = -1;
    public string name;
    public string description;
    public List<string> tags;
    public TraitTier tier;
    public Dictionary<CustomTraitComp,float> comps;
}