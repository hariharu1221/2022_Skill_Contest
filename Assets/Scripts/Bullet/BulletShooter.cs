using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private BulletType bullet;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float cool;
    [SerializeField] private EntityType type;

    public void SetShooter(float damage, float speed, float cool, EntityType type, BulletType bullet = BulletType.bullet)
    {
        this.damage = damage;
        this.speed = speed;
        this.cool = cool;
        this.type = type;
        this.bullet = bullet;
    }

    private void Start()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        while(true)
        {
            Fire();
            yield return new WaitForSeconds(cool);
        }
    }

    private void Fire()
    {
        var n = BulletPooling.Instance.GetBullet(bullet);
        n.transform.position = transform.position;
        if (bullet == BulletType.target) n.GetTargetRot(type);
        else n.transform.rotation = transform.rotation;
        n.SetBullet(speed, damage, type, transform.rotation.eulerAngles);
        BulletSubject.Instance.AddBullet(n);
    }
}
