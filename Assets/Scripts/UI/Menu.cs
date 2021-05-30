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

    private void Update()
    {
        if(open && Input.GetMouseButtonDown(0) && !es.currentSelectedGameObject)   //checks if no UI element currently being pressed
        {
            Time.timeScale = 1;
            open = false;
            countdown.transform.localScale = Vector3.one;
            levelSelectPanel.transform.localScale = Vector3.zero;
            levelNum.SetActive(false);
            anim.SetBool("isOpen", false);
            cam.Reset();
        }
    }

    public void Press()
    {
        if (!open)
        {
            Time.timeScale = 0;
            open = true;
            countdown.transform.localScale = Vector3.zero;
            levelNum.SetActive(true);
            anim.SetBool("isOpen", true);
            cam.ZoomOut();
        }
    }
}
