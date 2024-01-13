using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            settingPanel.SetActive(false);
        } else
        {
            settingPanel.SetActive(true);
        }
    }
}
