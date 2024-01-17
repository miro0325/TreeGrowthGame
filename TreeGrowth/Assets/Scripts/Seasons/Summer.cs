using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summer : SeasonBase
{
    private float chance;
    private float multiply;    
    public Summer(float _chance, float _multiply) : base()    
    {
        chance = _chance;
        multiply = _multiply;
        //Tree.Instance.SetExtraLeafChance(chance);
        //Tree.Instance.SetExpMultiplier(multiply);
    }

    public override void Init()
    {
        Tree.Instance.SetExtraLeafCount(2);
        Tree.Instance.SetExtraLeafChance(chance);
        Tree.Instance.SetExpMultiplier(multiply);
    }

    public override void Passive()
    {
        
    }

    public override void SeasonEvent()
    {
        int ran;
        ran = Random.Range(0, 9);
        if(ran == 0)
        {
            GameManager.Instance.weatherType = WeatherType.Storm;
        }
    }
}
