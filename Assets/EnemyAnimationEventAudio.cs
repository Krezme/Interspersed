using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventAudio : MonoBehaviour
{
   public RandomAudioPlayer shoot;

   public RandomAudioPlayer reload;

   public RandomAudioPlayerV2 soundBank;


   void ShootSound()
   {
       try
       {
       shoot.PlayRandomClip();
       }
       catch{}
   }
   
    void ReloadSound()
    {
        try
        {
        reload.PlayRandomClip();
        }
        catch{}
    }

    void SpitSound()
    {
        try
        {
        soundBank.PlayRandomClip(defaultBankIndex: 0);
        }
        catch{}
    }

    void DamageSound()
    {
        try
        {
        soundBank.PlayRandomClip(defaultBankIndex: 1);
        }
        catch{}
    }

    void DeathSound()
    {
        try
        {
        soundBank.PlayRandomClip(defaultBankIndex: 2); 
        }
        catch{}
    }
}
