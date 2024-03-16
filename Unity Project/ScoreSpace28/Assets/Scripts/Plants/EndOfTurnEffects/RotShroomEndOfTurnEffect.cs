using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotShroomEndOfTurnEffect : EndOfTurnEffect
{
    public PlantType DestroyType = PlantType.ROOT;

    public override void OnEndOfTurn()
    {
        List<GameObject> neighbors = gm.GetAdjacentTiles(transform.parent.gameObject);
        List<GameObject> destroyTargets = new List<GameObject>();

        foreach(GameObject tile in neighbors)
        {
            TileLogic tl = tile.GetComponent<TileLogic>();
            Transform plant = tl.GetPlant();

            if (plant && DestroyType == plant.GetComponent<PlantData>().Type)
            {
                destroyTargets.Add(plant.gameObject);
            }
        }

        if(destroyTargets.Count > 0)
        {
            Destroy(destroyTargets[Random.Range(0, destroyTargets.Count)]);
        }
    }
}
