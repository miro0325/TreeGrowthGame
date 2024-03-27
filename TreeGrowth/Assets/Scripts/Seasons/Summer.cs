using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SummerInfo
{
    public float chance;
    public float multiply;
}

public class Summer : ISeasonBase
{
    private float chance;
    private float multiply;

    public void Init(object obj)
    {
        SummerInfo info = (SummerInfo)obj;
        chance = info.chance;
        multiply = info.multiply;
        Tree.Instance.SetExtraLeafCount(2);
        Tree.Instance.SetExtraLeafChance(chance);
        Tree.Instance.SetExpMultiplier(multiply);
    }

    public void Passive()
    {
        
    }

    public void Reset()
    {

    }

    public void SeasonEvent()
    {
        int ran;
        ran = Random.Range(0, 9);
        if(ran == 0)
        {
            GameManager.Instance.weatherType = WeatherType.Storm;
        }
    }
}
