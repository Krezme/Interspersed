using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewGameIfNull : MonoBehaviour
{
    public GameObject gameObjectToSpawn;
    public GameObject spawnPos;
    private GameObject spawnedGameObject;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnedGameObject == null) {
            spawnedGameObject = Instantiate(gameObjectToSpawn, spawnPos.transform.position, Quaternion.identity);
        }
    }
}
