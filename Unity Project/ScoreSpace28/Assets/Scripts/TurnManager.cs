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

    public float TurnTransitionMinTime = 2.0f;
    public float CreatureDelay = 0.1f;
    public float CreatureGraceTimer = 5.0f;

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

    [HideInInspector]
    public bool TurnEnded = false;

    [HideInInspector]
    public bool TutorialPopup = true;

    int activeGameplayEffects = 0;
    float changeTimer = 0;

    void Awake()
    {
        GlobalGameData.Score = 0;
        GlobalGameData.PlantValueMultiplier = 1.0f;
        JungleSetData.BirdOfParadiseBonus = 0;
    }

    private void Start()
    {
    }

    void Update()
    {
        // If the tutorial popup is open, don't start the game yet
        if (TutorialPopup)
        {
            return;
        }

        // Countdown to starting the game
        if(InitialTimer > 0.0f)
        {
            InitialTimer -= Time.deltaTime;
            if(InitialTimer <= 0.0f)
            {
                TurnStart();
            }
            return;
        }

        // Timer to make sure we don't get softlocked by some bad gameplay effects
        changeTimer -= Time.deltaTime;
        if (changeTimer <= 0.0f)
        {
            activeGameplayEffects = 0;
            changeTimer = CreatureGraceTimer;
        }

        // Restart cheat code
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.S))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // When the game ends and we finish the last turn, start counting down to results
        if (GameEnded && !TurnEnded)
        {
            hm.DiscardHand();
            timer -= Time.deltaTime;
            if(timer <= 0.0f)
            {
                FinalScoreWindow.SetActive(true);
            }

            return;
        }
        else if(activeGameplayEffects == 0) // Otherwise, if nothing is active right now check if the game is over
        {
            CheckEndCondition();
        }

        // Counting down to next turn
        if(timer > 0.0f && TurnEnded && activeGameplayEffects == 0)
        {
            timer -= Time.deltaTime;

            if(timer <= 0.0f)
            {
                TurnStart();
                TurnEnded = false;
            }
            return;
        }
        
        // All cards have been played and no effects are still active
        if(cardsPlayed >= PlaysPerTurn && activeGameplayEffects == 0)
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

        if (TurnEnded)
        {
            return;
        }

        bool actuallyOver = GameEnded;
        GameEnded = false;

        if (!gm.DoneMakingGrid())
        {
            return;
        }

        if(gm.UnresolvedTilesCheck())
        {
            return;
        }

        // Check if we can still play any cards or we are still drawing some cards
        if (hm.GetCardObjects().Count == 0 || cards.CheckCardsPlayable(hm.GetCardObjects()) || cards.CurrentlyDrawing)
        {
            return;
        }

        if (!actuallyOver)
        {
            GameEnded = true;
            TurnEnd();
            return;
        }
        
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
        TurnEnded = true;

        hm.DiscardHand();

        // Reset turn specific plant data
        PlantData[] pds = FindObjectsOfType<PlantData>();
        foreach(PlantData pd in pds)
        {
            pd.ResetPollinate();
        }

        // Activate end of turn effects
        EndOfTurnEffect[] eots = FindObjectsOfType<EndOfTurnEffect>();
        foreach(EndOfTurnEffect eot in eots)
        {
            eot.OnEndOfTurn();
        }

        // Activate creature end of turns
        CreatureBehavior[] creatures = FindObjectsOfType<CreatureBehavior>();
        int i = 0;
        foreach(CreatureBehavior creature in creatures)
        {
            StartCoroutine(ActivateCreatureEffect(creature, i * CreatureDelay));
            ++i;
        }

        timer = TurnTransitionMinTime;
        changeTimer = CreatureGraceTimer;
    }

    public void CardPlayed()
    {
        cardsPlayed++;
        GameplayEffectStart();
        //CheckEndCondition();
    }

    public bool CanTrash()
    {
        return cardsTrashed < TrashesPerTurn;
    }

    public void CardTrashed()
    {
        cardsTrashed++;
    }

    public void GameplayEffectStart()
    {
        activeGameplayEffects += 1;
        changeTimer = CreatureGraceTimer;
    }

    public void GameplayEffectStop()
    {
        activeGameplayEffects = Mathf.Max(0, activeGameplayEffects - 1);
        changeTimer = CreatureGraceTimer;
    }

    IEnumerator ActivateCreatureEffect(CreatureBehavior creature, float delay)
    {
        yield return new WaitForSeconds(delay);

        creature.OnEndOfTurn();
    }
}
