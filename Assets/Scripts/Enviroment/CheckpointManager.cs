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
        Debug.Log(instance);
        if (instance != null) {
            Debug.LogError("There are two or more CheckpointManager scripts. Please leave only one CheckpointManager!");
        }else {
            instance = this;
        }

        if (checkpoints.Count <= 0) {
            
            checkpoints = new List<Checkpoint>(FindObjectsOfType<Checkpoint>());
            checkpoints = checkpoints.OrderBy(go => go.transform.position.x + go.transform.position.z).ToList();
        }
    }

#endregion

    public List<Checkpoint> checkpoints = new List<Checkpoint>();
    public int currentCheckpointIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
