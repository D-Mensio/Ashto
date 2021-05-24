using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public void OnPress()
    {
        Debug.Log("Restart button pressed");
        StartCoroutine(GameObject.Find("LevelManager").GetComponent<LevelManager>().Restart());
    }
}
