using OdinSerializer;
using System;

class BarrierTower : ConstructibleBuilding
{
    public BuildingUpgrade improveUpgrade;
    public BuildingUpgrade deepUpgrade;
    public BuildingUpgrade leyUpgrade;

    [OdinSerialize]
    internal int ActionPlan;
    
    public BarrierTower(Vec2i location) : base(location)
    {
        Name = "Quarry";
        Desc = "The quarry generates stone each turn. Its output and resource can be adjusted using set action plans.";
        spriteID = 16;
        buildingType = ConstructibleType.Quarry;

        ApplyConfigStats(Config.BuildCon.Quarry);
        improveUpgrade = AddUpgrade(improveUpgrade, Config.BuildCon.QuarryImproveUpgrade);
        deepUpgrade = AddUpgrade(deepUpgrade, Config.BuildCon.QuarryDeepUpgrade);
        leyUpgrade = AddUpgrade(leyUpgrade, Config.BuildCon.QuarryLeyLineUpgrade);

        ActionPlan = 0;
    }
    internal override void RunBuildingFunction()
    {
        ConstructionResources ownerResource = Owner.constructionResources;
        int StoneRoll = 0;
        int OreRoll = 0;
        int MSRoll = 0;
        int GoldRoll = 0;
        if (improveUpgrade.built)
        {
            switch (ActionPlan)
            {
                case 0:
                    StoneRoll = State.Rand.Next(Config.BuildCon.QuarryStoneMin + 1, Config.BuildCon.QuarryStoneMax + 1) ;
                    OreRoll = State.Rand.Next(Config.BuildCon.QuarryOreMin + 1, Config.BuildCon.QuarryOreMax + 1);
                    MSRoll = State.Rand.Next(Config.BuildCon.QuarryMSMin + 1, Config.BuildCon.QuarryMSMax + 1);
                    break;
                case 1:
                    StoneRoll = State.Rand.Next((int)Math.Ceiling(Config.BuildCon.QuarryStoneMin * 0.5f), (int)Math.Ceiling(Config.BuildCon.QuarryStoneMin * 1.75f));
                    OreRoll = State.Rand.Next((int)Math.Ceiling(Config.BuildCon.QuarryOreMin * 0.5f), (int)Math.Ceiling(Config.BuildCon.QuarryOreMax * 1.75f));
                    MSRoll = State.Rand.Next((int)Math.Ceiling(Config.BuildCon.QuarryMSMin * 0.5f), (int)Math.Ceiling(Config.BuildCon.QuarryMSMax * 1.75f));
                    break;
                case 2:
                    int mode = 0;
                    int rolls = 0;
                    while (rolls < 2)
                    {
                        mode = State.Rand.Next(4);
                        if (mode == 0)
                        {
                            GoldRoll = Config.BuildCon.QuarryGoldMax;
                        }
                        else if (mode == 1)
                        {
                            StoneRoll = Config.BuildCon.QuarryStoneMax;
                        }
                        else if (mode == 2)
                        {
                            OreRoll = Config.BuildCon.QuarryOreMax;
                        }
                        else
                        {
                            MSRoll = Config.BuildCon.QuarryMSMax;
                        }
                        rolls++;
                    }
                    break;
                case 3:
                    StoneRoll = State.Rand.Next(0, Config.BuildCon.QuarryStoneMin + Config.BuildCon.QuarryStoneMax);
                    OreRoll = State.Rand.Next(0, Config.BuildCon.QuarryOreMin + Config.BuildCon.QuarryOreMax);
                    MSRoll = State.Rand.Next(0, Config.BuildCon.QuarryStoneMin + Config.BuildCon.QuarryStoneMax);
                    GoldRoll= State.Rand.Next(0, Config.BuildCon.QuarryGoldMin + Config.BuildCon.QuarryGoldMax);
                    break;
                case 4:
                    if (State.Rand.Next(10) >= 6)
                        OreRoll = State.Rand.Next(Config.BuildCon.QuarryOreMin * 2, Config.BuildCon.QuarryOreMax * 2);
                    else
                        MSRoll = State.Rand.Next(Config.BuildCon.QuarryStoneMin * 2, Config.BuildCon.QuarryStoneMax * 2);
                    break;
                case 5:
                    GoldRoll = State.Rand.Next(0, Config.BuildCon.QuarryStoneMax * 2);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (ActionPlan)
            {
                case 0:
                    StoneRoll = State.Rand.Next(Config.BuildCon.QuarryStoneMin, Config.BuildCon.QuarryStoneMax);
                    OreRoll = State.Rand.Next(Config.BuildCon.QuarryOreMin, Config.BuildCon.QuarryOreMax);
                    MSRoll = State.Rand.Next(Config.BuildCon.QuarryMSMin, Config.BuildCon.QuarryMSMax);
                    break;
                case 1:
                    StoneRoll = State.Rand.Next((int)Math.Floor(Config.BuildCon.QuarryStoneMin * 0.5f), (int)Math.Ceiling(Config.BuildCon.QuarryStoneMin * 1.5f));
                    OreRoll = State.Rand.Next((int)Math.Floor(Config.BuildCon.QuarryOreMin * 0.5f), (int)Math.Ceiling(Config.BuildCon.QuarryOreMax * 1.5f));
                    MSRoll = State.Rand.Next((int)Math.Floor(Config.BuildCon.QuarryMSMin * 0.5f), (int)Math.Ceiling(Config.BuildCon.QuarryMSMax * 1.5f));
                    break;
                case 2:
                    int mode = State.Rand.Next(4);
                    if (mode == 0)
                    {
                        GoldRoll = Config.BuildCon.QuarryGoldMax;
                    }
                    else if (mode == 1)
                    {
                        StoneRoll = Config.BuildCon.QuarryStoneMax;
                    }
                    else if (mode == 2)
                    {
                        OreRoll = Config.BuildCon.QuarryOreMax;
                    }
                    else
                    {
                        MSRoll = Config.BuildCon.QuarryMSMax;
                    }
                    break;
                default:
                    break;
            }
        }

        if (!deepUpgrade.built) 
        {
            OreRoll = 0;
        }
        if (!leyUpgrade.built) 
        {
            MSRoll = 0;
        }

        ownerResource.AddResource(ConstructionresourceType.stone, StoneRoll);
        ownerResource.AddResource(ConstructionresourceType.ores, OreRoll);
        ownerResource.AddResource(ConstructionresourceType.manastones, MSRoll);
        Owner.AddGold(GoldRoll);
    }
}

