using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    public GameObject levelSelectPanel;
    public void OnPress()
    {
        Debug.Log("Level select button pressed");
        levelSelectPanel.transform.localScale = Vector3.one;
    }
}
