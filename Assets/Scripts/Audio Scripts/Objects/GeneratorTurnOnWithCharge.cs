using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneratorTurnOnWithCharge : MonoBehaviour
{
    [Header(" ")]
    [Header("                                            ---===== Written by Rhys =====---")]

    public GameObject EngineStartUp;

    public GameObject EngineStartUpLayer;

    public Animator LightSpin;

    public GameObject BellLightBulbA;

    public GameObject BellLightBulbB;

    private int Digit;

    public BoxCollider Collider;

    public Animator Shake;

    public GameObject GreasedLightning;


    private LayerMask layers;
    public UnityEvent OnEnter;
    

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet" && other.GetComponent<BulletProjectile>().statistics.isElectric)
        {
            EnableGenerator(other);
        }
    }

    public void EnableGenerator(Collider other) {
        Digit = Random.Range(0, 101);

        if (Digit <= 3)
        {
            EngineStartUpLayer.SetActive(true); /*- NiceTry at hiding*/
            LightSpin.enabled = true;
            Shake.enabled = true;
            GreasedLightning.SetActive(true);
            BellLightBulbA.SetActive(true);
            BellLightBulbB.SetActive(true);
            Collider.enabled = false;
            Debug.Log("Generator Charged");
            
            ExecuteOnEnter(other);
        }
        else
        {
            EngineStartUp.SetActive(true);
            LightSpin.enabled = true;
            Shake.enabled = true;
            GreasedLightning.SetActive(true);
            BellLightBulbA.SetActive(true);
            BellLightBulbB.SetActive(true);
            Collider.enabled = false;
            Debug.Log("Generator Charged");      

            ExecuteOnEnter(other);
        }
    }

    protected virtual void ExecuteOnEnter(Collider other)
    {
        OnEnter.Invoke();
    }
}