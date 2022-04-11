using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySubject : DesSingleton<EnemySubject>
{
    private List<Enemy> enemies;
    private List<Enemy> deathEnemies;
    [SerializeField] private GameObject enemyGruop;

    public bool isEndGame;


    private int disCount;
    public int DisCount { get { return disCount; } set { disCount = value; } }

    private void Awake()
    {
        SetInstance();

        isEndGame = false;

        disCount = 0;
        enemies = new List<Enemy>();
        deathEnemies = new List<Enemy>();
        if (!enemyGruop) enemyGruop = new GameObject("EnemyGroup");
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
            if (Utils.CheckEscape(enemy.transform.position))
                GameManager.Instance.PainBar.NowGaugeValue += 0.5f;
        }
    }

    private void DestroyEnemy()
    {
        foreach (Enemy enemy in deathEnemies)
        {
            disCount++;
            enemies.Remove(enemy);
            Destroy(enemy.gameObject);
        }
        deathEnemies.Clear();
    }

    public void AddEnemy(Enemy enemy)
    {
        enemy.transform.SetParent(enemyGruop.transform);
        enemies.Add(enemy);
    }

    public Enemy GetDistanceTargetEnemy(Vector3 target) //목표 지점으로부터 가장 가까운 적을 얻어옴
    {
        float minDis = float.MaxValue;
        int index = -1;
        for (int i = 0; i < enemies.Count; i++) 
        {
            float dis = Vector3.Distance(target, enemies[i].transform.position);
            if (minDis > dis)
            {
                minDis = dis;
                index = i;
            }
        }

        if (index == -1) return null;
        return enemies[index];
    }

    public Quaternion GetDistanceTargetEnemyRot(Vector3 target) //목표 지점으로부터 가장 가까운 적을까지의 각도
    {
        float minDis = 9999;
        int index = -1;
        for (int i = 0; i < enemies.Count; i++)
        {
            float dis = Vector3.Distance(target, enemies[i].transform.position);
            Debug.Log(dis);
            if (minDis > dis)
            {
                minDis = dis;
                index = i;
            }
        }

        if (index == -1) return Quaternion.Euler(new Vector3(0, 0, 0));
        Vector3 dir = enemies[index].transform.position - target;
        dir.y = 0;
        return Quaternion.LookRotation(dir.normalized);
    }

    public void AllDestroy()
    {
        foreach(Enemy enemy in enemies)
        {
            deathEnemies.Add(enemy);
        }
    }
}
