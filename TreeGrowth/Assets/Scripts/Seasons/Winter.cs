using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WinterInfo
{
    public float multiply;
    public int count;
    public SpriteRenderer renderer;
}

public class Winter : ISeasonBase
{
    private float multiply;
    private int count;
    private const float snowAmount = 0.5f;
    private float curSnow = 0;
    private SpriteRenderer spriteRenderer;

    public void Init(object obj)
    {
        WinterInfo winterInfo = (WinterInfo)obj;
        multiply = winterInfo.multiply;
        count = winterInfo.count;
        spriteRenderer = winterInfo.renderer;
        
        Tree.Instance.SetExtraLeafCount(count);
        Tree.Instance.SetExpMultiplier(multiply);
        Tree.Instance.SetExtraLeafChance(0);
        curSnow = spriteRenderer.material.GetFloat("_SnowAmount");
    }

    public void Passive()
    {
        var mat = spriteRenderer.material;
        curSnow -= Time.deltaTime * 0.1f;
        if(curSnow > snowAmount)
        {
            mat.SetFloat("_SnowAmount",curSnow);

        } else
        {
            curSnow = snowAmount;
            mat.SetFloat("_SnowAmount", curSnow);
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
