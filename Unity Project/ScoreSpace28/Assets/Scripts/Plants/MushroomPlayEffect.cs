using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPlayEffect : OnPlayEffect
{
    public float NeighborBonus = 1.0f;

    protected override void GameplayEffect()
    {
        List<GameObject> neighbors = gm.GetAdjacentTiles(transform.parent.gameObject);

        foreach(GameObject obj in neighbors)
        {
            Transform plant = obj.GetComponent<TileLogic>().GetPlant();

            if(plant && plant.gameObject.name.Contains("Mushroom"))
            {
                ScoreValue += NeighborBonus;
                plant.GetComponent<OnPlayEffect>().AddScore(NeighborBonus * GlobalGameData.NonTreeMultiplier);
            }
        }

        return;
    }
}
