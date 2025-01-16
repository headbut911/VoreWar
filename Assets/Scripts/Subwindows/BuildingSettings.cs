using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSettings : MonoBehaviour
{
    public Toggle BuildingSystemEnabled;
    public InputField BuildingSystemTurnLockout;

    public TMP_Dropdown BuildingDropdown;

    public InputField WoodCost;
    public InputField NaturalMaterialsCost;
    public InputField PrefabsCost;
    public InputField StoneCost;
    public InputField OresCost;
    public InputField ManaStonesCost;
    public InputField GoldCost;

    public InputField ConstructionTurns;
    public InputField BuildLimit;

    public GameObject UpgradeEditPrefab;
    public Transform UpgradeFolder;

    public GameObject NoneSettings;
    public WorkCampSettings WorkCampSettings;


    List<BuildingUpgrade> buildingUpgrades = new List<BuildingUpgrade>();
    int currentDropdownValue = 0;

    public void DropdownUpdate()
    {
        switch (currentDropdownValue)
        {
            case 0: // None
                WorkCampSettings.gameObject.SetActive(false);
                break;
            case 1: // Work Camp
                SaveText(Config.BuildCon.WorkCampResources);
                Config.BuildCon.WorkCampGold = int.TryParse(GoldCost.text, out int g) ? g: 15;
                Config.BuildCon.WorkCampBuildTime = int.TryParse(ConstructionTurns.text, out int ct) ? ct : 2;
                Config.BuildCon.WorkCampBuildLimit = int.TryParse(BuildLimit.text, out int bl) ? bl: -1;
                WorkCampSettings.gameObject.SetActive(false);
                WorkCampSettings.Save();
                break;
            default:
                break;
        }

        buildingUpgrades.Clear();
        ClearFolder();

        switch (BuildingDropdown.value)
        {
            case 0: // None
                break;
            case 1: // Work Camp
                WorkCamp();
                break;
            default:
                break;
        }
        LoadUpgrades();
        currentDropdownValue = BuildingDropdown.value;
    }

    private void SetText(ConstructionResources resources)
    {
        WoodCost.text = resources.Wood.ToString();
        StoneCost.text = resources.Wood.ToString();
        WoodCost.text = resources.Stone.ToString();
        NaturalMaterialsCost.text = resources.NaturalMaterials.ToString();
        OresCost.text = resources.Ores.ToString();
        PrefabsCost.text = resources.Prefabs.ToString();
        ManaStonesCost.text = resources.ManaStones.ToString();
    }
    private void SaveText(ConstructionResources resources)
    {
        resources.SetResources(
            int.TryParse(WoodCost.text, out int wc) ? wc : 0,
            int.TryParse(StoneCost.text, out int sc) ? sc : 0,
            int.TryParse(NaturalMaterialsCost.text, out int nmc) ? nmc : 0,
            int.TryParse(OresCost.text, out int oc) ? oc : 0,
            int.TryParse(PrefabsCost.text, out int pc) ? pc : 0,
            int.TryParse(ManaStonesCost.text, out int msc) ? msc : 0
            );
    }

    private void LoadUpgrades()
    {
        foreach (var upgrade in buildingUpgrades)
        {
            var obj = Instantiate(UpgradeEditPrefab, UpgradeFolder);
            var currentPrefab = obj.GetComponent<UpgradeEditorPrefab>();
            currentPrefab.UpgradeTurns.text = upgrade.upgradeTime.ToString();
            currentPrefab.UpgradeName.text = upgrade.Name.ToString();
            currentPrefab.UpgradeDesc.text = upgrade.Desc.ToString();
            currentPrefab.GoldCost.text = upgrade.GoldCost.ToString();
            currentPrefab.WoodCost.text = upgrade.ResourceToUpgrade.Wood.ToString();
            currentPrefab.NaturalMaterialsCost.text = upgrade.ResourceToUpgrade.NaturalMaterials.ToString();
            currentPrefab.PrefabsCost.text = upgrade.ResourceToUpgrade.Prefabs.ToString();
            currentPrefab.StoneCost.text = upgrade.ResourceToUpgrade.Stone.ToString();
            currentPrefab.OresCost.text = upgrade.ResourceToUpgrade.Ores.ToString();
            currentPrefab.ManaStonesCost.text = upgrade.ResourceToUpgrade.ManaStones.ToString();
            currentPrefab.AssociatedUpgrade = upgrade;
        }

    }
    private void SaveUpgrades()
    {
        int children = UpgradeFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            var currentPrefab = UpgradeFolder.GetChild(i).gameObject.GetComponent<UpgradeEditorPrefab>();
            currentPrefab.AssociatedUpgrade.upgradeTime = int.TryParse(currentPrefab.UpgradeTurns.text, out int ut) ? ut : 2;
            currentPrefab.AssociatedUpgrade.GoldCost = int.TryParse(currentPrefab.GoldCost.text, out int gc) ? gc : 50;
            currentPrefab.AssociatedUpgrade.ResourceToUpgrade.Wood = int.TryParse(currentPrefab.WoodCost.text, out int wc) ? wc : 50;
            currentPrefab.AssociatedUpgrade.ResourceToUpgrade.NaturalMaterials = int.TryParse(currentPrefab.NaturalMaterialsCost.text, out int nmc) ? nmc : 50;
            currentPrefab.AssociatedUpgrade.ResourceToUpgrade.Prefabs = int.TryParse(currentPrefab.PrefabsCost.text, out int pc) ? pc : 50;
            currentPrefab.AssociatedUpgrade.ResourceToUpgrade.Stone = int.TryParse(currentPrefab.StoneCost.text, out int sc) ? sc : 50;
            currentPrefab.AssociatedUpgrade.ResourceToUpgrade.Ores = int.TryParse(currentPrefab.OresCost.text, out int oc) ? oc : 50;
            currentPrefab.AssociatedUpgrade.ResourceToUpgrade.ManaStones = int.TryParse(currentPrefab.ManaStonesCost.text, out int msc) ? msc : 50;

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

    private void WorkCamp()
    {
        SetText(Config.BuildCon.WorkCampResources);
        GoldCost.text = Config.BuildCon.WorkCampGold.ToString();
        ConstructionTurns.text = Config.BuildCon.WorkCampBuildTime.ToString();
        BuildLimit.text = Config.BuildCon.WorkCampBuildLimit.ToString();
        WorkCampSettings.Load();
        buildingUpgrades.Add(Config.WorkCampImproveUpgrade);
        buildingUpgrades.Add(Config.WorkCampMerchantUpgrade);
        buildingUpgrades.Add(Config.WorkCampTradeUpgrade);
        WorkCampSettings.gameObject.SetActive(true);
    }

    internal void HardSave()
    {
        
    }
}
