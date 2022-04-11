using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] protected GameObject sprite;
    [SerializeField] protected GameObject hited;
    [SerializeField] protected GameObject hpBar;

    protected BossStatus Status;
    public float hp;
    protected float damage;
    public float DesDamage { get { return Status.desDamage; } }

    protected bool isAlive;

    protected virtual void Awake()
    {
        Status = JsonLoader.Load<BossStatus>("BossStatus");
        hp = Status.hp;
        damage = Status.damage;
        isAlive = true;

        if (!sprite) sprite = gameObject.transform.Find("sprite").gameObject;
        if (!hited) hited = gameObject.transform.Find("hited").gameObject;
        if (!hpBar) hpBar = gameObject.transform.Find("hpBar").gameObject;
    }

    private void Start()
    {
        StartCoroutine(Pattern());
    }

    private void Update()
    {
        hpBar.transform.localScale = new Vector3(10f * (hp / Status.hp), 5, 5);
    }

    protected virtual IEnumerator Pattern()
    {
        Vector3 pos = Player.Instance.transform.position + new Vector3(0, 0, 0);
        yield return MoveToPos(pos, 3, 3);

        int count = 0;
        while (isAlive)
        {

            switch (count)
            {
                case 0:
                    yield return pFiv();
                    break;
                case 1:
                    yield return pOne();
                    break;
                case 2:
                    yield return pTwo();
                    break;
                case 3:
                    yield return pThr();
                    break;
                case 4:
                    yield return pFor();
                    break;
                case 5:
                    yield return pFiv();
                    break;
            }

            count++;
            if (count >= 6) count = 0;
            yield return new WaitForSeconds(1f);
        }
    }

    #region PATTERN
    protected IEnumerator pOne()
    {
        StartCoroutine(MoveToPlayerPos(3f, 30, 5));
        int number_of_bullet = 7;
        float cool = 0;
        float alpha = 0;

        for (int k = 0; k < (360 / number_of_bullet) * 10; k++)
        {
            for (int i = 0; i < number_of_bullet; i++)
            {
                Vector3 rot = new Vector3(0, ((float)i / number_of_bullet) * 360f + 360f * alpha * 0.7f, 0);
                var n = InstantiateBullet(70, rot);
                n.SetSprite(true);
            }
            cool += 0.02f;
            alpha = Mathf.Sin(cool);
            yield return new WaitForSeconds(0.02f);
        }
    }

    protected IEnumerator pTwo()
    {
        StartCoroutine(MoveToPlayerPos(3f, 20, 5));
        int number_of_bullet = 300;
        float cool = 0;
        float alpha = 0;

        for (int i = 0; i < number_of_bullet; i++)
        {

            for (int k = 0; k < 6; k++)
            {
                Vector3 rot = new Vector3(0, i * 4f + (float)(k / 6f) * 360f * alpha, 0);
                var n = InstantiateBullet(50, rot);
            }
            for (int k = 0; k < 6; k++)
            {
                Vector3 rot = new Vector3(0, -i * 4f + (float)(k / 6f) * 360f * alpha, 0);
                var n = InstantiateBullet(50, rot);
                n.SetSprite(true);
            }
            cool += 0.04f;
            alpha = Mathf.Sin(cool);
            yield return new WaitForSeconds(0.04f);
        }
    }

    protected IEnumerator pThr()
    {
        int number_of_bullet = 5;

        for (int i = 0; i < number_of_bullet; i++)
        {
            if (i % 2 == 0)
                StartCoroutine(MoveToPlayerPos(0.5f, 10, 5, -10));
            else
                StartCoroutine(MoveToPlayerPos(0.5f, 10, 5, 10));

            for (int j = 0; j < 7; j++)
            {
                for (int k = 0; k < 6; k++)
                {
                    Vector3 rot = new Vector3(0, (k / 6f) * 360 + (float)i / number_of_bullet * 360f, 0);
                    var n = InstantiateBullet(90, rot);
                    n.SetSprite(true);
                }
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    #endregion

    protected IEnumerator pFor()
    {
        int number_of_bullet = 10;

        for (int i = 0; i < number_of_bullet; i++)
        {
            yield return MoveToPlayerPos(0.5f, 0, 3, 10);

            for (int k = 0; k < 30; k++)
            {
                Vector3 rot = new Vector3(0, (k / 20f) * 360 + (float)i / number_of_bullet * 360f, 0);
                var n = InstantiateBullet(90, rot);
                n.SetDelaySpeed(0, 0.4f);
                n.SetDelaySpeed(100, 1.2f);
                n.SetDelayTarget(Player.Instance.gameObject, 1.2f);

                n.SetSprite(true);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    protected IEnumerator pFiv()
    {
        int number_of_bullet = 4;

        for (int i = 0; i < number_of_bullet; i++)
        {
            yield return MoveToPlayerPos(0.5f, 0, 3, 10);

            for (int k = 0; k < 40; k++)
            {
                Vector3 rot = new Vector3(0, (k / 40f) * 360 + (float)i / number_of_bullet * 360f, 0);
                var n = InstantiateBullet(40, rot);
            }
            yield return new WaitForSeconds(0.1f);
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
        pos.z = nz;
        pos.x += nx;

        float cool = 0;
        while (cool < time)
        {
            cool += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);

            yield return new WaitForEndOfFrame();
        }
    }


    public virtual void IsReward()
    {
        if (hp > 0) return;

        GameManager.Instance.Clear = true;
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
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerLazer"))
        {
            hp -= 8f;
            IsReward();
        }
    }

    protected void ReturnSprite()
    {
        sprite.SetActive(true);
        hited.SetActive(false);
    }
}

public class BossStatus
{
    public float damage;
    public float hp;
    public int score;
    public float desDamage;

    public BossStatus()
    {
        desDamage = 35;
        damage = 10;
        hp = 20000;
        score = 100000;
    }
}