using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunTextPopUp : MonoBehaviour
{
    public string pupUpText = "Pop bubbles with your shotgun";
    public Text displayText;
    public GameObject toEnable;

    public GameObject SFXMaxStatisticsPickup;

    public void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            try {
                Instantiate(SFXMaxStatisticsPickup);
            } 
            catch (System.Exception) {}
            toEnable.SetActive(true);
            displayText.text = (pupUpText);
        }
    }
}
