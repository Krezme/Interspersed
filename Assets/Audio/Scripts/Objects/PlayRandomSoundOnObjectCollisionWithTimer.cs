using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D //The RandomAudioPlayer scipt was originally copied from Unity's 3DGameKit 'The Explorer' and to my knowledge, requires this namespace to function
{

    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(RandomAudioPlayer))]

    public class PlayRandomSoundOnObjectCollisionWithTimer : MonoBehaviour
    {
        public RandomAudioPlayer player;
        public float RequiredVelocity = 3;
        private float CollisionTimer = 0;
        private int Collisions = 0;

        //Creates inspector window slot in which the GameObject that contains the desired to be played RandomAudioPlayer Sctipt must be placed (In this case it should be tbe object that this script is also placed on)


        void Update()
        {
            CollisionTimer += Time.deltaTime;

            if (CollisionTimer > 1)
            {
                CollisionTimer = 0;
                Collisions = 0;
                Debug.Log("Ragdoll Collisions & Timer reset");
            }
        }




        void OnCollisionEnter(Collision collision)
        {

            

            if (collision.relativeVelocity.magnitude > RequiredVelocity)
            {
                //CollisionTimer += Time.deltaTime;
                Collisions ++;
                Debug.Log("Collisions" + Collisions);
              
                if (Collisions > 2)
                {

                    Debug.Log("Ragdoll sound avoided");
                }
                else
                {
                    player.PlayRandomClip();
                    Debug.Log("Ragdoll sound played");
                    
                }
                //player.PlayRandomClip();
                //Initiates the RandomAudioPlayer script within the assigned GameObject via the inspector window slot to play a random sound from its default bank
            }
        }
    }
}