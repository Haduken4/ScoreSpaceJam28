using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballTreeEndOfTurnEffect : EndOfTurnEffect
{
    public int TurnsPerEffect = 3;
    public float EffectScore = 10;

    int turnTimer = 0;
    bool flowering = false;

    Animator anim;

    protected override void Start()
    {
        base.Start();

        anim = GetComponent<Animator>();
    }

    public override void OnEndOfTurn()
    {
        if(flowering)
        {
            flowering = false;
            anim.SetTrigger("CB_Idle_NoFlowers");
            return;
        }

        ++turnTimer;

        if (turnTimer == 1)
        {
            anim.SetTrigger("CB_Idle_Flowering_01");
        }
        else if(turnTimer == 2)
        {
            anim.SetTrigger("CB_Idle_Flowering_02");
        }

        if(turnTimer >= TurnsPerEffect)
        {
            flowering = true;
            pd.AddScore(EffectScore);
            turnTimer = 0;
            anim.SetTrigger("CB_Idle_Flowered_03");
        }
    }
}
