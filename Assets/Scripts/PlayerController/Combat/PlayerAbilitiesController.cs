using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAbilitiesReferences {
    [HideInInspector]
    public string name;
    public PlayerAbility playerAbility;
    public bool isActive;
 
    public void Validate()
    {
        name = (playerAbility) ? playerAbility + " (Active: " + isActive + ")" : "No PlayerAbility";
    }
} 

public class PlayerAbilitiesController : MonoBehaviour
{
public Light CrystalGlow;

public Light SlimeGlow;

public Light ElectricGlow;

#region Singleton

    /// <summary>
    /// This is a singleton to allow this script to be accessed by all other scripts
    /// </summary>
    [HideInInspector]
    public static PlayerAbilitiesController instance;

    void Awake () {
        if (instance == null) {
            instance = this;
        }else {
            Debug.LogError("THERE ARE 2 PlayerAbilitiesController SCRIPTS IN EXISTANCE");
        }
    }

#endregion


    public int selectedAbility;

    // ? Old list used for referring to the abilities
    //public List<PlayerAbility> abilities;

    public PlayerAbilitiesReferences[] playerAbilitiesRef;

    [Header("Aiming")]
    public LayerMask aimColliderLayerMask = new LayerMask();
    public Transform rayBitch;

    [HideInInspector]
    public bool isAbilityActive;

    private bool hasChanged; // when the abilit changes

    public void ChangeArm() {
        for (int i = 0; i < playerAbilitiesRef.Length; i++) {
            playerAbilitiesRef[i].playerAbility.MorthToTarget(); // ! This is PlaceHolder FUNCTIONALITY NEEDS TO BE CHANGED ONLY FOR MID-TERM-REVIEW
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ? this is the previouse way for changing arms
        //abilities[selectedAbility].Ability(); // Uses the ability depending on selected arm

        // ? New way of chaning arms
        playerAbilitiesRef[selectedAbility].playerAbility.Ability();

        if (hasChanged) {
            for (int i = 0; i < playerAbilitiesRef.Length; i++) {
                if (i != selectedAbility) {
                    playerAbilitiesRef[i].playerAbility.RestartAbility();
                }
            }
            hasChanged = false;
        }

        if (selectedAbility == 0)
        {
            CrystalGlow.GetComponent<Light>().enabled = true;
            SlimeGlow.GetComponent<Light>().enabled = false;
            ElectricGlow.GetComponent<Light>().enabled = false;
        }
        else
        {
            CrystalGlow.GetComponent<Light>().enabled = false;
            SlimeGlow.GetComponent<Light>().enabled = true;
            ElectricGlow.GetComponent<Light>().enabled = false;
        }
    }

    public void SelectAbility(int newSelectedAbility) {
        selectedAbility = newSelectedAbility;
        hasChanged = true;
    }

    public void EnableAbility(int index) {
        playerAbilitiesRef[index].isActive = true;
    }

    public void DisableAbility(int index) {
        playerAbilitiesRef[index].isActive = false;
    }

    void OnValidate () {
        for (int i = 0; i < playerAbilitiesRef.Length; i++) {
            playerAbilitiesRef[i].Validate();
        }
    }
}