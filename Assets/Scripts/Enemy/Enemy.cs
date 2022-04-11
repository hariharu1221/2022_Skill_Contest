using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected BulletShooter shooter;
    [SerializeField] protected BulletType type;
    [SerializeField] protected GameObject sprite;
    [SerializeField] protected GameObject hited;
    [SerializeField] protected GameObject hpBar;

    protected EnemyStatus Status;
    protected float hp;
    protected float exp;
    protected float damage;
    public float DesDamage { get { return Status.desDamage; } }

    protected bool isDead;
    public bool IsDead { get { return isDead; } }

    protected virtual void Awake()
    {
        Status = JsonLoader.Load<EnemyStatus>("EnemyStatus");
        hp = Status.hp;
        exp = Status.exp;
        damage = Status.damage;
        isDead = false;

        if (!sprite) sprite = gameObject.transform.Find("sprite").gameObject;
        if (!hited) hited = gameObject.transform.Find("hited").gameObject;
        if (!hpBar) hpBar = gameObject.transform.Find("hpBar").gameObject;

        if (shooter)
            shooter.SetShooter(damage, 80, 0.8f, EntityType.enemy, type);
    }

    private void Start()
    {
        
    }

    public void HpMult(float mult)
    {
        Status.hp *= mult;
        hp *= mult;
    }

    public virtual void EnemyUpdate()
    {
        transform.Translate(new Vector3(0, 0, Time.deltaTime * Status.speed));
        hpBar.transform.localScale = new Vector3(0.5f * (hp / Status.hp), 1, 1);
    }

    public virtual void IsReward()
    {
        if (hp > 0 || isDead) return;

        PlayerSkillSystem.Instance.PlusExp(exp);
        isDead = true;
        GameManager.Instance.Score += Status.score;
    }

    protected IEnumerator MoveToPos(Vector3 pos, float time, float speed)
    {
        float cool = 0;
        while (cool < time)
        {
            cool += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);

            yield return new WaitForEndOfFrame();
        }
    }

    protected IEnumerator MoveToPlayerPos(float time, float nz, float speed, float nx = 0)
    {
        Vector3 pos = Player.Instance.transform.position;
        pos.z += nz;
        pos.x += nx;

        float cool = 0;
        while (cool < time)
        {
            cool += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);

            yield return new WaitForEndOfFrame();
        }
    }

    protected Bullet InstantiateBullet(float speed, Vector3 rotation, float damageMult = 1)
    {
        var n = BulletPooling.Instance.GetBullet();
        n.SetBullet(speed, damage * damageMult, EntityType.boss, rotation);
        n.transform.position = transform.position;
        BulletSubject.Instance.AddBullet(n);

        return n;
    }

    protected virtual void OnTriggerEnter(Collider other)
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

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerLazer"))
        {
            hp -= 2f;
            IsReward();
        }
    }

    protected void ReturnSprite()
    {
        sprite.SetActive(true);
        hited.SetActive(false);
    }
}

public class EnemyStatus
{
    public float damage;
    public float hp;
    public float exp;
    public int score;
    public float desDamage;
    public float speed;

    public EnemyStatus()
    {
        desDamage = 15;
        damage = 10;
        hp = 30;
        exp = 20;
        score = 1000;
        speed = 50;
    }
}