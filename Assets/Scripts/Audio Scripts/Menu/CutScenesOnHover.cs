using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenesOnHover : MonoBehaviour
{
    [Header(" ")]
    [Header("                                            ---===== Written by Rhys =====---")]

    public float Timer = 0.0f;

    AudioDontDestroyOnLoadWithFadeOutScript hoversource;

    void Awake()
    {
        Timer = 0.0f;
    }

    void OnEnable()
    {
        hoversource = GameObject.Find("MenuMusic").GetComponent<AudioDontDestroyOnLoadWithFadeOutScript>();

        hoversource.MenuHoverSource.Play();
    }    

    void Update()
    {
        Timer  += Time.deltaTime;

        if (Timer >= 0.1f)
        {
            this.gameObject.SetActive(false);
            Timer = 0.0f;
        }
    }
}
