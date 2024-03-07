using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

public class CardPile : MonoBehaviour
{
    public float CardDrawDelay = 0.1f;

    public Transform DeckObject = null;

    HandManager hm = null;
    DeckRandom<GameObject> cardPrefabs = new DeckRandom<GameObject>();

    public List<GameObject> DebugDeckCards = new List<GameObject>();
    public int DebugInitialDraw = 0;

    [SerializeField] private EventReference drawPacketsSound;

    // Start is called before the first frame update
    void Awake()
    {
        hm = FindFirstObjectByType<HandManager>();

        cardPrefabs.AddOptions(DebugDeckCards);
        if(DebugInitialDraw != 0)
        {
            DrawX(DebugInitialDraw);
        }
    }

    public void DrawX(int toDraw)
    {
        List<GameObject> drawn = cardPrefabs.DrawX(toDraw);

        if(!CheckCardsPlayable(drawn))
        {
            FindFirstObjectByType<TurnManager>().GameEnded = true;
            return;
        }

        int i = 0;
        foreach(GameObject prefab in drawn)
        {
            StartCoroutine(CreateAndInitCard(prefab, i * CardDrawDelay));
            ++i;
        }
        AudioManager.instance.PlayOneShot(drawPacketsSound, this.transform.position);

    }

    public bool CheckCardsPlayable(List<GameObject> cards)
    {
        TileLogic[] tiles = FindObjectsOfType<TileLogic>();

        List<PlantType> checkedTypes = new List<PlantType>();

        foreach (GameObject card in cards)
        {
            PlantType type = card.GetComponent<CardLogic>().CardType;
            if(checkedTypes.Contains(type))
            {
                continue;
            }

            // Check each tile to see if we can play this card on it
            foreach(TileLogic tl in tiles)
            {
                if(tl.CanPlant(type))
                {
                    return true; // If we can play any card, we should be fine here
                }
            }

            checkedTypes.Add(type);
        }

        return false;
    }

    IEnumerator CreateAndInitCard(GameObject prefab, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject card = Instantiate(prefab, hm.transform);
        card.transform.position = DeckObject.transform.position;
        card.transform.localScale *= 0.1f;

        hm.AddCard(card.transform);
    }
}
