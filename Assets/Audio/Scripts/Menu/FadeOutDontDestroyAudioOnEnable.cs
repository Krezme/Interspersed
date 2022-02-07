using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutDontDestroyAudioOnEnable : MonoBehaviour
{

    AudioDontDestroyOnLoadWithFadeOutScript fadeout;

    void Awake()
    {
        fadeout = GameObject.FindGameObjectWithTag("FadeOut").GetComponent<AudioDontDestroyOnLoadWithFadeOutScript>();

        fadeout.FadeOutAudio.SetActive(true);
    }

}
