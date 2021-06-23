using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

//Controls first scene loading and general settings on application start
public class GameManager : MonoBehaviour
{
    public int startScene;
    [SerializeField]
    private bool testing;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 90;
        int sceneToLoad;

        if (!PlayerPrefs.HasKey("LevelUnlocked"))
            PlayerPrefs.SetInt("LevelUnlocked", 1);
        if (!PlayerPrefs.HasKey("LastLevelPlayed"))
            PlayerPrefs.SetInt("LastLevelPlayed", 1);
        if (!PlayerPrefs.HasKey("AudioOn"))
            PlayerPrefs.SetInt("AudioOn", 1);

        if (testing)
            sceneToLoad = startScene;
        else
        {
            sceneToLoad = PlayerPrefs.GetInt("LastLevelPlayed");
        }
        LoadLevel(sceneToLoad);      
    }

    private void LoadLevel(int n)
    {
        SceneManager.LoadScene(n, LoadSceneMode.Single);
    }
}
