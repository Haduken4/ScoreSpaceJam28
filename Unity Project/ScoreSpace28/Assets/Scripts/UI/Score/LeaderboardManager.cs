using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class LeaderboardData
{
    public int score;
    public string name;
}


public class LeaderboardManager : MonoBehaviour
{
    public string Leaderboard = "Classic Set";

    bool loaded = false;
    bool thinking = false;
    bool succeeded = false;

    // Start is called before the first frame update
    void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                // Failed to start session
                loaded = false;
                return;
            }

            loaded = true;
        });
    }

    public bool SubmitScore(int score)
    {
        if(!loaded || thinking)
        {
            return false;
        }

        thinking = true;
        LootLockerSDKManager.SubmitScore(GlobalGameData.PlayerName, score, Leaderboard, GlobalGameData.PlayerName, SubmitResponse);

        return true;
    }

    void SubmitResponse(LootLockerSubmitScoreResponse response)
    {
        thinking = false;
        if (response.statusCode == 200)
        {
            succeeded = true;
        }
        else
        {
            succeeded = false;
        }
    }

    public void GetScore(int place, LeaderboardData dataOut)
    {
        LootLockerSDKManager.GetScoreList(Leaderboard, place, (response) =>
        {
            if(response.statusCode == 200 && dataOut != null)
            {
                if(response.items.Length > place - 1)
                {
                    dataOut.name = response.items[place - 1].metadata;
                    dataOut.score = response.items[place - 1].score;

                    Debug.Log(response.text);
                }
            }
        });
    }

    public bool Loaded()
    {
        return loaded;
    }

    public bool Thinking()
    {
        return thinking;
    }

    public bool Succeeded()
    {
        return succeeded;
    }
}
