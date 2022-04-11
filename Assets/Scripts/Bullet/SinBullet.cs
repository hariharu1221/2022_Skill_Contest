using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinBullet : Bullet
{
    public override void SetTag(EntityType type)
    {
        //player instance
        //bullet subject
        base.SetTag(type);
    }

    public override void ReturnToPool()
    {
        gameObject.SetActive(false);
        BulletPooling.Instance.ReturnToSinPool(this);
    }
}
