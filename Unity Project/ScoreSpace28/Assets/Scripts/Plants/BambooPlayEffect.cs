using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooPlayEffect : OnPlayEffect
{
    public float ScorePerUnique = 3;

    List<string> uniquePlants = new List<string>();

    protected override void GameplayEffect()
    {
        List<GameObject> neighbors = gm.GetAdjacentTiles(transform.parent.gameObject);

        foreach (GameObject obj in neighbors)
        {
            TileLogic tl = obj.GetComponent<TileLogic>();
            Transform plant = tl.GetPlant();

            // Modified tile
            if(tl.gameObject.name != "GridTile" && !uniquePlants.Contains(tl.gameObject.name))
            {
                uniquePlants.Add(tl.gameObject.name);
                effectScore += ScorePerUnique;
            }
            if(plant != null && !uniquePlants.Contains(plant.gameObject.name))
            {
                uniquePlants.Add(plant.gameObject.name);
                effectScore += ScorePerUnique;
            }
        }
    }
}
