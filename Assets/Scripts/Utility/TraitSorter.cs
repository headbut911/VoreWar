using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public enum TraitTier
{
    Harmful = -2,
    Negative = -1,
    Strange = 0,
    Common = 1,
    Uncommon = 2,
    Rare = 3,
    Epic = 4,
    Elite = 5,
    Legendary = 6,
    Cheat = 7,
    Hidden = 8
}

public class TaggedTrait
{
    internal string name;
    internal string tier;
    internal TraitTier tierValue;
    internal List<string> tags;
    internal Traits traitEnum;
}

public class TraitSorter
{
    public static List<TaggedTrait> TraitParser()
    {
        string readContents;
        FileStream traitJson = File.OpenRead(State.StorageDirectory + "\\taggedTraits.json");
        using (StreamReader streamReader = new StreamReader(traitJson))
        {
            readContents = streamReader.ReadToEnd();
        }
        List<TaggedTrait> taggedTraitsList = new List<TaggedTrait>();
        JObject results = JObject.Parse(readContents);
        IList<JToken> traitList = results["traits"].Children().ToList();

        foreach (JToken t in traitList) {
            TaggedTrait trait = new TaggedTrait();
            trait.name = t["name"].ToString();
            trait.tier = t["tier"].ToString();
            trait.tags = t["tags"].ToObject<List<string>>();
            trait.tierValue = (TraitTier)Enum.Parse(typeof(TraitTier), trait.tier);
            trait.traitEnum = (Traits)Enum.Parse(typeof(Traits), trait.name);
            taggedTraitsList = taggedTraitsList.Append(trait).ToList();
            //Debug.Log(taggedTraitsList.Count);
        }
        return taggedTraitsList;
    }
}
