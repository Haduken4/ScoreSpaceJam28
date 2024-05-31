using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeskBehavior : MonoBehaviour
{
    public Vector2 BlinkCooldownRange = new Vector2(5.0f, 15.0f);
    public float BlinkTime = 0.2f;
    public Sprite BlinkSprite = null;
    public Sprite NormalSprite = null;

    Animator animator = null;
    RawImage ri = null;
    float timer = 0.0f;
    bool animating = false;
    bool blinking = false;
    float animDuration = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(BlinkCooldownRange.x, BlinkCooldownRange.y);
        ri = GetComponent<RawImage>();

        //animator = GetComponent<Animator>();
        //AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        //if (clipInfo.Length > 0)
        //{
        //    animDuration = clipInfo[0].clip.length;
        //}
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
                ri.texture = NormalSprite.texture;
                timer = Random.Range(BlinkCooldownRange.x, BlinkCooldownRange.y);
            }

            return;
        }

        if (timer <= 0.0f)
        {
            blinking = true;
            ri.texture = BlinkSprite.texture;
            timer = BlinkTime;
        }
    }

    public void StartBounceAnimation()
    {
        //animating = true;
        //animator.playbackTime = 0;
        //animator.StartPlayback();
        //timer = animDuration;

        blinking = true;
        ri.texture = BlinkSprite.texture;
        timer = BlinkTime;
    }
}
