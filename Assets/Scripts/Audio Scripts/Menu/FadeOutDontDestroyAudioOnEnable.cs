using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutDontDestroyAudioOnEnable : MonoBehaviour
{
    [Header(" ")]
    [Header("                                            ---===== Written by Rhys =====---")]

    AudioDontDestroyOnLoadWithFadeOutScript fadeout;

    void Start()
    {
        fadeout = GameObject.FindGameObjectWithTag("FadeOut").GetComponent<AudioDontDestroyOnLoadWithFadeOutScript>();

        fadeout.FadeOutAudio.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
