using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenufFlyTrapPlayEffect : OnPlayEffect
{
    public float ScorePerBee = 10.0f;

    protected override void GameplayEffect()
    {
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bee"))
        {
            BeeBehavior bb = collision.GetComponent<BeeBehavior>();
            AddScore(ScorePerBee * bb.ScoreValue);
            Destroy(bb.gameObject);

            // Change my sprite to closed
        }
    }
}
