using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public int status; //0 - game running, 1 - menu open, 2 - level select open, 3 - credits open, 4 - close game window open
    public Menu menu;
    public LoadLevelPanel levelSelect;
    public CreditsPanel credits;
    public GameObject closeApplicationPanel;


    public bool open;
    private Animator closeWindowAnim;
    void Awake()
    {
        status = 0;
        open = false;
        closeWindowAnim = closeApplicationPanel.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPress();
        }
    }
    

    public void OnPress()
    {
        switch (status) {
            case 0:
                Open();
                status = 4;
                break;
            case 1:
                menu.Close();
                break;
            case 2:
                levelSelect.Close();
                break;
            case 3:
                credits.Close();
                break;
            case 4:
                Close();
                break;
        }
    }

    private void Open()
    {

        if (!open)
        {
            open = true;         
            closeWindowAnim.SetBool("isOpen", true);
            status = 4;
            Display();
        }
    }

    private void Close()
    {
        if (open)
        {
            open = false;
            closeWindowAnim.SetBool("isOpen", false);
            status = 0;
            Hide();
        }
    }

    public void Display()
    {
        transform.localScale = Vector3.one;
    }

    public void Hide()
    {
        transform.localScale = Vector3.zero;
    }



}
