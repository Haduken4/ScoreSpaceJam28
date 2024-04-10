using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodeManager : MonoBehaviour
{
    // Keep this list to 10 or less, its intended to be the cards for a given set
    public List<GameObject> SetCardList = new List<GameObject>();

    public KeyCode CheatKey = KeyCode.LeftShift;

    public HandManager HM = null;

    [HideInInspector]
    public bool UsedCheats = false;

    private void Awake()
    {
        UsedCheats = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(CheatKey))
        {
            // Check for card spawn cheats
            for(int i = 0; i < 10; ++i)
            {
                if(Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    SpawnCard(i);
                }
            }
        }
    }

    void SpawnCard(int cardIndex)
    {
        if(HM.GetCardObjects().Count >= HM.MaxHandSize)
        {
            return;
        }

        UsedCheats = true;

        GameObject card = Instantiate(SetCardList[cardIndex], HM.transform);
        card.transform.position = Vector3.zero;
        card.transform.localScale *= 0.1f;

        HM.AddCard(card.transform);
    }
}
