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
        if(gm.HoveredTile == null)
        {
            return false;
        }

        TileLogic tl = gm.HoveredTile.GetComponent<TileLogic>();
        if(!tl.CanPlant(CardType))
        {
            return false;
        }

        if(PlantToSpawn != null)
        {
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

        return false;
    }

    bool PlayPlant(TileLogic tl)
    {
        if (tl.GetPlant() != null)
        {
            return false;
        }

        GameObject plant = Instantiate(PlantToSpawn, gm.HoveredTile.transform);
        tl.InitPlant(SpawnOffset);
        plant.transform.position -= Vector3.forward;
        plant.transform.localScale *= 0.1f; // Start plant small so it can grow

        tm.CardPlayed();

        AudioManager.instance.PlayOneShot(OnPlaySound, this.transform.position);

        //tm.GameplayEffectStart();

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
