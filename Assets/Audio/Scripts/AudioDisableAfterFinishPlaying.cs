using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioDisableAfterFinishPlaying : MonoBehaviour
    
{
    [Header("When the audio clip has finished playing")]
    [Header("This script will DISABLE the GameObject")]
    [SerializeField]


    public AudioSource DisableAudiosourceAfterPlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if (!DisableAudiosourceAfterPlay.isPlaying)
            DisableAudiosourceAfterPlay.gameObject.SetActive(false);

    }
}
