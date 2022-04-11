using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoTitle : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => Title());
    }

    private void Title()
    {
        SceneManager.LoadScene(0);
    }
}
