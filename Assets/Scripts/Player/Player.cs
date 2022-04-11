using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : DesSingleton<Player>
{
    #region VARIABLE

    //Status
    [Header("플레이어 정보")]
    [SerializeField] private PlayerStatus status;

    [SerializeField] private GameObject invisOb;
    public PlayerStatus Status { get { return status; } }
    private float nowSpeed;

    //Private Value
    public PlayerState state;
    private bool invis;

    //Public Info
    [HideInInspector] public ExcutionState lowFlying;
    [HideInInspector] public ExcutionState boosting;
    [HideInInspector] public Vector2 vec;
    [HideInInspector] public Quaternion rot;
    [HideInInspector] public GaugePoint hpGauge;
    [HideInInspector] public GaugePoint fuelGauge;

    #endregion
    private void Awake()
    {
        SetInstance();
        SetVariable();
    }
    private void SetVariable()
    {
        status = JsonLoader.Load<PlayerStatus>("Player_Status");
        nowSpeed = status.speed;

        invis = false;
        state = PlayerState.normal;

        lowFlying = ExcutionState.none;
        boosting = ExcutionState.none;
        hpGauge = new GaugePoint(-5, status.hp, status.hp);
        fuelGauge = new GaugePoint(-5, status.fuel, status.fuel);
    }
    private void Update()
    {
        Movement();
        LowFlying();
        Boosting();
        GaugeManagement();
        StateManagement();
        transform.rotation = rot;

        if (invis || state == PlayerState.invis)
        {
            if (invisOb.activeSelf == false) invisOb.SetActive(true);
        }
        else
        {
            if (invisOb.activeSelf) invisOb.SetActive(false);
        }
    }

    private void Movement()
    {
        Vector3 pos = new Vector3(vec.x, 0, vec.y);
        transform.position += pos * Time.deltaTime * nowSpeed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -Utils.limit.x, Utils.limit.x), transform.position.y,
            Mathf.Clamp(transform.position.z, -Utils.limit.y, Utils.limit.y));
    }

    private void LowFlying()
    {
        if (state == PlayerState.overload || lowFlying == ExcutionState.none)
        {
            transform.position = Vector3.Lerp(transform.position, 
                new Vector3(transform.position.x, Utils.GetNormalY(), transform.position.z), Time.deltaTime * 10f);
            if (lowFlying != ExcutionState.none) lowFlying = ExcutionState.none;
            return;
        }
        
        switch(lowFlying)
        {
            case ExcutionState.ready:
                state = PlayerState.invis;
                lowFlying = ExcutionState.excution;
                break;
            case ExcutionState.excution:
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(transform.position.x, 0, transform.position.z), Time.deltaTime * 10f);
                break;
            case ExcutionState.end:
                state = PlayerState.normal;
                lowFlying = ExcutionState.none;
                break;
        }

        fuelGauge.NowGaugeValue -= Time.deltaTime * 3f;
    }

    private void Boosting()
    {
        if (state == PlayerState.overload || boosting == ExcutionState.none)
        {
            if (boosting != ExcutionState.none) boosting = ExcutionState.none;
            return;
        }

        switch (boosting)
        {
            case ExcutionState.ready:
                nowSpeed = status.speed * 2;
                boosting = ExcutionState.excution;
                break;
            case ExcutionState.excution:
                nowSpeed = status.speed * 2;
                break;
            case ExcutionState.end:
                nowSpeed = status.speed ;
                boosting = ExcutionState.none;
                break;
        }

        fuelGauge.NowGaugeValue -= Time.deltaTime;
    }

    private void StateManagement()
    {
        switch(state)
        {
            case PlayerState.normal:
                break;
            case PlayerState.overload:
                break;
        }
    }

    private void GaugeManagement()
    {
        if (fuelGauge.NowGaugeValue <= 0 && state != PlayerState.overload)
        {
            StartCoroutine(Overload(3f));
            fuelGauge.NowGaugeValue = fuelGauge.MaxGaugeValue;
            hpGauge.NowGaugeValue -= 10f;
        }
    }

    private IEnumerator Overload(float time)
    {
        state = PlayerState.overload;
        nowSpeed = status.speed / 2f;
        yield return new WaitForSeconds(time);
        state = PlayerState.normal;
        nowSpeed = status.speed;
    }

    public void GetDamage(float damage ,bool isInvis = false)
    {
        if (isInvis && invis == false)
        {
            StartCoroutine(GetInvis(3f));
            return;
        }
        if (invis || state == PlayerState.invis)
            return;
        hpGauge.NowGaugeValue -= damage;
        StartCoroutine(GetInvis(1.5f));
    }

    private IEnumerator GetInvis(float time)
    {
        invis = true;
        yield return new WaitForSeconds(time);
        invis = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            float h = other.GetComponent<Bullet>().GetDamage();
            GetDamage(h);
        }
        if (other.CompareTag("Enemy"))
        {
            float h = other.GetComponent<Enemy>().DesDamage;
            GetDamage(h);
        }
        if (other.CompareTag("Boss"))
        {
            float h = other.GetComponent<Boss>().DesDamage;
            GetDamage(h);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}

[System.Serializable]
public class PlayerStatus
{
    public float damage;
    public float hp;
    public float fuel;
    public float speed;

    public PlayerStatus()
    {
        damage = 10;
        hp = 400;
        fuel = 20;
        speed = 40;
    }
}

public enum PlayerState
{
    normal,
    overload,
    invis
}

public enum ExcutionState
{
    none,
    ready,
    excution,
    end
}