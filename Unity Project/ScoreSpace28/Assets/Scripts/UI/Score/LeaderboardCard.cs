using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardCard : MonoBehaviour
{
    public int Place = 1;
    public TextMeshProUGUI PlaceText = null;
    public TextMeshProUGUI NameText = null;
    public TextMeshProUGUI ScoreText = null;

    LeaderboardManager lm = null;

    LeaderboardData data = new LeaderboardData();

    float timer = 0.5f;
    int tries = 0;

    // Start is called before the first frame update
    void Start()
    {
        lm = FindFirstObjectByType<LeaderboardManager>();
        data.score = 0;
        data.name = "";
        lm.GetScore(Place, data);
    }

    // Update is called once per frame
    void Update()
    {
        if(!lm.Loaded())
        {
            PlaceText.text = Place.ToString();
            NameText.text = "Leaderboard Can't Connect";
            ScoreText.text = "";

            return;
        }
        else if(data.name == "" && tries < 3)
        {
            timer -= Time.deltaTime;
            if(timer <= 0.0f)
            {
                lm.GetScore(Place, data);
                timer = Random.Range(0.3f, 0.6f);
                ++tries;
            }
        }

        PlaceText.text = Place.ToString();
        NameText.text = data.name;
        ScoreText.text = data.score.ToString();
    }

    public void Refresh()
    {
        lm.GetScore(Place, data);
        tries = 0;
    }
}
