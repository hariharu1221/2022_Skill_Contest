using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBullet : Bullet
{

    public override void ReturnToPool()
    {
        gameObject.SetActive(false);
        BulletPooling.Instance.ReturnToToPool(this);
    }
}
