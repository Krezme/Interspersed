using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeleeStats {
    public float damage;
}

public class PlayerAbilitiesController : MonoBehaviour
{

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
    public List<PlayerAbility> abilities;

    [Header("Aiming")]
    public LayerMask aimColliderLayerMask = new LayerMask();
    public Transform rayBitch;

    public MeleeStats meleeStats;

    [HideInInspector]
    public bool isAbilityActive;

    public void ChangeArm() {
        for (int i = 0; i < abilities.Count; i++) {
            abilities[i].MorthToTarget(); // This is PlaceHolder FUNCTIONALITY NEEDS TO BE CHANGED ONLY FOR MID-TERM-REVIEW
        }
    }

    // Update is called once per frame
    void Update()
    {
        abilities[selectedAbility].Ability(); // Uses the ability depending on selected arm
    }
}