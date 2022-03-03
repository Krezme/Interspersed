using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Checkpoint : MonoBehaviour
{
    
    public bool defaultSpawnpoint;
    public Light[] lights;
    public Transform playerSpawnPos;
    public Vector3 offset = new Vector3 (0,1,0);
    public RandomAudioPlayer Bell;
    public RandomAudioPlayer Tune;

    // Start is called before the first frame update
    void Start()
    {
        if (defaultSpawnpoint) {
            SelectCheckpoint();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            SelectCheckpoint();
            foreach(Checkpoint checkpoint in CheckpointManager.instance.checkpoints) {
                if (checkpoint != this) {
                    checkpoint.DeselectCheckpoint();
                }
            }
        }
    }

    public void SelectCheckpoint () {
        CheckpointManager.instance.currentCheckpointIndex = Array.IndexOf(CheckpointManager.instance.checkpoints, this);
        foreach (Light light in lights) {
            light.enabled = true;
        }
        this.gameObject.GetComponent<Collider>().enabled = false;

        Bell.PlayRandomClip(); //Rhys - Plays Telephone ring sound within the selected Phone Box on activation
        Tune.PlayRandomClip(); //Rhys - Plays checkpoint activation melody
    }

    public void DeselectCheckpoint () {
        foreach (Light light in lights) {
            light.enabled = false;
        }
        this.gameObject.GetComponent<Collider>().enabled = true;
    }
}
