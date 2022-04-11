using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private Button RankingButton;
    [SerializeField] private Button EndButton;
    [SerializeField] private GameObject Sprite;
    private void Awake()
    {
        EndButton.onClick.AddListener(() => Exit());
        RankingButton.onClick.AddListener(() => GoRanking());
        StartButton.onClick.AddListener(() => GoInGame());
    }

    private void GoInGame()
    {
        StartCoroutine(GoInGameCor());
    }

    private IEnumerator GoInGameCor()
    {
        float cool = 0;
        while(cool < 0.8f)
        {
            Sprite.transform.position = Vector3.Lerp(Sprite.transform.position, new Vector3(Sprite.transform.position.x,
                Sprite.transform.position.y, -11), Time.deltaTime * 1f);
            cool += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        cool = 0;
        while (cool < 1.5f)
        {
            {
                Sprite.transform.position = Vector3.Lerp(Sprite.transform.position, new Vector3(Sprite.transform.position.x,
                    Sprite.transform.position.y, 80), Time.deltaTime * 5f);
                cool += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        SceneManager.LoadScene(1);
    }
    
    private void GoRanking()
    {
        StartCoroutine(GoRankingCor());
    }

    private IEnumerator GoRankingCor()
    {
        float cool = 0;
        while (cool < 0.8f)
        {
            Sprite.transform.position = Vector3.Lerp(Sprite.transform.position, new Vector3(Sprite.transform.position.x,
                Sprite.transform.position.y, -11), Time.deltaTime * 1f);
            cool += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        cool = 0;
        while (cool < 1.5f)
        {
            {
                Sprite.transform.position = Vector3.Lerp(Sprite.transform.position, new Vector3(Sprite.transform.position.x,
                    Sprite.transform.position.y, 80), Time.deltaTime * 5f);
                cool += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        SceneManager.LoadScene(3);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
