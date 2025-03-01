using MapObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackMagicTowerPanel : MonoBehaviour
{
    public GameObject AfflictionHolder;

    public Button SacButton;
    public Button ResButton;

    public Button NecrosisButton;
    public Button ErrosionButton;
    public Button AgonyButton;
    public Button LethargyButton;
    public TextMeshProUGUI AfflictionDescription;

    public TextMeshProUGUI SoulPower;

    BlackMagicTower BlackMagicTower;
    public void Open(ConstructibleBuilding building)
    {
        BlackMagicTower = (BlackMagicTower)building;
        SacButton.interactable = false;
        ResButton.interactable = false;

        AfflictionHolder.SetActive(BlackMagicTower.afflictUpgrade.built);
        NecrosisButton.onClick.AddListenerOnce(() =>
        {
            UpdateButtonInteract();
            NecrosisButton.interactable = false;
            BlackMagicTower.Affliction = StatusEffectType.Necrosis;
        });
        ErrosionButton.onClick.AddListenerOnce(() =>
        {
            UpdateButtonInteract();
            ErrosionButton.interactable = false;
            BlackMagicTower.Affliction = StatusEffectType.Errosion;
        });
        AgonyButton.onClick.AddListenerOnce(() =>
        {
            UpdateButtonInteract();
            AgonyButton.interactable = false;
            BlackMagicTower.Affliction = StatusEffectType.Agony;
        });
        LethargyButton.onClick.AddListenerOnce(() =>
        {
            UpdateButtonInteract();
            LethargyButton.interactable = false;
            BlackMagicTower.Affliction = StatusEffectType.Lethargy;
        });

        UpdateButtonInteract();
    }

    public void UpdateButtonInteract()
    {
        NecrosisButton.interactable = true;
        ErrosionButton.interactable = true;
        AgonyButton.interactable = true;
        LethargyButton.interactable = true;
    }
}