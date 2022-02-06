using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTurnOnWithCharge : MonoBehaviour
{

    public RandomAudioPlayer RadioShows;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet" && other.GetComponent<BulletProjectile>().statistics.isElectric)
        {
            RadioShows.PlayRandomClip();
            Debug.Log("RadioHit");
        }
    }
}