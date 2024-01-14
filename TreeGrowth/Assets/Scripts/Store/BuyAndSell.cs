using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyAndSell : MonoBehaviour
{
    public GameObject BuyItems;
    public GameObject SellItems;

    public void Click_Buy()
    {
        BuyItems.SetActive(true);
        SellItems.SetActive(false);
    }

    public void Click_Sell()
    {
        BuyItems.SetActive(false);
        SellItems.SetActive(true);
    }
}
