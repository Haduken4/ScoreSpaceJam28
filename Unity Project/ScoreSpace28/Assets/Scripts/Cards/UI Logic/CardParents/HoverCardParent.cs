using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCardParent : CardParent
{
    public Vector3 LocalPositionOffset = new Vector3(0, 50, 0);
    public Vector3 HoveredLocalScale = new Vector3(1.5f, 1.5f, 1.0f);

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
}
