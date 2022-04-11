using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSystem : DesSingleton<PlayerSkillSystem>
{
    [SerializeField] private BulletShooter shooter;
    [SerializeField] private LevelUpUI levelUpUI;

    private List<BulletShooter> shooters;
    private List<Skill> skills;

    private int level;
    public int Level { get { return level; } }
    public float nowExp;
    public float MaxExp { get { return 150 * Mathf.Sqrt(level); } }
    public const int MaxCount = 6;

    public float Damage { get { return Player.Instance.Status.damage + level; } }

    private bool isLevelUp;

    private void Awake()
    {
        SetInstance();
        SetVariable();
    }

    private void Start()
    {
        CheckWeapon();
    }

    private void SetVariable()
    {
        isLevelUp = false;
        level = 1;
        nowExp = 0;
        skills = new List<Skill>();
        shooters = new List<BulletShooter>();
        if (!levelUpUI) levelUpUI = FindObjectOfType<LevelUpUI>();
    }

    public void PlusExp(float value)
    {
        nowExp += value;
        CheckLevel();
    }

    private void CheckLevel()
    {
        if (isLevelUp) return;
        if (nowExp >= MaxExp)
        {
            LevelUp();
        }
    }

    public void Cheat()
    {
        if (isLevelUp) return;
        nowExp += MaxExp;
        CheckLevel();
    }

    private void LevelUp()
    {
        if (isLevelUp) return;
        isLevelUp = true;
        level++;

        int one, two, thr;
        if (skills.Count >= MaxCount)
        {
            int[] index = new int[MaxCount];
            for (int i = 0; i < MaxCount; i++) 
                index[i] = skills[i].Index;

            one = index[Random.Range(0, MaxCount)];
            two = index[Random.Range(0, MaxCount)];
            thr = index[Random.Range(0, MaxCount)];
        }
        else
        {
            one = Random.Range(0, SkillFactory.GetSkillCount());
            two = Random.Range(0, SkillFactory.GetSkillCount());
            thr = Random.Range(0, SkillFactory.GetSkillCount());
        }

        Time.timeScale = 0.3f;
        levelUpUI.SetOption(one, two, thr);
    }

    public void LevelUpEnd(Skill skill)
    {
        if (!isLevelUp) return;
        Time.timeScale = 1;
        GetSkill(skill);
        CheckWeapon();
        nowExp -= MaxExp;

        isLevelUp = false;
        CheckLevel();
    }

    private void CheckWeapon()
    {
        if (level >= 8)
        {
            DestoryWeapon();
            InstantiateWeapon(Vector3.forward * 4);
            InstantiateWeapon(Vector3.left * 2.25f);
            InstantiateWeapon(Vector3.right * 2.25f);
            InstantiateWeapon(Vector3.right * 3.35f - Vector3.forward * 2.2f);
            InstantiateWeapon(Vector3.left * 3.35f - Vector3.forward * 2.2f);
        }
        else if (level >= 5)
        {
            DestoryWeapon();
            InstantiateWeapon(Vector3.forward * 4);
            InstantiateWeapon(Vector3.left * 2.25f);
            InstantiateWeapon(Vector3.right * 2.25f);
        }
        else if (level >= 3)
        {
            DestoryWeapon();
            InstantiateWeapon(Vector3.left * 2.25f);
            InstantiateWeapon(Vector3.right * 2.25f);
        }
        else
        {
            DestoryWeapon();
            InstantiateWeapon(Vector3.forward * 4);
        }
    }

    private void InstantiateWeapon(Vector3 pos)
    {
        var n = Instantiate(shooter);
        n.transform.SetParent(gameObject.transform);
        n.SetShooter(Damage, 100, 0.1f, EntityType.player, BulletType.bullet);
        n.transform.localPosition = pos;
        n.transform.rotation = transform.rotation;
        shooters.Add(n);
    }

    private void DestoryWeapon()
    {
        foreach(BulletShooter weapon in shooters)
        {
            Destroy(weapon.gameObject);
        }
        shooters.Clear();
    }

    private void GetSkill(Skill skill)
    {
        bool ison = false;

        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].GetType() == skill.GetType())
            {
                skills[i].LevelUp();
                ison = true;
            }
        }

        if (!ison)
        {
            skills.Add(skill);
            StartCoroutine(skill.Passive());
        }
    }
}
