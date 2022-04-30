using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBaguetteModeOnTriggerEnter : MonoBehaviour
{
    public int eventIndex;
    public AudioSource baguetteMode;
    void OnTriggerEnter (Collider other) {
        if (SaveData.instance != null) {
            if (other.gameObject.tag == "Enemy") {
                try {
                    baguetteMode.Play();
                }catch (System.Exception) {}
                SaveData.instance.SetEventToComplete(eventIndex);
            }
        }
    }
}
