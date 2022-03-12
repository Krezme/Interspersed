using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlayerPlayRandomClipOnEnable : MonoBehaviour
{
    [Header(" ")]
    [Header("                                            ---===== Written by Rhys =====---")]

    public RandomAudioPlayer SelectedPlayer; 

    void Start()
    {
        SelectedPlayer.PlayRandomClip();
    }
}
