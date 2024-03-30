using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummingbirdBehavior : CreatureBehavior
{
    [Header("Movement")]
    public Vector2 MoveCountRange = new Vector2(4, 6);
    public float MoveRandomAngle = 30.0f;
    public float MoveSpeed = 12.0f;
    public Vector2 StopTimeRange = new Vector2(0.15f, 0.25f);
    public float AcceptableStopDist = 0.1f;

    [Header("Pollinating")]
    public float ScoreValue = 1.0f;
    public List<PlantType> PollinatorTypes = new List<PlantType>();
    public List<PlantIdentifier> FakePollinators = new List<PlantIdentifier>();

    Transform target = null;
    Transform lastTarget = null;
    Vector3 nextPos = Vector3.zero;
    Rigidbody2D rb2d = null;
    float timer = 0.0f;
    int moveCount = 0;
    bool fake = false;

    override protected void Start()
    {
        base.Start();

        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            rb2d.velocity = Vector2.zero;
            return;
        }

        if (timer > 0.0f)
        {
            rb2d.velocity = Vector2.zero;
            timer -= Time.deltaTime;
            return;
        }

        if(Vector3.Distance(nextPos, transform.position) <= AcceptableStopDist)
        {
            CalculateNextPos();
        }

        Vector2 dir = nextPos - transform.position;
        dir.Normalize();
        rb2d.velocity = dir * MoveSpeed;
    }

    void CalculateNextPos()
    {
        if (moveCount == 0) // Done moving
        {
            target.GetComponent<PlantData>().Pollinator = null;
            if(!fake)
            {
                target.GetComponent<PlantData>().AddScore(ScoreValue);
            }
            target.GetComponent<OnPollinateEffect>()?.PollinateEffect();
            lastTarget = target;
            target = null;

            return;
        }
        else if (moveCount == 1) // Last move, go to plant without anything fancy
        {
            nextPos = target.position;
        }
        else
        {
            Vector3 dir = target.position - transform.position;
            dir /= moveCount;
            dir.z = 0.0f;
            dir = Quaternion.Euler(0, 0, Random.Range(-MoveRandomAngle, MoveRandomAngle)) * dir;
            Debug.Log(dir);
            nextPos = transform.position + dir;
        }

        --moveCount;
    }

    public override void OnEndOfTurn()
    {
        GoToNewTarget();
    }

    void GoToNewTarget()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");

        List<Transform> possibleTargets = new List<Transform>();
        List<Transform> fakeTargets = new List<Transform>();

        foreach (GameObject plant in plants)
        {
            PlantData pd = plant.GetComponent<PlantData>();

            if (pd && PollinatorTypes.Contains(pd.Type))
            {
                if (pd.Pollinator == null && pd.transform != lastTarget)
                {
                    possibleTargets.Add(pd.transform);
                }
            }
            else if(pd && FakePollinators.Contains(pd.Identifier))
            {
                if(pd.Pollinator == null && pd.transform != lastTarget)
                {
                    possibleTargets.Add(pd.transform);
                    fakeTargets.Add(pd.transform);
                }
            }
        }

        if (possibleTargets.Count == 0)
        {
            // There were no plants which could be pollinated, fly away
            if (lastTarget == null)
            {
                //GoToDespawnPoint();
                return;
            }

            // Add our last target because there were no other targets
            possibleTargets.Add(lastTarget);
            if(fake)
            {
                fakeTargets.Add(lastTarget);
            }
        }

        target = possibleTargets[Random.Range(0, possibleTargets.Count)];
        fake = fakeTargets.Contains(target);

        // If we chose the same plant as last time for some reason
        if (Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            if(!fake)
            {
                target.GetComponent<PlantData>().AddScore(ScoreValue);
            }
            target.GetComponent<OnPollinateEffect>()?.PollinateEffect();
            lastTarget = target;
            target = null;
            return;
        }

        target.GetComponent<PlantData>().Pollinator = gameObject;
        timer = 0.0f;
        CalculateNextPos();
        GetComponent<SpriteRenderer>().flipX = target.position.x < transform.position.x;
    }

    public override void SetSpawnerPlant(Transform plant)
    {
        lastTarget = plant;
    }
}
