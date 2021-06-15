using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastSceneController : MonoBehaviour
{

    private Animator anim;
    public Color lightColor;
    public Color darkColor;
    public Camera cam;
    bool pressed;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isOpen", true);
        BackgroundManager back = GameObject.Find("Background").GetComponent<BackgroundManager>();
        back.SetColor(lightColor, darkColor);
        Menu menu = GameObject.Find("UI").GetComponent<Menu>();
        menu.levelNum.GetComponent<TextMeshProUGUI>().text = "";
        InputManager im = GameObject.Find("InputManager").GetComponent<InputManager>();
        im.cam = cam;
        pressed = false;
    }

    public void OnPress()
    {
        if (!pressed)
        {
            pressed = true;
            StartCoroutine(LoadScene());
        }
    }

    public IEnumerator LoadScene()
    {
        anim.SetBool("isOpen", false);
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

}
