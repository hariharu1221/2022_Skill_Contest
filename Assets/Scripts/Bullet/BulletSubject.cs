using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSubject : DesSingleton<BulletSubject>
{
    private List<Bullet> bullets;
    private List<Bullet> deathBullets;
    [SerializeField] private GameObject bulletGruop;

    public bool isEndGame;

    private void Awake()
    {
        SetInstance();

        isEndGame = false;
        bullets = new List<Bullet>();
        deathBullets = new List<Bullet>();
        if (!bulletGruop) bulletGruop = new GameObject("BulletGroup");
    }

    private void Update()
    {
        BulletUpdate();
        DestroyBullet();
    }

    private void BulletUpdate()
    {
        if (isEndGame) return;

        foreach (Bullet bullet in bullets)
        {
            if (!bullet.isStop) bullet.BulletUpdate();
            if (bullet.isHit || Utils.CheckEscape(bullet.transform.position))
                deathBullets.Add(bullet);
        }
    }

    private void DestroyBullet()
    {
        foreach(Bullet bullet in deathBullets)
        {
            bullets.Remove(bullet);
            bullet.ReturnToPool();
        }
        deathBullets.Clear();
    }
    
    public void AddBullet(Bullet bullet)
    {
        bullet.transform.SetParent(bulletGruop.transform);
        bullets.Add(bullet);
    }

    public void ChangeType(EntityType type)
    {
        foreach(Bullet bullet in bullets)
        {
            if (bullet.Type != type)
            {
                bullet.SetTag(type);
                bullet.SetSprite();
                bullet.damage /= 5;
                bullet.ReflectRot();

                if (bullet.Type == EntityType.boss)
                    bullet.damage /= 5;
            }
        }
    }
}
