using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreInOut : MonoBehaviour
{
    private RectTransform rectTransform;
    public Button BuyButton;
    public Button SellButton;
    public Button OutButton;
    public float moveSpeed = 300.0f;

    private Vector2 UptargetPosition;
    private Vector2 DowntargetPosition;

    public float Anchor_Y = 300.0f;

    public bool inStore = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        UptargetPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + Anchor_Y);;
        DowntargetPosition = rectTransform.anchoredPosition;

    }

    void Update()
    {
        StoreMove();
    }

    public void StoreMove()
    {
        BuyButton.onClick.AddListener(InStore);
        SellButton.onClick.AddListener(InStore);
        OutButton.onClick.AddListener(OutStore);

        // //상점 임시 단축키 S
        // if(Input.GetKeyDown(KeyCode.S))
        // {
        //     if(inStore == false)
        //     {
        //         InStore();
        //     }
        //     if(inStore == true)
        //     {
        //         OutStore();
        //     }
        // }

        if (inStore == true)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, UptargetPosition, moveSpeed * Time.deltaTime);
        }

        if (inStore == false)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, DowntargetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void InStore()
    {      
        inStore = true;
    }

    public void OutStore()
    {
        inStore = false;
    }
}