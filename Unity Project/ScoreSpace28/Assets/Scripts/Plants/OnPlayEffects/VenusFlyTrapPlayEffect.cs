using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusFlyTrapPlayEffect : OnPlayEffect
{
    //public SoundPlayer MyPlayer = null;
    public float ScorePerBee = 10.0f;
    public Animator animator = null;

    protected override void GameplayEffect()
    {
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bee"))
        {
            BeeBehavior bb = collision.GetComponent<BeeBehavior>();
            pd.AddScore(ScorePerBee * bb.ScoreValue);
            Destroy(bb.gameObject);
            tm.GameplayEffectStop();

            AudioManager.instance.PlayOneShot(FMODEvents_InGame.instance.VenusFlyChomps, this.transform.position);

            animator.SetTrigger("FlyTrapEat");
        }
    }
}
