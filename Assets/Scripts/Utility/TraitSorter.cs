using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static UnityEngine.EventSystems.EventTrigger;

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
public class TaggedTraitsList
{
    internal List<TaggedTrait> traits;
    internal List<string> tags;
}

public class TaggedTrait
{
    internal string name;
    internal TraitTier tier;
    internal string[] tags;
}

public class TraitSorter
{
    public static TaggedTraitsList TraitParser()
    {
        string readContents;
        using (StreamReader streamReader = new StreamReader(State.StorageDirectory, Encoding.UTF8))
        {
            readContents = streamReader.ReadToEnd();
        }
        return JsonConvert.DeserializeObject<TaggedTraitsList>(readContents);
    }
}
