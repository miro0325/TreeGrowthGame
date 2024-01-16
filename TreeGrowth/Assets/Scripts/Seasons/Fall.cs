using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fall : SeasonBase
{
    private int count;
    private float multiply;


    public Fall(int _count,float _multiply) : base()
    {
        count = _count;
        multiply = _multiply;
        //Tree.Instance.SetExtraLeafCount(count);
        //Tree.Instance.SetExpMultiplier(multiply);
    }
    
    public override void Init()
    {
        Tree.Instance.SetExtraLeafCount(count);
        Tree.Instance.SetExpMultiplier(multiply);
        Tree.Instance.SetExtraLeafCount(2);
        Tree.Instance.SetExtraLeafChance(10);
    }

    public override void Passive()
    {

    }

    public override void SeasonEvent()
    {
        
    }

}
