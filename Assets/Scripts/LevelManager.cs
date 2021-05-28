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
    private int nextLevel;

    public List<Target> targets;

    private TextMeshProUGUI countdownText;
    public GameObject cameraObject;
    private bool winCheckActive;

    private AudioSource winClip;

    private void Awake()
    {
        BackgroundManager back = GameObject.Find("Background").GetComponent<BackgroundManager>();
        back.SetColor(lightColor, darkColor);
        Menu menu = GameObject.Find("UI").GetComponent<Menu>();
        menu.cam = cameraObject.GetComponent<CameraController>();
        menu.levelNum.GetComponent<TextMeshProUGUI>().text = "-" + levelNumber + "-";
        GameObject countdown = GameObject.Find("Countdown");
        menu.countdown = countdown;
        countdownText = countdown.GetComponent<TextMeshProUGUI>();
        countdownText.text = "";
        winCheckActive = false;
        winClip = GetComponent<AudioSource>();
        nextLevel = levelNumber + 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Loaded Level " + levelNumber);
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
        int time = 3;
        bool broken = false;

        while (!broken && time > 0)
        {
            if (targets.All(x => x.active))
            {
                countdownText.text = time.ToString();
                //Play sound clip (clock ticking)
                //Debug.Log(time);
                time--;
                yield return new WaitForSeconds(1.2f);
            }
            else
            {
                countdownText.text = "";
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
            winClip.Play();
            countdownText.text = "";
            //Debug.Log("win");
            if (PlayerPrefs.GetInt("LevelUnlocked") < nextLevel)
                PlayerPrefs.SetInt("LevelUnlocked", nextLevel);
            StartCoroutine(LoadNextScene());
        }

    }

    private IEnumerator LoadNextScene()
    {
        GameObject.Find("Main Camera").GetComponent<CameraController>().LevelTransition();
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }

    public IEnumerator LoadScene(int n)
    {
        GameObject.Find("Main Camera").GetComponent<CameraController>().LevelTransition();
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(n, LoadSceneMode.Single);
    }

    public IEnumerator Restart()
    {
        Debug.Log("Restart");
        GameObject.Find("Main Camera").GetComponent<CameraController>().LevelTransition();
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(levelNumber, LoadSceneMode.Single);
    }

}
