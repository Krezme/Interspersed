using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(OptionsMenu.masterVol) * 20);
        mixer.SetFloat("MusicVolume", Mathf.Log10(OptionsMenu.musicVol) * 20);
        mixer.SetFloat("SFXVolume", Mathf.Log10(OptionsMenu.sfxVol) * 20);
    }

}
