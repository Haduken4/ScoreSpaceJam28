using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotBehavior : CreatureBehavior
{
    public float ScoreValue = 4;
    public float MoveSpeed = 4.5f;
    public float CoffeeSpeed = 7.0f;
    public float CurveStrength = 0.2f;
    public float NoCurveDist = 2.0f;
    public float SnapDist = 0.1f;

    Transform lastTree = null;
    Transform lastPoint = null;
    Transform target = null;
    Vector3 curveAffectorPoint = Vector3.zero;
    bool curving = true;

    bool goingToCoffee = false;
    bool ateCoffee = false; // restrict to 1 coffee per turn with current implementation
    bool extraTree = false;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    protected override void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
                if (target == null)
                {
                    return;
                }
            }

            Vector2 curveAdd = Vector2.zero;
            if(curving && targetDist < NoCurveDist && false)
            {
                curveAdd = (curveAffectorPoint - transform.position) * CurveStrength;
            }

            Vector2 dir = (target.position - transform.position);
            dir += curveAdd;
            rb2d.velocity = MoveSpeed * dir.normalized;
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    public override void OnEndOfTurn()
    {
        ateCoffee = false;
        tm.GameplayEffectStart();
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
        target.parent.GetComponent<PlantData>().AddScore(ScoreValue + JungleSetData.BirdOfParadiseBonus);

        if(extraTree)
        {
            extraTree = false;
            GoToNewTarget();
        }
        else // Otherwise this was our last tree
        {
            tm.GameplayEffectStop();
        }

        lastPoint = target;
        lastTree = target.parent;
        target.GetComponent<RoostPoint>().RoostingParrot = null;
        target = null;
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

        GameObject[] roostPoints = GameObject.FindGameObjectsWithTag("RoostPoint");

        List<Transform> validPoints = new List<Transform>();
        Transform repeatStorage = null;

        foreach (GameObject rp in roostPoints)
        {
            if(rp.transform.parent != lastTree && rp.transform != lastPoint)
            {
                repeatStorage = rp.transform;
            }

            if(rp.GetComponent<RoostPoint>().RoostingParrot == null && rp.transform.parent != lastTree)
            {
                validPoints.Add(rp.transform);
            }
        }

        target = validPoints.Count != 0 ? validPoints[Random.Range(0, validPoints.Count)] : repeatStorage;
        if (target == null)
        {
            // Leave this mortal coil, this should be impossible since you can't destroy trees in jungle set
            tm.GameplayEffectStop();
            return;
        }

        curving = true;
        CalculateTargetTrajectory();
        target.GetComponent<RoostPoint>().RoostingParrot = this;
    }

    bool AttemptGoToCoffee()
    {
        CoffeePlantEndOfTurnEffect[] coffees = FindObjectsOfType<CoffeePlantEndOfTurnEffect>();

        foreach(var coffee in coffees)
        {
            if(coffee.HasFruitAvailable())
            {
                coffee.ClaimFruit();
                target = coffee.transform;
                curving = false;
                goingToCoffee = true;
                break;
            }
        }

        return goingToCoffee;
    }

    // Sets our curve affector to give the arcing flight effect
    void CalculateTargetTrajectory()
    {
        // Set curve affector
        Vector2 dir = target.transform.position - transform.position;
        Vector2 perp = Vector2.zero;
        if (dir.x < 0)
        {
            perp.y = -dir.x;
            perp.x = dir.y;
        }
        else
        {
            perp.y = dir.x;
            perp.x = -dir.y;
        }

        curveAffectorPoint = (Vector2)transform.position + (dir / 0.5f + perp / 0.5f);
    }

    public void TreeInit(Transform point)
    {
        lastPoint = point;
        lastTree = point.parent;
    }
}
