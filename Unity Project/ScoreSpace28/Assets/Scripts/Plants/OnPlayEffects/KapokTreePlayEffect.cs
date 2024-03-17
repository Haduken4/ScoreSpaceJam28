using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KapokTreePlayEffect : OnPlayEffect
{
    public GameObject ParrotPrefab = null;

    public Vector2 ScaleMultiplierRange = new Vector2(0.9f, 1.1f);

    protected override void GameplayEffect()
    {
        base.GameplayEffect();
    }
}
