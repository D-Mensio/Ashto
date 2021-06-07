using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPanel : MonoBehaviour
{
    private bool open;
    private Animator creditsAnim;
    public GameObject backButton;

    private void Start()
    {
        open = false;
        creditsAnim = GetComponent<Animator>();
    }

    public void Open()
    {
        if (!open)
        {
            open = true;
            Debug.Log("Credits button pressed");
            creditsAnim.SetBool("isOpen", true);
            backButton.SetActive(true);
        }
    }

    public void Close()
    {
        if (open)
        {
            open = false;
            creditsAnim.SetBool("isOpen", false);
            backButton.SetActive(false);
        }
    }
}
