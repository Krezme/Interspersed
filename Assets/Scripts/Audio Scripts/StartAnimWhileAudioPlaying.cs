using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StartAnimWhileAudioPlaying : MonoBehaviour

{
    [Header(" ")]
    [Header("                                            ---===== Written by Rhys =====---")]

    [SerializeField]


    public AudioSource PlayingSource;

    void Update()
    {
        if (!PlayingSource.isPlaying)
        {
            CrystalArmTalkFlickerTrigger.instance.FlickerTriggerToggle(false);
        }
    }
}
