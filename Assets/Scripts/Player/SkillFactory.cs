using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFactory
{
    public static Skill GetSkill(int index)
    {
        if (index == 0) return new Skill_Zero();
        if (index == 1) return new Skill_One();
        if (index == 2) return new Skill_Two();
        if (index == 3) return new Skill_Thr();
        if (index == 4) return new Skill_For();
        if (index == 5) return new Skill_Fiv();

        return new Skill_Zero();
    }

    public static int GetSkillCount()
    {
        return 6;
    }
}
