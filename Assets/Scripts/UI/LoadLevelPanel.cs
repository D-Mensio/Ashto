using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelPanel : MonoBehaviour
{
    public Color[] levels;
    public GameObject content;
    public GameObject levelButton;
    private Animator levelSelectAnim;
    public GameObject backButtonObj;
    private BackButton backButton;

    private bool open;


    private void Start()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            GameObject button = Instantiate(levelButton);
            button.transform.SetParent(content.transform,false);
            button.GetComponent<LevelButton>().Initialize(i+1, levels[i]);
        }
        levelSelectAnim = GetComponent<Animator>();
        open = false;
        backButton = backButtonObj.GetComponent<BackButton>();
    }

    public void LoadLevel(int n)
    {
        Debug.Log("Load level button pressed");
        Close();
        StartCoroutine(GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadSceneAsync(n));
    }

    private void UpdateLock()
    {
        foreach(LevelButton b in GetComponentsInChildren(typeof(LevelButton)))
        {
            b.UpdateLock();
        }
        content.transform.position = new Vector3(content.transform.position.x, 0, content.transform.position.z);
    }

    public void Open()
    {
        if (!open)
        {
            open = true;
            Debug.Log("Level select button pressed");
            levelSelectAnim.SetBool("isOpen", true);
            backButton.status = 2;
            UpdateLock();
        }
    }

    public void Close()
    {
        if (open)
        {
            open = false;
            levelSelectAnim.SetBool("isOpen", false);
            backButton.status = 1;
        }
    }

}
