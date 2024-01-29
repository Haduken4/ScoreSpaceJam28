using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreText : MonoBehaviour
{
    TurnManager tm = null;
    TextMeshProUGUI text = null;

    // Start is called before the first frame update
    void Start()
    {
        tm = FindFirstObjectByType<TurnManager>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tm.NewHighScore)
        {
            text.text = "New High Score!!!";
            return;
        }

        text.text = "High Score: " + GlobalGameData.HighScore;
    }
}
