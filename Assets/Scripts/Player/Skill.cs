using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    public string text;
    public string name;
    public string spriteAddress;

    protected int level;
    protected float damage;
    protected float cool;
    protected int index;
    public int Index { get { return index; } }

    public float Damage { get { return damage + Player.Instance.Status.damage + PlayerSkillSystem.Instance.Level; } }


    public abstract void LevelUp();
    public virtual IEnumerator Passive()
    {
        yield return null;
    }
}

public class Skill_Zero : Skill
{
    public Skill_Zero()
    {
        text = "shot bullet rot circle";
        name = "Circle Bullet";
        spriteAddress = "Skill_Zero";
        level = 0;
        damage = 5;
        cool = 5;
        index = 0;
    }

    private int number_of_bullet = 20;

    public override void LevelUp()
    {
        level++;

        if (level == 0) cool = 5;
        if (level >= 1) cool = 4;
        if (level >= 2) damage = 7;
        if (level >= 3) number_of_bullet = 25;
        if (level >= 4) cool = 3;
        if (level >= 5) damage = 10;
        if (level >= 6) cool = 2;
        if (level >= 7) number_of_bullet = 30;
        if (level >= 8) number_of_bullet = 35;
        if (level >= 8) damage = 14;
        if (level >= 9)
        {
            damage = 7 + level;
        }
    }

    public override IEnumerator Passive()
    {
        while (true)
        {
            for (int i = 0; i < number_of_bullet; i++)
            {
                var n = BulletPooling.Instance.GetBullet();
                Vector3 rot = new Vector3(0, 360 * ((float)i / number_of_bullet), 0);
                n.SetBullet(90, damage, EntityType.player, rot);
                n.transform.position = Player.Instance.transform.position;
                BulletSubject.Instance.AddBullet(n);
                Debug.Log("1");
            }
            yield return new WaitForSeconds(cool);
        }
    }


}

public class Skill_One : Skill
{
    public Skill_One()
    {
        text = "주변에 백신을 흝뿌립니다.";
        name = "Book";
        spriteAddress = "Skill_Zero";
        level = 0;
        damage = 5;
        cool = 6;
        index = 1;
    }

    private int number_of_bullet = 50;

    public override void LevelUp()
    {
        level++;

        if (level == 0) cool = 6;
        if (level >= 1) cool = 5;
        if (level >= 2) damage = 7;
        if (level >= 3) number_of_bullet = 75;
        if (level >= 4) cool = 4;
        if (level >= 5) damage = 10;
        if (level >= 6) cool = 3;
        if (level >= 7) number_of_bullet = 100;
        if (level >= 8) number_of_bullet = 125;
        if (level >= 8) damage = 14;
        if (level >= 9)
        {
            damage = 7 + level;
        }
    }

    public override IEnumerator Passive()
    {
        while (true)
        {
            for (int i = 0; i < number_of_bullet; i++)
            {
                var n = BulletPooling.Instance.GetBullet();
                Vector3 rot = new Vector3(0, i * 10, 0);
                n.SetBullet(90, damage, EntityType.player, rot);
                n.transform.position = Player.Instance.transform.position;
                BulletSubject.Instance.AddBullet(n);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(cool);
        }
    }


}

public class Skill_Two : Skill
{
    public Skill_Two()
    {
        text = "일정 주기로 hp를 회복합니다.";
        name = "Natural Health";
        spriteAddress = "Skill_Zero";
        level = 0;
        damage = 5;
        cool = 1;
        index = 2;
    }

    private float health = 1;

    public override void LevelUp()
    {
        level++;

        if (level == 0) health = 1;
        if (level >= 1) health = 1.2f;
        if (level >= 2) health = 1.4f;
        if (level >= 3) health = 1.6f;
        if (level >= 4) health = 1.8f;
        if (level >= 5) health = 2f;
        if (level >= 6) health = 2.2f;
        if (level >= 7) health = 2.4f;
        if (level >= 8) health = 2.6f;
        if (level >= 8) cool = 0.8f;
        if (level >= 9)
        {
            health = 2f + 0.1f * level;
        }
    }

    public override IEnumerator Passive()
    {
        while (true)
        {
            Player.Instance.hpGauge.NowGaugeValue += health;
            yield return new WaitForSeconds(cool);
        }
    }


}

public class Skill_Thr : Skill
{
    public Skill_Thr()
    {
        text = "근처 적을 노려 쏩니다";
        name = "Target Bullet";
        spriteAddress = "Skill_Zero";
        level = 0;
        damage = 5;
        cool = 5;
        index = 3;
    }

    private int number_of_bullet = 20;

    public override void LevelUp()
    {
        level++;

        if (level == 0) cool = 7;
        if (level >= 1) cool = 6;
        if (level >= 2) damage = 7;
        if (level >= 3) number_of_bullet = 25;
        if (level >= 4) cool = 5;
        if (level >= 5) damage = 10;
        if (level >= 6) cool = 4;
        if (level >= 7) number_of_bullet = 30;
        if (level >= 8) number_of_bullet = 35;
        if (level >= 8) damage = 14;
        if (level >= 9)
        {
            damage = 7 + level;
        }
    }

    public override IEnumerator Passive()
    {
        while (true)
        {
            List<Bullet> bullets = new List<Bullet>();
            for (int i = 0; i < number_of_bullet; i++)
            {
                var n = BulletPooling.Instance.GetBullet();
                n.transform.position = new Vector3(-40 + 80f * i / number_of_bullet, 0, -5) + Player.Instance.transform.position;
                Quaternion rot = EnemySubject.Instance.GetDistanceTargetEnemyRot(n.transform.position);
                n.SetBullet(0, damage, EntityType.player, rot);
                BulletSubject.Instance.AddBullet(n);
                bullets.Add(n);
                yield return new WaitForSeconds(0.05f);
            }
            foreach(Bullet bullet in bullets)
            {
                bullet.Speed = 100;
            }
            yield return new WaitForSeconds(cool);
        }
    }
}

public class Skill_For : Skill
{
    public Skill_For()
    {
        text = "불렛을 반사합니다. (보스의 불렛의 데미지는 1/5로 감소합니다.)";
        name = "Reflect";
        spriteAddress = "Skill_Zero";
        level = 0;
        damage = 5;
        cool = 30;
        index = 4;
    }

    private int number_of_bullet = 20;

    public override void LevelUp()
    {
        level++;

        if (level == 0) cool = 30;
        if (level >= 1) cool = 29;
        if (level >= 2) cool = 28;
        if (level >= 3) cool = 27;
        if (level >= 4) cool = 26;
        if (level >= 5) cool = 25;
        if (level >= 6) cool = 24;
        if (level >= 7) cool = 23;
        if (level >= 8) cool = 22;
        if (level >= 8) cool = 21;
        if (level >= 9)
        {
            cool = 23 - level * 0.5f;
        }
    }

    public override IEnumerator Passive()
    {
        while (true)
        {
            BulletSubject.Instance.ChangeType(EntityType.player);
            yield return new WaitForSeconds(cool);
        }
    }
}

public class Skill_Fiv : Skill
{
    public Skill_Fiv()
    {
        text = "보스에게 강한 레이저를 발사 합니다";
        name = "Lazer";
        spriteAddress = "Skill_Zero";
        level = 0;
        damage = 5;
        cool = 30;
        index = 4;
    }

    private int number_of_bullet = 20;

    public override void LevelUp()
    {
        level++;

        if (level == 0) maxtime = 3;
        if (level >= 1) cool = 9;
        if (level >= 2) cool = 8;
        if (level >= 3) cool = 7;
        if (level >= 4) maxtime = 4;
        if (level >= 5) cool = 6;
        if (level >= 6) cool = 5;
        if (level >= 7) cool = 5;
        if (level >= 8) cool = 5;
        if (level >= 8) cool = 5;
    }

    float maxtime = 2;
    public override IEnumerator Passive()
    {
        GameObject n = Player.Instance.transform.Find("Lazer").gameObject;

        while (true)
        {
            float cool = 0;
            
            n.SetActive(true);
            while (cool < maxtime)
            {
                n.transform.localScale = new Vector3(2f * (cool/maxtime), n.transform.localScale.y, n.transform.localScale.z);
                cool += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            cool = 0;
            while (cool < maxtime)
            {
                n.transform.localScale = new Vector3(2f - 2f *(cool / maxtime), n.transform.localScale.y, n.transform.localScale.z);
                cool += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            n.SetActive(false);
            yield return new WaitForSeconds(cool);
        }
    }
}

public class Skill_Six : Skill
{
    public Skill_Six()
    {
        text = "보스에게 강한 레이저를 발사 합니다";
        name = "Lazer";
        spriteAddress = "Skill_Zero";
        level = 0;
        damage = 5;
        cool = 30;
        index = 4;
    }

    private int number_of_bullet = 20;

    public override void LevelUp()
    {
        level++;

        if (level == 0) maxtime = 3;
        if (level >= 1) cool = 9;
        if (level >= 2) cool = 8;
        if (level >= 3) cool = 7;
        if (level >= 4) maxtime = 4;
        if (level >= 5) cool = 6;
        if (level >= 6) cool = 5;
        if (level >= 7) cool = 5;
        if (level >= 8) cool = 5;
        if (level >= 8) cool = 5;
    }

    float maxtime = 2;
    public override IEnumerator Passive()
    {
        GameObject n = Player.Instance.transform.Find("Lazer").gameObject;

        while (true)
        {
            float cool = 0;

            n.SetActive(true);
            while (cool < maxtime)
            {
                n.transform.localScale = new Vector3(2f * (cool / maxtime), n.transform.localScale.y, n.transform.localScale.z);
                cool += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            cool = 0;
            while (cool < maxtime)
            {
                n.transform.localScale = new Vector3(2f - 2f * (cool / maxtime), n.transform.localScale.y, n.transform.localScale.z);
                cool += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            n.SetActive(false);
            yield return new WaitForSeconds(cool);
        }
    }
}
