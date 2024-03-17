using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBehavior : MonoBehaviour
{
    public Vector2 MoveTimeRange = new Vector2(2.0f, 3.0f);
    public float ScoreValue = 1.0f;
    public List<PlantType> PollinatorTypes = new List<PlantType>();

    Transform target = null;
    Transform lastTarget = null;
    Vector3 startPos = Vector3.zero;

    float timer = 0.0f;
    float moveTime = 0.0f;

    bool leavingGarden = false;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, target.position + (Vector3.forward * -0.2f), timer / moveTime);

            if(timer >= moveTime)
            {
                if(leavingGarden)
                {
                    Destroy(gameObject);
                    target = null;
                    return;
                }

                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/plantSFX/Bees", GetComponent<Transform>().position);
                target.GetComponent<PlantData>().Pollinator = null;
                target.GetComponent<PlantData>().AddScore(ScoreValue);
                target.GetComponent<OnPollinateEffect>()?.PollinateEffect();
                lastTarget = target;
                target = null;
            }
        }
    }

    public void GoToNewTarget()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");

        List<Transform> validPlants = new List<Transform>();

        foreach(GameObject plant in plants)
        {
            PlantData pd = plant.GetComponent<PlantData>();

            if(pd && PollinatorTypes.Contains(pd.Type))
            {
                if (pd.Pollinator == null && pd.transform != lastTarget)
                {
                    validPlants.Add(pd.transform);
                }
            }
        }

        if(validPlants.Count == 0)
        {
            // There were no plants which could be pollinated, fly away
            if(lastTarget == null)
            {
                GoToDespawnPoint();
                return;
            }

            // Add our last target because there were no other targets
            validPlants.Add(lastTarget);
        }

        startPos = transform.position;
        target = validPlants[Random.Range(0, validPlants.Count)];

        // If we chose the same plant as last time for some reason
        if(Vector2.Distance(startPos, target.position) <= 0.1f)
        {
            target.GetComponent<PlantData>().AddScore(ScoreValue);
            target.GetComponent<OnPollinateEffect>()?.PollinateEffect();
            lastTarget = target;
            target = null;
            return;
        }

        target.GetComponent<PlantData>().Pollinator = gameObject;
        timer = 0.0f;
        moveTime = Random.Range(MoveTimeRange.x, MoveTimeRange.y);
        GetComponent<SpriteRenderer>().flipX = target.position.x < startPos.x;
    }

    void GoToDespawnPoint()
    {
        leavingGarden = true;

        GameObject[] points = GameObject.FindGameObjectsWithTag("CreatureDespawnPoint");
        target = points[Random.Range(0, points.Length)].transform;

        startPos = transform.position;
        timer = 0.0f;
        moveTime = Random.Range(MoveTimeRange.x, MoveTimeRange.y);
        GetComponent<SpriteRenderer>().flipX = target.position.x < startPos.x;
    }

    public void SetSpawnerPlant(Transform plant)
    {
        lastTarget = plant;
    }
}
