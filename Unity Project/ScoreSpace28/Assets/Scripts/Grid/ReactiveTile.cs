using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReactiveTile : MonoBehaviour
{
    public Color DefaultTint = Color.white;
    public Color HoveredTint = new Color(0.9f, 0.9f, 0.7f, 1.0f);
    public Color LastThreeTilesTint = new Color(1.0f, 1.0f, 0.5f, 1.0f);

    public float LerpSpeed = 4.0f;
    public float SnapDist = 0.05f;

    GridManager gm = null;

    Color targetColor = Color.white;
    SpriteRenderer sr = null;
    TurnManager tm = null;
    ClickedCardParent ccp = null;

    // Start is called before the first frame update
    void Start()
    {
        targetColor = DefaultTint;
        gm = FindFirstObjectByType<GridManager>();
        tm = FindFirstObjectByType<TurnManager>();
        sr = GetComponent<SpriteRenderer>();
        ccp = FindFirstObjectByType<ClickedCardParent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tm.GameEnded)
        {
            sr.color = DefaultTint;
            return;
        }

        if(targetColor == LastThreeTilesTint && ccp.transform.childCount == 0)
        {
            targetColor = DefaultTint;
        }

        sr.color = Color.Lerp(sr.color, targetColor, Time.deltaTime * LerpSpeed);
        if(ColorDist(sr.color, targetColor) < SnapDist)
        {
            sr.color = targetColor;
        }
    }

    public void CursorEnter()
    {
        if (gm.HoveredTile != null)
        {
            gm.HoveredTile.targetColor = DefaultTint;
        }
        gm.HoveredTile = this;
        targetColor = HoveredTint;
    }

    public void FinalThreeTile()
    {
        targetColor = LastThreeTilesTint;
    }

    public void CursorExit()
    {
        gm.HoveredTile = null;
        targetColor = DefaultTint;
    }

    float ColorDist(Color c1, Color c2)
    {
        Vector4 v1 = c1;
        Vector4 v2 = c2;

        return Vector4.Distance(v1, v2);
    }
}
