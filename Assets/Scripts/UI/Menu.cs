using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject button;
    private Animator anim;
    public bool open;
    public CameraController cam;

    private EventSystem es;

    public GameObject levelNum;

    public GameObject countdown;

    public GameObject backButton;

    public GameObject levelSelectPanel;

    private void Awake()
    {
        
        open = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = button.GetComponent<Animator>();
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        es = EventSystem.current;
    }

    public void Close()
    {
        if(open)
        {
            Time.timeScale = 1;
            open = false;
            backButton.SetActive(false);
            countdown.transform.localScale = Vector3.one;
            levelSelectPanel.transform.localScale = Vector3.zero;
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
