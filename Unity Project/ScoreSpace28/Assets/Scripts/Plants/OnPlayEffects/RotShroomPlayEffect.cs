using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotShroomPlayEffect : OnPlayEffect
{
    public PlantType DestroyType = PlantType.ROOT;
    public float ScorePerDestroy = 3.0f;

    protected override void GameplayEffect()
    {
        List<GameObject> neighbors = gm.GetAdjacentTiles(transform.parent.gameObject);

        foreach (GameObject tile in neighbors)
        {
            TileLogic tl = tile.GetComponent<TileLogic>();
            Transform plant = tl.GetPlant();

            if (plant && DestroyType == plant.GetComponent<PlantData>().Type)
            {
                pd.AddScore(ScorePerDestroy);
                RootParticleEffect particles = plant.GetComponent<RootParticleEffect>();
                particles?.SpawnParticles();
                Destroy(plant.gameObject);
            }
        }
    }
}
