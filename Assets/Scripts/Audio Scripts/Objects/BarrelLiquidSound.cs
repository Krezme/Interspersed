using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelLiquidSound : MonoBehaviour
{
    public RandomAudioPlayer LiquidSounds;

    public GameObject MovingObject;

    private bool CanPlay = false;


    void Update()
    {
        if (MovingObject.GetComponent<Rigidbody>().velocity.magnitude > 1f)
        {
            CanPlay = true;
        }
        
        
        if (MovingObject.GetComponent<Rigidbody>().velocity.magnitude < 0.5f)
        {
            if (CanPlay == true)
            {
            LiquidSounds.PlayRandomClip();
            Debug.Log("Stopped");
            CanPlay = false;
            }   
        }
    }
}
