using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Centaur : TaurHumanHalf
{
    //readonly Sprite[] Sprites = State.GameManager.SpriteDictionary.CentaurParts;
    bool oversize = false;
    public Centaur()
    {
        AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.FeralHorseSkin); // Main body, legs, head, tail upper
        GentleAnimation = true;
        WeightGainDisabled = true;
        CanBeGender = new List<Gender>() { Gender.Male, Gender.Female, Gender.Hermaphrodite };
        TailTypes = 6;

        Weapon = new SpriteExtraInfo(6, WeaponSprite, WhiteColored);
        BodyAccessory = new SpriteExtraInfo(10, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor)); // Horse Body
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor)); // right hind leg
        BodyAccent2 = new SpriteExtraInfo(8, BodyAccentSprite2, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, s.Unit.HairColor)); // tail
        BodyAccent3 = new SpriteExtraInfo(5, BodyAccentSprite3, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor)); // sheath
        BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor)); // belly cover
        BodyAccent5 = new SpriteExtraInfo(4, BodyAccentSprite5, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor)); // left hind leg
        BodyAccent6 = new SpriteExtraInfo(1, BodyAccentSprite6, null, null); // right hind hoof
        BodyAccent7 = new SpriteExtraInfo(4, BodyAccentSprite7, null, null); // left hind hoof
        BodyAccent8 = new SpriteExtraInfo(11, BodyAccentSprite8, null, null); // Front Hooves
        SecondaryBelly = new SpriteExtraInfo(7, SecondaryBellySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor)); // Second Stomach
        Dick = new SpriteExtraInfo(4, DickSprite, WhiteColored);
        Balls = new SpriteExtraInfo(3, BallsSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralHorseSkin, s.Unit.AccessoryColor));
    }
    internal override int BreastSizes => 8;

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.TailType = State.Rand.Next(TailTypes);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        OffsetAllHumanParts(actor, 25, 49);
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) // Mane
    {
        if (actor.IsAttacking)
            return State.GameManager.SpriteDictionary.CentaurParts[1];
        if (actor.IsOralVoring)
            return State.GameManager.SpriteDictionary.CentaurParts[1];
        else
            return State.GameManager.SpriteDictionary.CentaurParts[0];
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Left Hind leg
    {
        return State.GameManager.SpriteDictionary.CentaurParts[5];
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Right Hind Hoof
    {
        return State.GameManager.SpriteDictionary.CentaurParts[6];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Belly Cover
    {
        if (actor.HasBelly)
            return null;
        else
            return State.GameManager.SpriteDictionary.CentaurParts[8];
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) //Sheath
    {
        if (actor.Unit.DickSize < 0) return null;
        if (Config.HideCocks) return null;
		
		if (actor.IsErect()) return State.GameManager.SpriteDictionary.CentaurParts[28];

		else return State.GameManager.SpriteDictionary.CentaurParts[29];
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Tail
    { 
        return State.GameManager.SpriteDictionary.CentaurParts[22 + actor.Unit.TailType];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Right Hind Leg
    {
        return State.GameManager.SpriteDictionary.CentaurParts[4];
    }

    protected override Sprite BodyAccentSprite7(Actor_Unit actor) // Left Hind Hoof
    {
        return State.GameManager.SpriteDictionary.CentaurParts[7];
    }

    protected override Sprite BodyAccentSprite8(Actor_Unit actor) // Face
    {
        if (actor.IsAttacking)
            return State.GameManager.SpriteDictionary.CentaurParts[3]; 
        if (actor.IsOralVoring)
            return State.GameManager.SpriteDictionary.CentaurParts[3]; 
        return State.GameManager.SpriteDictionary.CentaurParts[2]; 
    }

    protected override Sprite SecondaryBellySprite(Actor_Unit actor) // Second Stomach
    {
        int size = actor.GetStomach2Size(31);
        if (size + actor.GetWombSize(3) < 1) return State.GameManager.SpriteDictionary.CentaurParts[8];

        if (size > 4)
        {
            SecondaryBelly.layer = 9;
        }
        else
        {
            SecondaryBelly.layer = 6;
        }

        if (!actor.HasBelly)
            return null;

        if ( size >= 31 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
			AddOffset(SecondaryBelly, -23 * .625f, -7 * .625f);
            return State.GameManager.SpriteDictionary.CentaurParts[54];
        }

        if (size >= 29 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
			AddOffset(SecondaryBelly, -23 * .625f, -7 * .625f);
            return State.GameManager.SpriteDictionary.CentaurParts[53];
        }

        if (size >= 27 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
			AddOffset(SecondaryBelly, -23 * .625f, -7 * .625f);
            return State.GameManager.SpriteDictionary.CentaurParts[52];
        }

        if (size >= 25 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
			AddOffset(SecondaryBelly, -23 * .625f, -7 * .625f);
            return State.GameManager.SpriteDictionary.CentaurParts[51];
        }

        if (size >= 23 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
			AddOffset(SecondaryBelly, -23 * .625f, -7 * .625f);
            return State.GameManager.SpriteDictionary.CentaurParts[50];
        }

        if (size > 19) size = 19;

        return State.GameManager.SpriteDictionary.CentaurParts[30 + size];
    }

    protected override Sprite DickSprite(Actor_Unit actor)
    {
        if (actor.Unit.DickSize < 0) return null;
        if (Config.HideCocks) return null;

        if (actor.IsCockVoring) return State.GameManager.SpriteDictionary.CentaurParts[9];
		if (actor.IsErect()) return State.GameManager.SpriteDictionary.CentaurParts[9];

        return null;
    }
    
    protected override Sprite BallsSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasDick == true && !Config.HideCocks && actor.PredatorComponent?.BallsFullness > 0)
		{
			AddOffset(Balls, -48 * .625f, 0 * .625f);
        }
		else return null;

        int size = actor.GetBallSize(30);

        if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false)
        {
            return State.GameManager.SpriteDictionary.CentaurParts[79];
        }

        else if (size >= 29 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.CentaurParts[78];
        }

        else if (size >= 27 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.CentaurParts[77];
        }

        else if (size >= 25 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.CentaurParts[76];
        }

        else if (size >= 23 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.CentaurParts[75];
        }

        else if (size >= 21 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false))
        {
            return State.GameManager.SpriteDictionary.CentaurParts[74];
        }

        if (size > 19) size = 19;
        return State.GameManager.SpriteDictionary.CentaurParts[55 + size];
    }

    protected override Sprite WeaponSprite(Actor_Unit actor)
    {
        if (actor.Unit.HasWeapon && actor.Surrendered == false)
        {
            switch (actor.GetWeaponSprite())
            {
                case 0:
                    return State.GameManager.SpriteDictionary.HumansBodySprites3[132];
                case 1:
                    return State.GameManager.SpriteDictionary.HumansBodySprites3[133];
                case 2:
                    return State.GameManager.SpriteDictionary.HumansBodySprites3[134];
                case 3:
                    return State.GameManager.SpriteDictionary.HumansBodySprites3[135];
                case 4:
                    return State.GameManager.SpriteDictionary.HumansBodySprites3[136];
                case 5:
                    return State.GameManager.SpriteDictionary.HumansBodySprites3[138];
                case 6:
                    return State.GameManager.SpriteDictionary.HumansBodySprites3[139];
                case 7:
                    return State.GameManager.SpriteDictionary.HumansBodySprites3[141];
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
                return State.GameManager.SpriteDictionary.HumansVoreSprites[31];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[30];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[29];
            }

            if (leftSize > 28)
                leftSize = 28;

            return State.GameManager.SpriteDictionary.HumansVoreSprites[0 + leftSize];
        }
        else
        {
            return State.GameManager.SpriteDictionary.HumansVoreSprites[0 + actor.Unit.BreastSize];
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
                return State.GameManager.SpriteDictionary.HumansVoreSprites[63];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[62];
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
            {
                return State.GameManager.SpriteDictionary.HumansVoreSprites[61];
            }

            if (rightSize > 28)
                rightSize = 28;

            return State.GameManager.SpriteDictionary.HumansVoreSprites[32 + rightSize];
        }
        else
        {
            return State.GameManager.SpriteDictionary.HumansVoreSprites[32 + actor.Unit.BreastSize];
        }
    }
}