using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaggedTraitPrefab : MonoBehaviour
{
    public int id;
    public Text traitName;
    public Text traitTier;
    public Text description;
    public Text tags;

    public Button addButton;
    public Button removeButton;

    internal TaggedTrait taggedTrait;
}