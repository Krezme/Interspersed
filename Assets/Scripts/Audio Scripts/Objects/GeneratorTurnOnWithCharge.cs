using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTurnOnWithCharge : MonoBehaviour
{
    [Header(" ")]
    [Header("                                            ---===== Written by Rhys =====---")]


    public GameObject EngineStartUp;

    public Animator animator;

    public GameObject BellLightBulbs;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet" && other.GetComponent<BulletProjectile>().statistics.isElectric)
        {
            EngineStartUp.SetActive(true);
            animator.enabled = true;
            BellLightBulbs.SetActive(true);
            Debug.Log("Generator Charged");
        }
    }
}
