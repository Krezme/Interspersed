using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDontDestroyOnLoadWithFadeOutScript : MonoBehaviour
{

    public GameObject FadeOutAudio;

    public GameObject FadeInAudio;

    public static AudioDontDestroyOnLoadWithFadeOutScript instance;

    void Awake()
    {
        if (instance != null) {
            Destroy(instance.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
