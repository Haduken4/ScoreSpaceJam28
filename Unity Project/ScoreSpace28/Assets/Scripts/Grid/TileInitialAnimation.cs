using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInitialAnimation : MonoBehaviour
{
    public float JumpTime = 0.2f;
    public float DescentTime = 0.1f;

    public float OpactiyTime = 0.1f;

    public Vector3 Bottom = new Vector3(0, -0.3f, 0);
    public Vector3 Top = new Vector3(0, 0.5f, 0);

    float opacity = 0.0f;
    float opacityTimer = 0.0f;
    float timer = 0.0f;
    bool jumping = true;
    SpriteRenderer sr = null;

    Vector3 basePos = Vector3.zero;
    Vector3 addPos = Vector3.zero;

    bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        basePos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(done)
        {
            return;
        }

        if(sr)
        {
            Color c = sr.color;
            c.a = Mathf.Min(1, opacity);
            sr.color = c;

            opacityTimer += Time.deltaTime;
            opacity = Mathf.Lerp(0, 1, opacityTimer / OpactiyTime);
        }

        if(jumping)
        {
            addPos = Vector3.Lerp(Bottom, Top, timer / JumpTime);

            if(timer > JumpTime)
            {
                timer = 0.0f;
                jumping = false;
            }

            timer += Time.deltaTime;
        }
        else
        {
            addPos = Vector3.Lerp(Top, Vector3.zero, timer / DescentTime);

            if(timer > DescentTime)
            {
                timer = 0.0f;
                done = true;
            }

            timer += Time.deltaTime;
        }

        // Set the position - we always do this
        transform.localPosition = basePos + addPos;
        addPos.z -= addPos.y;
    }
}
