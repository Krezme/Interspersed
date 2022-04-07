using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RadioTowerEvent : MonoBehaviour
{

    public GameObject WarplingPrefab;

    public GameObject StrickenPrefab;

    public GameObject CarrierPrefab;


    /* public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject spawnPoint4;
    public GameObject spawnPoint5; */ //Rhys - Replaced with an array of spawn points

    public BoxCollider boxCollider;




    public LayerMask layers;
    public UnityEvent OnEnter;

    /* public GameObject[] EnemiesAlive; //Rhys - Creating array to store instantiated enemies */

    public List<EnemyStatisticsManager> EnemiesAlive;

    public Transform [] EnemySpawns; //Rhys - Creating array to store spawn points

    public int EnemiesInstantiated = 0;

    private int WaveCount = 0;

    public GameObject RadioAnnouncerSavedDX;

    private bool CanPlay = true;

    




    void Start()
    {
        boxCollider = GetComponent<BoxCollider>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        boxCollider.enabled = false;
        if (WaveCount == 0)
        {
            WaveOne();
            WaveCount++;
        }
    }


    void FixedUpdate()
    {
        foreach(EnemyStatisticsManager esm in EnemiesAlive)
        {
            if (esm.currentStats.health <= 0)
            {
                EnemiesAlive.Remove(esm); //Ensures that dead enemies are removed from the list
            }
        }
    }


    void Update()
    {
        if (EnemiesAlive.Count <= 0 && WaveCount == 1) //Checking that all enemies are dead & that wave one has happened
        {
            WaveTwo();
            WaveCount++;
        }

        if (EnemiesAlive.Count <= 0 && WaveCount == 2)
        {
            WaveThree();
            WaveCount++;
        }

        if (CanPlay == true && EnemiesAlive.Count <= 0 && WaveCount == 3)
        {
            RadioAnnouncerSavedDX.SetActive(true); //Activating RadioAnnouncerSaved dialogue which leasds into the credits
            CanPlay = false; //Preventing the dialogue from looping
        }
    }



    void WaveOne() //Starts when the player enters the area of the radio tower & the dialogue starts
    {
        
        /* EnemiesAlive[EnemiesInstantiated] = Instantiate(WarplingPrefab,EnemySpawns[0].transform.position,EnemySpawns[0].transform.rotation);
        EnemiesInstantiated++; */

        EnemiesAlive.Add(Instantiate(WarplingPrefab,EnemySpawns[0].transform.position,EnemySpawns[0].transform.rotation).GetComponent<EnemyStatisticsManager>()); //Instantiating the stated prefabs and ensuring its EnemyStatisticsManager is associated with each object individually to track their health
        EnemiesInstantiated++;

        EnemiesAlive.Add(Instantiate(WarplingPrefab,EnemySpawns[1].transform.position,EnemySpawns[1].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        EnemiesAlive.Add(Instantiate(WarplingPrefab,EnemySpawns[2].transform.position,EnemySpawns[2].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        EnemiesAlive.Add(Instantiate(WarplingPrefab,EnemySpawns[3].transform.position,EnemySpawns[3].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        EnemiesAlive.Add(Instantiate(WarplingPrefab,EnemySpawns[4].transform.position,EnemySpawns[4].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;
    }


    void WaveTwo() //Should start when the first wave of enemies has been defeated
    {
        EnemiesAlive.Add(Instantiate(WarplingPrefab,EnemySpawns[0].transform.position,EnemySpawns[0].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        EnemiesAlive.Add(Instantiate(WarplingPrefab,EnemySpawns[1].transform.position,EnemySpawns[1].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        EnemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[2].transform.position,EnemySpawns[2].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        EnemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[3].transform.position,EnemySpawns[3].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        EnemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[4].transform.position,EnemySpawns[4].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++; 
    }



    void WaveThree() //Should start when the second wave of enemies has been defeated //!MORE SPAWN POINTS SHOULD BE MADE FOR DIFFERENT ENEMY TYPES AND SHOULD PROBS BE MOVED IN GENERAL
    {
        EnemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[0].transform.position,EnemySpawns[0].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        EnemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[1].transform.position,EnemySpawns[1].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        EnemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[2].transform.position,EnemySpawns[2].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        EnemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[3].transform.position,EnemySpawns[3].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        EnemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[4].transform.position,EnemySpawns[4].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;
    } 
}
