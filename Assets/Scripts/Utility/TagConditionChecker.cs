using System.Collections.Generic;
using System.Linq;

public class TagConditionChecker
{
    public static void CompileTraitTagAssociateDict()
    {
        Dictionary<Traits, List<int>> tagTraitRelation = new Dictionary<Traits, List<int>>();
        foreach (UnitTag unitTag in State.UnitTagList)
        {
            foreach (Traits trait in unitTag.AssociatedTraits)
            {
                if (tagTraitRelation.ContainsKey(trait))
                {
                    if (!tagTraitRelation[trait].Contains(unitTag.id))
                        tagTraitRelation[trait].Add(unitTag.id);
                }
                else 
                {
                    tagTraitRelation.Add(trait, new List<int>());
                    tagTraitRelation[trait].Add(unitTag.id);
                }
            }
        }
    }

    public static bool IsTagActive(Unit owner, Unit target, UnitTagModifierEffect modifierEffect) 
    {
        return true;
    }
}