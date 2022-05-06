using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class EnableIfWaveEventHasEnded : MonoBehaviour
{
    public GameObject gameObjectToEnable;
    public WaveEventManager waveEventManager;
    [Header("Fungus flowchart")]
    public Flowchart flowchart;
    public string blockName;

    private bool enabledOnce;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (waveEventManager.hasEnded && !enabledOnce) {
            gameObjectToEnable.SetActive(true);
            if (flowchart != null) {
                flowchart.ExecuteBlock(blockName);
            }
            enabledOnce = true;
        }
    }
}
