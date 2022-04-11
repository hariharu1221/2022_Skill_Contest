using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugePoint
{
    public float MinGaugeValue;
    public float MaxGaugeValue;
    private float nowGaugeValue;
    public float NowGaugeValue
    {
        get
        {
            return nowGaugeValue;
        }
        set
        {
            if (value > MaxGaugeValue) nowGaugeValue = MaxGaugeValue;
            if (value < MinGaugeValue) nowGaugeValue = MinGaugeValue;
            else nowGaugeValue = value;
        }
    }

    public GaugePoint(float min, float max, float now = 0)
    {
        MinGaugeValue = min;
        MaxGaugeValue = max;
        nowGaugeValue = now;
    }
}
