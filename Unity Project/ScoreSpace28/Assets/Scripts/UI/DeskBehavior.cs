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
        }animator.enabled = false;
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
                animator.enabled = false;
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
                timer = Random.Range(BlinkCooldownRange.x, BlinkCooldownRange.y);
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

    void OnMouseDown()
    {
        animating = true;
        animator.enabled = true;
        animator.Play("BounceState");
        Debug.Log("Hello");

        //blinking = true;
        //sr.sprite = BlinkSprite.sprite;
        //timer = BlinkTime;
    }
}
