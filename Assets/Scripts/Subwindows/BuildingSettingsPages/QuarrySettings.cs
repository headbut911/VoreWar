using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuarrySettings : MonoBehaviour
{
    public InputField StoneMin;
    public InputField StoneMax;
    public InputField OreMin;
    public InputField OreMax;
    public InputField MSMin;
    public InputField MSMax;
    public InputField GoldMin;
    public InputField GoldMax;

    internal void Save()
    {
        Config.BuildCon.QuarryStoneMin = int.TryParse(StoneMin.text, out int smi) ? smi : 1;
        Config.BuildCon.QuarryStoneMax = int.TryParse(StoneMax.text, out int sma) ? sma : 3;
        Config.BuildCon.QuarryOreMin = int.TryParse(OreMin.text, out int omi) ? omi : 1;
        Config.BuildCon.QuarryOreMax = int.TryParse(OreMax.text, out int oma) ? oma : 3;
        Config.BuildCon.QuarryMSMin = int.TryParse(MSMin.text, out int mmi) ? mmi : 1;
        Config.BuildCon.QuarryMSMax = int.TryParse(MSMax.text, out int mma) ? mma : 3;
        Config.BuildCon.QuarryGoldMin = int.TryParse(GoldMin.text, out int gmi) ? gmi : 0;
        Config.BuildCon.QuarryGoldMax = int.TryParse(GoldMax.text, out int gma) ? gma : 20;
    }

    internal void Load()
    {
        StoneMax.text = Config.BuildCon.QuarryStoneMin.ToString();
        StoneMin.text = Config.BuildCon.QuarryStoneMax.ToString();
        OreMin.text = Config.BuildCon.QuarryOreMin.ToString();
        OreMax.text = Config.BuildCon.QuarryOreMax.ToString();
        MSMin.text = Config.BuildCon.QuarryMSMin.ToString();
        MSMax.text = Config.BuildCon.QuarryMSMax.ToString();
        GoldMin.text = Config.BuildCon.QuarryGoldMin.ToString();
        GoldMax.text = Config.BuildCon.QuarryGoldMax.ToString();
    }
}
