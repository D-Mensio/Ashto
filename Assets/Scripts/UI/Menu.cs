using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject button;
    private Animator anim;
    public bool open;
    public CameraController cam;

    public GameObject levelNum;

    public GameObject countdown;

    public GameObject backButton;

    public GameObject levelSelectPanel;
    private Animator levelSelectAnim;

    public GameObject creditsPanel;
    private Animator creditsAnim;



    private void Awake()
    {
        
        open = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = button.GetComponent<Animator>();

        levelSelectAnim = levelSelectPanel.GetComponent<Animator>();
        creditsAnim = creditsPanel.GetComponent<Animator>();
    }

    public void Close()
    {
        if(open)
        {
            Time.timeScale = 1;
            open = false;
            backButton.SetActive(false);
            countdown.transform.localScale = Vector3.one;
            levelSelectAnim.SetBool("isOpen", false);
            creditsAnim.SetBool("isOpen", false);
            levelNum.SetActive(false);
            anim.SetBool("isOpen", false);
            cam.Reset();
        }
    }

    public void Open()
    {
        if (!open)
        {
            Time.timeScale = 0;
            open = true;
            backButton.SetActive(true);
            countdown.transform.localScale = Vector3.zero;
            levelNum.SetActive(true);
            anim.SetBool("isOpen", true);
            cam.ZoomOut();
        }
    }
}
