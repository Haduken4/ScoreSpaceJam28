using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballTreeEndOfTurnEffect : EndOfTurnEffect
{
    public int TurnsPerEffect = 3;
    public float EffectScore = 10;

    public Sprite FloweringSprite = null;

    int turnTimer = 0;
    bool flowering = false;
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
        ++turnTimer;
        
        if(flowering)
        {
            flowering = false;
            sr.sprite = originalSprite;
        }

        if(turnTimer >= TurnsPerEffect)
        {
            sr.sprite = FloweringSprite;
            flowering = true;
            pd.AddScore(EffectScore);
            turnTimer = 0;
        }
    }
}
