using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchScene : MonoBehaviour
{
    void FadeIn()
    {
        SceneManager.LoadScene("Intro(A)");
    }

    public void ChangeScene()
    {
        Invoke("FadeIn", 1);
    }
}
