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

    private int currentWave;

    private bool wavesHaveBegan = false;

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
                if (waves[currentWave].rifts.Count == 0) {
                    if (enemies.Count == 0) {
                        
                        if (waves[currentWave].needsToDestroyGOAfterEachWave) {
                            foreach (GameObject gO in waves[currentWave].objectsToDestroy) {
                                Destroy(gO);
                            }
                            currentWave++;
                        }else { 
                            currentWave++;
                            ActivateRifts();
                        }
                    }
                }
            }
            catch (System.Exception) {}
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
