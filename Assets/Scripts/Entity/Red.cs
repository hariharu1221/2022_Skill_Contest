using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : Enemy
{
    protected override void Awake()
    {
        Status = JsonLoader.Load<EnemyStatus>("RedStatus");
        hp = Status.hp;
        exp = Status.exp;
        damage = Status.damage;
        isDead = false;

        if (!sprite) sprite = gameObject.transform.Find("sprite").gameObject;
        if (!hited) hited = gameObject.transform.Find("hited").gameObject;
        if (!hpBar) hpBar = gameObject.transform.Find("hpBar").gameObject;
    }

    public override void EnemyUpdate()
    {
        transform.Translate(new Vector3(0, 0, Time.deltaTime * Status.speed));
        hpBar.transform.localScale = new Vector3(0.5f * (hp / Status.hp), 1, 1);
    }

    public override void IsReward()
    {
        if (hp > 0 || isDead) return;

        GameManager.Instance.PainBar.NowGaugeValue += 5f;
        isDead = true;
        GameManager.Instance.Score += Status.score;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hp = 0;
            IsReward();
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
    }
}
