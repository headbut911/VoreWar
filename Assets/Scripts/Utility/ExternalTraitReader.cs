using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public enum TraitTier
{
    Harmful,
    Negative,
    Strange,
    Common,
    Uncommon,
    Rare,
    Epic,
    Elite,
    Legendary,
    Cheat,
    Hidden,
}

public class TaggedTrait
{
    internal string name;
    internal string tier;
    internal TraitTier tierValue;
    internal List<string> tags;
    internal Traits traitEnum;
}

public class ExternalTraitReader
{
    public static Dictionary<Traits, TaggedTrait> TaggedTraitParser()
    {
        string readContents;
        FileStream traitJson = File.OpenRead(State.StorageDirectory + "\\taggedTraits.json");
        using (StreamReader streamReader = new StreamReader(traitJson))
        {
            readContents = streamReader.ReadToEnd();
        }
        Dictionary<Traits, TaggedTrait> taggedTraitsList = new Dictionary<Traits, TaggedTrait>();
        JObject results = JObject.Parse(readContents);
        IList<JToken> traitList = results["traits"].Children().ToList();

        foreach (JToken t in traitList) {
            TaggedTrait trait = new TaggedTrait();
            trait.name = t["name"].ToString();
            trait.tier = t["tier"].ToString();
            trait.tags = t["tags"].ToObject<List<string>>();
            trait.tierValue = (TraitTier)Enum.Parse(typeof(TraitTier), trait.tier);
            trait.traitEnum = (Traits)Enum.Parse(typeof(Traits), trait.name);
            taggedTraitsList.Add(trait.traitEnum, trait);
            //Debug.Log(taggedTraitsList.Count);
        }
        return taggedTraitsList;
    }
    public static List<CustomTraitBoost> CustomTraitParser()
    {
        string readContents;
        FileStream traitJson = File.OpenRead(State.StorageDirectory + "\\userTraits.json");
        using (StreamReader streamReader = new StreamReader(traitJson))
        {
            readContents = streamReader.ReadToEnd();
        }
        List<CustomTraitBoost> customTraitsList = new List<CustomTraitBoost>();
        JObject results = JObject.Parse(readContents);
        return customTraitsList;
    }
}
