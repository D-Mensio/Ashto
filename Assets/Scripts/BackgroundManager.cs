using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private Color targetColor1;
    private Color targetColor2;
    private float offsetH;
    private float offsetV;
    private float speedH;
    private float speedV;

    void Start()
    {
        targetColor1 = gameObject.GetComponent<Renderer>().material.GetColor("_FirstColor");
        targetColor2 = gameObject.GetComponent<Renderer>().material.GetColor("_SecondColor");
        offsetH = 0;
        offsetV = 0;
        speedH = Random.Range(-2f, 2f);
        speedV = Random.Range(-2f, 2f);
        gameObject.GetComponent<Renderer>().material.SetFloat("_HorizontalSpeed", speedH);
        gameObject.GetComponent<Renderer>().material.SetFloat("_VerticalSpeed", speedV);
    }

    void Update()
    {
        var color1 = Color.Lerp(gameObject.GetComponent<Renderer>().material.GetColor("_FirstColor"), targetColor1, Time.unscaledDeltaTime * 1f);
        var color2 = Color.Lerp(gameObject.GetComponent<Renderer>().material.GetColor("_SecondColor"), targetColor2, Time.unscaledDeltaTime * 1f);
        gameObject.GetComponent<Renderer>().material.SetColor("_FirstColor", color1);
        gameObject.GetComponent<Renderer>().material.SetColor("_SecondColor", color2);
        offsetH += 0.01f * speedH * Time.unscaledDeltaTime;
        if (offsetH > 22)
            offsetH -= 22;
        else if (offsetH < -22)
            offsetH += 22;
        offsetV += 0.01f * speedV * Time.unscaledDeltaTime;
        if (offsetV > 15)
            offsetV -= 15;
        else if (offsetV < -15)
            offsetV += 15;
    }

    public void SetColor(Color color1, Color color2)
    {
        targetColor1 = color1;
        targetColor2 = color2;

        gameObject.GetComponent<Renderer>().material.SetFloat("_InitialOffsetH", offsetH);
        gameObject.GetComponent<Renderer>().material.SetFloat("_InitialOffsetV", offsetV);
        speedH = Random.Range(-2f, 2f);
        speedV = Random.Range(-2f, 2f);
        gameObject.GetComponent<Renderer>().material.SetFloat("_HorizontalSpeed", speedH);
        gameObject.GetComponent<Renderer>().material.SetFloat("_VerticalSpeed", speedV);
    }

}
