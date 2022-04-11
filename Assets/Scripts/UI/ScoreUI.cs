using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private List<Text> texts;

    public void Awake()
    {
        texts = new List<Text>();
        for (int i = 1; i <= 9; i++)
        {
            texts.Add(gameObject.transform.Find(i.ToString()).GetComponent<Text>());
        }
    }

    public void Update()
    {
        for (int i = 0; i < 9; i++)
        {
            if (ScoreManager.Instance.scores.Count <= i) break;
            texts[i].text = (i+1).ToString() + ". " + ScoreManager.Instance.scores[i].ToString();
        }
    }
}
