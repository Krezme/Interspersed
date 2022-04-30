using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBaguetteModeOnTriggerEnter : MonoBehaviour
{
    public int eventIndex;
    void OnTriggerEnter (Collider other) {
        if (SaveData.instance != null) {
            if (other.gameObject.tag == "Enemy") {
                SaveData.instance.SetEventToComplete(eventIndex);
            }
        }
    }
}
