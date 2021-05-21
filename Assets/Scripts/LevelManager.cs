using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int levelNumber;
    public Color lightColor;
    public Color darkColor;
    public int nextLevel;

    public List<Target> targets;

    private TextMeshProUGUI countdown;
    public GameObject cameraObject;
    private bool winCheckActive;

    private void Awake()
    {
        BackgroundManager back = GameObject.Find("Background").GetComponent<BackgroundManager>();
        back.SetColor(lightColor, darkColor);
        Menu menu = GameObject.Find("UI").GetComponent<Menu>();
        menu.cam = cameraObject.GetComponent<CameraController>();
        menu.levelNum.GetComponent<TextMeshProUGUI>().text = "-" + levelNumber + "-";
        countdown = GameObject.Find("Countdown").GetComponent<TextMeshProUGUI>();
        winCheckActive = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (!winCheckActive && targets.All(x => x.active))
        {
            winCheckActive = true;
            StartCoroutine(WinLevel());
        }

    }
    private IEnumerator WinLevel()
    {
        int time = 5;
        bool broken = false;

        while (!broken && time > 0)
        {
            if (targets.All(x => x.active))
            {
                countdown.text = time.ToString();
                //Play sound clip (clock ticking)
                //Debug.Log(time);
                time--;
                yield return new WaitForSeconds(1.2f);
            }
            else
            {
                countdown.text = "";
                //Play wrong clip
                broken = true;
            }
        }
        if (broken)
        {
            //Debug.Log("Broken win condition");
            winCheckActive = false;
        }
        else
        {
            //Play right clip
            countdown.text = "";
            //Debug.Log("win");
            GameObject.Find("Main Camera").GetComponent<CameraController>().LevelTransition();
            StartCoroutine(LoadNextScene());
        }

    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }
   
}
