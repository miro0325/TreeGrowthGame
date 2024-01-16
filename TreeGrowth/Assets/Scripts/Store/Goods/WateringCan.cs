using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : Goods
{
    public WateringCan(float _price) : base(_price)
    {

    }

    public override void Buy(ref float money)
    {
        base.Buy(ref money);
        Tree.Instance.increaseGrowth = Mathf.RoundToInt((float)(Tree.Instance.increaseGrowth * 1.5f));
    }

}
