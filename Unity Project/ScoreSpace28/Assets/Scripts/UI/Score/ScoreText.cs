using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public string PreText = "Score: ";

    public float ScoreValue = 0;

    TextMeshProUGUI text = null;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ScoreValue == 0 ? PreText + Mathf.Ceil(GlobalGameData.Score) : PreText + ScoreValue;
    }
}
