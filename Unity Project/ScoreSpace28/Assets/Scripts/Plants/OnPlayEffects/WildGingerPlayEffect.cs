using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildGingerPlayEffect : OnPlayEffect
{
    public PlantType SubtractType = PlantType.ROOT;
    public float SubtractValue = 2;

    protected override void GameplayEffect()
    {
        List<GameObject> neighbors = gm.GetAdjacentTiles(transform.parent.gameObject);

        foreach (GameObject obj in neighbors)
        {
            TileLogic tl = obj.GetComponent<TileLogic>();
            Transform plant = tl.GetPlant();

            if (plant && plant.GetComponent<PlantData>().Type == SubtractType)
            {
                plant.GetComponent<PlantData>().AddScore(Mathf.Ceil(SubtractValue));
            }
        }
    }
}
