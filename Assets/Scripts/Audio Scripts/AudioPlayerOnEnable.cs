using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class AudioPlayerOnEnable : MonoBehaviour
    {
        [Header(" ")]
        [Header("                                     ---===== From Rhys' Collection =====---")]

        public RandomAudioPlayer player;
        public bool stopOnDisable = false;

        void OnEnable()
        {
            player.PlayRandomClip();
        }

        private void OnDisable()
        {
            if (stopOnDisable)
                player.audioSource.Stop();
        }
    } 