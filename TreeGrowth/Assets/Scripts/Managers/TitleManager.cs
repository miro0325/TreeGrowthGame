using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSettingPanel()
    {
        if(settingPanel.activeSelf)
        {
            settingPanel.transform.DOScale(Vector3.zero, 1).OnComplete(() => DisableSettingPanel()) ;
        } else
        {
            settingPanel.SetActive(true);
            settingPanel.transform.localScale = Vector3.zero;
            settingPanel.transform.DOScale(Vector3.one, 1);
        }
    }
    private void DisableSettingPanel()
    {
        settingPanel.SetActive(false);
    }
}
