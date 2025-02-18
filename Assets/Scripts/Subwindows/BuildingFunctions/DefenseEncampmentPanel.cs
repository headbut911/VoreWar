using MapObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefenseEncampmentPanel : MonoBehaviour
{

    public GameObject MendingHolder;
    public GameObject EnhanceHolder;
    public GameObject Core2Holder;
    public GameObject Core3Holder;
    
    public TextMeshProUGUI CurrentPotentialDowntime;
    public TextMeshProUGUI SlotOneDowntime;
    public TextMeshProUGUI SlotTwoDowntime;
    public TextMeshProUGUI SlotThreeDowntime;

    public TextMeshProUGUI BarrierMagnitude;
    public Slider BarrierMagnitudeSlider;
    public TextMeshProUGUI MendingMagnitude;
    public Slider MendingMagnitudeSlider;
    public TextMeshProUGUI EmpowerMagnitude;
    public Slider EmpowerMagnitudeSlider;

    public Toggle CoreProtection;

    BarrierTower BarrierTower;

    public void Open(ConstructibleBuilding building)
    {
        
    }

}
