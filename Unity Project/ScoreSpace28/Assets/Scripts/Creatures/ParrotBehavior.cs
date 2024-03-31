using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotBehavior : CreatureBehavior
{
    public float MoveSpeed = 4.5f;
    public float CurveStrength = 0.2f;
    public float NoCurveDist = 2.0f;

    Transform lastTree = null;
    Transform target = null;
    Vector3 curveAffectorPoint = Vector3.zero;

    bool goingToCoffee = false;
    bool ateCoffee = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnEndOfTurn()
    {
        GoToNewTarget();
    }

    void GoToNewTarget()
    {
        // Check coffee first
        if(!ateCoffee)
        {
            if(AttemptGoToCoffee())
            {
                return;
            }
        }
    }

    bool AttemptGoToCoffee()
    {


        return goingToCoffee;
    }

    Vector2 PerpendicularUp(Vector2 v)
    {
        Vector2 vRet = v;
        vRet.x = v.y;
        vRet.y = v.x;
        if(v.x < 0)
        {
            vRet.y *= -1;
        }
        else
        {
            vRet.x *= -1;
        }

        return vRet;
    }
}
