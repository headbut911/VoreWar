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

    public TextMeshProUGUI GoldCount;


    public TextMeshProUGUI[] OwnedItems;
    public TextMeshProUGUI[] InStockItems;
    public TextMeshProUGUI[] ItemPrice;

    public void Open(ConstructibleBuilding building)
    {
        RefreshUI(building);
        BuyWood.onClick.AddListener(() =>
        {
            PurchaceItem(ConstructionresourceType.wood, (WorkCamp)building);
            RefreshUI(building);
        });
        SellWood.onClick.AddListener(() =>
        {
            SellItem(ConstructionresourceType.wood, (WorkCamp)building);
            RefreshUI(building);
        });
        BuyStone.onClick.AddListener(() =>
        {
            PurchaceItem(ConstructionresourceType.stone, (WorkCamp)building);
            RefreshUI(building);
        });
        SellStone.onClick.AddListener(() =>
        {
            SellItem(ConstructionresourceType.stone, (WorkCamp)building);
            RefreshUI(building);
        });
        BuyNM.onClick.AddListener(() =>
        {
            PurchaceItem(ConstructionresourceType.naturalmaterials, (WorkCamp)building);
            RefreshUI(building);
        });
        SellNM.onClick.AddListener(() =>
        {
            SellItem(ConstructionresourceType.naturalmaterials, (WorkCamp)building);
            RefreshUI(building);
        });
        BuyOres.onClick.AddListener(() =>
        {
            PurchaceItem(ConstructionresourceType.ores, (WorkCamp)building);
            RefreshUI(building);
        });
        SellOres.onClick.AddListener(() =>
        {
            SellItem(ConstructionresourceType.ores, (WorkCamp)building);
            RefreshUI(building);
        });
        BuyPrefabs.onClick.AddListener(() =>
        {
            PurchaceItem(ConstructionresourceType.prefabs, (WorkCamp)building);
            RefreshUI(building);
        });
        SellPrefabs.onClick.AddListener(() =>
        {
            SellItem(ConstructionresourceType.prefabs, (WorkCamp)building);
            RefreshUI(building);
        });
        BuyMS.onClick.AddListener(() =>
        {
            PurchaceItem(ConstructionresourceType.manastones, (WorkCamp)building);
            RefreshUI(building);
        });
        BuyMS.onClick.AddListener(() =>
        {
            SellItem(ConstructionresourceType.manastones, (WorkCamp)building);
            RefreshUI(building);
        });
    }

    void RefreshUI(ConstructibleBuilding building)
    {
        if (((WorkCamp)building).postUpgrade.built && building.active)
        {
            BuyWood.interactable = true;
            SellWood.interactable = true;
            BuyStone.interactable = true;
            SellStone.interactable = true;
        }

        if (((WorkCamp)building).merchantUpgrade.built && building.active)
        {
            BuyNM.interactable = true;
            SellNM.interactable = true;
            BuyOres.interactable = true;
            SellOres.interactable = true;
            BuyPrefabs.interactable = true;
            SellPrefabs.interactable = true;
            BuyMS.interactable = true;
            SellMS.interactable = true;
        }

        GoldCount.text = building.Owner.Gold.ToString();

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

        if (((WorkCamp)building).inStockItems.Wood <= 0 ||  GetItemPrice(ConstructionresourceType.wood, (WorkCamp)building) > building.Owner.Gold)
        {
            BuyWood.interactable = false;
        }
        if (building.Owner.constructionResources.Wood <= 0)
        {
            SellWood.interactable = false;
        }

        if (((WorkCamp)building).inStockItems.Stone <= 0 ||  GetItemPrice(ConstructionresourceType.stone, (WorkCamp)building) > building.Owner.Gold)
        {
            BuyStone.interactable = false;
        }
        if (building.Owner.constructionResources.Stone <= 0)
        {
            SellStone.interactable = false;
        }

        if (((WorkCamp)building).inStockItems.NaturalMaterials <= 0 ||  GetItemPrice(ConstructionresourceType.naturalmaterials, (WorkCamp)building) > building.Owner.Gold)
        {
            BuyNM.interactable = false;
        }
        if (building.Owner.constructionResources.NaturalMaterials <= 0)
        {
            SellNM.interactable = false;
        }

        if (((WorkCamp)building).inStockItems.Ores <= 0 ||  GetItemPrice(ConstructionresourceType.ores, (WorkCamp)building) > building.Owner.Gold)
        {
            BuyOres.interactable = false;
        }
        if (building.Owner.constructionResources.Ores <= 0)
        {
            SellOres.interactable = false;
        }

        if (((WorkCamp)building).inStockItems.Prefabs <= 0 ||  GetItemPrice(ConstructionresourceType.prefabs, (WorkCamp)building) > building.Owner.Gold)
        {
            BuyPrefabs.interactable = false;
        }
        if (building.Owner.constructionResources.Prefabs <= 0)
        {
            SellPrefabs.interactable = false;
        }

        if (((WorkCamp)building).inStockItems.ManaStones <= 0 ||  GetItemPrice(ConstructionresourceType.manastones, (WorkCamp)building) > building.Owner.Gold)
        {
            BuyMS.interactable = false;
        }
        if (building.Owner.constructionResources.ManaStones <= 0)
        {
            SellMS.interactable = false;
        }
    }

    void PurchaceItem(ConstructionresourceType type, WorkCamp building)
    {
        building.inStockItems.SpendResource(type, 1);
        building.Owner.SpendGold(GetItemPrice(type, building));
        building.Owner.constructionResources.AddResource(type, 1);
    }
    
    void SellItem(ConstructionresourceType type, WorkCamp building)
    {
        building.inStockItems.AddResource(type, 1);
        building.Owner.AddGold(GetItemPrice(type, building));
        building.Owner.constructionResources.SpendResource(type, 1);
    }
    int GetItemPrice(ConstructionresourceType type, WorkCamp building)
    {
        switch (type)
        {
            case ConstructionresourceType.wood:
                return 10;
            case ConstructionresourceType.stone:
                return 10;
            case ConstructionresourceType.ores:
                return 30;
            case ConstructionresourceType.naturalmaterials:
                return 30;
            case ConstructionresourceType.prefabs:
                return 70;
            case ConstructionresourceType.manastones:
                return 70;
            default:
                break;
        }
        return 0;
    }
}
