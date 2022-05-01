using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerSnapshotMenuReset : MonoBehaviour
{
    public AudioMixerSnapshot unpausedSnapshot;

    private float transitionTime = 0.3f;

    void Start()
    {
        unpausedSnapshot.TransitionTo(transitionTime);
    }
}
