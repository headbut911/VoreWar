﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class Singularity : BlankSlate
{

    public Singularity()
    {
        CanBeGender = new List<Gender>() { Gender.Female };
        WeightGainDisabled = true;
        ExtendedBreastSprites = true;
        GentleAnimation = true;

        Body = new SpriteExtraInfo(0, BodySprite, WhiteColored); // Back Body.
		Belly = new SpriteExtraInfo(1, null, WhiteColored); // Taur Belly.
		BodyAccent = new SpriteExtraInfo(2, BodyAccentSprite, WhiteColored); // Back Arms.
		BodyAccent2 = new SpriteExtraInfo(3, BodyAccentSprite2, WhiteColored); // Front Body.
		BodyAccent3 = new SpriteExtraInfo(4, BodyAccentSprite3, WhiteColored); // Human Belly.
		BodyAccent4 = new SpriteExtraInfo(5, BodyAccentSprite4, WhiteColored); // Front Arms.
		BodyAccent5 = new SpriteExtraInfo(6, BodyAccentSprite5, WhiteColored); // Right Breast.
		BodyAccent6 = new SpriteExtraInfo(7, BodyAccentSprite6, WhiteColored); // Left Breast.
		Head = new SpriteExtraInfo(3, HeadSprite, WhiteColored); // Head.
	}

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Singularity";
    }

	internal override void SetBaseOffsets(Actor_Unit actor)
    {  
		if (actor.GetLeftBreastSize(26) > 9 || actor.GetRightBreastSize(26) > 9 || actor.GetStomach2Size(39) + actor.GetWombSize(39) > 14 || actor.GetExclusiveStomachSize(24) > 9)
		{
			AddOffset(Belly, 0, -78 * .5f);	
		}
	}

    protected override Sprite BodySprite(Actor_Unit actor) // Back Body
    {
        if (actor.GetLeftBreastSize(26) > 9 || actor.GetRightBreastSize(26) > 9 || actor.GetStomach2Size(39) + actor.GetWombSize(39) > 14 || actor.GetExclusiveStomachSize(24) > 9) return null;		
        return State.GameManager.SpriteDictionary.Singularity[3];
    }
	
	internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Taur Belly
    {		
		if (actor.GetStomach2Size(39) + actor.GetWombSize(39) < 1) return null;
		if (actor.GetLeftBreastSize(26) > 9 || actor.GetRightBreastSize(26) > 9 || actor.GetStomach2Size(39) + actor.GetWombSize(39) > 14 || actor.GetExclusiveStomachSize(24) > 9)
		{
			int TBS = actor.GetStomach2Size(39) + actor.GetWombSize(39);
			if (TBS >= 38 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach2, PreyLocation.womb)) return State.GameManager.SpriteDictionary.Singularity[166]; 
			if (TBS >= 36 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach2, PreyLocation.womb)) return State.GameManager.SpriteDictionary.Singularity[165];
			if (TBS >= 34 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach2, PreyLocation.womb)) return State.GameManager.SpriteDictionary.Singularity[164];
			if (TBS >= 32 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach2, PreyLocation.womb)) return State.GameManager.SpriteDictionary.Singularity[163];
			if (TBS >= 30 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach2, PreyLocation.womb)) return State.GameManager.SpriteDictionary.Singularity[162];
			if (TBS >= 28 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach2, PreyLocation.womb)) return State.GameManager.SpriteDictionary.Singularity[161];
			if (TBS >= 26 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach2, PreyLocation.womb)) return State.GameManager.SpriteDictionary.Singularity[160];
			if (TBS > 25) TBS = 25;
			return State.GameManager.SpriteDictionary.Singularity[134 + TBS];
		}
		else
		{
			int TBS = actor.GetStomach2Size(39) + actor.GetWombSize(39);
			return State.GameManager.SpriteDictionary.Singularity[119 + TBS];
		}
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Back Arms
    {
		if (actor.GetLeftBreastSize(26) > 9 || actor.GetRightBreastSize(26) > 9 || actor.GetStomach2Size(39) + actor.GetWombSize(39) > 14 || actor.GetExclusiveStomachSize(24) > 9)
		{
			return null;
		}
		else
		{
			if (actor.IsEating) return State.GameManager.SpriteDictionary.Singularity[17];	
			return State.GameManager.SpriteDictionary.Singularity[16];	
		}
    }
	
	protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Front Body
    {
		if (actor.GetLeftBreastSize(26) > 9 || actor.GetRightBreastSize(26) > 9 || actor.GetStomach2Size(39) + actor.GetWombSize(39) > 14 || actor.GetExclusiveStomachSize(24) > 9)
		{
			if (actor.GetStomach2Size(32) + actor.GetWombSize(32) > 14) return State.GameManager.SpriteDictionary.Singularity[2];
			return State.GameManager.SpriteDictionary.Singularity[1];				
		}
		else
		{
			return State.GameManager.SpriteDictionary.Singularity[0];
		}		
    }
	
	protected override Sprite BodyAccentSprite3(Actor_Unit actor) // Human Belly
    {
		if (actor.GetExclusiveStomachSize(24) < 1) return null;
		if (actor.GetLeftBreastSize(26) > 9 || actor.GetRightBreastSize(26) > 9 || actor.GetStomach2Size(39) + actor.GetWombSize(39) > 14 || actor.GetExclusiveStomachSize(24) > 9)
		{
			int UBS = actor.GetExclusiveStomachSize(24);
			if (UBS >= 23 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach)) return State.GameManager.SpriteDictionary.Singularity[119]; 
			if (UBS >= 21 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach)) return State.GameManager.SpriteDictionary.Singularity[118];
			if (UBS > 20) UBS = 20;
			return State.GameManager.SpriteDictionary.Singularity[88 + UBS];
		}
		else
		{
			int UBS = actor.GetRightBreastSize(26);
			return State.GameManager.SpriteDictionary.Singularity[29 + UBS];
		}
    }
	
	protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Front Arms
    {
		if (actor.GetLeftBreastSize(26) > 9 || actor.GetRightBreastSize(26) > 9 || actor.GetStomach2Size(39) + actor.GetWombSize(39) > 14 || actor.GetExclusiveStomachSize(24) > 9)
		{
			if (actor.IsEating) return State.GameManager.SpriteDictionary.Singularity[14];
			if (actor.IsAttacking) return State.GameManager.SpriteDictionary.Singularity[15];
			return State.GameManager.SpriteDictionary.Singularity[13];
		}
		else
		{
			if (actor.IsEating) return State.GameManager.SpriteDictionary.Singularity[11];
			if (actor.IsAttacking) return State.GameManager.SpriteDictionary.Singularity[12];
			return State.GameManager.SpriteDictionary.Singularity[10];
		}
    }

	protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Right Breast
    {
		if (actor.GetLeftBreastSize(26) > 9 || actor.GetRightBreastSize(26) > 9 || actor.GetStomach2Size(39) + actor.GetWombSize(39) > 14 || actor.GetExclusiveStomachSize(24) > 9)
		{
			if (actor.PredatorComponent?.RightBreastFullness == 0) return State.GameManager.SpriteDictionary.Singularity[64]; 
			int RBS = actor.GetRightBreastSize(26);
			if (RBS >= 25 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast)) return State.GameManager.SpriteDictionary.Singularity[87]; 
			if (RBS >= 23 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast)) return State.GameManager.SpriteDictionary.Singularity[86];
			if (RBS >= 21 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast)) return State.GameManager.SpriteDictionary.Singularity[85];
			if (RBS > 20) RBS = 20;
			return State.GameManager.SpriteDictionary.Singularity[64 + RBS];
		}
		else
		{
			if (actor.PredatorComponent?.RightBreastFullness == 0) return State.GameManager.SpriteDictionary.Singularity[29]; 
			int RBS = actor.GetRightBreastSize(26);
			return State.GameManager.SpriteDictionary.Singularity[29 + RBS];
		}
    }
	
	protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Left Breast
    {
		
		if (actor.GetLeftBreastSize(26) > 9 || actor.GetRightBreastSize(26) > 9 || actor.GetStomach2Size(39) + actor.GetWombSize(39) > 14 || actor.GetExclusiveStomachSize(24) > 9)
		{
			if (actor.PredatorComponent?.LeftBreastFullness == 0) return State.GameManager.SpriteDictionary.Singularity[40]; 
			int LBS = actor.GetLeftBreastSize(26);
			if (LBS >= 25 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast)) return State.GameManager.SpriteDictionary.Singularity[63]; 
			if (LBS >= 23 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast)) return State.GameManager.SpriteDictionary.Singularity[62];
			if (LBS >= 21 && actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast)) return State.GameManager.SpriteDictionary.Singularity[61];
			if (LBS > 20) LBS = 20;
			return State.GameManager.SpriteDictionary.Singularity[40 + LBS];
		}
		else
		{
			if (actor.PredatorComponent?.LeftBreastFullness == 0) return State.GameManager.SpriteDictionary.Singularity[18]; 
			int LBS = actor.GetLeftBreastSize(26);
			return State.GameManager.SpriteDictionary.Singularity[18 + LBS];
		}
    }
	
	protected override Sprite HeadSprite(Actor_Unit actor) // Head
	{
		if (actor.GetLeftBreastSize(26) > 9 || actor.GetRightBreastSize(26) > 9 || actor.GetStomach2Size(39) + actor.GetWombSize(39) > 14 || actor.GetExclusiveStomachSize(24) > 9)
		{
			if (actor.IsEating) return State.GameManager.SpriteDictionary.Singularity[8];
			if (actor.IsAttacking) return State.GameManager.SpriteDictionary.Singularity[9];
			return State.GameManager.SpriteDictionary.Singularity[7];
		}
		else
		{
			if (actor.IsEating) return State.GameManager.SpriteDictionary.Singularity[5];
			if (actor.IsAttacking) return State.GameManager.SpriteDictionary.Singularity[6];
			return State.GameManager.SpriteDictionary.Singularity[4];
		}
	}		
}

