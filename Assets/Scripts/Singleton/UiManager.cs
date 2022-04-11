using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : DesSingleton<UiManager>
{
    public Image hpGauge;
    public Image fuelGauge;
    public Image expGauge;
    public Image painGauge;
    public Text scoreText;
    public Text misson;

    private void Awake()
    {
        SetInstance();
    }

    private void Update()
    {
        hpGauge.fillAmount = (Player.Instance.hpGauge.NowGaugeValue / Player.Instance.hpGauge.MaxGaugeValue) * 0.78f + 0.22f;
        fuelGauge.fillAmount = (Player.Instance.fuelGauge.NowGaugeValue / Player.Instance.fuelGauge.MaxGaugeValue) * 0.78f + 0.22f;
        expGauge.fillAmount = Mathf.Lerp(expGauge.fillAmount, PlayerSkillSystem.Instance.nowExp / PlayerSkillSystem.Instance.MaxExp, Time.deltaTime * 5);
        painGauge.fillAmount = Mathf.Lerp(painGauge.fillAmount, GameManager.Instance.PainBar.NowGaugeValue / (GameManager.Instance.PainBar.MaxGaugeValue - 5), Time.deltaTime);
        int score = (int)(GameManager.Instance.Score);
        scoreText.text = score.ToString();
           
    }

    public void SetMisson(string text)
    {
        misson.text = text;
    }
}

[System.Serializable]
public class GaugeFrontBack
{
    public Image front;
    public Image back;
}