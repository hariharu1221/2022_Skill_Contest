using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemies;
    private float minCool;
    private float maxCool;
    private float mult;

    private void Awake()
    {
        mult = 1;

        minCool = 3f;
        maxCool = 4f;
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (true)
        {
            Spawn();
            float cool = Random.Range(minCool, maxCool);
            yield return new WaitForSeconds(cool);
        }
    }

    private void Spawn()
    {
        int random = Random.Range(0, enemies.Count);
        Enemy n = Instantiate(enemies[random]);

        Vector3 pos = new Vector3(Random.Range(-40, 40), Utils.GetNormalY(), 200f) + new Vector3(transform.position.x, 0, transform.position.z);
        pos.y = Utils.GetNormalY();
        n.transform.position = pos;
        n.transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));

        n.HpMult(mult);
        EntitySubject.Instance.AddEnemy(n);
    }
}
