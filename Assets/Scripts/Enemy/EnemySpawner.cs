using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemies;
    private float minCool;
    private float maxCool;
    private float mult;

    private void Awake()
    {
        mult = 1;
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while(true)
        {
            DisCountCheck();
            Spawn();
            float cool = Random.Range(minCool, maxCool);
            yield return new WaitForSeconds(cool);
        }
    }

    private void Spawn()
    {
        int random = Random.Range(0, enemies.Count);
        Enemy n = Instantiate(enemies[random]);

        Vector3 pos = new Vector3(Random.Range(-50, 50), Utils.GetNormalY(), 200f) + new Vector3(transform.position.x, 0, transform.position.z);
        pos.y = Utils.GetNormalY();
        n.transform.position = pos;
        n.transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));

        n.HpMult(mult);
        EnemySubject.Instance.AddEnemy(n);
    }

    private void DisCountCheck()
    {
        if (EnemySubject.Instance.DisCount < 30)
        {
            minCool = 1f;
            maxCool = 1.5f;
            mult = 1;
        }
        else if (EnemySubject.Instance.DisCount < 60)
        {
            minCool = 0.7f;
            maxCool = 1.2f;
            mult = 1.2f;
        }
        else if (EnemySubject.Instance.DisCount < 100)
        {
            minCool = 0.6f;
            maxCool = 0.9f;
            mult = 1.5f;
        }
        else if (EnemySubject.Instance.DisCount < 150)
        {
            minCool = 0.5f;
            maxCool = 0.8f;
            mult = 2f;
        }
        else if (EnemySubject.Instance.DisCount < 200)
        {
            minCool = 0.4f;
            maxCool = 0.6f;
            mult = 2.5f;
        }
        else
        {
            minCool = 0.7f;
            maxCool = 1.5f;
            mult = 3f;
        }
    }
}
