using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyBoss : Boss
{
    protected override void Awake()
    {
        Status = JsonLoader.Load<BossStatus>("EazyBossStatus");
        hp = Status.hp;
        damage = Status.damage;
        isAlive = true;

        if (!sprite) sprite = gameObject.transform.Find("sprite").gameObject;
        if (!hited) hited = gameObject.transform.Find("hited").gameObject;
        if (!hpBar) hpBar = gameObject.transform.Find("hpBar").gameObject;
    }

    protected override IEnumerator Pattern()
    {
        Vector3 pos = Player.Instance.transform.position + new Vector3(0, 0, 0);
        yield return MoveToPos(pos, 3, 3);

        int count = 0;
        while (isAlive)
        {

            switch (count)
            {
                case 0:
                    yield return pFiv();
                    break;
                case 1:
                    yield return pThr();
                    break;
                case 2:
                    yield return pFor();
                    break;

            }

            count++;
            if (count >= 3) count = 0;
            yield return new WaitForSeconds(1f);
        }
    }
}
