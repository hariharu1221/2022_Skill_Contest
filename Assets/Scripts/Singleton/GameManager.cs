using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : DesSingleton<GameManager>
{
    [HideInInspector] public GaugePoint PainBar;
    [SerializeField] private Boss bossPrefabs;
    [SerializeField] private StageType stage;

    [SerializeField] GameObject End;
    [SerializeField] GameObject Next;

    private float score;
    public float Score { get { return score; } set { score = value; } }

    public bool isGameEnd;
    public bool onDes;
    public bool Clear;
    private bool isBoss;

    private const int MaxDisCount = 200;
    private void Awake()
    {
        PainBar = new GaugePoint(0, 105, 0);
        score = 0;

        isGameEnd = false;
        onDes = false;
        Clear = false;
        isBoss = false;

        PainBar.NowGaugeValue = 10;
    }

    private void Start()
    {
        End.GetComponentInChildren<Button>().onClick.AddListener(() => EndUI());
        Next.GetComponentInChildren<Button>().onClick.AddListener(() => NextUI());

        End.SetActive(false);
        Next.SetActive(false);

        SoundManager.Instance.PlayBGM(0.26f);
        StartCoroutine(shot());

        Time.timeScale = 1f;
    }

    IEnumerator shot()
    {
        while (!isGameEnd)
        {
            SoundManager.Instance.PlayFPX("shot", 0.3f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public Boss boss = null;
    public void Update()
    {
        if (!isBoss)
        {
            UiManager.Instance.SetMisson("Virus is Comming! (" + EnemySubject.Instance.DisCount.ToString() + " / " + MaxDisCount.ToString());
        }
        else if (isBoss)
        {
            UiManager.Instance.SetMisson("Kill Corona Virus! (0 / 1)");
        }
        else if (Clear)
        {
            UiManager.Instance.SetMisson("Kill Corona Virus! (1 / 1)");
        }

        if (EnemySubject.Instance.DisCount >= MaxDisCount && !isBoss)
        {
            var n = Instantiate(bossPrefabs);
            n.transform.position = Player.Instance.transform.position + new Vector3(0, 0, 150);
            boss = n;

            isBoss = true;
        }

        if (!isGameEnd)
        {
            score += 100f * Time.deltaTime;
        }
        if (isGameEnd && !onDes)
        {
            onDes = true;

            Player.Instance.GetComponent<PlayerController>().input.Disable();
            BulletSubject.Instance.isEndGame = true;
            EnemySubject.Instance.isEndGame = true;
        }

        if ((PainBar.NowGaugeValue >= 100 || Player.Instance.hpGauge.NowGaugeValue <= 0) && !isGameEnd)
        {
            isGameEnd = true;

            ScoreManager.Instance.EndScore((int)score);
            End.SetActive(true);
        }

        if (Clear && !isGameEnd)
        {
            isGameEnd = true;

            if (SceneManager.GetActiveScene().buildIndex == 1)
                ScoreManager.Instance.SaveScore((int)score);
            else
                ScoreManager.Instance.EndScore((int)score);
            Next.SetActive(true);
        }

        Cheat();
    }

    private void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.F1)) SceneManager.LoadScene(1);
        if (Input.GetKeyDown(KeyCode.F2)) SceneManager.LoadScene(2);
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (Player.Instance.state == PlayerState.invis) Player.Instance.state = PlayerState.normal;
            else Player.Instance.state = PlayerState.invis;

        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            EnemySubject.Instance.AllDestroy();
            if (boss != null)
            {
                boss.hp = 10;
            }
        }
        if (Input.GetKey(KeyCode.F5)) Player.Instance.hpGauge.NowGaugeValue += 0.4f;
        if (Input.GetKey(KeyCode.F6)) Player.Instance.hpGauge.NowGaugeValue += -0.4f;
        if (Input.GetKey(KeyCode.F7)) PainBar.NowGaugeValue += 0.1f;
        if (Input.GetKey(KeyCode.F8)) PainBar.NowGaugeValue += -0.1f;
        if (Input.GetKeyDown(KeyCode.F9)) EntitySubject.Instance.SpawnRed();
        if (Input.GetKeyDown(KeyCode.F10)) EntitySubject.Instance.SpawnWhite();
        if (Input.GetKeyDown(KeyCode.F11)) PlayerSkillSystem.Instance.Cheat();
        if (Input.GetKeyDown(KeyCode.F12)) EnemySubject.Instance.DisCount = MaxDisCount;

        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(0); ;
    }


    public void EndUI()
    {
        SceneManager.LoadScene(3);
    }

    public void NextUI()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            SceneManager.LoadScene(2);
        else
            SceneManager.LoadScene(3);
    }

    public void ClearBoss()
    {
        ScoreManager.Instance.SaveScore((int)score);
        //클리어 창
        //버튼 클릭시 다음 씬
        //stage 2일 경우 점수 계산
    }
}

public enum StageType
{
    one,
    two
}