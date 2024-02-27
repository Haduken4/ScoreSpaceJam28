using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInitialAnimation : MonoBehaviour
{
    public float JumpTime = 0.2f;
    public float DescentTime = 0.1f;

    public Vector3 Bottom = new Vector3(0, -0.3f, 0);
    public Vector3 Top = new Vector3(0, 0.5f, 0);

    public float OpactiyTime = 0.1f;

    public float MiddleScaleTime = 0.3f;
    // Time to return to (1, 1, 1)
    public float FinalScaleTime = 0.1f; 
    public Vector3 StartScale = new Vector3(0.3f, 0.3f, 1.0f);
    public Vector3 MiddleScale = new Vector3(1.2f, 1.2f, 1.0f);

    float opacity = 0.0f;
    float opacityTimer = 0.0f;

    float timer = 0.0f;
    bool jumping = true;
    SpriteRenderer sr = null;

    Vector3 basePos = Vector3.zero;
    Vector3 addPos = Vector3.zero;

    float scaleTimer = 0.0f;
    Vector3 scale = Vector3.zero;
    Vector3 baseScale = Vector3.zero;
    bool midScaling = true;

    bool doneJumping = false;
    bool doneScaling = false;
    bool done = false;

    bool reset = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        basePos = transform.localPosition;
        baseScale = transform.localScale;
        scale = StartScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(done)
        {
            if(!reset)
            {
                transform.localScale = baseScale;
                transform.localPosition = basePos;

                reset = true;
            }
            return;
        }

        ScaleLogic();

        SpriteLogic();

        JumpLogic();

        // Set the position - we always do this
        transform.localPosition = basePos + addPos;
        addPos.z -= addPos.y;

        // Set the scale - we always do this
        transform.localScale = scale;

        if(doneScaling && doneJumping)
        {
            done = true;
        }
    }

    void JumpLogic()
    {
        if (jumping)
        {
            addPos = Vector3.Lerp(Bottom, Top, timer / JumpTime);

            if (timer > JumpTime)
            {
                timer = 0.0f;
                jumping = false;
            }

            timer += Time.deltaTime;
        }
        else if (!doneJumping)
        {
            addPos = Vector3.Lerp(Top, Vector3.zero, timer / DescentTime);

            if (timer > DescentTime)
            {
                timer = 0.0f;
                doneJumping = true;
            }

            timer += Time.deltaTime;
        }
    }

    void SpriteLogic()
    {
        if (sr)
        {
            opacityTimer += Time.deltaTime;
            opacity = Mathf.Lerp(0, 1, opacityTimer / OpactiyTime);

            Color c = sr.color;
            c.a = Mathf.Min(1, opacity);
            sr.color = c;
        }
    }

    void ScaleLogic()
    {
        if (midScaling)
        {
            scaleTimer += Time.deltaTime;
            scale = Vector3.Lerp(StartScale, MiddleScale, scaleTimer / MiddleScaleTime);

            if (scaleTimer > MiddleScaleTime)
            {
                midScaling = false;
                scaleTimer = 0.0f;
            }
        }
        else if (!doneScaling)
        {
            scaleTimer += Time.deltaTime;
            scale = Vector3.Lerp(MiddleScale, baseScale, scaleTimer / FinalScaleTime);

            if (scaleTimer > FinalScaleTime)
            {
                doneScaling = true;
            }
        }
    }

    public bool DoneAnimating()
    {
        return done;
    }
}
