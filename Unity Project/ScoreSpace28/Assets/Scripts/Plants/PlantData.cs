using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantData : MonoBehaviour
{
    public PlantType Type = PlantType.MUSHROOM;
    [HideInInspector]
    public GameObject Pollinator = null;
    [HideInInspector]
    public float TotalScoreGained = 0;

}
