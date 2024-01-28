using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLogic : MonoBehaviour
{
    public Vector3 GroundMousePos = Vector3.zero;

    public GameObject MouseCharacter = null;
    public Vector3 MouseCharacterPos = Vector3.zero;

    static CursorLogic singleton = null;

    public static CursorLogic Instance()
    {
        return singleton;
    }

    private void Start()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
