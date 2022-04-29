using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningOver : MonoBehaviour
{
    public SpawnOverTime[] spawnOverTime;

    private int successfulScriptCompletions;

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < spawnOverTime.Length; i++) {
            if (spawnOverTime[i].currentSpawnEnemyIndex >= spawnOverTime[i].enemiesToSpawn.Length) {
                successfulScriptCompletions++;
            }
        }
        if (successfulScriptCompletions == spawnOverTime.Length) {
            Destroy(this.gameObject);
        }else {
            successfulScriptCompletions = 0;
        }
    }
}
