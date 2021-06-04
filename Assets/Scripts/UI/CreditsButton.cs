using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsButton : MonoBehaviour
{
    public GameObject creditsPanel;

    private Animator creditsAnim;

    private void Start()
    {
        creditsAnim = creditsPanel.GetComponent<Animator>();
    }

    public void OnPress()
    {
        creditsAnim.SetBool("isOpen", true);
        //PlayerPrefs.SetInt("LevelUnlocked", 1);
    }
}
