using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : DesSingleton<BulletPooling>
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private SinBullet sinBullet;
    [SerializeField] private ToBullet toBullet;

    private Queue<Bullet> bullets;
    private Queue<SinBullet> sinBullets;
    private Queue<ToBullet> toBullets;

    private void Awake()
    {
        SetInstance();
        SetVariable();
    }

    private void SetVariable()
    {
        bullets = new Queue<Bullet>();
        sinBullets = new Queue<SinBullet>();
        toBullets = new Queue<ToBullet>();
    }

    public Bullet GetBullet(BulletType type)
    {
        if (type == BulletType.bullet)
        {
            return GetBullet();
        }
        else if (type == BulletType.sinBullet)
        {
            return GetSinBullet();
        }
        else if (type == BulletType.toBullet)
        {
            return GetToBullet();
        }
        else
        {
            return GetBullet();
        }
    }

    public Bullet GetBullet()
    {
        Bullet n;

        if (bullets.Count == 0) n = Instantiate(bullet);
        else n = bullets.Dequeue();
        n.gameObject.SetActive(true);

        return n;
    }

    public SinBullet GetSinBullet()
    {
        SinBullet n;

        if (sinBullets.Count == 0) n = Instantiate(sinBullet);
        else n = sinBullets.Dequeue();
        n.gameObject.SetActive(true);

        return n;
    }

    public ToBullet GetToBullet()
    {
        ToBullet n;

        if (toBullets.Count == 0) n = Instantiate(toBullet);
        else n = toBullets.Dequeue();
        n.gameObject.SetActive(true);

        return n;
    }

    public void ReturnToPool(Bullet bullet)
    {
        bullets.Enqueue(bullet);
    }

    public void ReturnToSinPool(SinBullet bullet)
    {
        sinBullets.Enqueue(bullet);
    }

    public void ReturnToToPool(ToBullet bullet)
    {
        toBullets.Enqueue(bullet);
    }
}

public enum BulletType
{
    bullet,
    toBullet,
    sinBullet,
    target
}