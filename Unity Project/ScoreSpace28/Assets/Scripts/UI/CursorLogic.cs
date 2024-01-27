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

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        bool hitChar = false;
        int playerMask = LayerMask.NameToLayer("Player");
        int enemyMask = LayerMask.NameToLayer("Enemy");
        int neutralMask = LayerMask.NameToLayer("Neutral");
        int groundMask = LayerMask.NameToLayer("Ground");

        foreach (RaycastHit hit in hits)
        {
            int hitMask = hit.collider.gameObject.layer;

            if(!hitChar && (hitMask == playerMask || hitMask == enemyMask || hitMask == neutralMask))
            {
                hitChar = true;

                MouseCharacter = hit.collider.gameObject;
                MouseCharacterPos = hit.point;
            }

            if(hitMask == groundMask)
            {
                GroundMousePos = hit.point;
            }
        }
    }
}
