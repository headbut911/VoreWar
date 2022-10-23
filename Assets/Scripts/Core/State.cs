﻿using OdinSerializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public static class State
{
    static int saveErrors = 0;
    public const string Version = "39";
    public static World World;
    public static Rand Rand = new Rand();
    public static NameGenerator NameGen;
    public static GameManager GameManager;
    public static AssimilateList AssimilateList;

    internal static EventList EventList;

    internal static RaceSettings RaceSettings;

    public static bool TutorialMode;
    public static bool Warned = false;

    public static string SaveDirectory;
    public static string StorageDirectory;
    public static string MapDirectory;

    public static int RaceSlot;
    public static string RaceSaveDataName;

    static State()
    {
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            SaveDirectory = Application.persistentDataPath + $"Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar;
            MapDirectory = Application.persistentDataPath + $"Maps{Path.DirectorySeparatorChar}";
        }
        else
        {
            SaveDirectory = $"UserData{Path.DirectorySeparatorChar}Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = $"UserData{Path.DirectorySeparatorChar}";
            MapDirectory = $"UserData{Path.DirectorySeparatorChar}Maps{Path.DirectorySeparatorChar}";
        }
        try
        {
            Directory.CreateDirectory(StorageDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(MapDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(SaveDirectory.TrimEnd(new char[] { '\\', '/' }));
        }
        catch
        {
            SaveDirectory = Application.persistentDataPath + $"Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar;
            MapDirectory = Application.persistentDataPath + $"Maps{Path.DirectorySeparatorChar}";
            Directory.CreateDirectory(StorageDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(MapDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(SaveDirectory.TrimEnd(new char[] { '\\', '/' }));
        }


        try
        {
            if (File.Exists($"{StorageDirectory}males.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}males.txt", $"{StorageDirectory}males.txt");
            if (File.Exists($"{StorageDirectory}females.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}females.txt", $"{StorageDirectory}females.txt");
            if (File.Exists($"{StorageDirectory}monsters.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}monsters.txt", $"{StorageDirectory}monsters.txt");
            if (File.Exists($"{StorageDirectory}events.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}events.txt", $"{StorageDirectory}events.txt");
            if (File.Exists($"{StorageDirectory}armyNames.txt") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}armyNames.txt", $"{StorageDirectory}armyNames.txt");
        }
        catch
        {
            Debug.Log("Initial setup failed!");
        }

        FlagLoader.FlagLoader flagLoader = new FlagLoader.FlagLoader();
        flagLoader.LoadFlags();
        NameGen = new NameGenerator();
        EventList = new EventList();
        AssimilateList = new AssimilateList();
    }

    public static void SaveEditedRaces()
    {
        try
        {
            byte[] bytes = SerializationUtility.SerializeValue(RaceSettings, DataFormat.Binary);
            File.WriteAllBytes(RaceSaveDataName, bytes);
        }
        catch
        {
            Debug.LogWarning("Failed to properly save edited races!");
        }
    }

    public static void LoadRaceData()
    {
        RaceSlot = PlayerPrefs.GetInt("RaceEditorSlot", 1);
        ChangeRaceSlotUsed(RaceSlot);
    }

    public static void ChangeRaceSlotUsed(int num)
    {
        RaceSlot = num;
        PlayerPrefs.SetInt("RaceEditorSlot", num);
        if (RaceSlot <= 1)
            RaceSaveDataName = $"{StorageDirectory}EditedRaces.dat";
        else if (RaceSlot == 2)
            RaceSaveDataName = $"{StorageDirectory}EditedRaces2.dat";
        else if (RaceSlot == 3)
            RaceSaveDataName = $"{StorageDirectory}EditedRaces3.dat";
        LoadEditedRaces();
    }

    public static void LoadEditedRaces()
    {
        try
        {
            if (File.Exists(RaceSaveDataName))
            {
                byte[] bytes = File.ReadAllBytes(RaceSaveDataName);
                RaceSettings = SerializationUtility.DeserializeValue<RaceSettings>(bytes, DataFormat.Binary);
                GameManager.Start_Mode.miscText.text = "Successfully read race settings";
                RaceSettings.Sanitize();
            }
            else
            {
                RaceSettings = new RaceSettings();
                GameManager.Start_Mode.miscText.text = "No modified race settings found, using default";
            }
        }
        catch
        {
            RaceSettings = new RaceSettings();
            GameManager.Start_Mode.miscText.text = "Failed to properly read race settings";
        }
    }


    public static void Save(string filename)
    {
        try
        {
            for (int i = 0; i < 3; i++)
            {
                if (filename.EndsWith("/") || filename.EndsWith("\\"))
                    filename = filename.Remove(filename.Length - 1, 1);
                else
                    break;
            }

            World.SaveVersion = Version;
            if (GameManager.CurrentScene == GameManager.TacticalMode)
            {
                GameManager.CameraController.SaveTacticalCamera();
                World.TacticalData = GameManager.TacticalMode.Export();
            }
            else
                World.TacticalData = null;
            byte[] bytes = SerializationUtility.SerializeValue(World, DataFormat.Binary);
            File.WriteAllBytes(filename, bytes);
        }
        catch
        {
            saveErrors++;
            if (saveErrors < 3)
            {
                GameManager.CreateMessageBox($"Unable to save properly, {filename} didn't work (will only warn 3 times in a single session)");
            }
            else if (saveErrors == 3)
            {
                GameManager.CreateMessageBox($"Unable to save properly, {filename} didn't work (will no longer warn you this session)");
            }

        }

    }

    public static World PreviewSave(string filename)
    {
        if (filename.EndsWith("/") || filename.EndsWith("\\"))
            filename = filename.Remove(filename.Length - 1, 1);
        if (!File.Exists(filename))
        {
            return null;
        }
        World tempWorld;
        try
        {
            byte[] bytes = File.ReadAllBytes(filename);
            tempWorld = SerializationUtility.DeserializeValue<World>(bytes, DataFormat.Binary);
            return tempWorld;

        }
        catch (Exception)
        {
            return null;
        }
    }

    public static void Load(string filename, bool tutorial = false)
    {
        if (filename.EndsWith("/") || filename.EndsWith("\\"))
            filename = filename.Remove(filename.Length - 1, 1);
        if (!File.Exists(filename))
        {
            GameManager.CreateMessageBox("Couldn't find the saved file");
            return;
        }
        try
        {
            GameManager.StrategyMode.ClearData();
            GameManager.StrategyMode.CleanUpLingeringWindows();
            if (tutorial == false)
                GameManager.SwitchToMainMenu();
            byte[] bytes = File.ReadAllBytes(filename);
            World = SerializationUtility.DeserializeValue<World>(bytes, DataFormat.Binary);

            if (World.Empires != null)
            {
                World.MainEmpires = World.Empires.ToList();
                World.RefreshEmpires();
            }

            if (tutorial)
            {
                var catEmp = World.GetEmpireOfRace(Race.Cats);
                var impEmp = World.GetEmpireOfRace(Race.Imps);

                catEmp.Armies[0].SetEmpire(catEmp);
                impEmp.Armies[0].SetEmpire(impEmp);
                TutorialMode = true;
            }
            else
            {
                TutorialMode = false;
            }


            VillageBuildingList.SetBuildings(World.crazyBuildings);
            if (string.Compare(World.SaveVersion, "12") < 0)
            {
                World = null;
                GameManager.CreateMessageBox("This save file is from before version 12.  I took the liberty of doing a clean sweep when I added the new garrisons to improve the code quality. Sorry.  You can still load .map files from before version 12 though.");
                return;
            }
            Config.World = World.ConfigStorage;
            GameManager.Menu.Options.LoadFromStored();
            GameManager.Menu.CheatMenu.LoadFromStored();

            if (World.MercenaryHouses == null)
                World.MercenaryHouses = new MercenaryHouse[0];

            foreach (MercenaryHouse house in World.MercenaryHouses)
            {
                if (house.Mercenaries != null)
                {
                    foreach (var merc in house.Mercenaries)
                    {
                        merc.Unit.InitializeTraits();
                    }
                }
            }



            if (World.Claimables == null)
                World.Claimables = new ClaimableBuilding[0];

            //Always runs for new versions           
            if (World.SaveVersion != Version && World.AllActiveEmpires != null)
            {
                if (World.GetEmpireOfSide(700) == null)
                {
                    World.MainEmpires.Add(new Empire(new Empire.ConstructionArgs(700, Color.red, new Color(.6f, 0, 0), 5, StrategyAIType.Basic, TacticalAIType.Full, 700, 16, 16)));
                    World.RefreshEmpires();
                }
                else
                {
                    World.GetEmpireOfSide(700).Name = "Rebels";
                    if (World.EmpireOrder.Where(s => s.Side == 700).Any() == false)
                        World.EmpireOrder.Add(World.GetEmpireOfSide(700));
                }
                if (World.GetEmpireOfSide(701) == null)
                {
                    World.MainEmpires.Add(new Empire(new Empire.ConstructionArgs(701, Color.red, new Color(.6f, 0, 0), 7, StrategyAIType.Basic, TacticalAIType.Full, 701, 16, 16)));
                    World.RefreshEmpires();
                }
                else
                {
                    World.GetEmpireOfSide(701).Name = "Bandits";
                }
                if (string.Compare(World.SaveVersion, "30") < 0)
                {
                    if (World.AllActiveEmpires != null)
                    {
                        foreach (Village village in World.Villages)
                        {
                            village.ConvertToMultiRace();
                        }

                    }
                }
                ItemRepository newRepo = new ItemRepository();
                World.ItemRepository = newRepo;

                foreach (var unit in StrategicUtilities.GetAllUnits())
                {
                    unit.UpdateItems(newRepo);
                    unit.ReloadTraits();
                }
                foreach (Empire empire in World.AllActiveEmpires)
                {
                    foreach (Army army in empire.Armies)
                    {
                        foreach (Unit unit in army.Units)
                        {

                            if (unit.Side != army.Side)
                                unit.Side = army.Side;
                            if (unit.BodySize < 0) //Can take this out later, was a fix for 14H
                                unit.BodySize = 0;
                        }
                    }
                }


                if (Config.MaxSpellLevelDrop == 0)
                    Config.World.MaxSpellLevelDrop = 4;
            }



            if (string.Compare(World.SaveVersion, "18") < 0)
            {
                if (Config.LeaderLossLevels == 0)
                    Config.World.LeaderLossLevels = 1;
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.Race == Race.Goblins) //Re-randomize because the number of options has dropped
                            unit.EyeType = Rand.Next(3);
                    }
                }
            }

            if (string.Compare(World.SaveVersion, "20") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        int oldSpeed = unit.GetStatBase(Stat.Mind);
                        unit.ModifyStat(Stat.Agility, Math.Max(oldSpeed - 10, 0));
                        unit.ModifyStat(Stat.Mind, unit.Level + 10 - oldSpeed);
                    }
                }
            }

            if (string.Compare(World.SaveVersion, "21") < 0)
            {
                if (World.Villages != null)
                {
                    foreach (Village village in World.Villages)
                    {
                        if (village.buildings.Contains(VillageBuilding.empty)) //Removes Sub pens
                        {
                            village.buildings.Remove(VillageBuilding.empty);
                        }
                    }
                }
                if (World.Relations != null)
                {
                    RelationsManager.ResetRelations();
                }
            }

            if (string.Compare(World.SaveVersion, "21D") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (empire.StrategicAI == null)
                            continue;
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                StrategicUtilities.SetAIClass(unit);
                            }
                        }
                    }

                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                if (unit.Race == Race.Lizards) //Adjustment for the added clothing item
                                    if (unit.ClothingType == 4)
                                        unit.ClothingType = 5;
                                    else if (unit.ClothingType == 5)
                                        unit.ClothingType = 6;
                            }
                        }
                    }
                }

            }

            if (string.Compare(World.SaveVersion, "22") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire emp in World.AllActiveEmpires)
                    {
                        emp.Name = emp.Race.ToString();
                    }
                }
            }

            if (string.Compare(World.SaveVersion, "26") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                if (unit.Race == Race.Lizards) //Adjustment for the added clothing item
                                    if (unit.ClothingType >= 5)
                                        unit.ClothingType++;
                            }
                        }
                    }
                }
            }

            if (string.Compare(World.SaveVersion, "26A") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                if (unit.Race == Race.Abakhanskya) //Adjustment for the added clothing item
                                {
                                    unit.FixedGear = true;
                                    unit.Items[0] = State.World.ItemRepository.GetSpecialItem(SpecialItems.AbakWeapon);
                                }

                            }
                        }
                    }
                }
            }

            if (string.Compare(World.SaveVersion, "26D") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (empire.Leader?.Race == Race.Bees)
                            empire.Leader.ClothingType = 6;
                    }
                }
            }

            if (string.Compare(World.SaveVersion, "27A") < 0)
            {
                Config.World.Toggles["Defections"] = true;
            }

            if (string.Compare(World.SaveVersion, "28") < 0)
            {
                Config.World.OralWeight = 40;
                Config.World.BreastWeight = 40;
                Config.World.CockWeight = 40;
                Config.World.TailWeight = 40;
                Config.World.UnbirthWeight = 40;
                Config.World.AnalWeight = 40;
            }

            if (string.Compare(World.SaveVersion, "28C") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            foreach (Unit unit in army.Units)
                            {
                                if (unit.Race == Race.Succubi)
                                {
                                    if (unit.ClothingType2 == 3)
                                        unit.ClothingType2 = 2;
                                }

                            }
                        }
                    }
                }
            }

            if (string.Compare(World.SaveVersion, "28C") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        foreach (Army army in empire.Armies)
                        {
                            army.NameArmy(empire);
                        }
                    }
                }
            }

            if (string.Compare(World.SaveVersion, "29") < 0)
            {
                World.ConfigStorage.OverallMonsterCapModifier = 1;
                World.ConfigStorage.OverallMonsterSpawnRateModifier = 1;
            }

            if (string.Compare(World.SaveVersion, "30F") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    var raceData = Races.GetRace(Race.Bees);
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.Race == Race.Bees)
                            raceData.RandomCustom(unit);
                    }
                }

            }

            if (string.Compare(World.SaveVersion, "30H") < 0)
            {
                Config.World.AutoSurrenderChance = 1;

            }

            if (string.Compare(World.SaveVersion, "31") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (empire.CapitalCity != null)
                            empire.ReplacedRace = empire.CapitalCity.OriginalRace;
                        else
                            empire.ReplacedRace = empire.Race;
                    }
                }
            }

            if (string.Compare(World.SaveVersion, "32") < 0)
            {
                World.ConfigStorage.StartingPopulation = 99999;
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (empire.StrategicAI != null && empire.StrategicAI is StrategicAI ai)
                        {
#pragma warning disable CS0612 // Type or member is obsolete
                            if (ai.strongerAI)
#pragma warning restore CS0612 // Type or member is obsolete
                                ai.CheatLevel = 1;
                        }
                        if (empire.CapitalCity != null)
                            empire.ReplacedRace = empire.CapitalCity.OriginalRace;
                        else
                            empire.ReplacedRace = empire.Race;
                    }
                }
            }

            if (string.Compare(World.SaveVersion, "34") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.Race == Race.Bats || unit.Race == Race.Equines)
                        {
                            unit.TotalRandomizeAppearance();
                        }
                    }
                }
            }


            if (string.Compare(World.SaveVersion, "34B") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        if (empire.CapitalCity != null)
                            empire.ReplacedRace = empire.CapitalCity.OriginalRace;
                        else
                            empire.ReplacedRace = empire.Race;
                    }
                }
            }

            if (string.Compare(World.SaveVersion, "35B") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    World.UpdateBanditLimits();
                }
            }

            if (string.Compare(World.SaveVersion, "37A") < 0)
            {
                foreach (var unit in StrategicUtilities.GetAllUnits())
                {
                    if (unit.HasVagina == false)
                    {
                        if (unit.HasBreasts && !unit.HasDick)
                            unit.HasVagina = true;
                        else if (!unit.HasBreasts && unit.HasDick)
                            unit.HasVagina = false;
                        else if (Config.World.GetValue("HermsCanUB"))
                            unit.HasVagina = true;
                        else
                            unit.HasVagina = false;
                    }
                }
            }

            if (string.Compare(World.SaveVersion, "38C") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.Pronouns == null)
                        {
                            unit.GeneratePronouns();
                        }
                    }

                }
                else
                {
                    foreach (var unit in World.TacticalData.units)
                    {
                        if (unit.Unit.Pronouns == null)
                        {
                            unit.Unit.GeneratePronouns();
                        }
                    }
                }
            }


            if (string.Compare(World.SaveVersion, "38G") < 0)
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.GetGender() == Gender.Hermaphrodite || unit.GetGender() == Gender.Gynomorph)
                        {
                            unit.HasVagina = Config.HermsCanUB;
                        }
                    }
                }
                
            }

            if (string.Compare(World.SaveVersion, "39") < 0)
            {
                World.ConfigStorage.FogDistance = 2;

                if (World.AllActiveEmpires != null)
                {
                    foreach (var unit in StrategicUtilities.GetAllUnits())
                    {
                        if (unit.Race == Race.Humans)
                        {
                            unit.RandomizeAppearance();
                        }
                    }
                }
            }

            if (World.AllActiveEmpires != null)
            {
                foreach (Empire emp in World.AllActiveEmpires)
                {
                    if (emp.Side > 300)
                        continue;
                    var raceFlags = RaceSettings.GetRaceTraits(emp.Race);
                    if (raceFlags != null)
                    {
                        if (raceFlags.Contains(Traits.Prey))
                            emp.CanVore = false;
                    }
                }

                foreach (Empire emp in World.MainEmpires)
                {
                    if (emp.Side > 300)
                        continue;
                    if (RaceSettings.Exists(emp.Race))
                    {
                        emp.BannerType = RaceSettings.Get(emp.Race).BannerType;
                    }
                    else
                        emp.BannerType = 0;
                }

                foreach (var unit in StrategicUtilities.GetAllUnits())
                {
                    unit.InitializeTraits();
                }
            }
            if (World.Villages != null)
            {
                foreach (var village in World.Villages)
                {
                    if (village.FarmCount <= 0) village.UpdateFarms(8);
                    village.UpdateNetBoosts();
                }
            }



            if (Config.World.ArmyMP == 0)
                Config.World.ArmyMP = 3;

            if (Config.World.MaxArmies == 0)
                Config.World.MaxArmies = 12;

            if (Config.World.VillagersPerFarm == 0)
                Config.World.VillagersPerFarm = 6;

            if (Config.World.SoftLevelCap == 0)
                Config.World.SoftLevelCap = 999999;

            if (Config.World.HardLevelCap == 0)
                Config.World.HardLevelCap = 999999;

            if (Config.World.GoldMineIncome == 0)
                Config.World.GoldMineIncome = 40;

            if (Config.World.TacticalTerrainFrequency == 0)
                Config.World.TacticalTerrainFrequency = 10;

            if (Config.World.TacticalWaterValue == 0)
                Config.World.TacticalWaterValue = .29f;

            bool pureTactical = false;
            if (World.MainEmpires != null) //Is the detector for a pure tactical game.
            {
                if (World.AllActiveEmpires != null)
                {
                    foreach (Empire empire in World.AllActiveEmpires)
                    {
                        empire.LoadFix(); //Compatibility Temporary fix to bridge the gap between versions
                    }
                }
                else
                {
                    foreach (Empire empire in World.MainEmpires)
                    {
                        empire.LoadFix(); //Compatibility Temporary fix to bridge the gap between versions
                    }
                }

                foreach (Empire empire in World.AllActiveEmpires)
                {
                    foreach (Army army in empire.Armies)
                    {
                        foreach (Unit unit in army.Units)
                        {
                            unit.ReloadTraits();
                        }
                    }
                }

                if (World.Relations == null)
                    RelationsManager.ResetRelations();
                GameManager.ClearPureTactical();
                GameManager.SwitchToStrategyMode();
                GameManager.StrategyMode.GenericSetup();
                GameManager.StrategyMode.CheckIfOnlyAIPlayers();

                MercenaryHouse.UpdateStaticStock();
            }
            else //If Pure Tactical
            {
                //These two lines are there to catch a pure tactical game with the summon spell.
                ItemRepository newRepo = new ItemRepository();
                World.ItemRepository = newRepo;

                Config.WatchAIBattles = true;
                pureTactical = true;
            }

            if (World.TacticalData != null)
            {
                GameManager.SwitchToTacticalOnLoadedGame();
                GameManager.TacticalMode.LoadData(World.TacticalData);
                if (pureTactical)
                {
                    GameManager.TacticalMode.RefreshPureTacticalTraits();
                    GameManager.TacticalMode.ForceUpdate();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            GameManager.CreateMessageBox("Encountered an error when trying to load the save");
            return;
        }


    }
}

