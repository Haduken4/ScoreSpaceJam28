using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCardParent : CardParent
{
    public Vector3 LocalPositionOffset = new Vector3(0, 50, 0);
    public Vector3 HoveredLocalScale = new Vector3(1.5f, 1.5f, 1.0f);
    public float MinWidth = 300.0f;
    public float CanvasWidth = 2560.0f;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(currChild)
        {
            currChild.localPosition = LerpVectorToVector(currChild.localPosition, LocalPositionOffset);
            currChild.localScale = LerpVectorToVector(currChild.localScale, HoveredLocalScale, 0.1f);
            Vector3 ang = currChild.eulerAngles;
            ang.z = SlerpAngleToVal(ang.z, 0.0f);
            currChild.eulerAngles = ang;
        }
    }

    protected override void NewChild(RectTransform t)
    {
        base.NewChild(t);
        RectTransform rt = (RectTransform)transform;
        if (rt.anchoredPosition.x - (MinWidth / 2.0f) < CanvasWidth / -2.0f)
        {
            rt.anchoredPosition += Vector2.right * 60.0f;
        }
    }
}
