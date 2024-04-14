using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialUIMovement : MonoBehaviour
{
    public Transform toMove = null;

    public Vector3 VelocityStart = Vector2.zero;
    public Vector3 Acceleration = Vector2.zero;
    public Vector3 VelocityMin = Vector2.zero;

    Vector3 vel = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        if(toMove == null)
        {
            toMove = transform;
        }
        vel = VelocityStart;
    }

    // Update is called once per frame
    void Update()
    {
        vel += Acceleration * Time.deltaTime;
        vel = Vector3.Max(vel, VelocityMin);
        transform.parent.position += vel * Time.deltaTime;
    }
}
