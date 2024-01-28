using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public float MouseCollisionSize = 0.05f;

    ReactiveTile last = null;
    TrashCan lastTrash = null;

    ClickedCardParent clickParent = null;

    private void Start()
    {
        clickParent = FindFirstObjectByType<ClickedCardParent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;

        last = null;
        TrashCan trash = null;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, MouseCollisionSize);
        foreach(Collider2D hit in hits)
        {
            if(hit.gameObject.CompareTag("Tile") && last == null)
            {
                last = hit.GetComponent<ReactiveTile>();
                last.CursorEnter();
            }

            trash = hit.GetComponent<TrashCan>();

            if (trash && clickParent.transform.childCount == 1)
            {
                // Open trash can
                trash.OpenLid();
                lastTrash = trash;
            }
        }

        if(last != null)
        {
            last.CursorExit();
            last = null;
        }

        if(trash == null && lastTrash != null)
        {
            // Close trash can using lastTrash
            lastTrash.CloseLid();
            lastTrash = null;
        }
    }

    public TrashCan GetTrash()
    {
        return lastTrash;
    }
}
