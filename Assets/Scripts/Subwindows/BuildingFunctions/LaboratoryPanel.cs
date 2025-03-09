using MapObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaboratoryPanel : MonoBehaviour
{
    TextMeshProUGUI BrewCount;
    TextMeshProUGUI UnitPrice;
    TextMeshProUGUI DiscountValue;
    TextMeshProUGUI TotalCost;
    Button StartPotion;
    Button BrewPotion;
    Button IncCount;
    Button DecCount;

    GameObject BrewPanel;
    TextMeshProUGUI OverviewStats;

    TextMeshProUGUI OptionTitle1;
    TextMeshProUGUI OptionStats1;
    Button OptionSelect1;
    TextMeshProUGUI OptionTitle2;
    TextMeshProUGUI OptionStats2;
    Button OptionSelect2;
    TextMeshProUGUI OptionTitle3;
    TextMeshProUGUI OptionStats3;
    Button OptionSelect3;
    TextMeshProUGUI OptionTitle4;
    TextMeshProUGUI OptionStats4;
    Button OptionSelect4;

    Laboratory Laboratory;

    public void Open(ConstructibleBuilding building)
    {
        Laboratory = (Laboratory)building;
        StartPotion.GetComponent<Text>().text = $"New Potion\n ({Config.BuildCon.LaboratoryUpfrontCost} G)";
        if (Laboratory.Owner.Gold < Config.BuildCon.LaboratoryUpfrontCost)
        {
            StartPotion.interactable = false;
        }
        BrewPanel.gameObject.SetActive(false);
        OverviewStats.text = "";
    }

    void GenerateOption(int id)
    {

    }
}