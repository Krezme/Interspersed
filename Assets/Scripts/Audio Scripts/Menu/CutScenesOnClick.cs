using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenesOnClick : MonoBehaviour
{
    [Header(" ")]
    [Header("                                            ---===== Written by Rhys =====---")]

    AudioDontDestroyOnLoadWithFadeOutScript clicksource;

    void Start()
    {
        clicksource = GameObject.Find("MenuMusic").GetComponent<AudioDontDestroyOnLoadWithFadeOutScript>();

        clicksource.MenuClickSource.Play();
    }    
}