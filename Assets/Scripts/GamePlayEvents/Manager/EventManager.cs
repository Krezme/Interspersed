using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    [Tooltip("This index is from the SaveData script referring to the currentEventState and savedEventsState")]
    public int eventDataIndex;

    public List<GameObject> eventGameObjects;
    
    [Header("Arm unlocking")]
    public bool doesUnlockArm;

    public Arms armToUnlock;
    

    [Header("Ability unlocking")]
    public bool doesUnlockAbility;

    public Arms arm;
    
    public int abilityIndex;

    public void TriggerMyAwake() {
        if (SaveData.savedEventsState[eventDataIndex].eventComplete) {
            DissableEvent();
            AquireArm();
            AquireAbility();
        }
    }

    public void DissableEvent() {
        foreach (GameObject go in eventGameObjects) {
            go.SetActive(false);
        }
    }

    public void AquireArm() {
        if (doesUnlockArm) {
            if (armToUnlock == Arms.CrystalArm) {
                PlayerAbilitiesController.instance.EnableAbility(0);
            }
            else if (armToUnlock == Arms.SlimeArm) {
                PlayerAbilitiesController.instance.EnableAbility(1);
            }
        }
    }

    public void AquireAbility() {
        if (doesUnlockAbility) {
            if (arm == Arms.CrystalArm) {
                CristalArm.instance.EnableAbility(abilityIndex);
            }else if (arm == Arms.SlimeArm) {
                SlimeArm.insance.EnableAbility(abilityIndex);
            }
        }
    }

}

public enum Arms {
    None,
    CrystalArm,
    SlimeArm
}