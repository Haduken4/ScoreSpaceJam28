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

    // Start is called before the first frame update
    void Start()
    {
        gm = FindFirstObjectByType<GridManager>();
        pd = GetComponent<PlantData>();
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
            pd.AddScore(Mathf.Ceil(toAdd));
        }

        if(VisualEffect)
        {
            Instantiate(VisualEffect, VFXPoint);
        }
    }

    protected virtual void GameplayEffect()
    {

    }
}
