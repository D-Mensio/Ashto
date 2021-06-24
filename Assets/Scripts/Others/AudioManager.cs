using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    //private AudioListener audioListener;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //audioListener = GetComponent<AudioListener>();
    }

    private void OnApplicationFocus(bool focus)
    {
        if(focus)
            CheckAudio();
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
            CheckAudio();
    }

    private void CheckAudio()
    {
        if (!audioSource.isPlaying)
            audioSource.UnPause();
    }

}
