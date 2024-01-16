using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spring : SeasonBase
{
    float time;
    float coolTIme;
    int count;
    
    public Spring(float _coolTime,int _count, object _obj = null) : base(_obj)
    {
        coolTIme = _coolTime;
        count = _count;
    }

    public override void Init()
    {

    }

    public override void Passive()
    {
        time += Time.deltaTime;
        if (time > coolTIme)
        {
            time = 0;
            Tree.Instance.GainExp(count, true);
        }
    }

    
}
