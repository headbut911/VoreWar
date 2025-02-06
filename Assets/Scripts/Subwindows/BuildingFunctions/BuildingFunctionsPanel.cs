using MapObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class BuildingFunctionsPanel : MonoBehaviour
{

    public GameObject NoFunction;

    GameObject ActiveObject;

    public WorkCampPanel WorkCamp;
    public LumberSitePanel LumberSite;


    public void Open(ConstructibleBuilding building)
    {
        if (building == null)
            return;
        ActiveObject = NoFunction;
        ClearObjects();
        if (building is WorkCamp)
        {
            if (((WorkCamp)building).postUpgrade.built)
            {
                ActiveObject = WorkCamp.gameObject;
                ActiveObject.SetActive(true);
                WorkCamp.Open(building);
            }
            else
                NoFunction.SetActive(true);
        }
        else if (building is LumberSite)
        {
            ActiveObject = LumberSite.gameObject;
            ActiveObject.SetActive(true);
            LumberSite.Open(building);
        }
        else
            NoFunction.SetActive(true);
    }

    private void ClearObjects()
    {
        NoFunction.SetActive(false);
        ActiveObject.SetActive(false);
    }
    public void Close()
    {
        ClearObjects();
        gameObject.SetActive(false);
        State.GameManager.StrategyMode.Paused = false;
    }
}
