using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeePlantEndOfTurnEffect : EndOfTurnEffect
{
    public int TurnsPerFruit = 3;

    int turnTimer = 0;
    bool hasFruit = false;
    bool hadFruit = false;
    bool fruitClaimed = false;
    Animator anim = null;

    protected override void Start()
    {
        base.Start();

        anim = GetComponent<Animator>();
    }

    public override void OnEndOfTurn()
    {
        if(hasFruit)
        {
            return;
        }

        if(hadFruit)
        {
            hadFruit = false;
            return;
        }

        ++turnTimer;
        anim.SetTrigger("Coffee_Unripe_Idle");

        if (turnTimer >= TurnsPerFruit)
        {
            anim.SetTrigger("Coffee_Fruit_Idle");
            hasFruit = true;
            turnTimer = 0;
        }
    }

    public bool HasFruitAvailable()
    {
        return hasFruit && !fruitClaimed;
    }

    public void ClaimFruit()
    {
        fruitClaimed = true;
    }

    public void EatFruit()
    {
        hasFruit = false;
        hadFruit = true;
        fruitClaimed = false;
        anim.SetTrigger("Coffee_Parrot_Interaction");
    }
}
