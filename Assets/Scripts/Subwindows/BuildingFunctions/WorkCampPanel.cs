using MapObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkCampPanel : MonoBehaviour
{

    public Button BuyWood;
    public Button SellWood;
    public Button BuyStone;
    public Button SellStone;
    public Button BuyNM;
    public Button SellNM;
    public Button BuyOres;
    public Button SellOres;
    public Button BuyPrefabs;
    public Button SellPrefabs;
    public Button BuyMS;
    public Button SellMS;

    public TextMeshProUGUI[] OwnedItems;
    public TextMeshProUGUI[] InStockItems;
    public TextMeshProUGUI[] ItemPrice;

    public void Open(ConstructibleBuilding building)
    {
        RefreshUI(building);

        if (((WorkCamp)building).postUpgrade.built)
        {
            BuyWood.interactable = true;
            SellWood.interactable = true;
            BuyStone.interactable = true;
            SellStone.interactable = true;
        }

        if (((WorkCamp)building).merchantUpgrade.built)
        {
            BuyNM.interactable = true;
            SellNM.interactable = true;
            BuyOres.interactable = true;
            SellOres.interactable = true;
        }
    }

    void RefreshUI(ConstructibleBuilding building)
    {
        OwnedItems[0].text = building.Owner.constructionResources.Wood.ToString();
        OwnedItems[1].text = building.Owner.constructionResources.Stone.ToString();
        OwnedItems[2].text = building.Owner.constructionResources.NaturalMaterials.ToString();
        OwnedItems[3].text = building.Owner.constructionResources.Ores.ToString();
        OwnedItems[4].text = building.Owner.constructionResources.Prefabs.ToString();
        OwnedItems[5].text = building.Owner.constructionResources.ManaStones.ToString();


        InStockItems[0].text = ((WorkCamp)building).inStockItems.Wood.ToString();
        InStockItems[1].text = ((WorkCamp)building).inStockItems.Stone.ToString();
        InStockItems[2].text = ((WorkCamp)building).inStockItems.NaturalMaterials.ToString();
        InStockItems[3].text = ((WorkCamp)building).inStockItems.Ores.ToString();
        InStockItems[4].text = ((WorkCamp)building).inStockItems.Prefabs.ToString();
        InStockItems[5].text = ((WorkCamp)building).inStockItems.ManaStones.ToString();


        ItemPrice[0].text = "10";
        ItemPrice[1].text = "10";
        ItemPrice[2].text = "30";
        ItemPrice[3].text = "30";
        ItemPrice[4].text = "70";
        ItemPrice[5].text = "70";
    }

}
