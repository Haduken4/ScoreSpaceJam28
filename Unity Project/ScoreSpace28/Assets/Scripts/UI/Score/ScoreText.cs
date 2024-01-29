using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public string PreText = "Score: ";
    public Vector3 VelocityStart = Vector2.zero;
    public Vector3 Acceleration = Vector2.zero;
    public Vector3 VelocityMin = Vector2.zero;

    public float ScoreValue = 0;

    TextMeshProUGUI text = null;
    Vector3 vel = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        vel = VelocityStart;
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ScoreValue == 0 ? PreText + Mathf.Ceil(GlobalGameData.Score) : PreText + ScoreValue;

        vel += Acceleration * Time.deltaTime;
        vel = Vector3.Max(vel, VelocityMin);
        transform.parent.position += vel * Time.deltaTime;
    }
}
