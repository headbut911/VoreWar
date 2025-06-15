using System;
using System.Collections.Generic;
using UnityEngine;

class Lupine : DefaultRaceData
{
    readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.HumansBodySprites1;
    readonly Sprite[] Sprites2 = State.GameManager.SpriteDictionary.HumansBodySprites2;
    readonly Sprite[] Sprites3 = State.GameManager.SpriteDictionary.HumansBodySprites3;
    readonly Sprite[] Sprites4 = State.GameManager.SpriteDictionary.HumansVoreSprites;

    bool oversize = false;

    public Lupine()
    {
        BodySizes = 3;
        EarTypes = 8;
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.RedSkin);
        EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
        BeardStyles = 0;
        BodyAccentTypes1 = 6; // eyebrows
        BodyAccentTypes2 = 3;
        SpecialAccessoryCount = 0;
        AccessoryColors = 0;
        HairStyles = 36;
        MouthTypes = 12;

        ExtendedBreastSprites = true;


        BodySizes = 4;
        EyeTypes = 5;
        SpecialAccessoryCount = 12; // ears     
        HairStyles = 25;
        MouthTypes = 6;
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DeerLeaf);
        HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DeerSkin);
        BodyAccentTypes1 = 12; // antlers
        BodyAccentTypes2 = 7; // pattern types
        BodyAccentTypes3 = 2; // leg types

        ExtendedBreastSprites = true;
        FurCapable = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => LupineColor(s));
        Head = new SpriteExtraInfo(6, HeadSprite, null, (s) => LupineColor(s));
        BodyAccessory = new SpriteExtraInfo(20, AccessorySprite, null, (s) => LupineColor(s)); // Ears
        BodyAccent = new SpriteExtraInfo(4, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor)); // Right Arm
        BodyAccent2 = new SpriteExtraInfo(2, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor)); // Right Hand
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor)); // Body Pattern
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor)); // Arm Pattern
        BodyAccent5 = new SpriteExtraInfo(7, BodyAccentSprite5, WhiteColored); // Nose
        BodyAccent6 = new SpriteExtraInfo(6, BodyAccentSprite6, WhiteColored); // Hoofs
        BodyAccent7 = new SpriteExtraInfo(8, BodyAccentSprite7, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor)); // Mouth external
        BodyAccent8 = new SpriteExtraInfo(7, BodyAccentSprite8, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor)); // alternative legs
        Mouth = new SpriteExtraInfo(7, MouthSprite, WhiteColored);
        Hair = new SpriteExtraInfo(21, HairSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair2 = new SpriteExtraInfo(0, HairSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor));
        Hair3 = new SpriteExtraInfo(8, HairSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); // Eyebrows
        Beard = null;
        Eyes = new SpriteExtraInfo(7, EyesSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, s.Unit.EyeColor));
        SecondaryEyes = null;
        SecondaryAccessory = new SpriteExtraInfo(22, SecondaryAccessorySprite, WhiteColored); // Antlers
        Belly = new SpriteExtraInfo(14, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor));
        Weapon = new SpriteExtraInfo(3, WeaponSprite, WhiteColored);
        BackWeapon = null;
        BodySize = null;
        Breasts = new SpriteExtraInfo(17, BreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor));
        SecondaryBreasts = new SpriteExtraInfo(17, SecondaryBreastsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor));
        BreastShadow = null;
        Dick = new SpriteExtraInfo(11, DickSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor));
        Balls = new SpriteExtraInfo(10, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, s.Unit.SkinColor));


        clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing50Spaced);
    }


    internal override void SetBaseOffsets(Actor_Unit actor)
    {

        if (actor.Unit.HasBreasts)
        {
            if (actor.Unit.BodySize > 1)
            {
                AddOffset(Balls, 0, 3 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
            else
            {
                AddOffset(Balls, 0, 3 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
        }
        else
        {
            if (actor.Unit.BodySize > 1)
            {
                AddOffset(Balls, 0, 1 * .625f);
                AddOffset(Belly, 0, 1 * .625f);
            }
            else
            {
                AddOffset(Balls, 0, 0);
                AddOffset(Belly, 0, 1 * .625f);
            }
        }

        if (actor.GetWeaponSprite() == 0 || actor.GetWeaponSprite() == 4 || actor.GetWeaponSprite() == 6)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, -1 * .625f, 0);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 1 * .625f, -1 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 0, -1 * .625f);
                }
            }
        }
        else if (actor.GetWeaponSprite() == 1 || actor.GetWeaponSprite() == 3)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 0, -1 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 3 * .625f, -3 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 3 * .625f, -4 * .625f);
                }
            }
        }
        else if (actor.GetWeaponSprite() == 2)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, -1 * .625f, 2 * .625f);
                }
                else
                {
                    AddOffset(Weapon, -2 * .625f, 3 * .625f);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 0, 0);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
        }
        else if (actor.GetWeaponSprite() == 5 || actor.GetWeaponSprite() == 7)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 1 * .625f, -1 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 0, 0);
                }
            }
            else
            {
                if (actor.Unit.BodySize > 1)
                {
                    AddOffset(Weapon, 2 * .625f, -3 * .625f);
                }
                else
                {
                    AddOffset(Weapon, 2 * .625f, -3 * .625f);
                }
            }
        }
    }


    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);

        unit.EarType = State.Rand.Next(EarTypes);
        unit.BodyAccentType1 = State.Rand.Next(BodyAccentTypes1);
        unit.BodyAccentType2 = State.Rand.Next(BodyAccentTypes2);
    }

    internal override int DickSizes => 6;
    internal override int BreastSizes => 8;

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon == false)
        {
            if (actor.IsAttacking) return Sprites[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            return Sprites[0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return Sprites[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 1:
                return Sprites[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 2:
                return Sprites[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 3:
                return Sprites[3 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 4:
                return Sprites[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 5:
                return Sprites[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 6:
                return Sprites[2 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            case 7:
                return Sprites[1 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
            default:
                return Sprites[0 + (actor.Unit.BodySize * 4) + (actor.Unit.HasBreasts ? 0 : 12)];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsEating)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[4];
                }
                else
                {
                    return Sprites2[1];
                }
            }
            else
            {
                return Sprites2[7 + (actor.Unit.BodySize * 3)];
            }
        }
        else if (actor.IsAttacking)
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[5];
                }
                else
                {
                    return Sprites2[2];
                }
            }
            else
            {
                return Sprites2[8 + (actor.Unit.BodySize * 3)];
            }
        }
        else
        {
            if (actor.Unit.HasBreasts)
            {
                if (actor.Unit.BodySize > 1)
                {
                    return Sprites2[3];
                }
                else
                {
                    return Sprites2[0];
                }
            }
            else
            {
                return Sprites2[6 + (actor.Unit.BodySize * 3)];
            }

        }
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.MainlandElfParts[0 + actor.Unit.EarType]; //ears

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Extra weapon sprite
    {
        if (actor.Unit.HasWeapon == false)
        {
            return null;
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return null;
            case 1:
                return null;
            case 2:
                return null;
            case 3:
                return null;
            case 4:
                return null;
            case 5:
                return null;
            case 6:
                return null;
            case 7:
                return null;
            default:
                return null;
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Ears
    {
        if (actor.Unit.BodyAccentType1 == 0)
            return null;
        return State.GameManager.SpriteDictionary.MainlandElfParts[actor.Unit.EarType + actor.Unit.BodyAccentType2 * 8];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Extra weapon sprite
    {
        if (actor.Unit.HasWeapon == false)
        {
            return null;
        }

        switch (actor.GetWeaponSprite())
        {
            case 0:
                return null;
            case 1:
                return null;
            case 2:
                return null;
            case 3:
                return null;
            case 4:
                return State.GameManager.SpriteDictionary.MainlandElfParts[32];
            case 5:
                return null;
            case 6:
                return State.GameManager.SpriteDictionary.MainlandElfParts[33];
            case 7:
                return null;
            default:
                return null;
        }
    }

    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        if (actor.IsEating || actor.IsAttacking)
            return null;
        else
            return Sprites3[108 + actor.Unit.MouthType];
    }

    protected override Sprite HairSprite(Actor_Unit actor)
    {
        return Sprites2[71 + 2 * actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite2(Actor_Unit actor)
    {
        return Sprites2[72 + 2 * actor.Unit.HairStyle];
    }

    protected override Sprite HairSprite3(Actor_Unit actor)
    {
        return Sprites3[120 + actor.Unit.BodyAccentType1];
    }

    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return Sprites2[69];
        }
        else
        {
            return Sprites3[24 + 4 * actor.Unit.EyeType + ((actor.IsAttacking || actor.IsEating) ? 0 : 2)];
        }
    }

    protected override Sprite EyesSecondarySprite(Actor_Unit actor)
    {
        if (actor.Unit.IsDead && actor.Unit.Items != null)
        {
            return null;
        }
        else
        {
            return Sprites3[25 + 4 * actor.Unit.EyeType + ((actor.IsAttacking || actor.IsEating) ? 0 : 2)];
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly)
        {
            belly.transform.localScale = new Vector3(1, 1, 1);
            belly.SetActive(true);
            int size = actor.GetStomachSize(31, 0.7f);
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites4[105];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites4[104];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites4[103];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
            {
                AddOffset(Belly, 0, -33 * .625f);
                return Sprites4[102];
            }
            switch (size)
            {
                case 26:
                    AddOffset(Belly, 0, -14 * .625f);
                    break;
                case 27:
                    AddOffset(Belly, 0, -17 * .625f);
                    break;
                case 28:
                    AddOffset(Belly, 0, -20 * .625f);
                    break;
                case 29:
                    AddOffset(Belly, 0, -25 * .625f);
                    break;
                case 30:
                    AddOffset(Belly, 0, -27 * .625f);
                    break;
                case 31:
                    AddOffset(Belly, 0, -32 * .625f);
                    break;
            }

            return Sprites4[70 + size];
        }
        else
        {
            return null;
        }
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[24];
                case 1:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[25];
                case 2:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[28];
                case 3:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[29];
                case 4:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[26];
                case 5:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[27];
                case 6:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[30];
                case 7:
                    return State.GameManager.SpriteDictionary.MainlandElfParts[31];
                default:
                    return null;
            }
        }
        else
        {
            return null;
        }
    }

    protected override Sprite BreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        oversize = false;
        if (actor.PredatorComponent?.LeftBreastFullness > 0)
        {
            int leftSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f));
            if (leftSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
            {
                return Sprites4[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return Sprites4[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return Sprites4[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            return Sprites4[0 + leftSize];
        }
        else
        {
            return Sprites4[0 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite SecondaryBreastsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasBreasts == false)
            return null;
        if (actor.PredatorComponent?.RightBreastFullness > 0)
        {
            int rightSize = (int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f));
            if (rightSize > actor.Unit.DefaultBreastSize)
                oversize = true;
            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
            {
                return Sprites4[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return Sprites4[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return Sprites4[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            return Sprites4[32 + rightSize];
        }
        else
        {
            return Sprites4[32 + actor.Unit.BreastSize];
        }
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;

        if (actor.IsErect())
        {
            if ((actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
            {
                Dick.layer = 20;
                if (actor.IsCockVoring)
                {
                    return Sprites4[162 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites4[154 + actor.Unit.DickSize];
                }
            }
            else
            {
                Dick.layer = 13;
                if (actor.IsCockVoring)
                {
                    return Sprites4[146 + actor.Unit.DickSize];
                }
                else
                {
                    return Sprites4[138 + actor.Unit.DickSize];
                }
            }
        }

        Dick.layer = 11;
        return Sprites3[108 + actor.Unit.DickSize];
    }

    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == false)
            return null;
        if (actor.IsErect() && (actor.PredatorComponent?.VisibleFullness < .75f) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetRightBreastSize(32 * 32, 1f)) < 16) && ((int)Math.Sqrt((actor.Unit.DefaultBreastSize * actor.Unit.DefaultBreastSize) + actor.GetLeftBreastSize(32 * 32, 1f)) < 16))
        {
            Balls.layer = 19;
        }
        else
        {
            Balls.layer = 10;
        }
        int size = actor.Unit.DickSize;
        int offset = actor.GetBallSize(28, .8f);
        if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprites4[141];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprites4[140];
        }
        else if ((actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
        {
            AddOffset(Balls, 0, -22 * .625f);
            return Sprites4[139];
        }
        else if (offset >= 26)
        {
            AddOffset(Balls, 0, -22 * .625f);
        }
        else if (offset == 25)
        {
            AddOffset(Balls, 0, -16 * .625f);
        }
        else if (offset == 24)
        {
            AddOffset(Balls, 0, -13 * .625f);
        }
        else if (offset == 23)
        {
            AddOffset(Balls, 0, -11 * .625f);
        }
        else if (offset == 22)
        {
            AddOffset(Balls, 0, -10 * .625f);
        }
        else if (offset == 21)
        {
            AddOffset(Balls, 0, -7 * .625f);
        }
        else if (offset == 20)
        {
            AddOffset(Balls, 0, -6 * .625f);
        }
        else if (offset == 19)
        {
            AddOffset(Balls, 0, -4 * .625f);
        }
        else if (offset == 18)
        {
            AddOffset(Balls, 0, -1 * .625f);
        }

        if (offset > 0)
            return Sprites4[Math.Min(112 + offset, 138)];
        return Sprites4[106 + size];
    }

    ColorSwapPalette LupineColor(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType1 == 0)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LupineReversed, actor.Unit.SkinColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LupineSkin, actor.Unit.SkinColor);
    }
}
