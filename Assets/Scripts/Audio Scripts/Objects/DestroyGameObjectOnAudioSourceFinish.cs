using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectOnAudioSourceFinish : MonoBehaviour
{
  
  public GameObject ObjectToDestroy;

  public AudioSource SourceToWatch;

    void Update()
    {
        if (!SourceToWatch.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
