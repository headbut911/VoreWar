using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
                Config.BuildCon.WorkCampGold = int.TryParse(GoldCost.text, out int g) ? g : 15;
                Config.BuildCon.WorkCampBuildTime = int.TryParse(ConstructionTurns.text, out int ct) ? ct : 2;
                Config.BuildCon.WorkCampBuildLimit = int.TryParse(BuildLimit.text, out int bl) ? bl : -1;
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
        var rootObject = new RootObject();

        rootObject.BuildingSystemEnabled = Config.BuildCon.BuildingSystemEnabled;
        rootObject.BuildingSystemTurnLockout = Config.BuildCon.BuildingSystemTurnLockout;

        rootObject.workCamp = new WorkCampTempClass(Config.BuildCon.WorkCampBuildTime, Config.BuildCon.WorkCampGold, Config.BuildCon.WorkCampBuildLimit, Config.BuildCon.WorkCampGoldPerTurn, Config.BuildCon.WorkCampResources);
        rootObject.workCamp.stock = new ConstructionResourcesTempClass(Config.BuildCon.WorkCampTurnStock);
        rootObject.workCamp.price = new ConstructionResourcesTempClass(Config.BuildCon.WorkCampItemPrice);
        rootObject.workCamp.tradeUpgrade = new BuildingUpgradeTempClass(Config.BuildCon.WorkCampTradeUpgrade);
        rootObject.workCamp.merchantUpgrade = new BuildingUpgradeTempClass(Config.BuildCon.WorkCampMerchantUpgrade);
        rootObject.workCamp.improveUpgrade = new BuildingUpgradeTempClass(Config.BuildCon.WorkCampImproveUpgrade);

        using (StreamWriter file = new StreamWriter($"{State.StorageDirectory}buildingConfig.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, rootObject);

        }

    }
    internal void HardLoad()
    {
        string json = File.ReadAllText($"{State.StorageDirectory}buildingConfig.json");
        JObject results = JObject.Parse(json);

        Config.BuildCon.BuildingSystemEnabled = results["BuildingSystemEnabled"].ToObject<bool>();
        Config.BuildCon.BuildingSystemTurnLockout = int.TryParse(results["BuildingSystemTurnLockout"].ToString(), out int wcbstl) ? wcbstl : 1;

        Config.BuildCon.WorkCampGold = results["workCamp"]["gold"].ToObject<int>();
        Config.BuildCon.WorkCampBuildTime = results["workCamp"]["time"].ToObject<int>();
        Config.BuildCon.WorkCampBuildLimit = results["workCamp"]["limit"].ToObject<int>();
        Config.BuildCon.WorkCampGoldPerTurn = results["workCamp"]["goldPerTurn"].ToObject<int>();
        Config.BuildCon.WorkCampResources.SetResources(
            results["workCamp"]["resource"]["Wood"].ToObject<int>(),
            results["workCamp"]["resource"]["Stone"].ToObject<int>(),
            results["workCamp"]["resource"]["NaturalMaterials"].ToObject<int>(),
            results["workCamp"]["resource"]["Ores"].ToObject<int>(),
            results["workCamp"]["resource"]["Prefabs"].ToObject<int>(),
            results["workCamp"]["resource"]["ManaStones"].ToObject<int>());
        Config.BuildCon.WorkCampTurnStock.SetResources(
            results["workCamp"]["stock"]["Wood"].ToObject<int>(),
            results["workCamp"]["stock"]["Stone"].ToObject<int>(),
            results["workCamp"]["stock"]["NaturalMaterials"].ToObject<int>(),
            results["workCamp"]["stock"]["Ores"].ToObject<int>(),
            results["workCamp"]["stock"]["Prefabs"].ToObject<int>(),
            results["workCamp"]["stock"]["ManaStones"].ToObject<int>());
        Config.BuildCon.WorkCampItemPrice.SetResources(
            results["workCamp"]["price"]["Wood"].ToObject<int>(),
            results["workCamp"]["price"]["Stone"].ToObject<int>(),
            results["workCamp"]["price"]["NaturalMaterials"].ToObject<int>(),
            results["workCamp"]["price"]["Ores"].ToObject<int>(),
            results["workCamp"]["price"]["Prefabs"].ToObject<int>(),
            results["workCamp"]["price"]["ManaStones"].ToObject<int>());

        Config.BuildCon.WorkCampTradeUpgrade.GoldCost = results["workCamp"]["tradeUpgrade"]["gold"].ToObject<int>();
        Config.BuildCon.WorkCampTradeUpgrade.upgradeTime = results["workCamp"]["tradeUpgrade"]["time"].ToObject<int>();
        Config.BuildCon.WorkCampTradeUpgrade.ResourceToUpgrade.SetResources(
            results["workCamp"]["tradeUpgrade"]["resource"]["Wood"].ToObject<int>(),
            results["workCamp"]["tradeUpgrade"]["resource"]["Stone"].ToObject<int>(),
            results["workCamp"]["tradeUpgrade"]["resource"]["NaturalMaterials"].ToObject<int>(),
            results["workCamp"]["tradeUpgrade"]["resource"]["Ores"].ToObject<int>(),
            results["workCamp"]["tradeUpgrade"]["resource"]["Prefabs"].ToObject<int>(),
            results["workCamp"]["tradeUpgrade"]["resource"]["ManaStones"].ToObject<int>());

        Config.BuildCon.WorkCampMerchantUpgrade.GoldCost = results["workCamp"]["merchantUpgrade"]["gold"].ToObject<int>();
        Config.BuildCon.WorkCampMerchantUpgrade.upgradeTime = results["workCamp"]["merchantUpgrade"]["time"].ToObject<int>();
        Config.BuildCon.WorkCampMerchantUpgrade.ResourceToUpgrade.SetResources(
            results["workCamp"]["merchantUpgrade"]["resource"]["Wood"].ToObject<int>(),
            results["workCamp"]["merchantUpgrade"]["resource"]["Stone"].ToObject<int>(),
            results["workCamp"]["merchantUpgrade"]["resource"]["NaturalMaterials"].ToObject<int>(),
            results["workCamp"]["merchantUpgrade"]["resource"]["Ores"].ToObject<int>(),
            results["workCamp"]["merchantUpgrade"]["resource"]["Prefabs"].ToObject<int>(),
            results["workCamp"]["merchantUpgrade"]["resource"]["ManaStones"].ToObject<int>());

        Config.BuildCon.WorkCampImproveUpgrade.GoldCost = results["workCamp"]["improveUpgrade"]["gold"].ToObject<int>();
        Config.BuildCon.WorkCampImproveUpgrade.upgradeTime = results["workCamp"]["improveUpgrade"]["time"].ToObject<int>();
        Config.BuildCon.WorkCampImproveUpgrade.ResourceToUpgrade.SetResources(
            results["workCamp"]["improveUpgrade"]["resource"]["Wood"].ToObject<int>(),
            results["workCamp"]["improveUpgrade"]["resource"]["Stone"].ToObject<int>(),
            results["workCamp"]["improveUpgrade"]["resource"]["NaturalMaterials"].ToObject<int>(),
            results["workCamp"]["improveUpgrade"]["resource"]["Ores"].ToObject<int>(),
            results["workCamp"]["improveUpgrade"]["resource"]["Prefabs"].ToObject<int>(),
            results["workCamp"]["improveUpgrade"]["resource"]["ManaStones"].ToObject<int>());

    }

    class RootObject
    {
        public bool BuildingSystemEnabled { get; set; }
        public int BuildingSystemTurnLockout { get; set; }
        public WorkCampTempClass workCamp { get; set; }

    }

    class BuildingStandardTempClass
    {
        public int time { get; set; }
        public int gold { get; set; }
        public int limit { get; set; }
        public ConstructionResourcesTempClass resource { get; set; }
    }

    class WorkCampTempClass : BuildingStandardTempClass
    {
        public int goldPerTurn { get; set; }
        public ConstructionResourcesTempClass stock { get; set; }
        public ConstructionResourcesTempClass price { get; set; }
        public BuildingUpgradeTempClass tradeUpgrade { get; set; }
        public BuildingUpgradeTempClass merchantUpgrade { get; set; }
        public BuildingUpgradeTempClass improveUpgrade { get; set; }
        internal WorkCampTempClass(int time, int gold, int limit, int goldTurn, ConstructionResources resource)
        {
            this.time = time;
            this.gold = gold;
            this.limit = limit;
            this.goldPerTurn = goldTurn;
            this.resource = new ConstructionResourcesTempClass(resource);
        }
    }
    class BuildingUpgradeTempClass
    {
        public string name { get; set; }
        public string desc { get; set; }
        public int time { get; set; }
        public int gold { get; set; }
        public ConstructionResourcesTempClass resource { get; set; }
        internal BuildingUpgradeTempClass(BuildingUpgrade buildingUpgrade)
        {
            this.name = buildingUpgrade.Name;
            this.desc = buildingUpgrade.Desc;
            this.time = buildingUpgrade.upgradeTime;
            this.gold = buildingUpgrade.GoldCost;
            this.resource = new ConstructionResourcesTempClass(buildingUpgrade.ResourceToUpgrade);
        }
    }
    class ConstructionResourcesTempClass
    {
        public int Wood { get; set; }
        public int Stone { get; set; }
        public int NaturalMaterials { get; set; }
        public int Ores { get; set; }
        public int Prefabs { get; set; }
        public int ManaStones { get; set; }

        internal ConstructionResourcesTempClass(ConstructionResources incoming)
        {
            Wood = incoming.Wood;
            Stone = incoming.Stone;
            NaturalMaterials = incoming.NaturalMaterials;
            Ores = incoming.Ores;
            Prefabs = incoming.Prefabs;
            ManaStones = incoming.ManaStones;
        }
    }
}
