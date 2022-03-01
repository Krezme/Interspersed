using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlayerPlayRandomClipOnEnable : MonoBehaviour
{

    public RandomAudioPlayer SelectedPlayer; 

    void Start()
    {
        SelectedPlayer.PlayRandomClip();
    }
}
