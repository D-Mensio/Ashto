using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkManager : MonoBehaviour
{
    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }
}