using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static bool CheckEscape(Vector3 pos)
    {
        if (Mathf.Abs(pos.x) > limit.x + 80 || Mathf.Abs(pos.z) > limit.y + 250) return true;
        return false;
    }
    
    public static float GetNormalY()
    {
        return 20;
    }

    public static Vector2 limit { get { return new Vector2(160, 90); } }

    public static Vector3 LookAtToTarget(Vector3 origin, Vector3 target)
    {
        Vector3 pos = target - origin;
        pos.y = 0;
        return pos.normalized;
    }
}
