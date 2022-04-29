using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectOnAudioSourceFinish : MonoBehaviour
{
  
  public GameObject ObjectToDestroy;

  public AudioSource SourceToWatch;

  private bool HasPlayed = false;

    void Update()
    {
        if (SourceToWatch.isPlaying)
        {
            HasPlayed = true;
        }


        if (!SourceToWatch.isPlaying && HasPlayed)
        {
            Destroy(this.gameObject);
        }
    }
}
