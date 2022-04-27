using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableIfEventIsCompleted : MonoBehaviour
{
    public GameObject gameObjectToEnable;
    public int eventIndex;

    private bool hasBeenActivated = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (SaveData.instance.currentEventsState[eventIndex].eventComplete && !hasBeenActivated) {
            gameObjectToEnable.SetActive(true);
            hasBeenActivated = true;
        }
        
    }
}
