using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public float FadeTime = 0.25f;
    public float TargetAlpha = 0.0f;

    float timer = 0.0f;
    float startAlpha = 1.0f;
    string newScene = "";
    SpriteRenderer sr = null;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = startAlpha;
        sr.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < FadeTime)
        {
            timer += Time.deltaTime;
            Color c = sr.color;
            c.a = Mathf.Lerp(startAlpha, TargetAlpha, timer / FadeTime);
            if(timer >= FadeTime)
            {
                c.a = TargetAlpha;
                if(newScene.Length != 0)
                {
                    SceneManager.LoadScene(newScene);
                }
            }

            sr.color = c;
        }
    }

    public void StartChangingScenes(string scene)
    {
        newScene = scene;
        timer = 0.0f;
        startAlpha = 0.0f;
        TargetAlpha = 1.0f;
    }
}
