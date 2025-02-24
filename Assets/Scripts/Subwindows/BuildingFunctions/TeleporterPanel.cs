using MapObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeleporterPanel : MonoBehaviour
{
    Teleporter Teleporter;

    public void Open(ConstructibleBuilding building)
    {
        Teleporter = (Teleporter)building;

    }
}