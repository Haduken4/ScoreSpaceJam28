using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLogic : MonoBehaviour
{
    public int ManaCost = 0;
    public bool DiscardOnPlay = true;
    public GameObject PlantToSpawn = null;
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
        if(gm.HoveredTile != null && PlantToSpawn != null)
        {
            TileLogic tl = gm.HoveredTile.GetComponent<TileLogic>();
            if(tl.GetPlant() != null)
            {
                return false;
            }

            GameObject plant = Instantiate(PlantToSpawn, gm.HoveredTile.transform);
            tl.InitPlant();
            plant.transform.position -= Vector3.forward;
            plant.transform.localScale *= 0.1f; // Start plant small so it can grow

            FindFirstObjectByType<TurnManager>().CardPlayed();

            return true;
        }

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
