using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBehavior : MonoBehaviour
{
    public Vector2 MoveTimeRange = new Vector2(2.0f, 3.0f);
    public float ScoreValue = 1.0f;
    public bool PollinatesMushrooms = false;

    Transform target = null;
    Vector3 startPos = Vector3.zero;

    float timer = 0.0f;
    float moveTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, target.position + (Vector3.forward * -0.2f), timer / moveTime);

            if(timer >= moveTime)
            {
                target.GetComponent<OnPlayEffect>().Pollinator = null;
                target.GetComponent<OnPlayEffect>().AddScore(ScoreValue);
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
            if(plant.name.Contains("Flower") || (PollinatesMushrooms && plant.name.Contains("Mushroom")))
            {
                OnPlayEffect ope = plant.GetComponent<OnPlayEffect>();
                if (ope.Pollinator == null)
                {
                    validPlants.Add(ope.transform);
                }
            }
        }

        if(validPlants.Count == 0)
        {
            return;
        }

        startPos = transform.position;
        target = validPlants[Random.Range(0, validPlants.Count)];

        if(Vector2.Distance(startPos, target.position) <= 0.1f)
        {
            target.GetComponent<OnPlayEffect>().AddScore(ScoreValue);
            target = null;
            return;
        }

        target.GetComponent<OnPlayEffect>().Pollinator = gameObject;
        timer = 0.0f;
        moveTime = Random.Range(MoveTimeRange.x, MoveTimeRange.y);
    }
}
