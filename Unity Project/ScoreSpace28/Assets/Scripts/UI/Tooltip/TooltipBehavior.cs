using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        int children = transform.childCount;
        for(int i = 0; i < children; ++i)
        {
            Transform c = transform.GetChild(i);
            if(c.gameObject != PaneBG && c.gameObject != PaneText)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
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
