using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPlayEffect : OnPlayEffect
{
    public GameObject BeePrefab = null;

    public Vector2 ScaleMultiplierRange = new Vector2(0.9f, 1.1f);

    protected override void GameplayEffect()
    {
        GameObject bee = Instantiate(BeePrefab, transform.position + (Vector3.forward * -0.2f), Quaternion.identity);
        bee.transform.localScale *= Random.Range(ScaleMultiplierRange.x, ScaleMultiplierRange.y);
    }
}
