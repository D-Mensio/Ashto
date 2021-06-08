using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int startScene;
    public bool testing;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        int sceneToLoad;

        if (!PlayerPrefs.HasKey("LevelUnlocked"))
            PlayerPrefs.SetInt("LevelUnlocked", 1);
        if (!PlayerPrefs.HasKey("LastLevelPlayed"))
            PlayerPrefs.SetInt("LastLevelPlayed", 1);

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
