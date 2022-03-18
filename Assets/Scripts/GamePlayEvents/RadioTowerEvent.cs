using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTowerEvent : MonoBehaviour
{

    public GameObject warplingToSpawn;
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject spawnPoint4;
    public GameObject spawnPoint5;

    public BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        SpawnWarplings();
        //Destroy(this.gameObject);
        boxCollider.enabled = false;
    }

    void SpawnWarplings()
    {

        Instantiate(warplingToSpawn.transform, spawnPoint1.transform.position,Quaternion.identity);
        Instantiate(warplingToSpawn.transform, spawnPoint2.transform.position, Quaternion.identity);
        Instantiate(warplingToSpawn.transform, spawnPoint3.transform.position, Quaternion.identity);
        Instantiate(warplingToSpawn.transform, spawnPoint4.transform.position, Quaternion.identity);
        Instantiate(warplingToSpawn.transform, spawnPoint5.transform.position, Quaternion.identity);
    }

}
