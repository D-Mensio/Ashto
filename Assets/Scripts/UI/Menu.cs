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

    public GameObject backButtonObj;
    private BackButton backButton;

    public GameObject creditsPanel;
    private Animator creditsAnim;



    private void Awake()
    {
        
        open = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        backButton = backButtonObj.GetComponent<BackButton>();
        anim = button.GetComponent<Animator>();
        creditsAnim = creditsPanel.GetComponent<Animator>();
    }

    public void Close()
    {
        if(open)
        {
            Time.timeScale = 1;
            open = false;
            backButton.status = 0;
            backButton.Hide();
            countdown.transform.localScale = Vector3.one;
            creditsAnim.SetBool("isOpen", false);
            levelNum.SetActive(false);
            anim.SetBool("isOpen", false);
            if(cam)
                cam.Reset();
        }
    }

    public void Open()
    {
        if (!open)
        {
            Time.timeScale = 0;
            open = true;
            backButton.status = 1;
            backButton.Display();
            countdown.transform.localScale = Vector3.zero;
            levelNum.SetActive(true);
            anim.SetBool("isOpen", true);
            if(cam)
                cam.ZoomOut();
        }
    }
}
