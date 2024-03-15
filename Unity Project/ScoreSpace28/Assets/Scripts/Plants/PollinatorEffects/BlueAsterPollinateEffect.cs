using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAsterPollinateEffect : OnPollinateEffect
{
    public float ExtraScoreOnPollinate = 4;

    public override void PollinateEffect()
    {
        ope.AddScore(Mathf.Ceil(ExtraScoreOnPollinate));
    }
}