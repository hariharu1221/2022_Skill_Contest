using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreManager : Singleton<ScoreManager>
{
    public List<int> scores = new List<int>();

    private int nowScore;
    private void Awake()
    {
        SetInstance();
        nowScore = 0;

        scores.Sort();
        scores.Reverse();
    }

    public void EndScore(int score)
    {
        nowScore += score;

        scores.Add(nowScore);
        scores.Sort();
        scores.Reverse();

        nowScore = 0;
    }

    public void SaveScore(int score)
    {
        nowScore += score;
    }
}

public class ScoreValue
{
    public int score;
    public string name;
}