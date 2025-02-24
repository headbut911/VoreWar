using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.UIElements.GraphView;

public enum AcademyResearchType
{
    ArmyMP,
    Happiness,
    ImprintCost,
    MercRecruitCost,
    VilPopMax,
    PopBreedInc,
    ArmySize,
    GarrisonSize,
    FOWSightRange,
    ArmyHealRate,
    GoldMineIncome,
    TrainingEXP,
    TeamEXP,
    StartingEXP,
    UpkeepReduction,

    ResearchTypeCounter,
}

public static class AcademyResearch
{
    public static float GetValueFromEmpire(Empire empire, AcademyResearchType type)
    {
        if (!empire.AcademyResearchCompleted.Keys.Contains(type))
        {
            return 0;
        }
        return empire.AcademyResearchCompleted[type];
    }
}

