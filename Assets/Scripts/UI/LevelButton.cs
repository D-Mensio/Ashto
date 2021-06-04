using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int levelNum;
    public bool locked;
    public Color color;
    public Image contentImage;

    private GameObject levelSelectPanel;

    private void Start()
    {
        levelSelectPanel = GameObject.Find("LevelSelectPanel");
    }

    public void Initialize(int n, Color c)
    {
        levelNum = n;
        GetComponentInChildren<TextMeshProUGUI>().text = levelNum.ToString();
        color = c;
    }


    public void LoadLevel()
    {
        if (!locked)
        {
            levelSelectPanel.GetComponent<LoadLevelPanel>().LoadLevel(levelNum);
        }
    }

    public void UpdateLock()
    {
        locked = PlayerPrefs.GetInt("LevelUnlocked") < levelNum;
        if (locked)
        {
            contentImage.color = Color.gray;
        }
        else
        {
            contentImage.color = color;
        }
    }
}
