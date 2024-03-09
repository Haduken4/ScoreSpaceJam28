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

    [HideInInspector]
    public GameObject Pollinator = null;

    float timer = 0.0f;
    bool played = false;

    protected GridManager gm = null;
    protected ScoreTextManager stm = null;

    protected float effectScore = 0;

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

        float toAdd = (ScoreValue * GlobalGameData.PlantValueMultiplier) + effectScore;

        if(toAdd != 0)
        {
            AddScore(Mathf.Ceil(toAdd));
        }

        if(VisualEffect)
        {
            Instantiate(VisualEffect, VFXPoint);
        }
    }

    public void AddScore(float val)
    {
        if(val == 0)
        {
            return;
        }

        GlobalGameData.Score += val;
        stm.SpawnScoreText(ScoreTextPoint.position, val);
    }

    protected abstract void GameplayEffect();
}
