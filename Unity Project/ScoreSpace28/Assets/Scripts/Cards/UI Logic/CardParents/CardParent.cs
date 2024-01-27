using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardParent : MonoBehaviour
{
    public float CardLerpSpeed = 5.0f;
    public float MinSnapDistance = 0.2f;

    protected RectTransform currChild = null;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currChild = transform.childCount == 1 ? (RectTransform)transform.GetChild(0) : null;
        if(currChild != null)
        {
            NewChild(currChild);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        RectTransform c = transform.childCount == 1 ? (RectTransform)transform.GetChild(0) : null;
        if(currChild != c)
        {
            NewChild(c);
            currChild = c;
        }
    }

    protected Vector3 LerpVectorToVector(Vector3 v, Vector3 to, float snapMultiplier = 1.0f, float lerpSpeedMultiplier = 1.0f)
    {
        v = Vector3.Lerp(v, to, CardLerpSpeed * Time.deltaTime * lerpSpeedMultiplier);
        if (Vector3.Distance(v, to) <= MinSnapDistance * snapMultiplier)
        {
            v = to;
        }
        return v;
    }

    protected float SlerpAngleToVal(float f, float val, float snapMultiplier = 1.0f, float lerpSpeedMultiplier = 1.0f)
    {
        f = Mathf.LerpAngle(f, val, CardLerpSpeed * Time.deltaTime * lerpSpeedMultiplier);
        if (Mathf.Abs(f) <= MinSnapDistance * snapMultiplier)
        {
            f = val;
        }
        return f;
    }

    // Called when our child changes
    protected virtual void NewChild(RectTransform t)
    {

    }
}
