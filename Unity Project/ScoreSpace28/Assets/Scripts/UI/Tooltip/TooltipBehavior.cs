using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipData
{
    public List<string> TooltipStrings = new List<string>();
    public string PlantName = "";
    public int TotalScoreGained = 0;
}

public class TooltipBehavior : MonoBehaviour
{


    public float PaneWidth = 300.0f;

    // Set this in prefab editor
    public GameObject PaneBG = null;
    // Set this in prefab editor
    public GameObject PaneText = null;

    float left = 0;
    float leftY = 0;

    float right = 0;
    float rightY = 0;

    PlantData myPlant = null;

    void InitPanes()
    {
        if (myPlant == null)
        {
            return;
        }

        TooltipData data = myPlant.GetTooltipData();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitPanes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlant(PlantData plant)
    {
        myPlant = plant;
    }
}
