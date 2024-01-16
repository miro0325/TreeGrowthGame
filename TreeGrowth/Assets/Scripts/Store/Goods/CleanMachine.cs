using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanMachine : Goods
{
    public CleanMachine(float _price) : base(_price)
    {
    }

    public override void Buy(ref float money)
    {
        base.Buy(ref money);
        var bot = GameManager.Instance.GetMaidBot();
        if (!bot.gameObject.activeSelf)
        {
            bot.gameObject.SetActive(true);
            bot.Level++;
            return;
        } else
        {
            bot.Level++;
        }
    }
}
