using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsButton : MonoBehaviour
{


    private void Awake()
    {
    }

    public void OnPress()
    {
        //TODO, for now resets level progress
        PlayerPrefs.SetInt("LevelUnlocked", 1);
    }
}
