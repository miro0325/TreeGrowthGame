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
        Tree.Instance.SetExtraLeafCount(count);
        Tree.Instance.SetExpMultiplier(multiply);
    }
    
    public override void Init()
    {
        Tree.Instance.SetExtraLeafCount(count);
        Tree.Instance.SetExpMultiplier(multiply);
    }

    public override void Passive()
    {

    }

}
