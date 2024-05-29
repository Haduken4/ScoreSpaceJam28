using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainEffectBehavior : MonoBehaviour
{
    public ParticleSystem RainSystem = null;
    public float InitialRate = 5.0f;
    public float RatePerTree = 3.0f;

    int trees = 0;
    float currSpeed = 0.0f;

    public void RainTreePlanted()
    {
        ParticleSystem.EmissionModule e = RainSystem.emission;
        if(trees == 0)
        {
            e.enabled = true;
            currSpeed = InitialRate;
        }
        else
        {
            currSpeed += RatePerTree;
        }

        ++trees;
        e.rateOverTime = currSpeed;
    }
}
