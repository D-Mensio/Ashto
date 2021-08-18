using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//Component that manages scene loading for the current level and checks win condition on every frame
public class LastSceneController : LevelManager
{

    public Animator anim;
    public GameObject titleCardCanvas;

    private bool locked;


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
            anim.SetBool("isOpen", true);
            titleCardCanvas.transform.localScale = Vector3.one;
            locked = true;
            StartCoroutine(Unlock());
        }

    }

    private IEnumerator Unlock()
    {
        yield return new WaitForSeconds(3f);
        locked = false;
    }

    public void BackToFirstLevel()
    {
        if (!locked)
        {
            Debug.Log("Loading Last Level");
            anim.SetBool("isOpen", false);
            StartCoroutine(LoadSceneAsync(1));
        }
    }

}