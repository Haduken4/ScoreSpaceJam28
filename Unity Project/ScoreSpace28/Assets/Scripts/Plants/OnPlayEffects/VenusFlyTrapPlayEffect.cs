using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusFlyTrapPlayEffect : OnPlayEffect
{
    //public SoundPlayer MyPlayer = null;
    public float ScorePerBee = 10.0f;

    protected override void GameplayEffect()
    {
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bee"))
        {
            BeeBehavior bb = collision.GetComponent<BeeBehavior>();
            AddScore(ScorePerBee * bb.ScoreValue);
            Destroy(bb.gameObject);

            //AudioManager.instance.PlayOneShot(FMODEvents.instance3.VenusFlyChomps, this.transform.position);
        }
    }
}
