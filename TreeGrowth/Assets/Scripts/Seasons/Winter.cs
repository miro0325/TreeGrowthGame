using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winter : SeasonBase
{
    private float multiply;
    private int count;
    private const float snowAmount = 0.5f;
    private float curSnow = 0;
    private SpriteRenderer spriteRenderer;

    public Winter( int _count,float _multiply, SpriteRenderer renderer) : base() {
        multiply = _multiply;
        count = _count;
        spriteRenderer = renderer;
        //Tree.Instance.SetExtraLeafCount(count);
        //Tree.Instance.SetExpMultiplier(multiply);
    }
    
    public override void Init()
    {
        Tree.Instance.SetExtraLeafCount(count);
        Tree.Instance.SetExpMultiplier(multiply);
        //Tree.Instance.SetExtraLeafCount(2);
        Tree.Instance.SetExtraLeafChance(0);
        curSnow = spriteRenderer.material.GetFloat("_SnowAmount");
    }

    public override void Passive()
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

    public override void SeasonEvent()
    {
        
    }
}
