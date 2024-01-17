using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TouchScene : MonoBehaviour
{
    [SerializeField] FadeScript fade;
    void FadeIn()
    {
        SceneManager.LoadScene("Intro(A)");
    }
    public void InGame()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    public void ChangeTutorialScene()
    {
        fade.Fade(false);
        Invoke("InGame", 1);
    }
    public void ChangeIntroScene()
    {
        fade.Fade(false);
        Invoke("FadeIn", 1);
    }
}
