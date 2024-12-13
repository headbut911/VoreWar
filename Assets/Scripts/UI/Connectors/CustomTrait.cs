using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class CustomTrait : MonoBehaviour
{
    int current_id;
    public CustomTraitEditor CustomTraitEditor;
    public InputField name;
    public InputField description;
    public TMP_Dropdown tier;
    
    public TMP_InputField ExpRequired;
    public TMP_InputField ExpGain;
    public TMP_InputField ExpGainFromVore;
    public TMP_InputField ExpGainFromAbsorption;
    public TMP_InputField PassiveHeal;
    public TMP_InputField CapacityMult;
    public TMP_InputField OutgoingChanceToEscape;
    public TMP_InputField OutgoingMeleeDamage;
    public TMP_InputField OutgoingRangedDamage;
    public TMP_InputField OutgoingMagicDamage;
    public TMP_InputField OutgoingDigestionRate;
    public TMP_InputField OutgoingAbsorptionRate;
    public TMP_InputField OutgoingNutrition;
    public TMP_InputField OutgoingManaAbsorbHundreths;
    public TMP_InputField OutgoingMeleeShift;
    public TMP_InputField OutgoingRangedShift;
    public TMP_InputField OutgoingMagicShift;
    public TMP_InputField OutgoingVoreOddsMult;
    public TMP_InputField OutgoingGrowthRate;
    public TMP_InputField OutgoingCritRateShift;
    public TMP_InputField OutgoingCritDamageMult;
    public TMP_InputField OutgoingGrazeRateShift;
    public TMP_InputField OutgoingGrazeDamageMult;
    public TMP_InputField IncomingChanceToEscape;
    public TMP_InputField IncomingMeleeDamage;
    public TMP_InputField IncomingRangedDamage;
    public TMP_InputField IncomingMagicDamage;
    public TMP_InputField IncomingDigestionRate;
    public TMP_InputField IncomingAbsorptionRate;
    public TMP_InputField IncomingNutrition;
    public TMP_InputField IncomingManaAbsorbHundreths;
    public TMP_InputField IncomingMeleeShift;
    public TMP_InputField IncomingRangedShift;
    public TMP_InputField IncomingMagicShift;
    public TMP_InputField IncomingVoreOddsMult;
    public TMP_InputField IncomingGrowthRate;
    public TMP_InputField IncomingCritRateShift;
    public TMP_InputField IncomingCritDamageMult;
    public TMP_InputField IncomingGrazeRateShift;
    public TMP_InputField IncomingGrazeDamageMult;
    public TMP_InputField FlatHitReduction;
    public TMP_InputField SpeedLossFromWeightMultiplier;
    public TMP_InputField DodgeLossFromWeightMultiplier;
    public TMP_InputField BulkMultiplier;
    public TMP_InputField SpeedMultiplier;
    public TMP_InputField MinSpeed;
    public TMP_InputField SpeedBonus;
    public TMP_InputField MeleeAttacks;
    public TMP_InputField RangedAttacks;
    public TMP_InputField VoreAttacks;
    public TMP_InputField SpellAttacks;
    public TMP_InputField ManaMultiplier;
    public TMP_InputField StaminaMultiplier;
    public TMP_InputField VoreMinimumOdds;
    public TMP_InputField TurnCanFlee;
    public TMP_InputField DigestionImmunityTurns;
    public TMP_InputField HealthRegen;
    public TMP_InputField ManaRegen;
    public TMP_InputField OnLevelUpBonusToAllStats;
    public TMP_InputField OnLevelUpBonusToGiveToTwoRandomStats;
    public Toggle OnLevelUpAllowAnyStat;
    public TMP_InputField Scale;
    public TMP_InputField StatMult;
    public TMP_InputField VirtualDexMult;
    public TMP_InputField VirtualStrMult;
    public TMP_InputField FireDamageTaken;
    public TMP_InputField GrowthDecayRate;
    public TMP_InputField SightRangeBoost;

    internal void Open(int id)
    {
        gameObject.SetActive(true);
        CustomTraitEditor.gameObject.SetActive(false);
        current_id = id;
        LoadTrait(id);
    }

    public void LoadTrait(int id)
    {
        CustomTraitBoost trait = State.CustomTraitList.Where(x => x.id == id).FirstOrDefault();
        PermanentBoosts boost = trait.traitBoost;
        name.text = trait.name;
        description.text = trait.name;       
        tier.value = (int)trait.tier;
        tier.RefreshShownValue();
        ExpRequired.text = boost.ExpRequired.ToString();
        ExpGain.text = boost.ExpGain.ToString();
        ExpGainFromVore.text = boost.ExpGainFromVore.ToString();
        ExpGainFromAbsorption.text = boost.ExpGainFromAbsorption.ToString();
        PassiveHeal.text = boost.PassiveHeal.ToString();
        CapacityMult.text = boost.CapacityMult.ToString();
        OutgoingChanceToEscape.text = boost.Outgoing.ChanceToEscape.ToString();
        OutgoingMeleeDamage.text = boost.Outgoing.MeleeDamage.ToString();
        OutgoingRangedDamage.text = boost.Outgoing.RangedDamage.ToString();
        OutgoingMagicDamage.text = boost.Outgoing.MagicDamage.ToString();
        OutgoingDigestionRate.text = boost.Outgoing.DigestionRate.ToString();
        OutgoingAbsorptionRate.text = boost.Outgoing.AbsorptionRate.ToString();
        OutgoingNutrition.text = boost.Outgoing.Nutrition.ToString();
        OutgoingManaAbsorbHundreths.text = boost.Outgoing.ManaAbsorbHundreths.ToString();
        OutgoingMeleeShift.text = boost.Outgoing.MeleeShift.ToString();
        OutgoingRangedShift.text = boost.Outgoing.RangedShift.ToString();
        OutgoingMagicShift.text = boost.Outgoing.MagicShift.ToString();
        OutgoingVoreOddsMult.text = boost.Outgoing.VoreOddsMult.ToString();
        OutgoingGrowthRate.text = boost.Outgoing.GrowthRate.ToString();
        OutgoingCritRateShift.text = boost.Outgoing.CritRateShift.ToString();
        OutgoingCritDamageMult.text = boost.Outgoing.CritDamageMult.ToString();
        OutgoingGrazeRateShift.text = boost.Outgoing.GrazeRateShift.ToString();
        OutgoingGrazeDamageMult.text = boost.Outgoing.GrazeDamageMult.ToString();
        IncomingChanceToEscape.text = boost.Incoming.ChanceToEscape.ToString();
        IncomingMeleeDamage.text = boost.Incoming.MeleeDamage.ToString();
        IncomingRangedDamage.text = boost.Incoming.RangedDamage.ToString();
        IncomingMagicDamage.text = boost.Incoming.MagicDamage.ToString();
        IncomingDigestionRate.text = boost.Incoming.DigestionRate.ToString();
        IncomingAbsorptionRate.text = boost.Incoming.AbsorptionRate.ToString();
        IncomingNutrition.text = boost.Incoming.Nutrition.ToString();
        IncomingManaAbsorbHundreths.text = boost.Incoming.ManaAbsorbHundreths.ToString();
        IncomingMeleeShift.text = boost.Incoming.MeleeShift.ToString();
        IncomingRangedShift.text = boost.Incoming.RangedShift.ToString();
        IncomingMagicShift.text = boost.Incoming.MagicShift.ToString();
        IncomingVoreOddsMult.text = boost.Incoming.VoreOddsMult.ToString();
        IncomingGrowthRate.text = boost.Incoming.GrowthRate.ToString();
        IncomingCritRateShift.text = boost.Incoming.CritRateShift.ToString();
        IncomingCritDamageMult.text = boost.Incoming.CritDamageMult.ToString();
        IncomingGrazeRateShift.text = boost.Incoming.GrazeRateShift.ToString();
        IncomingGrazeDamageMult.text = boost.Incoming.GrazeDamageMult.ToString();
        FlatHitReduction.text = boost.FlatHitReduction.ToString();
        SpeedLossFromWeightMultiplier.text = boost.SpeedLossFromWeightMultiplier.ToString();
        DodgeLossFromWeightMultiplier.text = boost.DodgeLossFromWeightMultiplier.ToString();
        BulkMultiplier.text = boost.BulkMultiplier.ToString();
        SpeedMultiplier.text = boost.SpeedMultiplier.ToString();
        MinSpeed.text = boost.MinSpeed.ToString();
        SpeedBonus.text = boost.SpeedBonus.ToString();
        MeleeAttacks.text = boost.MeleeAttacks.ToString();
        RangedAttacks.text = boost.RangedAttacks.ToString();
        VoreAttacks.text = boost.VoreAttacks.ToString();
        SpellAttacks.text = boost.SpellAttacks.ToString();
        ManaMultiplier.text = boost.ManaMultiplier.ToString();
        StaminaMultiplier.text = boost.StaminaMultiplier.ToString();
        VoreMinimumOdds.text = boost.VoreMinimumOdds.ToString();
        TurnCanFlee.text = boost.TurnCanFlee.ToString();
        DigestionImmunityTurns.text = boost.DigestionImmunityTurns.ToString();
        HealthRegen.text = boost.HealthRegen.ToString();
        ManaRegen.text = boost.ManaRegen.ToString();
        OnLevelUpBonusToAllStats.text = boost.OnLevelUpBonusToAllStats.ToString();
        OnLevelUpBonusToGiveToTwoRandomStats.text = boost.OnLevelUpBonusToGiveToTwoRandomStats.ToString();
        OnLevelUpAllowAnyStat.isOn = boost.OnLevelUpAllowAnyStat;
        Scale.text = boost.Scale.ToString();
        StatMult.text = boost.StatMult.ToString();
        VirtualDexMult.text = boost.VirtualDexMult.ToString();
        VirtualStrMult.text = boost.VirtualStrMult.ToString();
        FireDamageTaken.text = boost.FireDamageTaken.ToString();
        GrowthDecayRate.text = boost.GrowthDecayRate.ToString();
        SightRangeBoost.text = boost.SightRangeBoost.ToString();
    }
    public void SaveTrait(int id)
    {
        CustomTraitBoost trait = State.CustomTraitList.Where(x => x.id == id).FirstOrDefault();
        PermanentBoosts boost = trait.traitBoost;
        OnLevelUpAllowAnyStat.isOn = boost.OnLevelUpAllowAnyStat;
        trait.name = name.text;
        trait.description = description.text;
        boost.ExpRequired = float.TryParse(ExpRequired.text, out float expRequired) ? expRequired : 1.0f;
        boost.ExpGain = float.TryParse(ExpGain.text, out float expGain) ? expGain : 1.0f;
        boost.ExpGainFromVore = float.TryParse(ExpGainFromVore.text, out float expGainFromVore) ? expGainFromVore : 1.0f;
        boost.ExpGainFromAbsorption = float.TryParse(ExpGainFromAbsorption.text, out float expGainFromAbsorption) ? expGainFromAbsorption : 1.0f;
        boost.PassiveHeal = float.TryParse(PassiveHeal.text, out float passiveHeal) ? passiveHeal : 1.0f;
        boost.CapacityMult = float.TryParse(CapacityMult.text, out float capacityMult) ? capacityMult : 1.0f;
        boost.Outgoing.ChanceToEscape = float.TryParse(OutgoingChanceToEscape.text, out float outgoingChanceToEscape) ? outgoingChanceToEscape : 1.0f;
        boost.Outgoing.MeleeDamage = float.TryParse(OutgoingMeleeDamage.text, out float outgoingMeleeDamage) ? outgoingMeleeDamage : 1.0f;
        boost.Outgoing.RangedDamage = float.TryParse(OutgoingRangedDamage.text, out float outgoingRangedDamage) ? outgoingRangedDamage : 1.0f;
        boost.Outgoing.MagicDamage = float.TryParse(OutgoingMagicDamage.text, out float outgoingMagicDamage) ? outgoingMagicDamage : 1.0f;
        boost.Outgoing.DigestionRate = float.TryParse(OutgoingDigestionRate.text, out float outgoingDigestionRate) ? outgoingDigestionRate : 1.0f;
        boost.Outgoing.AbsorptionRate = float.TryParse(OutgoingAbsorptionRate.text, out float oabsorptionRate) ? oabsorptionRate : 1.0f;
        boost.Outgoing.Nutrition = float.TryParse(OutgoingNutrition.text, out float onutrition) ? onutrition : 1.0f;
        boost.Outgoing.ManaAbsorbHundreths = int.TryParse(OutgoingManaAbsorbHundreths.text, out int omanaAbsorbHundreths) ? omanaAbsorbHundreths : 0;
        boost.Outgoing.MeleeShift = float.TryParse(OutgoingMeleeShift.text, out float meleeShift) ? meleeShift : 0;
        boost.Outgoing.RangedShift = float.TryParse(OutgoingRangedShift.text, out float rangedShift) ? rangedShift : 0;
        boost.Outgoing.MagicShift = float.TryParse(OutgoingMagicShift.text, out float magicShift) ? magicShift : 0;
        boost.Outgoing.VoreOddsMult = float.TryParse(OutgoingVoreOddsMult.text, out float voreOddsMult) ? voreOddsMult : 1.0f;
        boost.Outgoing.GrowthRate = float.TryParse(OutgoingGrowthRate.text, out float growthRate) ? growthRate : 1.0f;
        boost.Outgoing.CritRateShift = float.TryParse(OutgoingCritRateShift.text, out float critRateShift) ? critRateShift : 0;
        boost.Outgoing.CritDamageMult = float.TryParse(OutgoingCritDamageMult.text, out float critDamageMult) ? critDamageMult : 1.0f;
        boost.Outgoing.GrazeRateShift = float.TryParse(OutgoingGrazeRateShift.text, out float grazeRateShift) ? grazeRateShift : 0;
        boost.Outgoing.GrazeDamageMult = float.TryParse(OutgoingGrazeDamageMult.text, out float grazeDamageMult) ? grazeDamageMult : 1.0f;
        boost.Incoming.ChanceToEscape = float.TryParse(IncomingChanceToEscape.text, out float iChanceToEscape) ? iChanceToEscape : 1.0f;
        boost.Incoming.MeleeDamage = float.TryParse(IncomingMeleeDamage.text, out float iMeleeDamage) ? iMeleeDamage : 1.0f;
        boost.Incoming.RangedDamage = float.TryParse(IncomingRangedDamage.text, out float iRangedDamage) ? iRangedDamage : 1.0f;
        boost.Incoming.MagicDamage = float.TryParse(IncomingMagicDamage.text, out float iMagicDamage) ? iMagicDamage : 1.0f;
        boost.Incoming.DigestionRate = float.TryParse(IncomingDigestionRate.text, out float iDigestionRate) ? iDigestionRate : 1.0f;
        boost.Incoming.AbsorptionRate = float.TryParse(IncomingAbsorptionRate.text, out float iabsorptionRate) ? iabsorptionRate : 1.0f;
        boost.Incoming.Nutrition = float.TryParse(IncomingNutrition.text, out float inutrition) ? inutrition : 1.0f;
        boost.Incoming.ManaAbsorbHundreths = int.TryParse(IncomingManaAbsorbHundreths.text, out int imanaAbsorbHundreths) ? imanaAbsorbHundreths : 0;
        boost.Incoming.MeleeShift = float.TryParse(IncomingMeleeShift.text, out float imeleeShift) ? imeleeShift : 0;
        boost.Incoming.RangedShift = float.TryParse(IncomingRangedShift.text, out float irangedShift) ? irangedShift : 0;
        boost.Incoming.MagicShift = float.TryParse(IncomingMagicShift.text, out float imagicShift) ? imagicShift : 0;
        boost.Incoming.VoreOddsMult = float.TryParse(IncomingVoreOddsMult.text, out float ivoreOddsMult) ? ivoreOddsMult : 1.0f;
        boost.Incoming.GrowthRate = float.TryParse(IncomingGrowthRate.text, out float igrowthRate) ? igrowthRate : 1.0f;
        boost.Incoming.CritRateShift = float.TryParse(IncomingCritRateShift.text, out float icritRateShift) ? icritRateShift : 0;
        boost.Incoming.CritDamageMult = float.TryParse(IncomingCritDamageMult.text, out float icritDamageMult) ? icritDamageMult : 1.0f;
        boost.Incoming.GrazeRateShift = float.TryParse(IncomingGrazeRateShift.text, out float igrazeRateShift) ? igrazeRateShift : 0;
        boost.Incoming.GrazeDamageMult = float.TryParse(IncomingGrazeDamageMult.text, out float igrazeDamageMult) ? igrazeDamageMult : 1.0f;
        boost.FlatHitReduction = float.TryParse(FlatHitReduction.text, out float flatHitReduction) ? flatHitReduction : 1.0f;
        boost.SpeedLossFromWeightMultiplier = float.TryParse(SpeedLossFromWeightMultiplier.text, out float speedLossFromWeightMultiplier) ? speedLossFromWeightMultiplier : 1.0f;
        boost.DodgeLossFromWeightMultiplier = float.TryParse(DodgeLossFromWeightMultiplier.text, out float dodgeLossFromWeightMultiplier) ? dodgeLossFromWeightMultiplier : 1.0f;
        boost.BulkMultiplier = float.TryParse(BulkMultiplier.text, out float bulkMultiplier) ? bulkMultiplier : 1.0f;
        boost.SpeedMultiplier = float.TryParse(SpeedMultiplier.text, out float speedMultiplier) ? speedMultiplier : 1.0f;
        boost.MinSpeed = int.TryParse(MinSpeed.text, out int minSpeed) ? minSpeed : 1;
        boost.SpeedBonus = int.TryParse(SpeedBonus.text, out int speedBonus) ? speedBonus : 0;
        boost.MeleeAttacks = int.TryParse(MeleeAttacks.text, out int meleeAttacks) ? meleeAttacks : 1;
        boost.RangedAttacks = int.TryParse(RangedAttacks.text, out int rangedAttacks) ? rangedAttacks : 1;
        boost.VoreAttacks = int.TryParse(VoreAttacks.text, out int voreAttacks) ? voreAttacks : 1;
        boost.SpellAttacks = int.TryParse(SpellAttacks.text, out int spellAttacks) ? spellAttacks : 1;
        boost.ManaMultiplier = float.TryParse(ManaMultiplier.text, out float manaMultiplier) ? manaMultiplier : 1.0f;
        boost.StaminaMultiplier = float.TryParse(StaminaMultiplier.text, out float staminaMultiplier) ? staminaMultiplier : 1.0f;
        boost.VoreMinimumOdds = int.TryParse(VoreMinimumOdds.text, out int voreMinimumOdds) ? voreMinimumOdds : 0;
        boost.TurnCanFlee = int.TryParse(TurnCanFlee.text, out int turnCanFlee) ? turnCanFlee : 8;
        boost.DigestionImmunityTurns = int.TryParse(DigestionImmunityTurns.text, out int digestionImmunityTurns) ? digestionImmunityTurns : Config.DigestionGraceTurns;
        boost.HealthRegen = int.TryParse(HealthRegen.text, out int healthRegen) ? healthRegen : 0;
        boost.ManaRegen = int.TryParse(ManaRegen.text, out int manaRegen) ? manaRegen : 0;
        boost.OnLevelUpBonusToAllStats = int.TryParse(OnLevelUpBonusToAllStats.text, out int onLevelUpBonusToAllStats) ? onLevelUpBonusToAllStats : 0;
        boost.OnLevelUpBonusToGiveToTwoRandomStats = int.TryParse(OnLevelUpBonusToGiveToTwoRandomStats.text, out int onLevelUpBonusToGiveToTwoRandomStats) ? onLevelUpBonusToGiveToTwoRandomStats : 0;
        boost.OnLevelUpAllowAnyStat = OnLevelUpAllowAnyStat.isOn;
        boost.Scale = float.TryParse(Scale.text, out float scale) ? scale : 1.0f;
        boost.StatMult = float.TryParse(StatMult.text, out float statMult) ? statMult : 1.0f;
        boost.VirtualDexMult = float.TryParse(VirtualDexMult.text, out float virtualDexMult) ? virtualDexMult : 1.0f;
        boost.VirtualStrMult = float.TryParse(VirtualStrMult.text, out float virtualStrMult) ? virtualStrMult : 1.0f;
        boost.FireDamageTaken = float.TryParse(FireDamageTaken.text, out float fireDamageTaken) ? fireDamageTaken : 1.0f;
        boost.GrowthDecayRate = float.TryParse(GrowthDecayRate.text, out float growthDecayRate) ? growthDecayRate : 1.0f;
        boost.SightRangeBoost = int.TryParse(SightRangeBoost.text, out int sightRangeBoost) ? sightRangeBoost : 0;
    }

    public void SaveClose()
    {
        SaveTrait(current_id);
        DiscardClose();
    }

    public void DiscardClose()
    {
        gameObject.SetActive(false);
        CustomTraitEditor.Open();
    }

    public void Remove()
    {
        var rem = State.CustomTraitList.Where(x => current_id == x.id).FirstOrDefault();
        State.CustomTraitList.Remove(rem);
        DiscardClose();
    }
}