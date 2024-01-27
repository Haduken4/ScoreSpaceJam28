using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public float MouseCollisionSize = 0.05f;

    ReactiveTile last = null;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, MouseCollisionSize);
        foreach(Collider2D hit in hits)
        {
            if(hit.gameObject.CompareTag("Tile"))
            {
                last = hit.GetComponent<ReactiveTile>();
                last.CursorEnter();
                return;
            }
        }

        if(last != null)
        {
            last.CursorExit();
            last = null;
        }
    }
}
