using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantIdentifier
{
    INVALID = 0,
    ROOT = 1,


    // CLASSIC SET //
    CLASSIC_REDCAP = 10,
    CLASSIC_PARASITIC = 11,
    CLASSIC_LILY = 12,
    CLASSIC_ASTER = 13,
    CLASSIC_SHRUB = 14,
    CLASSIC_VFT = 15,
    CLASSIC_MANGROVE = 16,
    CLASSIC_WILLOW = 17,
    CLASSIC_GRASS = 18,
    CLASSIC_BAMBOO = 19,

    // JUNGLE SET //
    JUNGLE_GLOWSHROOM = 20,
    JUNGLE_EARTHSTAR = 21,
    JUNGLE_ROTSHROOM = 22,
    JUNGLE_HELICONIA = 23,
    JUNGLE_BOP = 24,
    JUNGLE_COFFEE = 25,
    JUNGLE_GINGER = 26,
    JUNGLE_KAPOK = 27,
    JUNGLE_CANNONBALL = 28,
    JUNGLE_MAHOGANY = 29
}

public class PlantData : MonoBehaviour
{
    public PlantType Type = PlantType.MUSHROOM;
    public PlantIdentifier Identifier = PlantIdentifier.INVALID;

    public Transform ScoreTextPoint = null;

    [HideInInspector]
    public GameObject Pollinator = null;
    [HideInInspector]
    public float TotalScoreGained = 0;

    float scoreThisFrame = 0;
    ScoreTextManager stm = null;

    private void Start()
    {
        stm = FindFirstObjectByType<ScoreTextManager>();
        if(ScoreTextPoint == null)
        {
            ScoreTextPoint = transform;
        }    
    }

    private void Update()
    {
        if (scoreThisFrame != 0)
        {
            AddScoreReal(scoreThisFrame);
            scoreThisFrame = 0;
        }
    }

    public void AddScore(float val)
    {
        scoreThisFrame += val;
    }

    public void AddScoreNoDisplay(float val)
    {
        GlobalGameData.Score += val;
        TotalScoreGained += val;
    }

    void AddScoreReal(float val)
    {
        if (val == 0)
        {
            return;
        }

        GlobalGameData.Score += val;
        DisplayScore(val);
        TotalScoreGained += val;
    }

    public void DisplayScore(float val)
    {
        stm.SpawnScoreText(ScoreTextPoint.position, val);
    }
}
