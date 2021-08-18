using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevCommands
{
    [MenuItem("Dev/ResetProgress")]
    static void ResetProgress()
    {
        PlayerPrefs.SetInt("LevelUnlocked", 1);
        PlayerPrefs.SetInt("LastLevelPlayed", 1);
    }

    [MenuItem("Dev/UnlockToStartScene")]
    static void UnlockN()
    {
        int n = GameObject.Find("GameManager").GetComponent<GameManager>().startScene;
        PlayerPrefs.SetInt("LevelUnlocked", n);
    }

}
