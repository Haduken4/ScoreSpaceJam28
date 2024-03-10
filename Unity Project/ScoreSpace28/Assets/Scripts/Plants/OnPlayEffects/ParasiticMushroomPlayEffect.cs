using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiticMushroomPlayEffect : OnPlayEffect
{
    public GameObject BeePrefab = null;
    public float ScoreGainsPerSiphon = 0.5f;
    public float SizeGainsPerSiphon = 0.1f;

    public List<PlantIdentifier> SiphonTargets = new List<PlantIdentifier>();

    protected override void GameplayEffect()
    {
        
    }
}
