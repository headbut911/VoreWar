using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

public enum TraitTier
{
    Harmful,
    Negative,
    Neutral,
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
    public static void CustomTraitParser()
    {
        string readContents;
        string[] files = Directory.GetFiles(State.CustomTraitDirectory, "*.json", SearchOption.AllDirectories);
        List<CustomTraitBoost> customTraitsList = new List<CustomTraitBoost>();

        foreach (string file in files) 
        {

        }
        State.CustomTraitList = customTraitsList;



    }
    public static bool CustomTraitSaver(CustomTraitBoost trait)
    {
        bool modifying = false;
        foreach (var item in State.CustomTraitList)
        {
            if (item.name.ToLower() == trait.name.ToLower() && item.id == trait.id)
            {
                modifying = true;
                break;
            }
        }

        if (!modifying && File.Exists($"{State.CustomTraitDirectory}{trait.name}.json")) 
        { 
            return false;
        }

        using (StreamWriter sw = new StreamWriter($"{State.CustomTraitDirectory}{trait.name}.json"))
        {
            sw.WriteLine(JsonConvert.SerializeObject(trait));

        }
        return true;
    }
}
