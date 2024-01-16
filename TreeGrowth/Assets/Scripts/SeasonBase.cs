using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeasonType
{
    Spring = 0,
    Summer = 1,
    Fall = 2,
    Winter = 3

}

public abstract class SeasonBase
{
    public SeasonType type;

    [SerializeField] protected int duration;
    protected object obj;

    public SeasonBase(object _obj = null)
    {
        obj = _obj;
    }
    public abstract void Passive();
}
