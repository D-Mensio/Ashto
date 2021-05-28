using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelButton : MonoBehaviour
{
    public GameObject levelSelectPanel;
    public Text inputValue;
    public void OnPress()
    {
        Debug.Log("Load level button pressed");
        StartCoroutine(GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene(Int32.Parse(inputValue.text)));
        levelSelectPanel.transform.localScale = Vector3.zero;
    }
}
