using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeScript : MonoBehaviour
{
    public Image Panel;
    FadeUtil fade;
    float time = 0f;
    float F_time = 1f;

    private void Start()
    {
        fade = new FadeUtil(Panel);
    }

    public void Fade(bool start)
    {
        if (start == true)
        {
            fade.StartFadeOff().OnComplete(() => { Panel.gameObject.SetActive(false); });
        }
        else
        {
            Panel.gameObject.SetActive(true);
            fade.StartFadeOn();
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