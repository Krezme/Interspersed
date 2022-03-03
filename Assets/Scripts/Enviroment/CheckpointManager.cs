using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

#region Singleton

    public static CheckpointManager instance;

    void Awake () {
        if (instance != null) {
            Debug.LogError("There are two or more CheckpointManager scripts. Please leave only one CheckpointManager!");
        }
        instance = this;

        checkpoints = FindObjectsOfType<Checkpoint>();
    }

#endregion

    public Checkpoint[] checkpoints;
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
