using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class EnableIfWaveEventHasEnded : MonoBehaviour
{
    public GameObject gameObjectToEnable;

    public GameObject gameObjectToEnable2;

    public GameObject gameObjectToDisable;

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

            try
            {
                gameObjectToEnable2.SetActive(true);
                gameObjectToDisable.SetActive(false);
            }
            catch{}





            if (flowchart != null) {
                flowchart.ExecuteBlock(blockName);
            }
            enabledOnce = true;
        }
    }
}
