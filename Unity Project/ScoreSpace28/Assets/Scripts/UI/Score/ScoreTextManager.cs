using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextManager : MonoBehaviour
{
    public GameObject ScorePrefab = null;

    public void SpawnScoreText(Vector3 pos, float value)
    {
        GameObject t = Instantiate(ScorePrefab, pos, Quaternion.identity, transform);
        t.GetComponent<ScoreText>().ScoreValue = value;
    }
}
