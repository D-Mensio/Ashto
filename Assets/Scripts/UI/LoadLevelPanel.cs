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
    

    private void Start()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            GameObject button = Instantiate(levelButton);
            button.transform.SetParent(content.transform,false);
            button.GetComponent<LevelButton>().Initialize(i+1, levels[i]);
        }
        levelSelectAnim = GetComponent<Animator>();
    }

    public void LoadLevel(int n)
    {
        Debug.Log("Load level button pressed");
        StartCoroutine(GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadScene(n));
        levelSelectAnim.SetBool("isOpen", false);
    }

    public void UpdateLock()
    {
        foreach(LevelButton b in GetComponentsInChildren(typeof(LevelButton)))
        {
            b.UpdateLock();
        }
        content.transform.position = new Vector3(content.transform.position.x, 0, content.transform.position.z);
    }
}
