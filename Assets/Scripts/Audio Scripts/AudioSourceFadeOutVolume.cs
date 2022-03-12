using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceFadeOutVolume : MonoBehaviour

{
    [Header(" ")]
    [Header("                                     ---===== From Rhys' Collection =====---")]
    [Header("It can also optionally disable game Object when reaching the Target Volume ")]
    [Header("Target variable over the amount of time set on the Fade Out Time variable ")]
    [Header("This script will fade out the AudioSource the volume set on the")]
    [SerializeField]
    private int m_FadeOutTimeSeconds = 1;
    public AudioSource m_AudioSource;
    public float TargetAudioVolume = 0.0f;
    public bool DisableOwnGameObjectAtTargetVolume = false;

    private void Update()
    {
        if (m_AudioSource.volume > TargetAudioVolume)
        {
            m_AudioSource.volume = m_AudioSource.volume - (Time.deltaTime / (m_FadeOutTimeSeconds + 1));

        }

        if (m_AudioSource.volume <= TargetAudioVolume)
        {
            m_AudioSource.volume = TargetAudioVolume;
        }

        if (m_AudioSource.volume == TargetAudioVolume)

        {
            if (DisableOwnGameObjectAtTargetVolume == true)
                gameObject.SetActive(false);
        }

        else
        {
        }
    }
    
}