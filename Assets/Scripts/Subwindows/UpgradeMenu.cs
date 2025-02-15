using MapObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{

    public GameObject UpgradeButtonPrefab;
    public Transform UpgradeFolder;

    public BuildingFunctionsPanel BuildingFunctinosMenu;

    public TextMeshProUGUI CurrentGold;
    public TextMeshProUGUI CurrentWood;
    public TextMeshProUGUI CurrentStone;
    public TextMeshProUGUI CurrentNM;
    public TextMeshProUGUI CurrentOres;
    public TextMeshProUGUI CurrentPrefabs;
    public TextMeshProUGUI CurrentMS;

    Dictionary<UpgradePrefab, BuildingUpgrade> upgradePrefabs;

    public void Open(ConstructibleBuilding building)
    {
        ClearFolder();
        CurrentGold.text = building.Owner.Gold.ToString();
        CurrentWood.text = building.Owner.constructionResources.Wood.ToString();
        CurrentStone.text = building.Owner.constructionResources.Stone.ToString();
        CurrentNM.text = building.Owner.constructionResources.NaturalMaterials.ToString();
        CurrentOres.text = building.Owner.constructionResources.Ores.ToString();
        CurrentPrefabs.text = building.Owner.constructionResources.Prefabs.ToString();
        CurrentMS.text = building.Owner.constructionResources.ManaStones.ToString();

        upgradePrefabs = new Dictionary<UpgradePrefab, BuildingUpgrade>();

        foreach (BuildingUpgrade upgrade in building.Upgrades)
        {
            if (upgrade.built)
                continue;
            var obj = Instantiate(UpgradeButtonPrefab, UpgradeFolder);
            var currentPrefab = obj.GetComponent<UpgradePrefab>();
            currentPrefab.UpgradeTurns.text = upgrade.upgradeTime.ToString();
            currentPrefab.UpgradeName.text = upgrade.Name.ToString();
            currentPrefab.UpgradeDesc.text = upgrade.Desc.ToString();
            currentPrefab.GoldCost.text = upgrade.GoldCost.ToString();
            currentPrefab.Wood.text = upgrade.ResourceToUpgrade.Wood.ToString();
            currentPrefab.NaturalMaterials.text = upgrade.ResourceToUpgrade.NaturalMaterials.ToString();
            currentPrefab.Prefabs.text = upgrade.ResourceToUpgrade.Prefabs.ToString();
            currentPrefab.Stone.text = upgrade.ResourceToUpgrade.Stone.ToString();
            currentPrefab.Ores.text = upgrade.ResourceToUpgrade.Ores.ToString();
            currentPrefab.ManaStones.text = upgrade.ResourceToUpgrade.ManaStones.ToString();
            currentPrefab.DoUpgrade.onClick.AddListener(() =>
            {
                upgrade.built = true;
                building.turnsToUpgrade += upgrade.upgradeTime;
                currentPrefab.DoUpgrade.interactable = false;
                building.Owner.constructionResources.SpendProvidedResources(upgrade.ResourceToUpgrade);
                building.Owner.SpendGold(upgrade.GoldCost);
                RefreshMats(building);
                RefreshFolderInteractables(building);
                State.GameManager.StrategyMode.RedrawVillages();
            });
            if (!building.Owner.constructionResources.CanBuildWithCurrentResources(upgrade.ResourceToUpgrade) || upgrade.GoldCost > building.Owner.Gold)
                currentPrefab.DoUpgrade.interactable = false;
            upgradePrefabs.Add(currentPrefab, upgrade);
        }
        BuildingFunctinosMenu.Open(building);
        gameObject.SetActive(true);
    }

    void RefreshMats(ConstructibleBuilding building)
    {
        CurrentGold.text = building.Owner.Gold.ToString();
        CurrentWood.text = building.Owner.constructionResources.Wood.ToString();
        CurrentStone.text = building.Owner.constructionResources.Stone.ToString();
        CurrentNM.text = building.Owner.constructionResources.NaturalMaterials.ToString();
        CurrentOres.text = building.Owner.constructionResources.Ores.ToString();
        CurrentPrefabs.text = building.Owner.constructionResources.Prefabs.ToString();
        CurrentMS.text = building.Owner.constructionResources.ManaStones.ToString();
    }

    void RefreshFolderInteractables(ConstructibleBuilding building)
    {
        foreach (var item in upgradePrefabs)
        {
            if (!building.Owner.constructionResources.CanBuildWithCurrentResources(item.Value.ResourceToUpgrade) || item.Value.GoldCost > building.Owner.Gold)
                item.Key.DoUpgrade.interactable = false;
        }
    }

    private void ClearFolder()
    {
        int children = UpgradeFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(UpgradeFolder.GetChild(i).gameObject);
        }
    }
    public void Close()
    {
        ClearFolder();
        gameObject.SetActive(false);
        State.GameManager.StrategyMode.Regenerate();
        State.GameManager.StrategyMode.Paused = false;
    }
}
