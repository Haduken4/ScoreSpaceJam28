using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public enum PlantType { MUSHROOM, FLOWER, SHRUB, TREE, GRASS, ROOT }

public class CardLogic : MonoBehaviour
{
    public GameObject PlantToSpawn = null;
    public Vector3 SpawnOffset = Vector3.zero;
    public bool TileModifier = false;

    public PlantType CardType = PlantType.MUSHROOM;

    public EventReference OnPlaySound;

    bool inPlayArea = false;

    GridManager gm = null;
    TurnManager tm = null;

    private void Start()
    {
        gm = FindFirstObjectByType<GridManager>();
        tm = FindFirstObjectByType<TurnManager>();
    }

    // Create indicators
    public void EnterPlayArea()
    {
        // Make sure its still our turn
        if (!tm.TurnEnded)
        {
            inPlayArea = true;
        }

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
        Debug.Log("Trying to play card");

        ReactiveTile backupTile = FindFirstObjectByType<FollowCursor>().GetFirstHitTile();
        if(gm.HoveredTile == null && backupTile == null)
        {
            Debug.Log("Backup AND hovered tile were null");
            return false;
        }

        TileLogic tl = gm.HoveredTile.GetComponent<TileLogic>();
        if (tl == null)
        {
            Debug.Log("Hovered tile was null, using backup");
            tl = backupTile.GetComponent<TileLogic>();
        }

        if(!tl.CanPlant(CardType))
        {
            Debug.Log("Not allowed to plant on this tile");
            return false;
        }

        if(PlantToSpawn != null)
        {
            Debug.Log("Attempting to spawn plant");
            return PlayPlant(tl);
        }
        else if(TileModifier && tl.GetPlant() == null)
        {
            // Get tile modifier component and modify the tile I guess
            GetComponent<CardTileModifier>().ModifyTile(tl);
            tm.CardPlayed();
            tm.GameplayEffectStop();
            AudioManager.instance.PlayOneShot(OnPlaySound, this.transform.position);
            return true;
        }


        Debug.Log("No plant to spawn and not a tile modifier");
        return false;
    }

    bool PlayPlant(TileLogic tl)
    {
        if (tl.GetPlant() != null)
        {
            Debug.Log("The tile has a plant (should be impossible here)");
            return false;
        }

        GameObject plant = Instantiate(PlantToSpawn, gm.HoveredTile.transform);
        tl.InitPlant(SpawnOffset);
        plant.transform.position -= Vector3.forward;
        plant.transform.localScale *= 0.1f; // Start plant small so it can grow

        tm.CardPlayed();

        AudioManager.instance.PlayOneShot(OnPlaySound, this.transform.position);

        //tm.GameplayEffectStart();

        Debug.Log("Successfuly spawned the plant and played the card");
        return true;
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
