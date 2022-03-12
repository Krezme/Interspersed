using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceFadeInVolume: MonoBehaviour

{
    [Header(" ")]
    [Header("                                     ---===== From Rhys' Collection =====---")]
    [Header("Target Volume")]
    [Header("set on the Fade In Time variable. It can also optionally disable its own game Object when reaching the ")]
    [Header("This script will fade in the AudioSource the volume set on the Target variable over the amount of time")]
    [SerializeField]
    private int m_FadeInTimeSeconds = 1;
    public AudioSource m_AudioSource;
    public float TargetAudioVolume = 1.0f;
    //public bool DisableOwnGameObjectAtTargetVolume = true;


    private void Start()
    {
        m_AudioSource.volume = 0.0f;
    }

    void OnEnable()
    {
        m_AudioSource.volume = 0.0f;
    }

    private void Update()
    {
        if (m_AudioSource.volume < TargetAudioVolume)
        {
            m_AudioSource.volume = m_AudioSource.volume + (Time.deltaTime / (m_FadeInTimeSeconds + 1));

        }

        if (m_AudioSource.volume >= TargetAudioVolume)
        {
            m_AudioSource.volume = TargetAudioVolume;
        }

    }
}