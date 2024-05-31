using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskBehavior : MonoBehaviour
{
    public Vector2 BlinkCooldownRange = new Vector2(5.0f, 15.0f);
    public float BlinkTime = 0.2f;
    public Sprite BlinkSprite = null;
    public Sprite NormalSprite = null;

    Animator animator = null;
    SpriteRenderer sr = null;
    float timer = 0.0f;
    bool animating = false;
    bool blinking = false;
    float animDuration = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(BlinkCooldownRange.x, BlinkCooldownRange.y);
        sr = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length > 0)
        {
            animDuration = clipInfo[0].clip.length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (animating)
        {
            if(timer <= 0.0f)
            {
                animating = false;
                timer = Random.Range(BlinkCooldownRange.x, BlinkCooldownRange.y);
            }

            return;
        }

        if (blinking)
        {
            if(timer <= 0.0f)
            {
                blinking = false;
                sr.sprite = NormalSprite;
            }

            return;
        }

        if (timer <= 0.0f)
        {
            blinking = true;
            sr.sprite = BlinkSprite;
            timer = BlinkTime;
        }
    }

    public void StartBounceAnimation()
    {
        animating = true;
        animator.playbackTime = 0;
        animator.StartPlayback();
        timer = animDuration;
    }
}
