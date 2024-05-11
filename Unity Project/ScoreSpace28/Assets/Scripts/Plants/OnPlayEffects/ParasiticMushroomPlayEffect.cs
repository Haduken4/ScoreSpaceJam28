using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiticMushroomPlayEffect : OnPlayEffect
{
    public GameObject BeePrefab = null;
    public GameObject TextPrefab = null;
    public float ScoreGainsPerSiphon = 0.5f;
    public float SizeGainsPerSiphon = 0.1f;
    public float PerfectScore = 5.0f;
    public float PerfectSize = 0.5f;

    public List<PlantType> SiphonTargets = new List<PlantType>();

    protected override void GameplayEffect()
    {
        List<GameObject> neighbors = gm.GetAdjacentTiles(transform.parent.gameObject);
        int siphonCount = 0;
        foreach(GameObject tile in neighbors)
        {
            TileLogic tl = tile.GetComponent<TileLogic>();
            Transform plant = tl.GetPlant();

            if (plant && SiphonTargets.Contains(plant.GetComponent<PlantData>().Type))
            {
                ++siphonCount;
            }
        }

        float extraScore = Mathf.Floor(ScoreGainsPerSiphon * siphonCount);
        float sizeMult = 1.0f + (SizeGainsPerSiphon * siphonCount);

        // Perfect
        if (siphonCount == 8)
        {
            extraScore += PerfectScore;
            sizeMult += PerfectSize;

            // Spawn effect text
            GameObject t = Instantiate(TextPrefab, FindFirstObjectByType<Canvas>().transform);
            t.transform.position = pd.ScoreTextPoint.position + Vector3.up;
        }

        GameObject bee = Instantiate(BeePrefab, transform.position + (Vector3.forward * -0.2f), Quaternion.identity);
        bee.transform.localScale *= sizeMult;
        bee.GetComponent<BeeBehavior>().SetSpawnerPlant(transform);
        bee.GetComponent<BeeBehavior>().ScoreValue += extraScore;
    }
}
