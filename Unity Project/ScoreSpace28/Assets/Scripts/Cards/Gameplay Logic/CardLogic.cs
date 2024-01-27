using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLogic : MonoBehaviour
{
    public int ManaCost = 0;
    public bool DiscardOnPlay = true;
    public GameObject DiscardAnimPrefab = null;
    bool inPlayArea = false;

    GridManager gm = null;

    private void Start()
    {
        gm = FindFirstObjectByType<GridManager>();
    }

    // Create indicators
    public void EnterPlayArea()
    {
        inPlayArea = true;

        BroadcastMessage("OnEnterPlayArea", SendMessageOptions.DontRequireReceiver);
    }
    
    // Remove indicators
    public void ExitPlayArea()
    {
        inPlayArea = false;

        BroadcastMessage("OnExitPlayArea", SendMessageOptions.DontRequireReceiver);
    }

    public bool PlayCard()
    {
        // check if we're over a tile


        return false;
    }

    public void RemoveCard()
    {
        Destroy(gameObject);
    }

    public bool InPlayArea()
    {
        return inPlayArea;
    }
}
