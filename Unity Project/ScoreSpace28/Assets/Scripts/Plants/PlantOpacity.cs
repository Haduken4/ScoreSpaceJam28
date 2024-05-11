using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantOpacity : MonoBehaviour
{
    public float PlayingOpacity = 0.5f;
    public float LerpSpeed = 0.3f;

    ClickedCardParent ccp = null;
    SpriteRenderer sr = null;

    // Start is called before the first frame update
    void Start()
    {
        ccp = FindFirstObjectByType<ClickedCardParent>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color c = sr.color;

        if (ccp.transform.childCount == 1)
        {
            c.a = Mathf.Lerp(c.a, PlayingOpacity, LerpSpeed);
        }
        else
        {
            c.a = Mathf.Lerp(c.a, 1.0f, LerpSpeed);
        }

        sr.color = c;
    }
}
