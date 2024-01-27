using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnPlayEffect : MonoBehaviour
{
    public float ActivationDelay = 0.1f;

    public GameObject VisualEffect = null;
    public Transform VFXPoint = null;

    public Transform ScoreTextPoint = null;
    public float ScoreValue = 2;
    public bool IsTree = false;

    float timer = 0.0f;
    bool played = false;

    protected GridManager gm = null;
    protected ScoreTextManager stm = null;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindFirstObjectByType<GridManager>();
        stm = FindFirstObjectByType<ScoreTextManager>();
        timer = ActivationDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(played)
        {
            return;
        }

        timer -= Time.deltaTime;
        if(timer <= 0.0f)
        {
            PlayEffect();
            played = true;
        }
    }

    void PlayEffect()
    {
        GameplayEffect();
        
        float toAdd = IsTree ? ScoreValue * GlobalGameData.TreeMultiplier : ScoreValue * GlobalGameData.NonTreeMultiplier;

        AddScore(ScoreValue);

        if(VisualEffect)
        {
            Instantiate(VisualEffect, VFXPoint);
        }
    }

    public void AddScore(float val)
    {
        GlobalGameData.Score += val;
        stm.SpawnScoreText(ScoreTextPoint.position, val);
    }

    protected abstract void GameplayEffect();
}
