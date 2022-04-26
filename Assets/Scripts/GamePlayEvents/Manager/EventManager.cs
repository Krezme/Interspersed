using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    [Tooltip("This index is from the SaveData script referring to the currentEventState and savedEventsState")]
    public int eventDataIndex;
    
    [Header("Arm unlocking")]
    public bool doesUnlockArm;

    public Arms armToUnlock;
    

    [Header("Ability unlocking")]
    public bool doesUnlockAbility;

    public Arms arm;
    
    public int abilityIndex;

    public void TriggerMyAwake() {
        /* if () {

        } */
    }

    public void OnDisable() {

    }

}

public enum Arms {
    None,
    CrystalArm,
    SlimeArm
}