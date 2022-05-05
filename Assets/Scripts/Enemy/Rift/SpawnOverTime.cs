using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemiesToSpawn {
    public GameObject enemy;
    public int numberOfEnemies;
}

public class SpawnOverTime : MonoBehaviour
{
    public GameObject spawnPosition;
    public EnemiesToSpawn[] enemiesToSpawn;
    public float delayBetweenSpawns;
    public WaveEventManager waveEventManager;

    [HideInInspector]
    public int currentSpawnEnemyIndex;
    private int spawnedEnemyNumber;

    private float timePassed;

    void Update() {
        timePassed += Time.deltaTime;
        if (timePassed >= delayBetweenSpawns) {
            if (currentSpawnEnemyIndex < enemiesToSpawn.Length && spawnedEnemyNumber < enemiesToSpawn[currentSpawnEnemyIndex].numberOfEnemies) {
                GameObject tempGO = Instantiate(enemiesToSpawn[currentSpawnEnemyIndex].enemy, spawnPosition.transform.position, Quaternion.identity);
                if (waveEventManager != null) {
                    waveEventManager.enemies.Add(tempGO);
                    waveEventManager.enemiesCurrentWave.Add(tempGO);
                }
                spawnedEnemyNumber++;
                if (spawnedEnemyNumber == enemiesToSpawn[currentSpawnEnemyIndex].numberOfEnemies) {
                    currentSpawnEnemyIndex++;
                    spawnedEnemyNumber = 0;
                }
            }
            timePassed -= delayBetweenSpawns;
        }
    }
}
