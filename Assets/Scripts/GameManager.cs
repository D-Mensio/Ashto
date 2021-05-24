using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int startScene;
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(startScene);
    }

    private void LoadLevel(int n)
    {
        SceneManager.LoadScene(n, LoadSceneMode.Single);
    }
}
