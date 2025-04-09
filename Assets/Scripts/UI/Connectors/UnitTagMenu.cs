using CruxClothing;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public enum UnitTagModifierEffect
{    
    None,
    ChanceToEscapeMult,
    MeleeDamageMult,
    RangedDamageMult,
    MagicDamageMult,
    DigestionRateMult,
    AbsorptionRateMult,
    NutritionMult,
    ManaAbsorbHundreths,
    MeleeShift,
    RangedShift,
    MagicShift,
    VoreOddsMult,
    GrowthRateMult,
    CritRateShift,
    CritDamageMult,
    GrazeRateShift,
    GrazeDamageMult,
}
public enum UnitTagModifierCase
{
    None,
    AgainstTargetIf,
    AgainstTargetIfNot,
    AgainstAnyTarget, // ignores target
    FromTargetIf,
    FromTargetIfNot,
    FromAnyTarget, // ignores target
}
public enum UnitTagModifierTarget
{
    All,
    Race,
    Trait,
    Tag,
    Gender,
    StatusEffect,
    Enemy,
    Ally,
    HasRanged,
    HasMelee,
    HasMagicBook,
    EmpireRace,
    MercenaryRace,
    MonsterRace,
    SpecialMercenaryRace,
    Predator,
    Full,
    Digesting,
    Absorbing,
    GreaterLevel,
    GreaterBodySize,
}

public class UnitTagMenu : MonoBehaviour
{
    int current_id;
    UnitTag unitTag;
    public UnitTagEditor UnitTagEditor;
    public Transform Folder;

    public InputField name;

    public Button SelectTraitsButton;

    public Button AddBtn;
    internal Button newModButton;

    public UnitTagModifierItem ModifierPrefab;
    internal List<UnitTagModifierItem> ModifierList;

    internal void Open(int id)
    {
        gameObject.SetActive(true);
        UnitTagEditor.gameObject.SetActive(false);
        current_id = id;
        ModifierList = new List<UnitTagModifierItem>();
        unitTag = State.UnitTagList.Where(x => x.id == current_id).FirstOrDefault();
        LoadTag();
        CreateAddButton();

        SelectTraitsButton.onClick.AddListener(() =>
        {
            State.GameManager.VariableEditor.Open(unitTag);
        });
    }

    private void CreateAddButton()
    {
        if (newModButton != null)
        {
            newModButton.onClick.RemoveAllListeners();
            Destroy(newModButton.gameObject);
        }
        newModButton = Instantiate(AddBtn, Folder);
        var btn = newModButton.GetComponent<Button>();
        var btnTxt = btn.GetComponentInChildren<Text>();
        btnTxt.text = "Add";
        btn.onClick.AddListener(() =>
        {
            var created = CreateModifierItem();
            ModifierList.Add(created);
            CreateAddButton();
        });
    }
    private UnitTagModifierItem CreateModifierItem()
    {
        var obj = Instantiate(ModifierPrefab, Folder);
        var ob = obj.GetComponent<UnitTagModifierItem>();

        // Populate effect dropdown
        ob.tagEffect.options.Clear();
        foreach (UnitTagModifierEffect tagEff in ((UnitTagModifierEffect[])Enum.GetValues(typeof(UnitTagModifierEffect))))
        {
            ob.tagEffect.options.Add(new TMP_Dropdown.OptionData(tagEff.ToString()));
        }
        ob.tagEffect.RefreshShownValue();

        // Populate case dropdown
        ob.tagCase.options.Clear();
        foreach (UnitTagModifierCase tagCase in ((UnitTagModifierCase[])Enum.GetValues(typeof(UnitTagModifierCase))))
        {
            ob.tagCase.options.Add(new TMP_Dropdown.OptionData(tagCase.ToString()));
        }
        ob.tagCase.RefreshShownValue();

        // Populate target dropdown
        ob.tagTarget.options.Clear();
        foreach (UnitTagModifierTarget tagTarget in ((UnitTagModifierTarget[])Enum.GetValues(typeof(UnitTagModifierTarget))))
        {
            ob.tagTarget.options.Add(new TMP_Dropdown.OptionData(tagTarget.ToString()));
        }
        ob.tagTarget.RefreshShownValue();

        ob.duplicateButton.onClick.AddListenerOnce(() =>
        {
            UnitTagModifierItem dupe = CreateModifierItem();
            dupe.tagEffect.value = ob.tagEffect.value;
            dupe.tagCase.value = ob.tagCase.value;
            dupe.tagTarget.value = ob.tagTarget.value;
            dupe.targetValue.value = ob.targetValue.value;
        });
        ob.DeleteButton.onClick.AddListenerOnce(() =>
        {
            ModifierList.Remove(ob);
            Destroy(ob);
        });

        ob.tagTarget.onValueChanged.AddListener((int val) =>
        {
            UnitTagModifierTarget targetType = (UnitTagModifierTarget)val;
            switch (targetType)
            {
                case UnitTagModifierTarget.Race:
                    ob.targetValue.gameObject.SetActive(true);
                    // Populate target race dropdown
                    ob.targetValue.options.Clear();
                    foreach (Race targetRace in ((Race[])Enum.GetValues(typeof(Race))))
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(targetRace.ToString()));
                    }
                    ob.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.Trait:
                    ob.targetValue.gameObject.SetActive(true);
                    // Populate Trait Dropdown
                    ob.targetValue.options.Clear();
                    foreach (RandomizeList rl in State.RandomizeLists)
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(rl.name.ToString()));
                    }
                    foreach (CustomTraitBoost ct in State.CustomTraitList)
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(ct.name.ToString()));
                    }
                    foreach (ConditionalTraitContainer cdt in State.ConditionalTraitList)
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(cdt.name.ToString()));
                    }
                    foreach (Traits traitId in ((Traits[])Enum.GetValues(typeof(Traits))).OrderBy(s =>
                    {
                        return s >= Traits.LightningSpeed ? "ZZZ" + s.ToString() : s.ToString();
                    }))
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(traitId.ToString()));
                    }
                    ob.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.Tag:
                    ob.targetValue.gameObject.SetActive(true);
                    // Populate target tag dropdown
                    ob.targetValue.options.Clear();
                    foreach (UnitTag uTag in State.UnitTagList)
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(uTag.name.ToString()));
                    }
                    ob.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.Gender:
                    ob.targetValue.gameObject.SetActive(true);
                    // Populate target gender dropdown
                    ob.targetValue.options.Clear();
                    foreach (Gender targetGender in ((Gender[])Enum.GetValues(typeof(Gender))))
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(targetGender.ToString()));
                    }
                    ob.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.StatusEffect:
                    ob.targetValue.gameObject.SetActive(true);
                    // Populate target status effect dropdown
                    ob.targetValue.options.Clear();
                    foreach (StatusEffectType statusEffectType in ((StatusEffectType[])Enum.GetValues(typeof(StatusEffectType))))
                    {
                        ob.targetValue.options.Add(new TMP_Dropdown.OptionData(statusEffectType.ToString()));
                    }
                    ob.targetValue.RefreshShownValue();
                    break;
                default:
                    ob.targetValue.gameObject.SetActive(false);
                    ob.targetValue.options.Clear();
                    break;
            }
        });
        
        ob.tagCase.onValueChanged.AddListener((int val) =>
        {
            if (val == (int)UnitTagModifierCase.AgainstAnyTarget || val == (int)UnitTagModifierCase.FromAnyTarget)
            {
                ob.tagTarget.gameObject.SetActive(false);
                ob.targetValue.gameObject.SetActive(false);
            }
            else
            {
                ob.tagTarget.gameObject.SetActive(true);
                ob.targetValue.gameObject.SetActive(ob.targetValue.IsActive());
            }
        });
        ob.targetValue.gameObject.SetActive(false);
        return ob;
    }


    public void LoadTag()
    {
        name.text = unitTag.name;
        foreach (var item in unitTag.modifiers)
        {
            UnitTagModifierItem loadMod = CreateModifierItem();
            loadMod.tagEffect.value = (int)item.tagEffect;
            loadMod.tagCase.value = (int)item.tagCase;
            loadMod.tagTarget.value = (int)item.target;
            switch (item.target)
            {
                case UnitTagModifierTarget.Race:
                    loadMod.targetValue.gameObject.SetActive(true);
                    // Populate target race dropdown
                    loadMod.targetValue.options.Clear();
                    foreach (Race targetRace in ((Race[])Enum.GetValues(typeof(Race))))
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(targetRace.ToString()));
                    }
                    loadMod.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.Trait:
                    loadMod.targetValue.gameObject.SetActive(true);
                    // Populate Trait Dropdown
                    loadMod.targetValue.options.Clear();
                    foreach (RandomizeList rl in State.RandomizeLists)
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(rl.name.ToString()));
                    }
                    foreach (CustomTraitBoost ct in State.CustomTraitList)
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(ct.name.ToString()));
                    }
                    foreach (ConditionalTraitContainer cdt in State.ConditionalTraitList)
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(cdt.name.ToString()));
                    }
                    foreach (Traits traitId in ((Traits[])Enum.GetValues(typeof(Traits))).OrderBy(s =>
                    {
                        return s >= Traits.LightningSpeed ? "ZZZ" + s.ToString() : s.ToString();
                    }))
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(traitId.ToString()));
                    }
                    loadMod.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.Tag:
                    loadMod.targetValue.gameObject.SetActive(true);
                    // Populate target tag dropdown
                    loadMod.targetValue.options.Clear();
                    foreach (UnitTag uTag in State.UnitTagList)
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(uTag.name.ToString()));
                    }
                    loadMod.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.Gender:
                    loadMod.targetValue.gameObject.SetActive(true);
                    // Populate target gender dropdown
                    loadMod.targetValue.options.Clear();
                    foreach (Gender targetGender in ((Gender[])Enum.GetValues(typeof(Gender))))
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(targetGender.ToString()));
                    }
                    loadMod.targetValue.RefreshShownValue();
                    break;
                case UnitTagModifierTarget.StatusEffect:
                    loadMod.targetValue.gameObject.SetActive(true);
                    // Populate target status effect dropdown
                    loadMod.targetValue.options.Clear();
                    foreach (StatusEffectType statusEffectType in ((StatusEffectType[])Enum.GetValues(typeof(StatusEffectType))))
                    {
                        loadMod.targetValue.options.Add(new TMP_Dropdown.OptionData(statusEffectType.ToString()));
                    }
                    loadMod.targetValue.RefreshShownValue();
                    break;
                default:
                    loadMod.targetValue.gameObject.SetActive(false);
                    loadMod.targetValue.options.Clear();
                    break;
            }
            loadMod.targetValue.value = item.targetValue;
            loadMod.modifierValue.text = item.modifierValue.ToString();
        }
    }

    public void SaveTag()
    {
        unitTag.name = name.text;

        foreach (var mod in ModifierList)
        {
            UnitTagModifier newMod = new UnitTagModifier();
            newMod.id = unitTag.modifiers.Count();
            newMod.modifierValue = float.TryParse(mod.modifierValue.text, out float mv) ? mv : 1;
            newMod.target = (UnitTagModifierTarget)mod.tagTarget.value;
            switch (newMod.target)
            {
                case UnitTagModifierTarget.Race:
                    if (Enum.TryParse(mod.targetValue.options[mod.targetValue.value].text, out Race race))
                    {
                        newMod.targetValue = (int)race;
                    }
                    break;
                case UnitTagModifierTarget.Trait:
                    if (State.RandomizeLists.Any(rl => rl.name == mod.targetValue.options[mod.targetValue.value].text))
                    {
                        newMod.targetValue = (int)State.RandomizeLists.Where(rl => rl.name == mod.targetValue.options[mod.targetValue.value].text).FirstOrDefault()?.id;
                    }
                    if (State.CustomTraitList.Any(ct => ct.name == mod.targetValue.options[mod.targetValue.value].text))
                    {
                        newMod.targetValue = (int)State.CustomTraitList.Where(ct => ct.name == mod.targetValue.options[mod.targetValue.value].text).FirstOrDefault()?.id;
                    }
                    if (State.ConditionalTraitList.Any(cdt => cdt.name == mod.targetValue.options[mod.targetValue.value].text))
                    {
                        newMod.targetValue = (int)State.ConditionalTraitList.Where(cdt => cdt.name == mod.targetValue.options[mod.targetValue.value].text).FirstOrDefault()?.id;
                    }
                    if (Enum.TryParse(mod.targetValue.options[mod.targetValue.value].text, out Traits trait))
                    {
                        newMod.targetValue = (int)trait;
                    }
                    break;
                case UnitTagModifierTarget.Tag:
                    newMod.targetValue = mod.targetValue.value;
                    break;
                case UnitTagModifierTarget.Gender:
                    if (Enum.TryParse(mod.targetValue.options[mod.targetValue.value].text, out Gender gender))
                    {
                        newMod.targetValue = (int)gender;
                    }
                    break;
                case UnitTagModifierTarget.StatusEffect:
                    if (Enum.TryParse(mod.targetValue.options[mod.targetValue.value].text, out StatusEffectType statusEffect))
                    {
                        newMod.targetValue = (int)statusEffect;
                    }
                    break;
                default:
                    break;
            }
            unitTag.modifiers.Add(newMod);
        }
        ExternalTraitHandler.UnitTagSaver(unitTag);

    }

    public void SaveClose()
    {
        SaveTag();
        DiscardClose();
    }

    public void DiscardClose()
    {
        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }
        gameObject.SetActive(false);
        UnitTagEditor.Open();
    }

    public void Remove()
    {
        var rem = State.UnitTagList.Where(x => current_id == x.id).FirstOrDefault();
        ExternalTraitHandler.UnitTagRemover(rem);
        if (State.UnitTagList.Contains(rem))
        {
            State.UnitTagList.Remove(rem);  
        }
        DiscardClose();        
    }
}