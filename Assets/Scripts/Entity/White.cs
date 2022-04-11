using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class White : Enemy
{
    protected override void Awake()
    {
        Status = JsonLoader.Load<EnemyStatus>("WhiteStatus");
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

        PlayerSkillSystem.Instance.PlusExp(exp);
        isDead = true;
        GameManager.Instance.Score += Status.score;
        GetRandomItem();
    }

    public void GetRandomItem()
    {
        int random = Random.Range(0, 4);

        switch(random)
        {
            case 0:
                PlayerSkillSystem.Instance.PlusExp(PlayerSkillSystem.Instance.MaxExp);
                break;
            case 1:
                Player.Instance.GetDamage(0, true);
                break;
            case 2:
                Player.Instance.hpGauge.NowGaugeValue += 10;
                break;
            case 3:
                GameManager.Instance.PainBar.NowGaugeValue -= 5f;
                break;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            hp -= other.GetComponent<Bullet>().GetDamage();

            sprite.SetActive(false);
            hited.SetActive(true);
            Invoke("ReturnSprite", 0.05f);

            IsReward();
        }
        if (other.CompareTag("Player"))
        {
            hp = 0;
            IsReward();
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerLazer"))
        {
            hp -= 5f;
            IsReward();
        }
    }
}
