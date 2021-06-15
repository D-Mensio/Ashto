using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPanel : MonoBehaviour
{
    private bool open;
    private Animator creditsAnim;

    public GameObject backButtonObj;
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
        }
    }

    public void Close()
    {
        if (open)
        {
            open = false;
            creditsAnim.SetBool("isOpen", false);
            backButton.status = 1;
        }
    }
}
