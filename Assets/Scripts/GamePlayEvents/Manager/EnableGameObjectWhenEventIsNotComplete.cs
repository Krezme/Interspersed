using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjectWhenEventIsNotComplete : MonoBehaviour
{

    public int eventIndex;

    public GameObject[] enableGameObjects;

    // Start is called before the first frame update
    void Start()
    {
        TryEnablingGameObjects();
    }

    // Update is called once per frame
    void TryEnablingGameObjects()
    {
        if (SaveData.savedEventsState != null) {
            if (!SaveData.savedEventsState[eventIndex].eventComplete){
                EnablingGameObjects();
            }
        }else { 
            EnablingGameObjects();
        }
    }

    void EnablingGameObjects () {
        foreach (GameObject gO in enableGameObjects) {
            gO.SetActive(true);
        }
    }

}
