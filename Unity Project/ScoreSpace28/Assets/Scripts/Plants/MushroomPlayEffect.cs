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

            if(NeighborBonus != 0 && plant && plant.gameObject.name.Contains("Mushroom"))
            {
                ScoreValue += NeighborBonus;
                plant.GetComponent<OnPlayEffect>().AddScore(NeighborBonus);
            }
            
            if(plant && Parasitic && !plant.gameObject.name.Contains("Mushroom"))
            {
                Vector3 oldPlantPos = tl.GetPlant().position;
                Destroy(tl.GetPlant().gameObject);
                tl.transform.DetachChildren();

                GameObject mush = Instantiate(MushroomPrefab, tl.transform);
                tl.InitPlant(SpawnOffset);

                Vector3 mushPos = mush.transform.position;
                mushPos.z = oldPlantPos.z;
                mush.transform.position = oldPlantPos;
                mush.transform.localScale *= 0.1f; // Start plant small so it can grow
            }
        }
    }
}
