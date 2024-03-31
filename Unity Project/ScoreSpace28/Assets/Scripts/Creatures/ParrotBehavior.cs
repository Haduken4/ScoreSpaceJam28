using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotBehavior : CreatureBehavior
{
    public float MoveSpeed = 4.5f;
    public float CoffeeSpeed = 7.0f;
    public float CurveStrength = 0.2f;
    public float NoCurveDist = 2.0f;
    public float SnapDist = 0.1f;

    Transform lastTree = null;
    Transform target = null;
    Vector3 curveAffectorPoint = Vector3.zero;

    bool goingToCoffee = false;
    bool ateCoffee = false;
    bool extraTree = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            float targetDist = Vector2.Distance(transform.position, target.position);

            if (targetDist <= SnapDist)
            {
                ReachedTarget();
            }
        }
    }

    public override void OnEndOfTurn()
    {
        ateCoffee = false;
        GoToNewTarget();
    }

    void ReachedTarget()
    {
        if(goingToCoffee)
        {
            goingToCoffee = false;
            ateCoffee = true;
            extraTree = true;
            target.GetComponent<CoffeePlantEndOfTurnEffect>().EatFruit();
            GoToNewTarget();
            return;
        }

        // Gain score for going to a tree

        if(extraTree)
        {
            extraTree = false;
            GoToNewTarget();
        }
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
