using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    private Animator anim;
    public bool open;
    private CameraController cam;

    private EventSystem es;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        open = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    private void Update()
    {
        if(open && Input.GetMouseButtonDown(0) && !EventSystem.current.currentSelectedGameObject)   //checks if no UI element currently being pressed
        {
            Time.timeScale = 1;
            open = false;
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
            anim.SetBool("isOpen", true);
            cam.ZoomOut();
        }
    }
}
