using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Component managing the initialization and dynamic setting of parameters for the background shader
public class BackgroundManager : MonoBehaviour
{
    //target colors of the background gradient, used for gradual switch between scenes
    private Color targetColor1;
    private Color targetColor2;

    //current offset of the background texture, used to keep track of its movement between scenes
    private float offsetH;
    private float offsetV;

    //horizontal and vertical speed of the background texture, value randomly calculated (between -2 and +2) for each new scene
    private float speedH;
    private float speedV;
    [SerializeField]
    private float horizontalSpeedModifier;
    [SerializeField]
    private float verticalSpeedModifier;

    void Start()
    {
        offsetH = 0;
        offsetV = 0;
        SetColor(targetColor1, targetColor2);
        targetColor1 = gameObject.GetComponent<Renderer>().material.GetColor("_FirstColor");
        targetColor2 = gameObject.GetComponent<Renderer>().material.GetColor("_SecondColor");

        speedH = Random.Range(-2f, 2f);
        speedV = Random.Range(-2f, 2f);

        gameObject.GetComponent<Renderer>().material.SetFloat("_HorizontalSpeed", speedH);
        gameObject.GetComponent<Renderer>().material.SetFloat("_VerticalSpeed", speedV);

        gameObject.GetComponent<Renderer>().material.SetFloat("_DimensionMultiplier", ((float)Screen.height / Screen.width) / 3 + (1f/3));
    }

    void Update()
    {
        var color1 = Color.Lerp(gameObject.GetComponent<Renderer>().material.GetColor("_FirstColor"), targetColor1, Time.unscaledDeltaTime * 1f);
        var color2 = Color.Lerp(gameObject.GetComponent<Renderer>().material.GetColor("_SecondColor"), targetColor2, Time.unscaledDeltaTime * 1f);

        gameObject.GetComponent<Renderer>().material.SetColor("_FirstColor", color1);
        gameObject.GetComponent<Renderer>().material.SetColor("_SecondColor", color2);

        gameObject.GetComponent<Renderer>().material.SetFloat("_HorizontalOffset", offsetH);
        gameObject.GetComponent<Renderer>().material.SetFloat("_VerticalOffset", offsetV);

        offsetH += horizontalSpeedModifier * speedH * Time.unscaledDeltaTime;
        offsetV += verticalSpeedModifier * speedV * Time.unscaledDeltaTime;
    }

    //Sets target colors and recalculates speed values
    public void SetColor(Color color1, Color color2)
    {
        targetColor1 = color1;
        targetColor2 = color2;

        speedH = Random.Range(-2f, 2f);
        speedV = Random.Range(-2f, 2f);
    }

}
