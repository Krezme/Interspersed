using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerChecker : MonoBehaviour
{
    public GameObject tower;
    public GameObject towerSpawnPoint;
    private Transform lookPlayer;
    private GameObject playerLook;
    public float lookAtPlayerSpeed;
    public LayerMask playerLayer;
    public Transform rayCastStartPosition;
    public GameObject player;
    public bool throwingEnabled;

    public void Start()
    {
        playerLook = GameObject.FindGameObjectWithTag("Player");
        throwingEnabled = true;
    }
    private void Update()
    {
         
         lookPlayer = playerLook.transform;

         FacePlayer();
         RaycastHit hit;
            if (Physics.Raycast(rayCastStartPosition.position, -transform.forward, out hit, playerLayer))
            {
              if (throwingEnabled == true)
              {
                 cooldowntimertowerthrow();
                  throwingEnabled = false;
            }                      
            }
            
        
    }

    void ShootAtPlayer()
    {
        
        Transform _tower = Instantiate(tower.transform, towerSpawnPoint.transform.position, Quaternion.identity);
        _tower.transform.rotation = towerSpawnPoint.transform.rotation;       

    }


    IEnumerator ShootingAtPlayercooldown()
    {
        yield return new WaitForSeconds(2f);
        ShootAtPlayer();
    }

    IEnumerator DestroyGigachu()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

     void cooldowntimertowerthrow()
    {
        
        StartCoroutine(ShootingAtPlayercooldown());
        StartCoroutine(DestroyGigachu());
        

    }


    void FacePlayer()
    {
        Vector3 direction = (lookPlayer.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookAtPlayerSpeed);

    }

}
