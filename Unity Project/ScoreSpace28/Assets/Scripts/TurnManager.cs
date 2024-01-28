using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int DrawPerTurn = 5;

    public int PlaysPerTurn = 3;
    int cardsPlayed = 0;
    public int TrashesPerTurn = 2;
    int cardsTrashed = 0;

    public float TurnTransitionTime = 2.0f;
    public float TurnTransitionTimeBees = 4.0f;

    public HandManager hm = null;
    public CardPile cards = null;

    float timer = 0.0f;

    void Awake()
    {
        
    }

    private void Start()
    {
        TurnStart();
    }

    void Update()
    {
        // Counting down to next turn
        if(timer > 0.0f && cardsPlayed == PlaysPerTurn)
        {
            timer -= Time.deltaTime;

            if(timer <= 0.0f)
            {
                TurnStart();
            }
            return;
        }
        
        if(cardsPlayed >= PlaysPerTurn)
        {
            TurnEnd();
        }
    }

    void TurnStart()
    {
        Debug.Log("Turn Started");
        cards.DrawX(DrawPerTurn);
        cardsPlayed = 0;
        cardsTrashed = 0;

        timer = -1.0f; // Make sure turn doesnt end early
    }

    void TurnEnd()
    {
        Debug.Log("Turn Ended");
        hm.DiscardHand();

        // Activate bees
        GameObject[] bees = GameObject.FindGameObjectsWithTag("Bee");
        foreach(GameObject bee in bees)
        {
            BeeBehavior bb = bee.GetComponent<BeeBehavior>();
        }

        timer = bees.Length > 0 ? TurnTransitionTimeBees : TurnTransitionTime;
    }

    public void CardPlayed()
    {
        cardsPlayed++;
    }

    public bool CanTrash()
    {
        return cardsTrashed < TrashesPerTurn;
    }

    public void CardTrashed()
    {
        cardsTrashed++;
    }
}
