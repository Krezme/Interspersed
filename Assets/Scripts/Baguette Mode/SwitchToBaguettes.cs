using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToBaguettes : MonoBehaviour
{
    public int eventIndex;

    public GameObject[] gameObjectsToEnable;

    public GameObject[] gameObjectsToDissable;

    private bool baguetteModeEnabled;

    void FixedUpdate()
    {
        if (!baguetteModeEnabled) {
            if (SaveData.instance != null) {
                if (SaveData.instance.currentEventsState[eventIndex].eventComplete) {
                    if (gameObjectsToEnable.Length > 0) {
                        ToggleGameObjects(gameObjectsToEnable, true);
                    }
                    if (gameObjectsToDissable.Length > 0) {
                        ToggleGameObjects(gameObjectsToDissable, false);
                    }
                    baguetteModeEnabled = true;
                }
            }
        }
    }

    void ToggleGameObjects (GameObject[] gameObjectsArray, bool state) {
        foreach(GameObject gO in gameObjectsArray) {
            gO.SetActive(state);
        }
    }
}
