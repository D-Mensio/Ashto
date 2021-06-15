using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGameButton : MonoBehaviour
{
    public void OnPress()
    {
        Application.Quit();
    }
}
