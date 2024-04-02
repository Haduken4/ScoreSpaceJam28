using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic : MonoBehaviour
{
    public enum PlantAlignment { CENTER_BOTTOM, CENTER_CENTER, CENTER_TOP }

    public float PlantScaleLerpSpeed = 8.0f;
    public float PlantScaleSnapDist = 0.05f;

    //public Vector3 PlantLocalPos = new Vector3(0, 0, -1.0f);
    public PlantAlignment Alignment = PlantAlignment.CENTER_BOTTOM;

    public List<PlantType> AllowedPlants = new List<PlantType>();

    Vector3 plantNormalSize = Vector3.one;
    Vector3 plantPosOffset = Vector3.zero;
    Transform plant = null;

    [HideInInspector]
    public bool IsPlant = false;
    [HideInInspector]
    public float BaseScoreValue = 0;

    private void Update()
    {
        if(plant)
        {
            plant.localScale = Vector3.Lerp(plant.localScale, plantNormalSize, PlantScaleLerpSpeed * Time.deltaTime);
            if (Vector3.Distance(plantNormalSize, plant.localScale) < PlantScaleSnapDist)
            {
                plant.localScale = plantNormalSize;
            }
        }
    }

    public Transform GetPlant()
    {
        plant = null;
        if(transform.childCount != 0)
        {
            plant = transform.GetChild(0);
        }

        return plant;
    }

    public bool CanPlant(PlantType type)
    {
        if((AllowedPlants.Count == 0 || AllowedPlants.Contains(type)) && plant == null)
        {
            return true;
        }

        return false;
    }

    public void InitPlant(Vector3 posOffset)
    {
        GetPlant();
        if(plant)
        {
            plantNormalSize = plant.localScale;
            plantPosOffset = posOffset;
            plant.localPosition = CalcAlignedPosition();
        }
    }

    public void CreateTooltip()
    {

    }

    public void DestroyTooltip()
    {

    }

    Vector3 CalcAlignedPosition()
    {
        Vector3 pos = Vector3.zero;
        pos.z = -1;

        if(Alignment == PlantAlignment.CENTER_BOTTOM)
        {
            pos.y = plant.localScale.y / 2.0f;
        }

        return pos + plantPosOffset;
    }
}
