using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

#region Singleton
    public static EnemyManager instance;
    void Awake () {
        if (instance != null) {
            Debug.LogError("THERE ARE 2 EnemyManager SCRIPTS IN EXISTANCE! PLEASE ONLY KEEP ONE EnemyManager SCRIPT!");
        }
        else {
            instance = this;
        }
    }
#endregion

    public List<EnemyStatisticsManager> enemyStatisticsManagers = new List<EnemyStatisticsManager>();

    public float maxEnemyActiveDistance = 1000; // To make sure that default all enemies will be active

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyStatisticsManagers.Count > 0) {
            foreach (EnemyStatisticsManager esm in enemyStatisticsManagers) {
                if (Vector3.Distance(ThirdPersonPlayerController.instance.gameObject.transform.position, esm.transform.position) > maxEnemyActiveDistance && esm.gameObject.activeSelf) {
                    esm.gameObject.SetActive(false);
                }
                else if (Vector3.Distance(ThirdPersonPlayerController.instance.gameObject.transform.position, esm.transform.position) <= maxEnemyActiveDistance && !esm.gameObject.activeSelf) {
                    esm.gameObject.SetActive(true);
                }
            }
        }
    }
}
