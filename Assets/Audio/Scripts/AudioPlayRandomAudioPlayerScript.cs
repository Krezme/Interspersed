using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D
{ 
public class AudioPlayRandomAudioPlayerScript : MonoBehaviour
    {

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
}
