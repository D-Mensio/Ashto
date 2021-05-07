using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var color1 = Color.Lerp(gameObject.GetComponent<Renderer>().material.GetColor("_FirstColor"), Color.red, Time.deltaTime * 1f);
        var color2 = Color.Lerp(gameObject.GetComponent<Renderer>().material.GetColor("_SecondColor"), Color.gray, Time.deltaTime * 1f);
        gameObject.GetComponent<Renderer>().material.SetColor("_FirstColor", color1);
        gameObject.GetComponent<Renderer>().material.SetColor("_SecondColor", color2);
    }

}
