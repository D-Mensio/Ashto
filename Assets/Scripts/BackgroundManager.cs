using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private Color targetColor1;
    private Color targetColor2;

    void Start()
    {
        targetColor1 = gameObject.GetComponent<Renderer>().material.GetColor("_FirstColor");
        targetColor2 = gameObject.GetComponent<Renderer>().material.GetColor("_SecondColor");
    }

    void Update()
    {
        var color1 = Color.Lerp(gameObject.GetComponent<Renderer>().material.GetColor("_FirstColor"), targetColor1, Time.deltaTime * 1f);
        var color2 = Color.Lerp(gameObject.GetComponent<Renderer>().material.GetColor("_SecondColor"), targetColor2, Time.deltaTime * 1f);
        gameObject.GetComponent<Renderer>().material.SetColor("_FirstColor", color1);
        gameObject.GetComponent<Renderer>().material.SetColor("_SecondColor", color2);
    }

    public void SetColor(Color color1, Color color2)
    {
        targetColor1 = color1;
        targetColor2 = color2;
    }

}
