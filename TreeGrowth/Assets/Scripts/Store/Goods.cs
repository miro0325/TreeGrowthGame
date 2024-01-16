using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods
{
    private float price;

    public Goods(float _price)
    {
        price = _price;
    }

    public void InitPrice(float _price)
    {
        this.price = _price;
    }

    public virtual void Buy(ref float money)
    {
        if(money >= price)
        {
            money-=price;

        }
    }
}
