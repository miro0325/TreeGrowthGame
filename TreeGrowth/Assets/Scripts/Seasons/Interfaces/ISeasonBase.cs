using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherType
{
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

public interface ISeasonBase
{
    public void Init(object obj);

    public void Reset();

    public void Passive();

    public void SeasonEvent();
}
