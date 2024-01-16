using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Light2D = UnityEngine.Rendering.Universal.Light2D;
using DG.Tweening;

public class LightControl : MonoBehaviour
{
    [SerializeField] Light2D myLight;
    [SerializeField] float speed;
    [SerializeField] float time;
    [SerializeField] float maxStrength;
    [SerializeField] float minStrength;
    private float curTime = 0;
    private float curStrength = 0;
    private bool isBright = false;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateLighting();
    }

    void UpdateLighting()
    {
        if(isBright)
            curStrength += Time.deltaTime * speed;
        else
            curStrength -= Time.deltaTime * speed;
        myLight.intensity = curStrength;
        if(isBright && curStrength >= maxStrength)
        {
            curStrength = maxStrength;
            isBright = !isBright;
        } else if(!isBright && curStrength <= minStrength)
        {
            curStrength = minStrength;
            isBright= !isBright;
        }
    }
}
