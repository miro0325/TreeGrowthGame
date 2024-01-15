using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript1 : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;


    private void Start()
    {
        Fade(true);
    }
    public void Fade(bool start)
    {
        if (start == true)
        {
            StartCoroutine(FadeStart());
        }
        else
        {
            StartCoroutine(FadeEnd());
        }

    }

    IEnumerator FadeEnd()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        alpha.a = 0;
        Panel.color = alpha;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0f, 1f, time);
            Panel.color = alpha;
            yield return null;
        }
        time = 0;


    }

    IEnumerator FadeStart()
    {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        alpha.a = 1;
        Panel.color = alpha;

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1f, 0f, time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        time = 0;

    }

}