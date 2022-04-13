using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventAudio : MonoBehaviour
{
   public RandomAudioPlayer shoot;

   public RandomAudioPlayer reload;


   void ShootSound()
   {
       shoot.PlayRandomClip();
   }
   

    void ReloadSound()
    {
        reload.PlayRandomClip();
    }
}
