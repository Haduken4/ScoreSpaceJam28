using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPlayEffect : OnPlayEffect
{
    public GameObject BeePrefab = null;

    protected override void GameplayEffect()
    {
        Instantiate(BeePrefab, transform.position + (Vector3.forward * -0.2f), Quaternion.identity);
    }
}
