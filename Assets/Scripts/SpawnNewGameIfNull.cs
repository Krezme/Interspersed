using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewGameIfNull : MonoBehaviour
{
    public GameObject gameObjectToSpawn;
    public GameObject spawnPos;

    

    public float distanceToSpawnNew;
    private GameObject spawnedGameObject;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnedGameObject == null || Vector3.Distance(spawnedGameObject.transform.position, this.gameObject.transform.position) >= distanceToSpawnNew) {
            spawnedGameObject = Instantiate(gameObjectToSpawn, spawnPos.transform.position, Quaternion.identity);
        }
    }
}
