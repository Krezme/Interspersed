using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableIfWaveEventHasEnded : MonoBehaviour
{
    public GameObject gameObjectToEnable;
    public WaveEventManager waveEventManager;

    private bool enabledOnce;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (waveEventManager.hasEnded && !enabledOnce) {
            gameObjectToEnable.SetActive(true);
            enabledOnce = true;
        }
    }
}
