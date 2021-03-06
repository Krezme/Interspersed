using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CheckpointManager : MonoBehaviour
{

#region Singleton

    public static CheckpointManager instance;

    void Awake () {
        if (instance != null) {
            Debug.LogError("There are two or more CheckpointManager scripts. Please leave only one CheckpointManager!");
        }else {
            instance = this;
        }
        if (SaveData.instance == null) {
            if (checkpoints.Count <= 0) {
                
                checkpoints = new List<Checkpoint>(FindObjectsOfType<Checkpoint>());
                checkpoints = checkpoints.OrderBy(go => go.transform.position.x + go.transform.position.z).ToList();
            }
        }
    }

#endregion

    public List<Checkpoint> checkpoints = new List<Checkpoint>();
    public int currentCheckpointIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetCheckPointFromSaveData () {
        if (SaveData.instance != null) {
            if (checkpoints.Count <= 0) {
                checkpoints = new List<Checkpoint>(FindObjectsOfType<Checkpoint>());
                checkpoints = checkpoints.OrderBy(go => go.transform.position.x + go.transform.position.z).ToList();
            }
            if (SaveData.hasLoaded) {
                currentCheckpointIndex = SaveData.lastCheckpoint;
                checkpoints[currentCheckpointIndex].EnableThisCheckpoint();
                SaveData.instance.needsToMovePlayer = true;
            }
            for (int i = 0; i < checkpoints.Count; i++) {
                checkpoints[i].DoThisOnStart();
            }
            if (SaveData.instance.needsToMovePlayer) {
                SaveData.instance.hasLoadedCheckpoint = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
