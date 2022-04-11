using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }
    public float damage;
    private EntityType type;
    public EntityType Type { get { return type; } }

    [HideInInspector] public bool isStop;
    [HideInInspector] public bool isHit;

    public virtual void SetBullet(float speed, float damage, EntityType type, Vector3 rotation)
    {
        this.speed = speed;
        this.damage = damage;
        this.type = type;
        isStop = false;
        isHit = false;

        SetTag(type);
        SetSprite();
        transform.rotation = Quaternion.Euler(rotation);
    }

    public virtual void SetBullet(float speed, float damage, EntityType type, Quaternion rotation)
    {
        this.speed = speed;
        this.damage = damage;
        this.type = type;
        isStop = false;
        isHit = false;

        SetTag(type);
        SetSprite();
        transform.rotation = rotation;
    }

    public virtual void SetTag(EntityType type)
    {
        this.type = type;

        if (type == EntityType.player)
        {
            this.tag = "PlayerBullet";
        }
        else
        {
            this.tag = "EnemyBullet";
        }
    }

    public void SetSprite(bool red = false)
    {
        if (type == EntityType.player)
        {
            gameObject.transform.Find("player").gameObject.SetActive(true);
            gameObject.transform.Find("enemy").gameObject.SetActive(false);
            gameObject.transform.Find("red").gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.Find("player").gameObject.SetActive(false);
            gameObject.transform.Find("enemy").gameObject.SetActive(true);
            gameObject.transform.Find("red").gameObject.SetActive(false);
        }


        if (red )
        {
            gameObject.transform.Find("player").gameObject.SetActive(false);
            gameObject.transform.Find("enemy").gameObject.SetActive(false);
            gameObject.transform.Find("red").gameObject.SetActive(true);
        }
    }

    public void ReflectRot()
    {
        Vector3 rot = transform.rotation.eulerAngles - new Vector3(0, -180,0);
        transform.rotation = Quaternion.Euler(rot);
    }

    public virtual void BulletUpdate()
    {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    public float GetDamage()
    {
        if (isHit) return 0;
        isHit = true;
        SoundManager.Instance.PlayFPX("hit", 0.4f);
        return damage;
    }

    public virtual void ReturnToPool()
    {
        gameObject.SetActive(false);
        BulletPooling.Instance.ReturnToPool(this);
    }

    public void SetDelaySpeed(float speed, float time)
    {
        StartCoroutine(setDelaySpeed(speed, time));
    }

     IEnumerator setDelaySpeed(float speed, float time)
    {
        yield return new WaitForSeconds(time);
        this.speed = speed;
    }

    public void SetDelayRot(Vector3 pos, float time)
    {
        StartCoroutine(setDelayRot(pos, time));
    }

    IEnumerator setDelayRot(Vector3 pos, float time)
    {
        yield return new WaitForSeconds(time);
        this.transform.rotation = Quaternion.Euler(pos);
    }

    public void SetDelayTarget(GameObject g, float time)
    {
        StartCoroutine(setDelayTarget(g, time));
    }

    IEnumerator setDelayTarget(GameObject g, float time)
    {
        yield return new WaitForSeconds(time);
        Quaternion rot = Quaternion.LookRotation(Utils.LookAtToTarget(transform.position, g.transform.position));
        this.transform.rotation = rot;
    }

    public Quaternion GetTargetRot(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        dir.y = 0;
        return Quaternion.Euler(dir.normalized);
    }

    public Quaternion GetTargetRot(EntityType type)
    {
        Vector3 dir = Vector3.zero;

        if (type == EntityType.player) dir = Player.Instance.transform.position - transform.position;
        else dir = Player.Instance.transform.position - transform.position;

        dir.y = 0;
        return Quaternion.Euler(dir.normalized);
    }
}

public enum EntityType
{
    player,
    enemy,
    boss
}