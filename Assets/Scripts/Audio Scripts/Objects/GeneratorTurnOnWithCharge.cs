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
    private InventoryController.InventoryChecker[] inventoryChecks; //Anything referring to this is essentially a redundant dependency from another script of mine, hence why I made it private - Currently cba to write it out since it works


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet" && other.GetComponent<BulletProjectile>().statistics.isElectric)
        {
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
    }

    protected virtual void ExecuteOnEnter(Collider other)
    {
        OnEnter.Invoke();
        for (var i = 0; i < inventoryChecks.Length; i++)
        {
            inventoryChecks[i].CheckInventory(other.GetComponentInChildren<InventoryController>());
        }
    }
}
