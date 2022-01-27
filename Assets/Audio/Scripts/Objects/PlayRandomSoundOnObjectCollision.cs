using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D //The RandomAudioPlayer scipt was originally copied from Unity's 3DGameKit 'The Explorer' and to my knowledge, requires this namespace to function
{

    [RequireComponent(typeof(AudioSource))]

    public class PlayRandomSoundOnObjectCollision : MonoBehaviour
    {
        public RandomAudioPlayer player;
        public float RequiredVelocity = 3;
        //Creates inspector window slot in which the GameObject that contains the desired to be played RandomAudioPlayer Sctipt must be placed (In this case it should be tbe object that this script is also placed on)

        void OnCollisionEnter(Collision collision)
        {
            if (collision.relativeVelocity.magnitude > RequiredVelocity)
            {
                player.PlayRandomClip();
                //Initiates the RandomAudioPlayer script within the assigned GameObject via the inspector window slot to play a random sound from its default bank
            }
        }
    }
}