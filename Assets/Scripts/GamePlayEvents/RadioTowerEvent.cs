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

    /* public GameObject[] enemiesAlive; //Rhys - Creating array to store instantiated enemies */

    public List<EnemyStatisticsManager> enemiesAlive;

    public Transform [] EnemySpawns; //Rhys - Creating array to store spawn points

    public Transform [] InterludeSpawns;

    public GameObject Rift1;
    public GameObject Rift2;
    public GameObject Rift3;

    public bool Rift1Destroyed = false;
    //!public BoxCollider Rift1Collider;
    public bool Rift2Destroyed = false;
    //!public BoxCollider Rift2Collider;
    public bool Rift3Destroyed = false;
    //!public BoxCollider Rift3Collider;
    
    



    public int EnemiesInstantiated = 0;

    public float WaveCount = 0f;

    public GameObject RadioAnnouncerSavedDX;

    private bool CanPlay = true;

    //private float RiftTimer = 0f;

    
    


    void Start()
    {
        boxCollider = GetComponent<BoxCollider>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        //boxCollider.enabled = false;
        if (WaveCount == 0 && other.gameObject.tag == "Player")
        {
            WaveOne();
            WaveCount++;
        }
    }


    void FixedUpdate()
    {
        foreach(EnemyStatisticsManager esm in enemiesAlive)
        {
            if (esm.currentStats.health <= 0)
            {
                enemiesAlive.Remove(esm); //Ensures that dead enemies are removed from the list
            }
        }
    }


    void Update()
    {
        /* RiftTimer += Time.deltaTime;
        if (RiftTimer >= 1f)
        {
            RiftTimer = 0;
        } */

        if (Rift1.activeSelf == false)
        {
            Rift1Destroyed = true;
        }

        if (Rift2.activeSelf == false)
        {
            Rift2Destroyed = true;
        }

        if (Rift3.activeSelf == false)
        {
            Rift3Destroyed = true;
        }





        if (enemiesAlive.Count <= 0 && WaveCount == 1) //Checking that all enemies are dead & that wave one has happened
        {
            InterludeOne();
            WaveCount = 1.5f; //Interludes are between rounds so I thought I'd symbolise that within the wave number aswell
        }

        if (enemiesAlive.Count <= 0 && WaveCount == 1.5f && Rift1Destroyed == true) //Checking that all enemies are dead & that wave one has happened
        {
            WaveTwo();
            WaveCount = 2;
        }

        if (enemiesAlive.Count <= 0 && WaveCount == 2) //Checking that all enemies are dead & that wave one has happened
        {
            InterludeTwo();
            WaveCount = 2.5f; //Interludes are between rounds so I thought I'd symbolise that within the wave number aswell
        }

        if (enemiesAlive.Count <= 0 && WaveCount == 2.5f && Rift1Destroyed == true && Rift2Destroyed == true)
        {
            WaveThree();
            WaveCount = 3;
        }

        if (enemiesAlive.Count <= 0 && WaveCount == 3) //Checking that all enemies are dead & that wave one has happened
        {
            InterludeThree();
            WaveCount = 3.5f; //Interludes are between rounds so I thought I'd symbolise that within the wave number aswell
        }


        if (CanPlay == true && enemiesAlive.Count <= 0 && WaveCount == 3.5 && Rift1Destroyed == true && Rift2Destroyed == true && Rift3Destroyed == true)
        {
            RadioAnnouncerSavedDX.SetActive(true); //Activating RadioAnnouncerSaved dialogue which leasds into the credits
            CanPlay = false; //Preventing the dialogue from looping
        }
    }



    void WaveOne() //Starts when the player enters the area of the radio tower & the dialogue starts
    {
        
        /* enemiesAlive[EnemiesInstantiated] = Instantiate(WarplingPrefab,EnemySpawns[0].transform.position,EnemySpawns[0].transform.rotation);
        EnemiesInstantiated++; */

        enemiesAlive.Add(Instantiate(WarplingPrefab,EnemySpawns[0].transform.position,EnemySpawns[0].transform.rotation).GetComponent<EnemyStatisticsManager>()); //Instantiating the stated prefabs and ensuring its EnemyStatisticsManager is associated with each object individually to track their health
        EnemiesInstantiated++;

        enemiesAlive.Add(Instantiate(WarplingPrefab,EnemySpawns[1].transform.position,EnemySpawns[1].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        enemiesAlive.Add(Instantiate(WarplingPrefab,EnemySpawns[2].transform.position,EnemySpawns[2].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        enemiesAlive.Add(Instantiate(WarplingPrefab,EnemySpawns[3].transform.position,EnemySpawns[3].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        enemiesAlive.Add(Instantiate(WarplingPrefab,EnemySpawns[4].transform.position,EnemySpawns[4].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;
    }



    void InterludeOne() //Rifts open up after each wave, these spawn warplings at a constant rate until the portal has been destroyed
    {
        Rift1Destroyed = false;
        Rift1.SetActive(true);
        enemiesAlive.Add(Instantiate(StrickenPrefab,InterludeSpawns[0].transform.position,InterludeSpawns[0].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++; 
    }



    void WaveTwo() //Should start when the first wave of enemies has been defeated
    {
        enemiesAlive.Add(Instantiate(WarplingPrefab,EnemySpawns[0].transform.position,EnemySpawns[0].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        enemiesAlive.Add(Instantiate(WarplingPrefab,EnemySpawns[1].transform.position,EnemySpawns[1].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        enemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[2].transform.position,EnemySpawns[2].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        enemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[3].transform.position,EnemySpawns[3].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        enemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[4].transform.position,EnemySpawns[4].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++; 
    }


    void InterludeTwo() //Now 2 rifts open up and more enemies spawn until both rifts are destroyed
    {
        Rift1Destroyed = false;
        Rift1.SetActive(true);
        enemiesAlive.Add(Instantiate(StrickenPrefab,InterludeSpawns[0].transform.position,InterludeSpawns[0].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++; 

        Rift2Destroyed = false;
        Rift2.SetActive(true);
        enemiesAlive.Add(Instantiate(StrickenPrefab,InterludeSpawns[1].transform.position,InterludeSpawns[1].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++; 
    }


    void WaveThree() //Should start when the second wave of enemies has been defeated //!MORE SPAWN POINTS SHOULD BE MADE FOR DIFFERENT ENEMY TYPES AND SHOULD PROBS BE MOVED IN GENERAL
    {
        enemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[0].transform.position,EnemySpawns[0].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        enemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[1].transform.position,EnemySpawns[1].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        enemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[2].transform.position,EnemySpawns[2].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        enemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[3].transform.position,EnemySpawns[3].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;

        enemiesAlive.Add(Instantiate(StrickenPrefab,EnemySpawns[4].transform.position,EnemySpawns[4].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++;
    } 


    void InterludeThree() //Finally, All three rifts are enabled and all hell breaks loose - May god have mercy on your soul
    {
        Rift1Destroyed = false;
        Rift1.SetActive(true);
        enemiesAlive.Add(Instantiate(StrickenPrefab,InterludeSpawns[0].transform.position,InterludeSpawns[0].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++; 

        Rift2Destroyed = false;
        Rift2.SetActive(true);
        enemiesAlive.Add(Instantiate(StrickenPrefab,InterludeSpawns[1].transform.position,InterludeSpawns[1].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++; 

        Rift3Destroyed = false;
        Rift3.SetActive(true);
        enemiesAlive.Add(Instantiate(StrickenPrefab,InterludeSpawns[2].transform.position,InterludeSpawns[2].transform.rotation).GetComponent<EnemyStatisticsManager>());
        EnemiesInstantiated++; 
    }
}