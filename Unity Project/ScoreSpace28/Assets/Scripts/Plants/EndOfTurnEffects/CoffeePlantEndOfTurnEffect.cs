using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeePlantEndOfTurnEffect : EndOfTurnEffect
{
    public int TurnsPerFruit = 3;

    public Sprite FruitSprite = null;

    int turnTimer = 0;
    bool hasFruit = false;
    bool fruitClaimed = false;
    SpriteRenderer sr = null;
    Sprite originalSprite;

    protected override void Start()
    {
        base.Start();

        sr = GetComponent<SpriteRenderer>();
        originalSprite = sr.sprite;
    }

    public override void OnEndOfTurn()
    {
        if(hasFruit)
        {
            return;
        }

        ++turnTimer;

        if (turnTimer >= TurnsPerFruit)
        {
            sr.sprite = FruitSprite;
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
        fruitClaimed = false;
        sr.sprite = originalSprite;
    }
}
