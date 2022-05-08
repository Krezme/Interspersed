using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnDestroy : MonoBehaviour
{
    public GameObject SFXSound;

    public void OnDestroy() {
        try {
            Instantiate(SFXSound, transform.position, Quaternion.identity);
        } 
        catch (System.Exception) {}
    }
}
