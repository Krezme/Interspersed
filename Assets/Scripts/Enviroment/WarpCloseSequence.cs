using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpCloseSequence : MonoBehaviour
{
    public float closeFadeOutTime = 2f;

    private bool needsToFadeOut = false;

    private float fadeOutPassedTime;

    public AudioSource sfxTeleport;

    public void ToggleFadeOut( bool state) {
        needsToFadeOut = state;
        sfxTeleport.Play();
    }

    void Update () {
        if (needsToFadeOut) {
            fadeOutPassedTime += Time.deltaTime;
            // ! THIS IS WHERE THE FADE OUT OF THE PORTAL SHADER NEEDS TO BE CHANGED
            if (!sfxTeleport.isPlaying) {
                needsToFadeOut = false;
                fadeOutPassedTime = 0;
                Destroy(this.gameObject);
            }
        }
    }
}
