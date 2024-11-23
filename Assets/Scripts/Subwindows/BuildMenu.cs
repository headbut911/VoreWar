using MapObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Experimental.UIElements.StyleEnums;

public class BuildMenu : MonoBehaviour
{

    List<BuildingPrefab> PossibleBuildings;

    public GameObject BuildItemPrefab;
    public Transform BuildingFolder;

    bool pausedState = false;

    public void Open()
    {
        pausedState = State.GameManager.StrategyMode.Paused;
        State.GameManager.StrategyMode.Paused = true;
        gameObject.SetActive(true);
        PossibleBuildings = new List<BuildingPrefab>();
        var enabledBuildings = Config.World.GetBuildingInfo();
        int counter = 0;
        foreach (var building in enabledBuildings)
        {
            if (!building.enabled)
                continue;
            BuildingPrefab currentPrefab = BuildItemPrefab.GetComponent<BuildingPrefab>();
            currentPrefab.BuildingName.text = building.Name;
            currentPrefab.BuildTurns.text = building.baseBuildTurns.ToString();
            currentPrefab.GoldCost.text = building.GoldCost.ToString();
            currentPrefab.Wood.text = building.ResourceToBuild.Wood.ToString();
            currentPrefab.NaturalMaterials.text = building.ResourceToBuild.NaturalMaterials.ToString();
            currentPrefab.Prefabs.text = building.ResourceToBuild.Prefabs.ToString();
            currentPrefab.Stone.text = building.ResourceToBuild.Stone.ToString();
            currentPrefab.Ores.text = building.ResourceToBuild.Ores.ToString();
            currentPrefab.ManaStones.text = building.ResourceToBuild.ManaStones.ToString();
            currentPrefab.linkedBuilding = DetermineType(building);
            PossibleBuildings.Add(Instantiate(currentPrefab, BuildingFolder).GetComponent<BuildingPrefab>());
            counter++;
        }
    }

    private ConstructibleType DetermineType(ConstructibleBuilding build)
    {
        if (build is WorkCamp)
            return ConstructibleType.WorkCamp;
        return (ConstructibleType)(-1);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        State.GameManager.StrategyMode.Paused = pausedState;
    }
}
