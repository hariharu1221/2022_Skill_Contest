using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_One : Enemy
{
    protected override void Awake()
    {
        Status = JsonLoader.Load<EnemyStatus>("Enemy_One_Status");
        hp = Status.hp;
        exp = Status.exp;
        damage = Status.damage;
        isDead = false;

        if (!sprite) sprite = gameObject.transform.Find("sprite").gameObject;
        if (!hited) hited = gameObject.transform.Find("hited").gameObject;
        if (!hpBar) hpBar = gameObject.transform.Find("hpBar").gameObject;

        if (shooter)
            shooter.SetShooter(damage, 80, 0.4f, EntityType.enemy, type);
    }

    private void Start()
    {
        StartCoroutine(Pattern());
    }

    private IEnumerator Pattern()
    {
        float random = Random.Range(-30, 30);
        yield return MoveToPlayerPos(3f, 30, 2, random);

        int count = 0;
        while(true)
        {
            switch(count)
            {
                case 0:
                    yield return pOne();
                    break;
                case 1:
                    yield return pTwo();
                    break;
                case 2:
                    yield return pEnd();
                    break;
            }

            count++;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator pOne()
    {
        float random = Random.Range(-30, 30);
        StartCoroutine(MoveToPlayerPos(3f, 30, 2, random));

        int number_of_bullet = 4;

        for (int i = 0; i < number_of_bullet; i++)
        {
            for (int k = 0; k < 20; k++)
            {
                Vector3 rot = new Vector3(0, (k / 40f) * 360 + (float)i / number_of_bullet * 360f, 0);
                var n = InstantiateBullet(40, rot);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator pTwo()
    {
        int number_of_bullet = 5;

        for (int i = 0; i < number_of_bullet; i++)
        {
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

    private IEnumerator pEnd()
    {
        int x;
        if (transform.position.x < 0) x = -1;
        else x = 1;

        while(true)
        {
            Vector3 pos = new Vector3(x, 0, -0.3f);
            transform.position += pos * Time.deltaTime * 70;
            yield return new WaitForEndOfFrame();
        }
    }

    public override void EnemyUpdate()
    {

    }
}
