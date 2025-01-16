using MapObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Experimental.UIElements.StyleEnums;

public class BuildMenu : MonoBehaviour
{


    public GameObject BuildItemPrefab;
    public Transform BuildingFolder;

    public TextMeshProUGUI CurrentGold;
    public TextMeshProUGUI CurrentWood;
    public TextMeshProUGUI CurrentStone;
    public TextMeshProUGUI CurrentNM;
    public TextMeshProUGUI CurrentOres;
    public TextMeshProUGUI CurrentPrefabs;
    public TextMeshProUGUI CurrentMS;

    public void Open(Empire empire)
    {
        ClearFolder();
        State.GameManager.StrategyMode.Paused = true;
        gameObject.SetActive(true);
        var enabledBuildings = Config.World.GetBuildingInfo();
        CurrentGold.text = empire.Gold.ToString();
        CurrentWood.text = empire.constructionResources.Wood.ToString();
        CurrentStone.text = empire.constructionResources.Stone.ToString();
        CurrentNM.text = empire.constructionResources.NaturalMaterials.ToString();
        CurrentOres.text = empire.constructionResources.Ores.ToString();
        CurrentPrefabs.text = empire.constructionResources.Prefabs.ToString();
        CurrentMS.text = empire.constructionResources.ManaStones.ToString();
        foreach (var building in enabledBuildings)
        {
            if (!building.enabled)
                continue;
            var obj = Instantiate(BuildItemPrefab, BuildingFolder);
            var currentPrefab = obj.GetComponent<BuildingPrefab>();
            currentPrefab.BuildingName.text = building.Name;
            currentPrefab.BuildingDesc.text = building.Desc;
            currentPrefab.BuildTurns.text = building.baseBuildTurns.ToString();
            currentPrefab.GoldCost.text = building.GoldCost.ToString();
            currentPrefab.Wood.text = building.ResourceToBuild.Wood.ToString();
            currentPrefab.NaturalMaterials.text = building.ResourceToBuild.NaturalMaterials.ToString();
            currentPrefab.Prefabs.text = building.ResourceToBuild.Prefabs.ToString();
            currentPrefab.Stone.text = building.ResourceToBuild.Stone.ToString();
            currentPrefab.Ores.text = building.ResourceToBuild.Ores.ToString();
            currentPrefab.ManaStones.text = building.ResourceToBuild.ManaStones.ToString();
            currentPrefab.linkedBuilding = DetermineType(building);
            if (!empire.constructionResources.CanBuildWithCurrentResources(building.ResourceToBuild) || building.GoldCost > empire.Gold)
            {
                currentPrefab.Construct.interactable = false;
            }
            currentPrefab.Construct.onClick.AddListener(() =>
            {
                Close();
                State.GameManager.StrategyMode.InitiateBuildMode(currentPrefab.linkedBuilding);
            });
        }
    }

    private ConstructibleType DetermineType(ConstructibleBuilding build)
    {
        if (build is WorkCamp)
            return ConstructibleType.WorkCamp;
        return (ConstructibleType)(-1);
    }

    private void ClearFolder()
    {
        int children = BuildingFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(BuildingFolder.GetChild(i).gameObject);
        }
    }
    public void Close()
    {
        ClearFolder();
        gameObject.SetActive(false);
        State.GameManager.StrategyMode.Paused = false;
        State.GameManager.StrategyMode.BuildMode = false;
    }
}
