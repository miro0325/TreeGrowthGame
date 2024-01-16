using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winter : SeasonBase
{
    private float multiply;
    private int count;

    public Winter( int _count,float _multiply) : base() {
        multiply = _multiply;
        count = _count;
        //Tree.Instance.SetExtraLeafCount(count);
        //Tree.Instance.SetExpMultiplier(multiply);
    }
    
    public override void Init()
    {
        Tree.Instance.SetExtraLeafCount(count);
        Tree.Instance.SetExpMultiplier(multiply);
    }

    public override void Passive()
    {

    }

    public override void SeasonEvent()
    {
        
    }
}
