using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public float InitialTimer = 3.0f;

    public int DrawPerTurn = 5;

    public int PlaysPerTurn = 3;
    int cardsPlayed = 0;
    public int TrashesPerTurn = 2;
    int cardsTrashed = 0;

    public float TurnTransitionTime = 2.0f;
    public float TurnTransitionTimeBees = 4.0f;

    public int GameEndMusicSwap = 2;

    public HandManager hm = null;
    public GridManager gm = null;
    public CardPile cards = null;
    public GameObject FinalScoreWindow = null;

    float timer = 0.0f;

    [HideInInspector]
    public bool NewHighScore = false;

    [HideInInspector]
    public bool GameEnded = false;

    void Awake()
    {
        GlobalGameData.Score = 0;
        GlobalGameData.PlantValueMultiplier = 1.0f;
    }

    private void Start()
    {
    }

    void Update()
    {
        if(InitialTimer > 0.0f)
        {
            InitialTimer -= Time.deltaTime;
            if(InitialTimer <= 0.0f)
            {
                TurnStart();
            }
            return;
        }

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (GameEnded)
        {
            hm.DiscardHand();
            timer -= Time.deltaTime;
            if(timer <= 0.0f)
            {
                FinalScoreWindow.SetActive(true);
            }

            return;
        }
        else
        {
            CheckEndCondition();
        }

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

    void CheckEndCondition()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.G) && Input.GetKeyDown(KeyCode.E))
        {
            GameEnded = true;
            if (GlobalGameData.Score > GlobalGameData.HighScore)
            {
                NewHighScore = true;
                GlobalGameData.HighScore = GlobalGameData.Score;
            }
            timer = 2.0f;
            return;
        }

        if(!gm.DoneMakingGrid())
        {
            return;
        }

        foreach (TileLogic tl in FindObjectsOfType<TileLogic>())
        {
            if (tl.GetPlant() == null)
            {
                return;
            }
        }

        // Check if we can still play any cards
        if (cards.CheckCardsPlayable(hm.GetCardObjects()))
        {
            return;
        }

        GameEnded = true;
        if (GlobalGameData.Score > GlobalGameData.HighScore)
        {
            NewHighScore = true;
            GlobalGameData.HighScore = GlobalGameData.Score;
        }
        timer = 2.0f;
        AudioManager.instance.SetMusicParameter(GameEndMusicSwap);
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

        // Activate end of turn effects
        EndOfTurnEffect[] eots = FindObjectsOfType<EndOfTurnEffect>();
        foreach(EndOfTurnEffect eot in eots)
        {
            eot.OnEndOfTurn();
        }

        // Activate creature end of turns
        CreatureBehavior[] creatures = FindObjectsOfType<CreatureBehavior>();
        foreach(CreatureBehavior creature in creatures)
        {
            creature.OnEndOfTurn();
        }

        timer = creatures.Length > 0 ? TurnTransitionTimeBees : TurnTransitionTime;
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
