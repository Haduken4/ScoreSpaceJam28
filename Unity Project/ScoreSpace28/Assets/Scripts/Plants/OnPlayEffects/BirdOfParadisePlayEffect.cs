using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdOfParadisePlayEffect : OnPlayEffect
{
    public float BonusEffectStrength = 1;

    protected override void GameplayEffect()
    {
        JungleSetData.BirdOfParadiseBonus += BonusEffectStrength;
    }
}
