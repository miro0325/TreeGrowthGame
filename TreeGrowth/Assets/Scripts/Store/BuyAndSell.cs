using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyAndSell : MonoBehaviour
{
    private RectTransform Buy_rectTransform;
    private RectTransform Sell_rectTransform;

    public GameObject BuyBar;
    public GameObject SellBar;

    // BuyItem에서 0은 물뿌리개, 1은 비료, 2는 도우미 로봇(?)으로 설정해놨습니다만 추후에 바꿀 수도 있습니다.
    // 현재 여기 스탯 레벨(BuyItemLevel)이랑 Tree 쪽의 스탯 레벨을 연동하지 않았습니다.
    public float [] BuyItemPrice = new float[3];
    public int [] BuyItemLevel = new int[3];
    public Text[] BuyItemPrice_Text = new Text[3];
    public Text[] BuyItemLevel_Text = new Text[3];

    public int [] SellItemPrice = new int[3];
    public int [] SellItemLeaf = new int[3];
    public Text[] SellItems_Text = new Text[3];
    public Text[] SellItemsMoney_Text = new Text[3];
    public GameObject[] SellChoice = new GameObject[3];
    public int SellItemNumber = 1; // SellItem을 한 번에 몇 개를 팔 것인가를 고를 수 있게 하는 변수
    public int ChosenNum;
    public bool OtherButtonActive = false;

    public Text Money;
    public Text Leaf;

    private Goods[] goodsArray = new Goods[3];

    private bool isOnBuyPanel = false;

    void Start()
    {
        BuyItemPrice[0] = 100; BuyItemPrice[1] = 100; BuyItemPrice[2] = 2000;

        SellItemPrice[0] = 1500; SellItemPrice[1] = 20000; SellItemPrice[2] = 100000;
        SellItemLeaf[0] = 20; SellItemLeaf[1] = 100; SellItemLeaf[2] = 500;

        for(int i = 0; i < 3; i++)
        {
            BuyItemLevel[i] = 0;
            SellItemsMoney_Text[i].text = SellItemPrice[i].ToString();
            SellItems_Text[i].text = "Leaf: " + SellItemLeaf[i].ToString();
            SellChoice[i].SetActive(false);
        }

        Buy_rectTransform = BuyBar.GetComponent<RectTransform>();
        Sell_rectTransform = SellBar.GetComponent<RectTransform>();

        goodsArray[0] = new WateringCan(BuyItemPrice[0]);
        goodsArray[1] = new Fertilizer(BuyItemPrice[1]);
        goodsArray[2] = new CleanMachine(BuyItemPrice[2]);
    }

    void Update()
    {
        Money.text = GameManager.Money.ToString();
        Leaf.text = "Leaf: " + GameManager.Leaf.ToString();
        ChangePanel();
        for(int i = 0; i < BuyItemLevel.Length; i++)
        {
           BuyItemLevel_Text[i].text = "Lv." + BuyItemLevel[i].ToString();
           BuyItemPrice_Text[i].text = BuyItemPrice[i].ToString();
        }
    }

    private void ChangePanel()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!isOnBuyPanel)
            {
                Click_Buy();
                isOnBuyPanel = true;
            } else
            {
                Click_Sell();
                isOnBuyPanel=false;
            }
        }
    }

    public void Click_Buy()
    {
        Sell_rectTransform.SetAsFirstSibling();
    }

    public void Click_Sell()
    {   
        Buy_rectTransform.SetAsFirstSibling();
    }

    public void SellItemNumber_Plus()
    {
        if(SellItemLeaf[ChosenNum] * (SellItemNumber + 1) > GameManager.Leaf)
            SellItemNumber = 1;
        else
            SellItemNumber++;

        SellItems_Text[ChosenNum].text = SellItemNumber.ToString();
        SellItemsMoney_Text[ChosenNum].text = (SellItemPrice[ChosenNum] * SellItemNumber).ToString();
    }
    public void SellItemNumber_Minus()
        {
            if(SellItemNumber == 1)
                SellItemNumber = GameManager.Leaf / SellItemLeaf[ChosenNum];
            else
                SellItemNumber--;

            SellItems_Text[ChosenNum].text = SellItemNumber.ToString();
            SellItemsMoney_Text[ChosenNum].text = (SellItemPrice[ChosenNum] * SellItemNumber).ToString();      
        }
    public void SellItemNumber_Click()
    {
        GameManager.Leaf -= SellItemLeaf[ChosenNum] * SellItemNumber;
        GameManager.Money += SellItemPrice[ChosenNum] * SellItemNumber;
        SellItemNumber = 1;
        SellItems_Text[ChosenNum].text = "Leaf: " + SellItemLeaf[ChosenNum].ToString();
        SellItemsMoney_Text[ChosenNum].text = SellItemPrice[ChosenNum].ToString();
        SellChoice[ChosenNum].SetActive(false);
        OtherButtonActive = false;
    }

    public void Sell_0()
    {
        if (GameManager.Leaf >= SellItemLeaf[0] && OtherButtonActive == false)
        {
            OtherButtonActive = true;
            ChosenNum = 0;
            SellItems_Text[ChosenNum].text = SellItemNumber.ToString();
            SellChoice[0].SetActive(true);
        }
        else
        { // 임시로 로그에만 표시해 놓았습니다.
            if(OtherButtonActive == true)
                Debug.Log("다른 선택지가 활성화 중입니다.");
            else
                Debug.Log("나뭇잎 개수가 부족합니다.");
        }
    }

    public void Sell_1()
    {
        if (GameManager.Leaf >= SellItemLeaf[1] && OtherButtonActive == false)
        {
            OtherButtonActive = true;
            ChosenNum = 1;
            SellItems_Text[ChosenNum].text = SellItemNumber.ToString();
            SellChoice[1].SetActive(true);
        }
        else
        { // 임시로 로그에만 표시해 놓았습니다.
            if(OtherButtonActive == true)
                Debug.Log("다른 선택지가 활성화 중입니다.");
            else
                Debug.Log("나뭇잎 개수가 부족합니다.");
        }
    }

    public void Sell_2()
    {
        if (GameManager.Leaf >= SellItemLeaf[2] && OtherButtonActive == false)
        {
            OtherButtonActive = true;
            ChosenNum = 2;
            SellItems_Text[ChosenNum].text = SellItemNumber.ToString();
            SellChoice[2].SetActive(true);
        }
        else
        { // 임시로 로그에만 표시해 놓았습니다.
            if(OtherButtonActive == true)
                Debug.Log("다른 선택지가 활성화 중입니다.");
            else
                Debug.Log("나뭇잎 개수가 부족합니다.");
        }
    }

    public void Buy_0()
    {
        if (GameManager.Money >= BuyItemPrice[0])
        {
            BuyItemLevel[0]++;
            goodsArray[0].Buy(ref GameManager.Money);
            BuyItemPrice[0] *= 1.25f;
            BuyItemPrice[0] = (int)BuyItemPrice[0];
            goodsArray[0].InitPrice(BuyItemPrice[0]);
        }
        else
        { // 임시로 로그에만 표시해 놓았습니다.
            Debug.Log("돈이 부족합니다.");
        }
    }

    public void Buy_1()
    {
        if (GameManager.Money >= BuyItemPrice[1] && BuyItemLevel[1] < 900)
        {
            BuyItemLevel[1]++;
            goodsArray[1].Buy(ref GameManager.Money);
            //GameManager.Money -= BuyItemPrice[1];
            BuyItemPrice[1] *= 1.25f;
            BuyItemPrice[1]  = (int)BuyItemPrice[1];
            goodsArray[1].InitPrice(BuyItemPrice[1]);
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
        if (GameManager.Money >= BuyItemPrice[2])
        {
            BuyItemLevel[2]++;
            goodsArray[2].Buy(ref GameManager.Money);
            //GameManager.Money -= BuyItemPrice[2];
            BuyItemPrice[2] *= 1.25f;
            BuyItemPrice[2] = (int)BuyItemPrice[2];
            goodsArray[2].InitPrice(BuyItemPrice[2]);
        }
        else
        { // 임시로 로그에만 표시해 놓았습니다.
            Debug.Log("돈이 부족합니다.");
        }
    }
}
