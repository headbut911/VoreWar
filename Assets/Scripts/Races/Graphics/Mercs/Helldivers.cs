using System.Collections.Generic;
using UnityEngine;

class Helldivers : BlankSlate
{
    internal Helldivers()
    {
        SpecialAccessoryCount = 0;
        AvoidedEyeTypes = 0;
        AvoidedMouthTypes = 0;

        HairColors = 1;
        HairStyles = 1;
        SkinColors = 1;
        EyeTypes = 1;
        EyeColors = 1;
        SecondaryEyeColors = 1;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
        BodySizes = 2;//Simple Light set and Heavy set
        BodyAccentTypes1 = 1;//Armor set
        BodyAccentTypes2 = 7;//Race Detail/Accessories
        AllowedMainClothingTypes = new List<MainClothing>();
        AllowedWaistTypes = new List<MainClothing>();
        AllowedClothingHatTypes = new List<ClothingAccessory>();
        AvoidedMainClothingTypes = 0;
        MouthTypes = 0;


        ExtendedBreastSprites = false;

        Body = null;
        Head = null;
        BodyAccessory = null;
        BodyAccent = new SpriteExtraInfo(5, BodyAccentSprite, WhiteColored);//Armor
        BodyAccent2 = new SpriteExtraInfo(2, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor));//Race Detail/Accessories
        BodyAccent3 = new SpriteExtraInfo(12, BodyAccentSprite3, WhiteColored);//Over-Faulding
        BodyAccent4 = null;
        BodyAccent5 = null;
        BodyAccent6 = null;
        BodyAccent7 = null;
        BodyAccent8 = null;
        BodyAccent9 = null;
        BodyAccent10 = null;
        Mouth = null;
        Hair = null;
        Hair2 = null;
        Eyes = null;
        SecondaryEyes = null;
        SecondaryAccessory = null;
        Belly = new SpriteExtraInfo(15, null, WhiteColored);
        SecondaryBelly = null;
        Weapon = new SpriteExtraInfo(6, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(16, BreastsSprite, WhiteColored);
        Dick = null;
        Balls = new SpriteExtraInfo(10, BallsSprite, WhiteColored);
    }
    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
    }
    internal override int BreastSizes => 5;
    internal override int DickSizes => 1;

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.SetActive(true);
            if (actor.PredatorComponent.VisibleFullness > 4)
            {
                float extraCap = actor.PredatorComponent.VisibleFullness - 4;
                float xScale = Mathf.Min(1 + (extraCap / 5), 1.8f);
                float yScale = Mathf.Min(1 + (extraCap / 40), 1.1f);
                belly.transform.localScale = new Vector3(xScale, yScale, 1);
            }
            else
                belly.transform.localScale = new Vector3(1, 1, 1);
            return State.GameManager.SpriteDictionary.Helldivers1[29 + actor.GetStomachSize(14)];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
            if (actor.IsAttacking)
                return State.GameManager.SpriteDictionary.Helldivers2[1 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
            return State.GameManager.SpriteDictionary.Helldivers2[0 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        return State.GameManager.SpriteDictionary.Helldivers1[22 + actor.Unit.BodyAccentType2];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        return State.GameManager.SpriteDictionary.Helldivers2[2 + (actor.Unit.BodySize < 1 ? 3 : 0) + (!actor.Unit.HasBreasts ? 7 : 0)];
    }

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts)
            return State.GameManager.SpriteDictionary.Helldivers2[17 + actor.Unit.BreastSize];
        return null;
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.PredatorComponent?.BallsFullness > 0)
        {
            if ((actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls)) || (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls)))
            {
                if (actor.GetBallSize(8, 0.7f) == 8)
                    return State.GameManager.SpriteDictionary.Helldivers1[47];
                else if (actor.GetBallSize(8, 0.8f) == 8)
                    return State.GameManager.SpriteDictionary.Helldivers1[46];
                else if (actor.GetBallSize(8, 0.9f) == 8)
                    return State.GameManager.SpriteDictionary.Helldivers1[45];
            }
            return State.GameManager.SpriteDictionary.Helldivers1[45 + actor.GetBallSize(8)];
        }
        return null;
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            return State.GameManager.SpriteDictionary.Helldivers1[53 + actor.GetWeaponSprite()];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => null;
    protected override Sprite BackWeaponSprite(Actor_Unit actor) => null;
    protected override Color BodyAccessoryColor(Actor_Unit actor) => Color.white;
    protected override Color BodyColor(Actor_Unit actor) => Color.white;
    protected override Sprite BodySizeSprite(Actor_Unit actor) => null;
    protected override Sprite BodySprite(Actor_Unit actor) => null;
    internal override Color ClothingColor(Actor_Unit actor) => Color.white;
    protected override Sprite DickSprite(Actor_Unit actor) => null;
    protected override Color EyeColor(Actor_Unit actor) => Color.white;
    protected override Sprite EyesSecondarySprite(Actor_Unit actor) => null;
    protected override Sprite EyesSprite(Actor_Unit actor) => null;
    protected override Color HairColor(Actor_Unit actor) => Color.white;
    protected override Sprite HairSprite(Actor_Unit actor) => null;
    protected override Sprite HairSprite2(Actor_Unit actor) => null;
    protected override Sprite MouthSprite(Actor_Unit actor) => null;
    protected override Color ScleraColor(Actor_Unit actor) => Color.white;
    protected override Sprite SecondaryAccessorySprite(Actor_Unit actor) => null;


}

