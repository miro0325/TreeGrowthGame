using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fertilizer : Goods
{
    public Fertilizer(float _price) : base(_price)
    {

    }

    public override void Buy(ref float money)
    {
        base.Buy(ref money);
        Tree.Instance.AddLeafChance(0.5f);
    }
}
