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
    // Start is called before the first frame update
    void Start()
    {
        int sceneToLoad;
        if (testing)
            sceneToLoad = startScene;
        else if (PlayerPrefs.HasKey("LevelUnlocked"))
        {
            sceneToLoad = PlayerPrefs.GetInt("LevelUnlocked");
        }
        else
        {
            PlayerPrefs.SetInt("LevelUnlocked", 1);
            sceneToLoad = 1;
        }
        LoadLevel(sceneToLoad);
    }

    private void LoadLevel(int n)
    {
        SceneManager.LoadScene(n, LoadSceneMode.Single);
    }
}
