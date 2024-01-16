using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; set; }

    [Header("Tutorial Panels")]
    public GameObject[] tutorialPanels;

    private int currentStep = 0;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            ShowCurrentStep();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CompleteCurrentStep();
        }
    }

    private void ShowCurrentStep()
    {
        if (currentStep < tutorialPanels.Length)
        {
            for (int i = 0; i < tutorialPanels.Length; i++)
            {
                tutorialPanels[i].SetActive(i == currentStep);
            }
        }
        else
        {
            Debug.Log("All tutorial steps completed!");
        }
    }

    private void CompleteCurrentStep()
    {
        if (currentStep < tutorialPanels.Length)
        {
            tutorialPanels[currentStep].SetActive(false);

            currentStep++;

            ShowCurrentStep();
        }
        else
        {
            Debug.Log("All tutorial steps completed!");
        }
    }
}
