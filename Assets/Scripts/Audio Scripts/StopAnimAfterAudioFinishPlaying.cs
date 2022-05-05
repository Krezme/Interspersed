using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StopAnimAfterAudioFinishPlaying : MonoBehaviour

{
    [Header(" ")]
    [Header("                                            ---===== Written by Rhys =====---")]

    [SerializeField]


    public AudioSource PlayingSource;

    public GameObject nextSource;

    private bool hasPlayed = false;


    // Update is called once per frame
    void Update()
    {
        if (PlayingSource.isPlaying)
        {
            hasPlayed = true;
        }

        if (!PlayingSource.isPlaying && hasPlayed == true)
        {
            try
            {
                nextSource.SetActive(true);
            }
            catch{}
            PlayingSource.gameObject.SetActive(false);
            CrystalArmTalkFlickerTrigger.instance.FlickerTriggerToggle(false);
        }
        else
        {
            CrystalArmTalkFlickerTrigger.instance.FlickerTriggerToggle(true);
        }
    }
}
