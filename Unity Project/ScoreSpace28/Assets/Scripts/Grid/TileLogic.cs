using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic : MonoBehaviour
{
    public enum PlantAlignment { CENTER_BOTTOM, CENTER_CENTER, CENTER_TOP }

    public float PlantScaleLerpSpeed = 8.0f;
    public float PlantScaleSnapDist = 0.05f;

    public PlantAlignment Alignment = PlantAlignment.CENTER_BOTTOM;

    public List<PlantType> AllowedPlants = new List<PlantType>();

    public GameObject TooltipPrefab = null;
    public Vector3 TooltipOffset = new Vector3(2, 0, 0);

    Vector3 plantNormalSize = Vector3.one;
    Vector3 plantPosOffset = Vector3.zero;
    Transform plant = null;
    GameObject currTooltip = null;

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
        if (currTooltip)
        {
            DestroyTooltip();
        }

        currTooltip = Instantiate(TooltipPrefab, GameObject.Find("Canvas").transform);

        // placing left or right based on whether x is + or - should cover all side edge cases
        currTooltip.transform.position = transform.position + (transform.position.x < 0.0f ? -TooltipOffset : TooltipOffset);
        // position should consider canvas ceiling and floor (but the tooltip should do this itself, it needs to figure out how big it is first)
        // send the tooltip data about our plant if we have one
    }

    public void DestroyTooltip()
    {
        // Later on we will probably want to have some kind of "closing" animation, or something else to make it not instant (maybe a fade-out?)
        Destroy(currTooltip);
        currTooltip = null;
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
