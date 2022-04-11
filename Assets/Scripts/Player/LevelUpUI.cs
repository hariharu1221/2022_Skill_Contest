using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelUpUI : MonoBehaviour
{
    private RectTransform rect;
    public Option opOne;
    public Option opTwo;
    public Option opThr;

    private bool isSelcet = false;
    private Skill selectSkill;

    public void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void SetOption(int one, int two, int thr)
    {
        gameObject.SetActive(true);

        GetOption(ref opOne, one);
        GetOption(ref opTwo, two);
        GetOption(ref opThr, thr);

        isSelcet = false;
        selectSkill = null;
        StartCoroutine(ShowUI());
    }

    public IEnumerator ShowUI()
    {
        float cool = 0;
        while (cool < 0.25f)
        {
            rect.anchoredPosition = new Vector2(2000 - (2000 * cool * 4), -50);
            cool += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        cool = 0;
        while (!isSelcet)
        {
            if (cool >= 3f)
            {
                SelectSkill(opOne.skill);
            }
            cool += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        cool = 0;
        while (cool < 0.25f)
        {
            rect.anchoredPosition = new Vector2(2000 * cool * 4, -50);
            cool += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        PlayerSkillSystem.Instance.LevelUpEnd(selectSkill);
        ReleaseImage();
        gameObject.SetActive(false);
    }

    public void SelectSkill(Skill skill)
    {
        selectSkill = skill;
        isSelcet = true;
    }

    public void SetImage(Option op)
    {
        Addressables.LoadAssetAsync<Sprite>(op.skill.spriteAddress).Completed +=
            (AsyncOperationHandle<Sprite> handle) =>
            {
                op.image.sprite = handle.Result;
                op.handle = handle;
            };
    }

    public void ReleaseImage()
    {
        Addressables.Release(opOne.handle);
        Addressables.Release(opTwo.handle);
        Addressables.Release(opThr.handle);
    }

    public void GetOption(ref Option op, int index)
    {
        Skill skill = SkillFactory.GetSkill(index);
        op.skill = skill;

        SetImage(op);
        op.text.text = skill.text;
        op.name.text = skill.name;
        op.button.onClick.AddListener(() => SelectSkill(skill));
    }
}

[System.Serializable]
public class Option
{
    public AsyncOperationHandle handle;
    public Image image;
    public Button button;
    public Text text;
    public Text name;
    public Skill skill;
}