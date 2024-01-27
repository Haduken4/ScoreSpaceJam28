using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardPile : MonoBehaviour
{
    public float CardDrawDelay = 0.1f;

    public Transform DeckObject = null;

    HandManager hm = null;
    DeckRandom<GameObject> cardPrefabs = new DeckRandom<GameObject>();

    public List<GameObject> DebugDeckCards = new List<GameObject>();
    public int DebugInitialDraw = 0;

    // Start is called before the first frame update
    void Awake()
    {
        hm = FindFirstObjectByType<HandManager>();

        cardPrefabs.AddOptions(DebugDeckCards);
    }

    public void DrawX(int toDraw)
    {
        List<GameObject> drawn = cardPrefabs.DrawX(toDraw);

        int i = 0;
        foreach(GameObject prefab in drawn)
        {
            StartCoroutine(CreateAndInitCard(prefab, i * CardDrawDelay));
            ++i;
        }
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
