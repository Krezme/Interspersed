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
    

    public void Start()
    {
        playerLook = GameObject.FindGameObjectWithTag("Player");
       
    }
    private void Update()
    {
         
         lookPlayer = playerLook.transform;

         FacePlayer();
         RaycastHit hit;
            if (Physics.Raycast(rayCastStartPosition.position, -transform.forward, out hit, playerLayer))
            {

            cooldowntimertowerthrow();
                                    
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
        
    }

     void cooldowntimertowerthrow()
    {
        
        StartCoroutine(ShootingAtPlayercooldown());
        ShootAtPlayer();
        StartCoroutine(ShootingAtPlayercooldown());
        Destroy(this.gameObject);

    }


    void FacePlayer()
    {
        Vector3 direction = (lookPlayer.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookAtPlayerSpeed);

    }

}
