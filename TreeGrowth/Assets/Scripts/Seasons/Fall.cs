using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public struct FallInfo
{
    public int count;
    public float multiply;
}

public class Fall : ISeasonBase
{
    private int count;
    private float multiply;

    public void Init(object obj)
    {
        FallInfo info = (FallInfo)obj;
        count = info.count;
        multiply = info.multiply;
        Tree.Instance.SetExtraLeafCount(count);
        Tree.Instance.SetExpMultiplier(multiply);
        Tree.Instance.SetExtraLeafCount(2);
        Tree.Instance.SetExtraLeafChance(10);
    }

    public void Passive()
    {

    }

    public void Reset()
    {

    }

    public void SeasonEvent()
    {
        
    }

}
