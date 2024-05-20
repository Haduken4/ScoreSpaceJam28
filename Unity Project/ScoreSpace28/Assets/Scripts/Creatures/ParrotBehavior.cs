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
    public float MinSpeed = 1.0f;
    public float Acceleration = 2.5f;

    public RuntimeAnimatorController LandingAnimation = null;
    public RuntimeAnimatorController TakeoffAnimation = null;

    Transform lastTree = null;
    Transform lastPoint = null;
    Transform target = null;
    Vector3 curveAffectorPoint = Vector3.zero;
    bool curving = true;

    bool goingToCoffee = false;
    bool ateCoffee = false; // restrict to 1 coffee per turn with current implementation
    bool extraTree = false;

    Animator animator = null;
    bool animating = false;
    Rigidbody2D rb2d;
    Vector2 vel = Vector2.zero;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        animator.StopPlayback();
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

            if(!animating && targetDist <= NoCurveDist)
            {
                animator.speed = 1;
                animator.runtimeAnimatorController = LandingAnimation;
                animating = true;
            }
            
            // CURVE STUFF
            //Vector2 curveAdd = Vector2.zero;
            //if(curving && targetDist < NoCurveDist && false)
            //{
            //    curveAdd = (curveAffectorPoint - transform.position) * CurveStrength;
            //}

            
            vel += vel.normalized * (targetDist < NoCurveDist ? -Acceleration : Acceleration) * Time.deltaTime;
            vel = Vector2.ClampMagnitude(vel, MoveSpeed);
            vel = vel.magnitude <= MinSpeed ? vel.normalized * MinSpeed : vel;
            //dir += curveAdd;
            rb2d.velocity = vel;
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    public override void OnEndOfTurn()
    {
        ateCoffee = false;
        animating = false;
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
            lastPoint = target;
            lastTree = target.parent;
            target.GetComponent<RoostPoint>().RoostingParrot = null;
            target = null;
            GoToNewTarget();
            animating = false;
            return;
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
        animator.runtimeAnimatorController = TakeoffAnimation;
        if (target.position.x <= transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        vel = (target.position - transform.position).normalized;
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
                vel = (target.position - transform.position).normalized;
                if (target.position.x <= transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
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
