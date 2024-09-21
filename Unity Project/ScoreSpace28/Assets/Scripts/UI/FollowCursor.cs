using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public float MouseCollisionSize = 0.05f;
    public GameObject ClickParticles = null;

    ReactiveTile last = null;

    ClickedCardParent clickParent = null;
    HoverCardParent hoverParent = null;
    TurnManager tm = null;

    private void Start()
    {
        clickParent = FindFirstObjectByType<ClickedCardParent>();
        hoverParent = FindFirstObjectByType<HoverCardParent>();
        tm = FindFirstObjectByType<TurnManager>();
    }

    public ReactiveTile GetFirstHitTile()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, MouseCollisionSize);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.CompareTag("Tile"))
            {
                return hit.GetComponent<ReactiveTile>();
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;

        if (Input.GetMouseButtonDown(0) && !hoverParent.HasChild())
        {
            Instantiate(ClickParticles, mousePos - Vector3.forward * 0.3f, Quaternion.identity);
        }

        if (last != null)
        {
            last.CursorExit();
            last = null;
        }
        DeskBehavior desk = null;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, MouseCollisionSize);
        foreach(Collider2D hit in hits)
        {
            if(hit.gameObject.CompareTag("Tile") && last == null)
            {
                last = hit.GetComponent<ReactiveTile>();
                last.CursorEnter();
            }

            desk = hit.GetComponent<DeskBehavior>();
        }

        if(desk && Input.GetMouseButtonUp(0))
        {
            desk.PlayBounceAnimation();
        }
    }
}
