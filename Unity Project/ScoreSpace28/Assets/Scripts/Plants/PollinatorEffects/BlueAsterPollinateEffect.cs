using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAsterPollinateEffect : OnPollinateEffect
{
    public float ExtraScoreOnPollinate = 4;
    public ParticleSystem PollinateParticles = null;

    public override void PollinateEffect()
    {
        pd.AddScore(Mathf.Ceil(ExtraScoreOnPollinate));
        if (PollinateParticles)
        {
            PollinateParticles.Play();
        }
    }
}
