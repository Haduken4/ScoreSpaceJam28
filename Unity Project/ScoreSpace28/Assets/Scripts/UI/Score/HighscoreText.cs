using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreText : MonoBehaviour
{
    public bool Number = false;

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
        if(!Number)
        {
            text.text = tm.NewHighScore ? "NEW BEST!!!" : "BEST";
        }
        else
        {
            if(tm.NewHighScore)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                text.text = Mathf.Ceil(GlobalGameData.HighScore).ToString();
            }
        }
    }
}
