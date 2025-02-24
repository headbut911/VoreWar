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
    BlackMagicTower BlackMagicTower;

    public void Open(ConstructibleBuilding building)
    {
        BlackMagicTower = (BlackMagicTower)building;

    }
}