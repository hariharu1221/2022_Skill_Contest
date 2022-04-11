using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySubject : DesSingleton<EntitySubject>
{
    private List<Enemy> enemies;
    private List<Enemy> deathEnemies;
    [SerializeField] private GameObject entityGroup;

    [SerializeField] private Red red;
    [SerializeField] private White white;

    public bool isEndGame;


    private int disCount;
    public int DisCount { get { return disCount; } }

    private void Awake()
    {
        SetInstance();

        isEndGame = false;

        disCount = 0;
        enemies = new List<Enemy>();
        deathEnemies = new List<Enemy>();
        if (!entityGroup) entityGroup = new GameObject("EntityGroup");
    }

    private void Update()
    {
        EnemyUpdate();
        DestroyEnemy();
    }

    private void EnemyUpdate()
    {
        if (isEndGame) return;

        foreach (Enemy enemy in enemies)
        {
            enemy.EnemyUpdate();
            if (enemy.IsDead || Utils.CheckEscape(enemy.transform.position))
                deathEnemies.Add(enemy);
        }
    }

    private void DestroyEnemy()
    {
        foreach (Enemy enemy in deathEnemies)
        {
            enemies.Remove(enemy);
            Destroy(enemy.gameObject);
        }
        deathEnemies.Clear();
    }

    public void AddEnemy(Enemy enemy)
    {
        enemy.transform.SetParent(entityGroup.transform);
        enemies.Add(enemy);
    }

    public void SpawnWhite()
    {
        Enemy n = Instantiate(white);

        Vector3 pos = new Vector3(0, Utils.GetNormalY(), 200f) + new Vector3(transform.position.x, 0, transform.position.z);
        pos.y = Utils.GetNormalY();
        n.transform.position = pos;
        n.transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));

        AddEnemy(n);
    }

    public void SpawnRed()
    {
        Enemy n = Instantiate(red);

        Vector3 pos = new Vector3(0, Utils.GetNormalY(), 200f) + new Vector3(transform.position.x, 0, transform.position.z);
        pos.y = Utils.GetNormalY();
        n.transform.position = pos;
        n.transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));

        AddEnemy(n);
    }
}
