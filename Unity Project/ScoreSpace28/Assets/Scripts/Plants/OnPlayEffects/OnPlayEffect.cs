using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayEffect : MonoBehaviour
{
    public float ActivationDelay = 0.1f;

    public GameObject VisualEffect = null;
    public Transform VFXPoint = null;

    public float ScoreValue = 2;

    float timer = 0.0f;
    bool played = false;

    protected GridManager gm = null;

    protected float effectScore = 0;

    protected PlantData pd = null;

    protected TurnManager tm = null;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindFirstObjectByType<GridManager>();
        tm = FindFirstObjectByType<TurnManager>();
        pd = GetComponent<PlantData>();
        timer = ActivationDelay;
        //tm.GameplayEffectStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(played)
        {
            return;
        }

        timer -= Time.deltaTime;
        if(timer <= 0.0f && !played)
        {
            PlayEffect();
            played = true;
            tm.GameplayEffectStop();
        }
    }

    void PlayEffect()
    {
        GameplayEffect();

        float toAdd = (ScoreValue * GlobalGameData.PlantValueMultiplier) + effectScore;

        if(toAdd != 0)
        {
            pd.AddScore(Mathf.Ceil(toAdd));
        }

        if(VisualEffect)
        {
            Instantiate(VisualEffect, VFXPoint);
        }
    }

    protected virtual void GameplayEffect()
    {
        Debug.Log("Gameplay Effect Not Implemented: " + gameObject.name);
    }
}
