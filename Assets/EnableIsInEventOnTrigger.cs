using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableIsInEventOnTrigger : MonoBehaviour
{
    void OnTriggerEnter (Collider other) {
        if (other.tag == "Player") {
            if (SaveData.instance != null) {
                SaveData.instance.inTheMiddleOfAnEvent = true;
            }
        }
    }
}
