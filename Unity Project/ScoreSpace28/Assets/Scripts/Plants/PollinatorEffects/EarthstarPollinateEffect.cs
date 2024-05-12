using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthstarPollinateEffect : OnPollinateEffect
{
    public PlantType BuffEffectType = PlantType.MUSHROOM;
    public float BuffEffectValue = 1;

    public override void PollinateEffect()
    {
        List<GameObject> neighbors = gm.GetAdjacentTiles(transform.parent.gameObject);

        foreach (GameObject obj in neighbors)
        {
            TileLogic tl = obj.GetComponent<TileLogic>();
            Transform plant = tl.GetPlant();

            if(plant && plant.GetComponent<PlantData>().Type == BuffEffectType)
            {
                plant.GetComponent<PlantData>().AddScore(BuffEffectValue + JungleSetData.BirdOfParadiseBonus);
            }
        }
    }
}
