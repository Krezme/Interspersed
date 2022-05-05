using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {
    public List<GameObject> rifts;

    public bool needsToDestroyGOAfterEachWave;
    public GameObject[] objectsToDestroy;
}

public class WaveEventManager : MonoBehaviour
{
    public Wave[] waves;
    
    public List<GameObject> enemies;

    public List<GameObject> enemiesCurrentWave;

    private int currentWave;

    private bool wavesHaveBegan = false;

    [HideInInspector]
    public bool hasEnded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wavesHaveBegan) {
            CheckIfRiftsAreOpen();
            CheckIfEnemiesAreAlive();
            try {
                if (waves[currentWave].rifts.Count <= 0) {
                    if (enemies.Count <= 0) {
                        if (currentWave >= waves.Length -1) {
                            hasEnded = true;
                            Debug.Log("HAS ENDED");
                        }
                        else{        
                            if (waves[currentWave].needsToDestroyGOAfterEachWave) {
                                foreach (GameObject gO in waves[currentWave].objectsToDestroy) {
                                    Destroy(gO);
                                }
                                currentWave++;
                            }else { 
                                for (int i = 0; i < enemiesCurrentWave.Count; i++) {
                                    GameObject temp = enemiesCurrentWave[i];
                                    enemiesCurrentWave.Remove(enemiesCurrentWave[i]);
                                    Destroy(temp);
                                }
                                currentWave++;
                                ActivateRifts();
                            }
                        } 
                    }
                }
            }
            catch (System.Exception) {}
            for (int i = 0; i < enemiesCurrentWave.Count; i++) {
                if (enemiesCurrentWave[i] == null) {
                    enemiesCurrentWave.Remove(enemiesCurrentWave[i]);
                }
            }
        }
        
    }

    public void WaveStart () {
        wavesHaveBegan = true;
        ActivateRifts();
    }

    void ActivateRifts () {
        foreach (GameObject gO in waves[currentWave].rifts) {
            gO.SetActive(true);
        }   
    }

    void CheckIfRiftsAreOpen () {
        for (int i = 0; i < waves.Length; i++) {
            for (int j = 0; j < waves[i].rifts.Count; j++) {
                if (waves[i].rifts[j] == null) {
                    waves[i].rifts.Remove(waves[i].rifts[j]);
                }
            }
        }
    }

    void CheckIfEnemiesAreAlive () {
        for (int i = 0; i < enemies.Count; i++) {
            try{
                if (enemies[i].GetComponent<EnemyStatisticsManager>() == null || enemies[i] == null) {
                    enemies.Remove(enemies[i]);
                }
            }
            catch (System.Exception) {
                enemies.Remove(enemies[i]);
            }
        }
    }
}
