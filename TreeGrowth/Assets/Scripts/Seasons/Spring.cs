using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public struct SpringInfo
{
    public float coolTime;
    public int count;
    public SpriteRenderer spriteRenderer;

}

public class Spring : ISeasonBase
{
    private float time;
    private float coolTIme;
    private const float snowAmount = 0.5f;
    private float curSnow;
    private int count;
    private SpriteRenderer spriteRenderer;

    public void Init(object obj)
    {
        SpringInfo info = (SpringInfo)obj;
        coolTIme = info.coolTime;
        count = info.count;
        spriteRenderer = info.spriteRenderer;
        Debug.Log(info.spriteRenderer);
        Tree.Instance.SetExtraLeafCount(2);
        Tree.Instance.SetExpMultiplier(1);
        //Tree.Instance.SetExtraLeafCount(2);
        curSnow = spriteRenderer.material.GetFloat("_SnowAmount");
    }

    public void Passive()
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

    public void SeasonEvent()
    {
        
    }

    public void Reset()
    {
        curSnow = spriteRenderer.material.GetFloat("_SnowAmount");
    }
}
