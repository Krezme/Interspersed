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

    public Animator animator;

    public GameObject BellLightBulbA;

    public GameObject BellLightBulbB;

    private int Digit;

    public BoxCollider Collider;




    private LayerMask layers;
    public UnityEvent OnEnter;
    private InventoryController.InventoryChecker[] inventoryChecks;






    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet" && other.GetComponent<BulletProjectile>().statistics.isElectric)
        {
            Digit = Random.Range(0, 101);

            if (Digit < 10)
            {
                EngineStartUpLayer.SetActive(true); /*- NiceTry at hiding*/
                animator.enabled = true;
                BellLightBulbA.SetActive(true);
                BellLightBulbB.SetActive(true);
                Collider.enabled = false;
                Debug.Log("Generator Charged");
                //---------------------------------------------------------------------------------

                ExecuteOnEnter(other);

            }
            else
            {
                EngineStartUp.SetActive(true);
                animator.enabled = true;
                BellLightBulbA.SetActive(true);
                BellLightBulbB.SetActive(true);
                Collider.enabled = false;
                Debug.Log("Generator Charged");
                //---------------------------------------------------------------------------------

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
