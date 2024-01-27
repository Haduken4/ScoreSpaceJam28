using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCardParent : CardParent
{
    public float scaleSpeed = 5.0f;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(currChild)
        {
            currChild.localPosition = LerpVectorToVector(currChild.localPosition, Vector3.zero);
            currChild.localScale = LerpVectorToVector(currChild.localScale, Vector3.one, 0.1f, scaleSpeed);
            Vector3 ang = currChild.localEulerAngles;
            ang.z = SlerpAngleToVal(ang.z, 0.0f);
            currChild.localEulerAngles = ang;
        }
    }
}
