using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDontDestroyOnLoadWithFadeOutScript : MonoBehaviour
{

    public GameObject FadeOutAudio;

    public GameObject FadeInAudio;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
