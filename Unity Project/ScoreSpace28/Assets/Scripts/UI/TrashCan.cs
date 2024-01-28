using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public Sprite OpenSprite = null;
    public Sprite ClosedSprite = null;

    SpriteRenderer sr = null;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void OpenLid()
    {
        sr.sprite = OpenSprite;
    }

    public void CloseLid()
    {
        sr.sprite = ClosedSprite;
    }
}
