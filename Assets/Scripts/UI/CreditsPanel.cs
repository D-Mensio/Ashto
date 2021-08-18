using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPanel : MonoBehaviour
{
    private bool open;
    private Animator creditsAnim;

    public GameObject backButtonObj;
    public GameObject backButton2;
    private BackButton backButton;

    private void Start()
    {
        open = false;
        creditsAnim = GetComponent<Animator>();
        backButton = backButtonObj.GetComponent<BackButton>();
    }

    public void Open()
    {
        if (!open)
        {
            open = true;
            Debug.Log("Credits button pressed");
            creditsAnim.SetBool("isOpen", true);
            backButton.status = 3;
            backButton2.SetActive(true);
        }
    }

    public void Close()
    {
        if (open)
        {
            open = false;
            creditsAnim.SetBool("isOpen", false);
            backButton.status = 1;
            backButton2.SetActive(false);
        }
    }
}
