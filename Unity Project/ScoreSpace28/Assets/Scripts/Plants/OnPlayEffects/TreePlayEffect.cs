using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlayEffect : OnPlayEffect
{
    public GameObject RootPrefab = null;
    public Vector3 rootPosOffset = Vector3.zero;

    public float GlobalMultiplierAdd = 0;

    protected override void GameplayEffect()
    {
        // Spawn roots on adjacent tiles and destroy existing plants
        List<GameObject> neighbors = gm.GetAdjacentTiles(transform.parent.gameObject);
        foreach(GameObject tile in neighbors)
        {
            TileLogic tl = tile.GetComponent<TileLogic>();
            if(tl.GetPlant() == null) {}
            else if(tl.GetPlant().GetComponent<PlantData>().Type == PlantType.TREE)
            {
                continue;
            }
            else
            {
                Destroy(tl.GetPlant().gameObject);
                tile.transform.DetachChildren();
            }

            GameObject plant = Instantiate(RootPrefab, tile.transform);
            tl.InitPlant(rootPosOffset);
            plant.transform.position -= Vector3.forward * 0.5f;
            plant.transform.localScale *= 0.1f; // Start plant small so it can grow
        }

        GlobalGameData.PlantValueMultiplier += GlobalMultiplierAdd;
        if(GlobalMultiplierAdd != 0)
        {
            FindFirstObjectByType<RainEffectBehavior>().RainTreePlanted();
        }

        float score = 0;
        foreach(OnPlayEffect plant in FindObjectsOfType<OnPlayEffect>())
        {
            if(plant == this)
            {
                continue;
            }

            score += plant.ScoreValue * GlobalMultiplierAdd;
        }
        if(score != 0)
        {
            pd.AddScore(Mathf.Ceil(score));
        }
    }
}
