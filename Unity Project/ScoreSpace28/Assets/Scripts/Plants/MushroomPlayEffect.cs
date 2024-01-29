using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPlayEffect : OnPlayEffect
{
    public float NeighborBonus = 1.0f;

    public bool Parasitic = false;
    public GameObject MushroomPrefab = null;
    public Vector3 SpawnOffset = Vector3.zero;

    protected override void GameplayEffect()
    {
        List<GameObject> neighbors = gm.GetAdjacentTiles(transform.parent.gameObject);

        foreach(GameObject obj in neighbors)
        {
            TileLogic tl = obj.GetComponent<TileLogic>();
            Transform plant = tl.GetPlant();

            if(plant && plant.gameObject.name.Contains("Mushroom"))
            {
                ScoreValue += NeighborBonus;
                plant.GetComponent<OnPlayEffect>().AddScore(NeighborBonus);
            }
            else if(plant && Parasitic)
            {
                Destroy(tl.GetPlant().gameObject);
                obj.transform.DetachChildren();

                GameObject mush = Instantiate(MushroomPrefab, tl.transform);
                tl.InitPlant(SpawnOffset);
                mush.transform.position -= Vector3.forward;
                mush.transform.localScale *= 0.1f; // Start plant small so it can grow
            }
        }
    }
}
