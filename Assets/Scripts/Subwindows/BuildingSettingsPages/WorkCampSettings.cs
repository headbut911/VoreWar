using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkCampSettings : MonoBehaviour
{
    public InputField GoldPerTurn;
    
    public InputField WoodPrice;
    public InputField NaturalMaterialsPrice;
    public InputField PrefabsPrice;
    public InputField StonePrice;
    public InputField OresPrice;
    public InputField ManaStonesPrice;

    public InputField WoodStock;
    public InputField NaturalMaterialsStock;
    public InputField PrefabsStock;
    public InputField StoneStock;
    public InputField OresStock;
    public InputField ManaStonesStock;

    internal void Save()
    {
        Config.BuildCon.WorkCampGoldPerTurn = int.TryParse(GoldPerTurn.text, out int gpt) ? gpt : 100;
        Config.BuildCon.WorkCampTurnStock.SetResources(
            int.TryParse(WoodStock.text, out int ws) ? ws : 10,
            int.TryParse(StoneStock.text, out int ss) ? ss : 10,
            int.TryParse(NaturalMaterialsStock.text, out int nms) ? nms : 5,
            int.TryParse(OresStock.text, out int os) ? os : 5,
            int.TryParse(PrefabsStock.text, out int ps) ? ps : 3,
            int.TryParse(ManaStonesStock.text, out int mss) ?  mss : 3
            );

        Config.BuildCon.WorkCampItemPrice.SetResources(
            int.TryParse(WoodPrice.text, out int wp) ? wp : 10,
            int.TryParse(StonePrice.text, out int sp) ? sp : 10,
            int.TryParse(NaturalMaterialsPrice.text, out int nmp) ? nmp : 25,
            int.TryParse(OresPrice.text, out int op) ? op : 25,
            int.TryParse(PrefabsPrice.text, out int pp) ? pp : 50,
            int.TryParse(ManaStonesPrice.text, out int msp) ?  msp : 50
            );
    }

    internal  void Load()
    {
        GoldPerTurn.text = Config.BuildCon.WorkCampGoldPerTurn.ToString();

        WoodStock.text = Config.BuildCon.WorkCampTurnStock.Wood.ToString();
        StoneStock.text = Config.BuildCon.WorkCampTurnStock.Wood.ToString();
        NaturalMaterialsStock.text = Config.BuildCon.WorkCampTurnStock.Stone.ToString();
        OresStock.text = Config.BuildCon.WorkCampTurnStock.Ores.ToString();
        PrefabsStock.text = Config.BuildCon.WorkCampTurnStock.Prefabs.ToString();
        ManaStonesStock.text = Config.BuildCon.WorkCampTurnStock.ManaStones.ToString();

        WoodPrice.text = Config.BuildCon.WorkCampItemPrice.Wood.ToString();
        StonePrice.text = Config.BuildCon.WorkCampItemPrice.Stone.ToString();
        NaturalMaterialsPrice.text = Config.BuildCon.WorkCampItemPrice.NaturalMaterials.ToString();
        OresPrice.text = Config.BuildCon.WorkCampItemPrice.Ores.ToString();
        PrefabsPrice.text = Config.BuildCon.WorkCampItemPrice.Prefabs.ToString();
        ManaStonesPrice.text = Config.BuildCon.WorkCampItemPrice.ManaStones.ToString();
    }
}
