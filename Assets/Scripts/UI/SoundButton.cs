using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public Sprite soundOffImage;
    public Sprite soundOnImage;

    private Image image;
    private bool soundOn;

    private void Awake()
    {
        soundOn = true;
        image = GetComponent<Image>();
        image.sprite = soundOnImage;
        if (PlayerPrefs.GetInt("AudioOn") == 0)
            OnPress();
    }

    public void OnPress()
    {
        if (soundOn)
        {
            soundOn = false;
            AudioListener.volume = 0;
            image.sprite = soundOffImage;
            PlayerPrefs.SetInt("AudioOn", 0);
        }
        else
        {
            soundOn = true;
            AudioListener.volume = 1;
            image.sprite = soundOnImage;
            PlayerPrefs.SetInt("AudioOn", 1);
        }
    }
}
