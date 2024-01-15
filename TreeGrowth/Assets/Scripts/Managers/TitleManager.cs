using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    private bool isPopUp = false;
    
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
        AudioManager.instance.PlaySFX("BtSound");
        if (isPopUp) return;
        if(settingPanel.activeSelf)
        {
            isPopUp = true;
            settingPanel.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => DisableSettingPanel()) ;
        } else
        {
            isPopUp = true;
            settingPanel.SetActive(true);
            settingPanel.transform.localScale = Vector3.zero;
            settingPanel.transform.DOScale(Vector3.one, 0.3f).OnComplete(() => isPopUp = false);
        }
    }
    private void DisableSettingPanel()
    {
        isPopUp = false;
        settingPanel.SetActive(false);
    }
}
