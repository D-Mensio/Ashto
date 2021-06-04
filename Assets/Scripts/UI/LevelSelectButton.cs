using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    public GameObject levelSelectPanel;
    private Animator levelSelectAnim;

    private void Start()
    {
        levelSelectAnim = levelSelectPanel.GetComponent<Animator>();
    }
    public void OnPress()
    {
        Debug.Log("Level select button pressed");
        levelSelectAnim.SetBool("isOpen", true);
        levelSelectPanel.GetComponent<LoadLevelPanel>().UpdateLock();
    }
}
