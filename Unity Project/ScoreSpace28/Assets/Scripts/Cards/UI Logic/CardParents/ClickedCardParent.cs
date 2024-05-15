using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickedCardParent : CardParent
{
    public Vector3 ClickedLocalScale = new Vector3(0.8f, 0.8f, 0.8f);
    public Vector3 InPlayLocalScale = new Vector3(0.2f, 0.2f, 0.2f);
    public float LeanStrength = 0.1f;
    public float distanceScaling = 0.2f;
    public float LeanMinDist = 2.0f;

    Vector3 childPos = Vector3.zero;
    bool inPlay = true;

    Canvas myCanvas = null;

    RectTransform rt;

    protected override void Start()
    {
        base.Start();
        myCanvas = GetComponentInParent<Canvas>();
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Vector2 mousePos = Input.mousePosition / myCanvas.scaleFactor;
        rt.anchoredPosition = mousePos - (myCanvas.GetComponent<RectTransform>().sizeDelta / 2.0f);

        if(currChild)
        {
            childPos = LerpVectorToVector(childPos, Vector3.zero);
            currChild.localPosition = childPos;

            currChild.localScale = LerpVectorToVector(currChild.localScale, inPlay ? InPlayLocalScale : ClickedLocalScale, 0.05f, 1.5f);

            Vector3 ang = currChild.localEulerAngles;
            Vector3 dir = transform.position - currChild.position;
            float dist = dir.magnitude;
            // Lean in the direction of the mouse if its far enough
            float targetAngle = 0.0f;
            if (dist >= LeanMinDist)
            {
                targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg * LeanStrength * (distanceScaling * distanceScaling);
            }
            ang.z = SlerpAngleToVal(ang.z, targetAngle);
            currChild.localEulerAngles = ang;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(currChild && collision.name == "HandArea")
        {
            inPlay = false;
            currChild.GetComponent<CardLogic>()?.ExitPlayArea();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currChild && collision.name == "HandArea")
        {
            inPlay = true;
            currChild.GetComponent<CardLogic>()?.EnterPlayArea();
        }
    }

    protected override void NewChild(RectTransform t)
    {
        if(t == null)
        {
            return;
        }
        childPos = t.localPosition;

        List<GameObject> remainingPlayableTiles = FindFirstObjectByType<GridManager>().GetPlayableTiles(t.gameObject);

        if (remainingPlayableTiles.Count <= 3)
        {
            foreach (GameObject tile in remainingPlayableTiles)
            {
                tile.GetComponent<ReactiveTile>().FinalThreeTile();
            }
        }
    }
}
