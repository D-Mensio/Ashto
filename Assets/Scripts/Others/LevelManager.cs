using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//Component that manages scene loading for the current level and checks win condition on every frame
public class LevelManager : MonoBehaviour
{
    public int levelNumber;
    public Color lightColor;
    public Color darkColor;
    private int nextLevel;

    public List<TargetActivation> targets;

    private TextMeshProUGUI countdownText;
    public GameObject cameraObject;
    private bool winCheckActive;

    private AudioSource winClip;

    private bool loadingScene;

    private void Awake()
    {
        BackgroundManager back = GameObject.Find("Background").GetComponent<BackgroundManager>();
        back.SetColor(lightColor, darkColor);
        Menu menu = GameObject.Find("UI").GetComponent<Menu>();
        menu.cam = cameraObject.GetComponent<CameraController>();
        menu.levelNum.GetComponent<TextMeshProUGUI>().text = "-" + levelNumber + "-";
        InputManager im = GameObject.Find("InputManager").GetComponent<InputManager>();
        im.cam = cameraObject.GetComponent<Camera>();
        GameObject countdown = GameObject.Find("Countdown");
        menu.countdown = countdown;
        countdownText = countdown.GetComponent<TextMeshProUGUI>();
        countdownText.text = "";
        winCheckActive = false;
        winClip = GetComponent<AudioSource>();
        nextLevel = levelNumber + 1;
        PlayerPrefs.SetInt("LastLevelPlayed", levelNumber);
        loadingScene = false;
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
                //Debug.Log(time);
                time--;
                yield return new WaitForSeconds(1.2f);
            }
            else
            {
                countdownText.text = "";
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
            StartCoroutine(LoadSceneAsync(nextLevel));
        }

    }

    public IEnumerator LoadSceneAsync(int n)
    {
        if (!loadingScene)
        {
            loadingScene = true;
            AsyncOperation op = SceneManager.LoadSceneAsync(n);
            op.allowSceneActivation = false;
            GameObject.Find("Main Camera").GetComponent<CameraController>().LevelTransition();
            yield return new WaitForSecondsRealtime(1f);
            op.allowSceneActivation = true;
        }
    }


    public void Restart()
    {
        Debug.Log("Restart");
        StartCoroutine(LoadSceneAsync(levelNumber));
    }

}
