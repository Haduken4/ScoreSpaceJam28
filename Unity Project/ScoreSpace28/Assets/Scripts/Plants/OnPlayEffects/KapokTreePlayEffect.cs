using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KapokTreePlayEffect : OnPlayEffect
{
    public GameObject ParrotPrefab = null;

    public Vector2 ScaleMultiplierRange = new Vector2(0.9f, 1.1f);

    protected override void GameplayEffect()
    {
        // Spawn parrot on a roost point child
        if (!ParrotPrefab)
        {
            return;
        }

        RoostPoint[] points = GetComponentsInChildren<RoostPoint>();
        if(points.Length == 0)
        {
            return;
        }

        Transform spawnPoint = points[Random.Range(0, points.Length)].transform;

        GameObject pollinator = Instantiate(ParrotPrefab, spawnPoint.position - Vector3.forward, Quaternion.identity);
        pollinator.transform.localScale *= Random.Range(ScaleMultiplierRange.x, ScaleMultiplierRange.y);
        pollinator.GetComponent<CreatureBehavior>().SetSpawnerPlant(transform);
        pollinator.GetComponent<ParrotBehavior>().TreeInit(spawnPoint);
    }
}
