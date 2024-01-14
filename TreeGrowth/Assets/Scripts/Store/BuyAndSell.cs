using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyAndSell : MonoBehaviour
{
    public GameObject BuyItems;
    public GameObject SellItems;

    // BuyItem에서 0은 물뿌리개, 1은 비료, 2는 도우미 로봇(?)으로 설정해놨습니다만 추후에 바꿀 수도 있습니다.
    // 현재 여기 스탯 레벨(BuyItemLevel)이랑 Tree 쪽의 스탯 레벨을 연동하지 않았습니다.
    public float [] BuyItemPrice = new float[3];
    public int [] BuyItemLevel = new int[3];
    public Text[] BuyItems_Text = new Text[3];

    public int [] SellItemPrice = new int[3];
    public int [] SellItemLeaf = new int[3];

    // 임시로 쓴 변수입니다. Tree Script와 나중에 맞춰야합니다.
    public Text Money;
    public Text Leaf;
    public float money = 0;
    public int leaf = 1000;

    void Start()
    {
        for(int i = 0; i < BuyItemLevel.Length; i++)
            BuyItemLevel[i] = 0;
        BuyItemPrice[0] = 100; BuyItemPrice[1] = 100; BuyItemPrice[2] = 2000;

        SellItemPrice[0] = 1500; SellItemPrice[1] = 20000; SellItemPrice[2] = 100000;
        SellItemLeaf[0] = 20; SellItemLeaf[1] = 100; SellItemLeaf[2] = 500;
    }

    void Update()
    {
        Money.text = "Money: " + money.ToString();
        Leaf.text = "Leaf: " + leaf.ToString();

        for(int i = 0; i < BuyItems_Text.Length; i++)
            BuyItems_Text[i].text = "Lv." + BuyItemLevel[i] + " " + BuyItemPrice[i];
    }

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

    public void Sell_0()
    {
        if (leaf >= SellItemLeaf[0])
        {
            leaf -= SellItemLeaf[0];
            money += SellItemPrice[0];
        }
        else
        { // 임시로 로그에만 표시해 놓았습니다.
            Debug.Log("나뭇잎 개수가 부족합니다.");
        }
    }

    public void Sell_1()
    {
        if (leaf >= SellItemLeaf[1])
        {
            leaf -= SellItemLeaf[1];
            money += SellItemPrice[1];
        }
        else
        { // 임시로 로그에만 표시해 놓았습니다.
            Debug.Log("나뭇잎 개수가 부족합니다.");
        }
    }

    public void Sell_2()
    {
        if (leaf >= SellItemLeaf[2])
        {
            leaf -= SellItemLeaf[2];
            money += SellItemPrice[2];
        }
        else
        { // 임시로 로그에만 표시해 놓았습니다.
            Debug.Log("나뭇잎 개수가 부족합니다.");
        }
    }

    public void Buy_0()
    {
        if (money >= BuyItemPrice[0])
        {
            BuyItemLevel[0]++;
            money -= BuyItemPrice[0];
            BuyItemPrice[0] *= 1.25f;
            BuyItemPrice[0] = (int)BuyItemPrice[0];
        }
        else
        { // 임시로 로그에만 표시해 놓았습니다.
            Debug.Log("돈이 부족합니다.");
        }
    }

    public void Buy_1()
    {
        if (money >= BuyItemPrice[1] && BuyItemLevel[1] < 900)
        {
            BuyItemLevel[1]++;
            money -= BuyItemPrice[1];
            BuyItemPrice[1] *= 1.25f;
            BuyItemPrice[1]  = (int)BuyItemPrice[1];
        }
        else
        { // 임시로 로그에만 표시해 놓았습니다.
            if(BuyItemLevel[1] >= 900)
                Debug.Log("이미 최대레벨입니다.");
            else
                Debug.Log("돈이 부족합니다.");
        }
    }

    public void Buy_2()
    {
        if (money >= BuyItemPrice[2])
        {
            BuyItemLevel[2]++;
            money -= BuyItemPrice[2];
            BuyItemPrice[2] *= 1.25f;
            BuyItemPrice[2] = (int)BuyItemPrice[2];
        }
        else
        { // 임시로 로그에만 표시해 놓았습니다.
            Debug.Log("돈이 부족합니다.");
        }
    }
}
