using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeePlantEndOfTurnEffect : EndOfTurnEffect
{
    public int TurnsPerFruit = 3;

    public Sprite FruitSprite = null;

    int turnTimer = 0;
    bool hasFruit = false;
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

    public bool HasFruit()
    {
        return hasFruit;
    }

    public void EatFruit()
    {
        hasFruit = false;
        sr.sprite = originalSprite;
    }
}
