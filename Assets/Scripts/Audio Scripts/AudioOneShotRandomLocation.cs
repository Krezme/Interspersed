using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[RequireComponent(typeof(AudioSource))]
public class AudioOneShotRandomLocation : MonoBehaviour
{
    [Header(" PLEASE ENSURE PLAY ON AWAKE AND LOOP ARE NOT ENABLED!! ")]

    [Tooltip("The MINIMUM amount of TIME TO WAIT before playback")]
    [Range(0, 60)] public float minTimeToWait = 5.0f;
    [Tooltip("The MAXIMUM amount of TIME TO WAIT before playback")]
    [Range(0, 60)] public float maxTimeToWait = 10.0f;

    [Tooltip("The MINIMUM Random Volume to playback")]
    [Range(0, 1)] public float minVolume = 1.0f;
    [Tooltip("The MAXIMUM Random Volume to playback")]
    [Range(0, 1)] public float maxVolume = 1.0f;

    [Tooltip("The MINIMUM Random Pitch to playback")]
    [Range(-3, 3)] public float minPitch = 1.0f;
    [Tooltip("The MAXIMUM Random Pitch to playback.")]
    [Range(-3, 3)] public float maxPitch = 1.0f;

    float timer = 0.0f;
    float nextInterval = 1.0f;
    AudioSource audioSource = null;

    [Tooltip("Assign the AudioClips to this Bank")]
    public AudioClip[] AudioClips;
    [Tooltip("Assign the Empty GameObjects to this Bank")]
    public Transform[] AltTransforms;


    void Start()
    {
        nextInterval = Random.Range(minTimeToWait, maxTimeToWait);
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        if (audioSource == null)
            Debug.Log("AUDIO WARNING - You have not added an AudioSource to this OneShotAudioRandomLocation script!", this);

        if (AudioClips.Length == 0)
            Debug.Log("AUDIO WARNING - You have not assigned any AudioClip/s to this OneShotAudioRandomLocation!", this);
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextInterval && !audioSource.isPlaying)
        {
            audioSource.clip = AudioClips[Random.Range(0, AudioClips.Length - 1)];
            audioSource.volume = Random.Range(minVolume, maxVolume);

            if (AltTransforms.Length >= 1)
            {
                int m_NewPosition = Random.Range(0, AltTransforms.Length - 1);
                if (AltTransforms[m_NewPosition] != null)
                {
                    transform.position = AltTransforms[m_NewPosition].transform.position;
                }
            }

            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.Play();
            nextInterval = Random.Range(minTimeToWait, maxTimeToWait);
            timer = 0.0f;
        }
    }
}

