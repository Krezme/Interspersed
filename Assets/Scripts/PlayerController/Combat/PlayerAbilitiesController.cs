using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentAbilityStats {

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

    [HideInInspector]
    public bool isAbilityActive;

    public void ChangeArm() {
        abilities[selectedAbility].MorthToTarget();
    }

    // Update is called once per frame
    void Update()
    {
        abilities[selectedAbility].Ability();
    }
}