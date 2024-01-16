using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherType {
    None,
    Storm,

}

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

    public abstract void Init();

    public abstract void Passive();

    public abstract void SeasonEvent();
}
