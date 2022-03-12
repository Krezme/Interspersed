using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTurnOnWithCharge : MonoBehaviour
{
    [Header(" ")]
    [Header("                                            ---===== Written by Rhys =====---")]


    public GameObject EngineStartUp;

    public GameObject EngineStartUpLayer;

    public Animator animator;

    public GameObject BellLightBulbs;

    public int Digit;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet" && other.GetComponent<BulletProjectile>().statistics.isElectric)
        {
            Digit = Random.Range(0, 101);
   
            if (Digit <10)
            {
                EngineStartUpLayer.SetActive(true);
                animator.enabled = true;
                BellLightBulbs.SetActive(true);
                Debug.Log("Generator Charged");
            }
            else
            {
                EngineStartUp.SetActive(true);
                animator.enabled = true;
                BellLightBulbs.SetActive(true);
                Debug.Log("Generator Charged");
            }
        }
    }
}
