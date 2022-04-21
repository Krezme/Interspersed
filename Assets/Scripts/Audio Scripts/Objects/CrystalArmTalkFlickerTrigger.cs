using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalArmTalkFlickerTrigger : MonoBehaviour
{
    #region singleton
    public static CrystalArmTalkFlickerTrigger instance;
    void Awake() 
    {
    if (instance != null) 
    {
        Debug.LogError("There are two instances of CrystalArmTalkFlickerTrigger! Please leave only one instance");
    }
    else 
    {
    instance = this;
    }
    }
    #endregion
    
    public Animator armAnim;

    public void FlickerTriggerToggle(bool state)
    {
        armAnim.SetBool("isTalking", state);
    }
}
