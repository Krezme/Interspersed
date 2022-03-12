using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 
public class AudioPlayRandomAudioPlayerScript : MonoBehaviour
    {
        [Header(" ")]
        [Header("                                     ---===== From Rhys' Collection =====---")]

        public RandomAudioPlayer RandomAudioPlayerGameObject;

        void OnEnable()
        {
            if (RandomAudioPlayerGameObject != null)
                RandomAudioPlayerGameObject = RandomAudioPlayerGameObject.GetComponent<RandomAudioPlayer>();

                RandomAudioPlayerGameObject.PlayRandomClip();
        }

        void Reset()
        {
            if (RandomAudioPlayerGameObject != null)
                RandomAudioPlayerGameObject = RandomAudioPlayerGameObject.GetComponent<RandomAudioPlayer>();
        }

    }

