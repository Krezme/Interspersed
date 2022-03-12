using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioEnableAfterFinishPlaying : MonoBehaviour

{
    [Header(" ")]
    [Header("                                            ---===== Written by Rhys =====---")]

    [SerializeField]


    public AudioSource PlayingSource;

    public GameObject ObjectToEnable;


    // Update is called once per frame
    void Update()
    {
        if (!PlayingSource.isPlaying)
        {
            PlayingSource.gameObject.SetActive(false);
            ObjectToEnable.SetActive(true);
        }
    }
}
