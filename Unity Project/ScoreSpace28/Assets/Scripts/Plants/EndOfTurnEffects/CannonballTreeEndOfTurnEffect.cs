using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballTreeEndOfTurnEffect : EndOfTurnEffect
{
    public int TurnsPerEffect = 3;
    public float EffectScore = 10;

    int turnTimer = 0;

    public override void OnEndOfTurn()
    {
        ++turnTimer;

        if(turnTimer >= TurnsPerEffect)
        {

        }
    }
}
