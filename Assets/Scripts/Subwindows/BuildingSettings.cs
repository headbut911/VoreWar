using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
    public LumberSiteSettings LumberSiteSettings;


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
                SaveText(Config.BuildConfig.WorkCamp.Resources);
                Config.BuildConfig.WorkCamp.Gold = int.TryParse(GoldCost.text, out int wcg) ? wcg : 15;
                Config.BuildConfig.WorkCamp.BuildTime = int.TryParse(ConstructionTurns.text, out int wcct) ? wcct : 2;
                Config.BuildConfig.WorkCamp.BuildLimit = int.TryParse(BuildLimit.text, out int wcbl) ? wcbl : -1;
                WorkCampSettings.gameObject.SetActive(false);
                WorkCampSettings.Save();
                break;
            case 2: // Lumber Site
                SaveText(Config.BuildConfig.LumberSite.Resources);
                Config.BuildConfig.LumberSite.Gold = int.TryParse(GoldCost.text, out int lsg) ? lsg : 15;
                Config.BuildConfig.LumberSite.BuildTime = int.TryParse(ConstructionTurns.text, out int lsct) ? lsct : 2;
                Config.BuildConfig.LumberSite.BuildLimit = int.TryParse(BuildLimit.text, out int lsbl) ? lsbl : -1;
                LumberSiteSettings.gameObject.SetActive(false);
                LumberSiteSettings.Save();
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
            case 2: // Work Camp
                LumberSite();
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
        SetText(Config.BuildConfig.WorkCamp.Resources);
        GoldCost.text = Config.BuildConfig.WorkCamp.Gold.ToString();
        ConstructionTurns.text = Config.BuildConfig.WorkCamp.BuildTime.ToString();
        BuildLimit.text = Config.BuildConfig.WorkCamp.BuildLimit.ToString();
        WorkCampSettings.Load();
        buildingUpgrades.Add(Config.BuildConfig.WorkCampImproveUpgrade);
        buildingUpgrades.Add(Config.BuildConfig.WorkCampMerchantUpgrade);
        buildingUpgrades.Add(Config.BuildConfig.WorkCampTradeUpgrade);
        WorkCampSettings.gameObject.SetActive(true);
    }
    
    private void LumberSite()
    {
        SetText(Config.BuildConfig.LumberSite.Resources);
        GoldCost.text = Config.BuildConfig.LumberSite.Gold.ToString();
        ConstructionTurns.text = Config.BuildConfig.LumberSite.BuildTime.ToString();
        BuildLimit.text = Config.BuildConfig.LumberSite.BuildLimit.ToString();
        LumberSiteSettings.Load();
        buildingUpgrades.Add(Config.BuildConfig.LumberSiteCarpenterUpgrade);
        buildingUpgrades.Add(Config.BuildConfig.LumberSiteGreenHouseUpgrade);
        buildingUpgrades.Add(Config.BuildConfig.LumberSiteLodgeUpgrade);
        LumberSiteSettings.gameObject.SetActive(true);
    }

    internal void HardSave()
    {
        var rootObject = new RootObject();

        rootObject.BuildingSystemEnabled = Config.BuildConfig.BuildingSystemEnabled;
        rootObject.BuildingSystemTurnLockout = Config.BuildConfig.BuildingSystemTurnLockout;

        rootObject.workCamp = new WorkCampTempClass(Config.BuildConfig.WorkCamp, Config.BuildConfig.WorkCampGoldPerTurn);
        rootObject.workCamp.stock = new ConstructionResourcesTempClass(Config.BuildConfig.WorkCampTurnStock);
        rootObject.workCamp.price = new ConstructionResourcesTempClass(Config.BuildConfig.WorkCampItemPrice);
        rootObject.workCamp.tradeUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.WorkCampTradeUpgrade);
        rootObject.workCamp.merchantUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.WorkCampMerchantUpgrade);
        rootObject.workCamp.improveUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.WorkCampImproveUpgrade);

        rootObject.lumberSite = new LumberCampTempClass(Config.BuildConfig.LumberSite, Config.BuildConfig.LumberSiteWorkerCap);
        rootObject.lumberSite.lodgeUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.LumberSiteLodgeUpgrade);
        rootObject.lumberSite.greenhouseUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.LumberSiteGreenHouseUpgrade);
        rootObject.lumberSite.carpenterUpgrade = new BuildingUpgradeTempClass(Config.BuildConfig.LumberSiteCarpenterUpgrade);

        using (StreamWriter file = new StreamWriter($"{State.StorageDirectory}buildingConfig.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, rootObject);

        }

    }
    internal void HardLoad()
    {
        string json = File.ReadAllText($"{State.StorageDirectory}buildingConfig.json");
        JObject results = new JObject();
        try
        {
            results = JObject.Parse(json);
        }
        catch (Exception)
        {
            Debug.Log("Empty Building Settings JSON");
            return;
        }

        Config.BuildConfig.BuildingSystemEnabled = results["BuildingSystemEnabled"].ToObject<bool>();
        Config.BuildConfig.BuildingSystemTurnLockout = int.TryParse(results["BuildingSystemTurnLockout"].ToString(), out int wcbstl) ? wcbstl : 1;

        LoadBuilding("workCamp", Config.BuildConfig.WorkCamp);
        Config.BuildConfig.WorkCampGoldPerTurn = results["workCamp"]["goldPerTurn"].ToObject<int>();
        LoadResource("workCamp", "stock", Config.BuildConfig.WorkCampTurnStock);
        LoadResource("workCamp", "price", Config.BuildConfig.WorkCampItemPrice);
        LoadUpgrade("workCamp", "tradeUpgrade", Config.BuildConfig.WorkCampTradeUpgrade);
        LoadUpgrade("workCamp", "merchantUpgrade", Config.BuildConfig.WorkCampMerchantUpgrade);
        LoadUpgrade("workCamp", "improveUpgrade", Config.BuildConfig.WorkCampImproveUpgrade);

        LoadBuilding("lumberSite", Config.BuildConfig.WorkCamp);
        Config.BuildConfig.LumberSiteWorkerCap = results["lumberSite"]["workerCap"].ToObject<int>();


        void LoadBuilding(string buildingName, GeneralBuildingConfig BuildingObect)
        {
            BuildingObect.Gold = results[buildingName]["standardInfo"]["gold"].ToObject<int>();
            BuildingObect.BuildTime = results[buildingName]["standardInfo"]["time"].ToObject<int>();
            BuildingObect.BuildLimit = results[buildingName]["standardInfo"]["limit"].ToObject<int>();
            BuildingObect.AICanBuild = results[buildingName]["standardInfo"]["aicanbuild"].ToObject<bool>();
            BuildingObect.Resources.SetResources(
                results[buildingName]["standardInfo"]["resource"]["Wood"].ToObject<int>(),
                results[buildingName]["standardInfo"]["resource"]["Stone"].ToObject<int>(),
                results[buildingName]["standardInfo"]["resource"]["NaturalMaterials"].ToObject<int>(),
                results[buildingName]["standardInfo"]["resource"]["Ores"].ToObject<int>(),
                results[buildingName]["standardInfo"]["resource"]["Prefabs"].ToObject<int>(),
                results[buildingName]["standardInfo"]["resource"]["ManaStones"].ToObject<int>());
        }

        void LoadResource(string building, string substring, ConstructionResources resources)
        {
            resources.SetResources(
                results[building][substring]["Wood"].ToObject<int>(),
                results[building][substring]["Stone"].ToObject<int>(),
                results[building][substring]["NaturalMaterials"].ToObject<int>(),
                results[building][substring]["Ores"].ToObject<int>(),
                results[building][substring]["Prefabs"].ToObject<int>(),
                results[building][substring]["ManaStones"].ToObject<int>());
        }

        void LoadUpgrade(string building, string substring, BuildingUpgrade upgrade)
        {
            upgrade.GoldCost = results[building][substring]["gold"].ToObject<int>();
            upgrade.upgradeTime = results[building][substring]["time"].ToObject<int>();
            upgrade.ResourceToUpgrade.SetResources(
                results[building][substring]["resource"]["Wood"].ToObject<int>(),
                results[building][substring]["resource"]["Stone"].ToObject<int>(),
                results[building][substring]["resource"]["NaturalMaterials"].ToObject<int>(),
                results[building][substring]["resource"]["Ores"].ToObject<int>(),
                results[building][substring]["resource"]["Prefabs"].ToObject<int>(),
                results[building][substring]["resource"]["ManaStones"].ToObject<int>());
        }

    }



    class RootObject
    {
        public bool BuildingSystemEnabled { get; set; }
        public int BuildingSystemTurnLockout { get; set; }
        public WorkCampTempClass workCamp { get; set; }
        public LumberCampTempClass lumberSite { get; set; }

    }

    class BuildingStandardInherit
    {
        public BuildingStandardTempClass standardInfo { get; set; }
    }

    class BuildingStandardTempClass
    {
        public int time { get; set; }
        public int gold { get; set; }
        public int limit { get; set; }
        public bool aicanbuild { get; set; }
        public ConstructionResourcesTempClass resource { get; set; }

        internal BuildingStandardTempClass(int time, int gold, int limit, bool aibuild, ConstructionResources resource)
        {
            this.time = time;
            this.gold = gold;
            this.limit = limit;
            this.aicanbuild = aibuild;
            this.resource = new ConstructionResourcesTempClass(resource);
        }
    }

    class WorkCampTempClass : BuildingStandardInherit
    {
        public int goldPerTurn { get; set; }
        public ConstructionResourcesTempClass stock { get; set; }
        public ConstructionResourcesTempClass price { get; set; }
        public BuildingUpgradeTempClass tradeUpgrade { get; set; }
        public BuildingUpgradeTempClass merchantUpgrade { get; set; }
        public BuildingUpgradeTempClass improveUpgrade { get; set; }
        internal WorkCampTempClass(GeneralBuildingConfig configClass, int goldPerTurn)
        {
            standardInfo = new BuildingStandardTempClass(configClass.BuildTime, configClass.Gold, configClass.BuildLimit, configClass.AICanBuild, configClass.Resources);
            this.goldPerTurn = goldPerTurn;
        }
    }
    class LumberCampTempClass : BuildingStandardInherit
    {
        public int workerCap { get; set; }
        public BuildingUpgradeTempClass lodgeUpgrade { get; set; }
        public BuildingUpgradeTempClass greenhouseUpgrade { get; set; }
        public BuildingUpgradeTempClass carpenterUpgrade { get; set; }
        internal LumberCampTempClass(GeneralBuildingConfig configClass, int cap)
        {
            standardInfo = new BuildingStandardTempClass(configClass.BuildTime, configClass.Gold, configClass.BuildLimit, configClass.AICanBuild, configClass.Resources);
            this.workerCap = cap;
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
