using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spring : SeasonBase
{
    private float time;
    private float coolTIme;
    private const float snowAmount = 0.5f;
    private float curSnow;
    private int count;
    private SpriteRenderer spriteRenderer;

    public Spring(float _coolTime,int _count, SpriteRenderer renderer) : base()
    {
        coolTIme = _coolTime;
        count = _count;
        spriteRenderer = renderer;
        curSnow = spriteRenderer.material.GetFloat("_SnowAmount");
    }

    public override void Init()
    {
        Tree.Instance.SetExtraLeafCount(2);
        Tree.Instance.SetExpMultiplier(1);
        //Tree.Instance.SetExtraLeafCount(2);
        curSnow = spriteRenderer.material.GetFloat("_SnowAmount");
    }

    public override void Passive()
    {
        if(curSnow < 1)
        {
            curSnow+=Time.deltaTime*0.2f;
            spriteRenderer.material.SetFloat("_SnowAmount",curSnow);
        } else
        {
            curSnow = 1;
            spriteRenderer.material.SetFloat("_SnowAmount", curSnow);
        }
        time += Time.deltaTime;
        if (time > coolTIme)
        {
            time = 0;
            Tree.Instance.GainExp(count, true);
        }
    }

    public override void SeasonEvent()
    {
        
    }
    
}
