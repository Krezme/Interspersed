using System.Collections;
using System.Collections.Generic;
using UnityEngine;




    [RequireComponent(typeof(AudioSource))]

    public class PlayRandomSoundOnObjectTrigger : MonoBehaviour
    {
        [Header(" ")]
        [Header("                                            ---===== Written by Rhys =====---")]

        public RandomAudioPlayer player;
        //Creates inspector window slot in which the GameObject that contains the desired to be played RandomAudioPlayer Sctipt must be placed (In this case it should be tbe object that this script is also placed on)

        void OnTriggerEnter() //This script was written to apply the same functionality of object collions to the crystal bullets as I noticed the bullet collider was set to a trigger
        {
                player.PlayRandomClip();
                //Initiates the RandomAudioPlayer script within the assigned GameObject via the inspector window slot to play a random sound from its default bank
        }
    }
    //Next is to implement surface detection using the same method as used for the footsteps
