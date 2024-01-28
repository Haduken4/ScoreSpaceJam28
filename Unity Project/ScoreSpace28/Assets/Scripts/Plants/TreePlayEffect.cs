using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlayEffect : OnPlayEffect
{
    public GameObject RootPrefab = null;

    protected override void GameplayEffect()
    {
        // Spawn roots on adjacent tiles and destroy existing plants
        List<GameObject> neighbors = gm.GetAdjacentTiles(transform.parent.gameObject);
        foreach(GameObject tile in neighbors)
        {
            TileLogic tl = tile.GetComponent<TileLogic>();
            if(tl.GetPlant() != null)
            {
                Destroy(tl.GetPlant().gameObject);
                tile.transform.DetachChildren();
            }

            GameObject plant = Instantiate(RootPrefab, tile.transform);
            tl.InitPlant();
            plant.transform.position -= Vector3.forward;
            plant.transform.localScale *= 0.1f; // Start plant small so it can grow
        }
    }
}