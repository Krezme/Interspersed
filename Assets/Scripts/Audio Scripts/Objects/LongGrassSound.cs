using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongGrassSound : MonoBehaviour
{
    
    private float VolumeDigit;

    public AudioSource m_AudioSource;

    private float AudioVolume;

    public BoxCollider m_BoxCollider;

    //private float TransitionSpeed = 0.5f;
    private float PlayerSpeed = 0f;




    private float m_FadeOutTimeSeconds = 1f;

    private float TargetAudioVolume = 0.0f;






    void Start()
    {
        PlayerSpeed = GameObject.FindWithTag("Player").GetComponent<ThirdPersonPlayerController>().speed;
        //m_AudioSource = this.gameObject.GetComponent<AudioSource>();
        //m_BoxCollider = this.gameObject.GetComponent<BoxCollider>();
        //AudioVolume = GetComponent<AudioSource>().volume;
    }

     /* void Update()
    {
        AudioVolume = AudioSource.volume;
    }  */


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_AudioSource.volume = GameObject.FindWithTag("Player").GetComponent<ThirdPersonPlayerController>().speed / 50;
        }
        else
        {
            m_AudioSource.volume = m_AudioSource.volume - (Time.deltaTime / (m_FadeOutTimeSeconds + 1));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
             m_AudioSource.volume = m_AudioSource.volume - (Time.deltaTime / (m_FadeOutTimeSeconds + 1));
        }
    }

}