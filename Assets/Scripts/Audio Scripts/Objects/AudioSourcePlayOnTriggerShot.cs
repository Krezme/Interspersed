using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePlayOnTriggerShot : MonoBehaviour
{
    [Header(" ")]
    [Header("                                            ---===== Written by Rhys =====---")]

    public AudioSource AudioSource;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            AudioSource.Play();
        }
    }
}