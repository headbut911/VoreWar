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
        if (PossibleBuildings == null)
        {
            PossibleBuildings = new List<BuildingPrefab>();
            for (int i = 0; i < Config.NumberOfBuildings; i++)
            {
                PossibleBuildings.Add(Instantiate(BuildItemPrefab, BuildingFolder).GetComponent<BuildingPrefab>());
            }
        }
        Refresh();

    }

    public void Refresh()
    {

    }

    public void CreateDiplomacyReport()
    {
        StringBuilder sb = new StringBuilder();
        List<string> Allies = new List<string>();
        List<string> Neutral = new List<string>();
        List<string> Enemies = new List<string>();
        List<Empire> list = State.World.MainEmpires.Where(s => s.KnockedOut == false).ToList();
        //list.Append(State.World.GetEmpireOfRace(Race.Goblins));

        foreach (Empire emp in list)
        {
            Allies.Clear();
            Neutral.Clear();
            Enemies.Clear();

            foreach (Empire emp2 in list)
            {
                if (emp == emp2)
                    continue;
                if (emp.Side >= 700 || emp2.Side >= 700)
                    continue;
                var relation = RelationsManager.GetRelation(emp.Side, emp2.Side);
                switch (relation.Type)
                {
                    case RelationState.Neutral:
                        Neutral.Add(emp2.Name.ToString());
                        break;
                    case RelationState.Allied:
                        Allies.Add(emp2.Name.ToString());
                        break;
                    case RelationState.Enemies:
                        Enemies.Add(emp2.Name.ToString());
                        break;
                }
            }
            string allies = Allies.Count() > 0 ? $"Allies:<color=blue> {string.Join(", ", Allies)}</color>" : "";
            string neutrals = Neutral.Count() > 0 ? $"Neutral: {string.Join(", ", Neutral)}" : "";
            string enemies;
            if (Enemies.Count > 6)
                enemies = "Enemies: <color=red>all others</color>";
            else
                enemies = Enemies.Count() > 0 ? $"Enemies: <color=red>{string.Join(", ", Enemies)}</color>" : "";

            sb.AppendLine($"{emp.Name} - {allies} {neutrals} {enemies} ");
        }
        State.GameManager.CreateFullScreenMessageBox(sb.ToString());
    }

    public void Close()
    {
        gameObject.SetActive(false);
        State.GameManager.StrategyMode.Paused = pausedState;
    }
}
