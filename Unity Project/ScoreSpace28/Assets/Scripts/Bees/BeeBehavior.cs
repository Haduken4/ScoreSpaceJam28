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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToNewTarget()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
        foreach(GameObject plant in plants)
        {
            if(plant.name.Contains("Flower") || (PollinatesMushrooms && plant.name.Contains("Mushroom")))
            {

            }
        }
    }
}
