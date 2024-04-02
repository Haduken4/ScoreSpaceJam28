using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPlayEffect : OnPlayEffect
{
    public GameObject PollinatorPrefab = null;

    public Vector2 ScaleMultiplierRange = new Vector2(0.9f, 1.1f);

    protected override void GameplayEffect()
    {
        if(!PollinatorPrefab)
        {
            return;
        }

        GameObject pollinator = Instantiate(PollinatorPrefab, transform.position + (Vector3.forward * -0.2f), Quaternion.identity);
        pollinator.transform.localScale *= Random.Range(ScaleMultiplierRange.x, ScaleMultiplierRange.y);
        pollinator.GetComponent<CreatureBehavior>().SetSpawnerPlant(transform);
        //pd.Pollinator = pollinator;
    }
}
