using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(SphereCollider))]
public class Checkpoint : MonoBehaviour
{
    
    public bool defaultSpawnpoint;
    public Light[] lights;
    public Transform playerSpawnPos;
    public Vector3 offset = new Vector3 (0,1,0);
    public Animator doorAnimation;
    public RandomAudioPlayer Bell;
    public RandomAudioPlayer Tune;

    public GameObject checkpointUI;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveData.instance == null) {
            DoThisOnStart();
        }
        
    }

    public void DoThisOnStart() {
        Debug.Log("Hello????/");
        if (SaveData.instance != null) {
            if (SaveData.hasLoaded && CheckpointManager.instance.currentCheckpointIndex == CheckpointManager.instance.checkpoints.IndexOf(this)) {
                EnableThisCheckpoint();
            }
            else if (defaultSpawnpoint) {
                SelectDefaultCheckpoint();
            }
        }
        else {
            Debug.Log("SaveData.instance == null");
            if (defaultSpawnpoint) {
                SelectDefaultCheckpoint();
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (SaveData.instance != null) {
                if (SaveData.instance.saveChanged || CheckpointManager.instance.currentCheckpointIndex != CheckpointManager.instance.checkpoints.IndexOf(this)) {
                    EnteredCheckPoint();
                    SaveData.instance.RecordState();
                }
            }else {
                EnteredCheckPoint();
            }
        }
    }
    
    void EnteredCheckPoint() {
        SelectCheckpoint();
        foreach(Checkpoint checkpoint in CheckpointManager.instance.checkpoints) {
            if (checkpoint != this) {
                checkpoint.DeselectCheckpoint();
            }
        }
    }

    public void SelectDefaultCheckpoint () {
        if (SaveData.instance != null) {
            if (!SaveData.hasLoaded) {
                EnableThisCheckpoint();
                SaveData.lastCheckpoint = CheckpointManager.instance.currentCheckpointIndex;
                SaveData.instance.RecordState();
            }
        }
        else {
            EnableThisCheckpoint();
        }
    }
    
    public void EnableThisCheckpoint () {
        CheckpointManager.instance.currentCheckpointIndex = CheckpointManager.instance.checkpoints.IndexOf(this);
        foreach (Light light in lights) {
            light.enabled = true;
        }
        ToggleTriggerCollider(false);
    }

    public void SelectCheckpoint () {
        CheckpointManager.instance.currentCheckpointIndex = CheckpointManager.instance.checkpoints.IndexOf(this);
        foreach (Light light in lights) {
            light.enabled = true;
        }
        ToggleTriggerCollider(false);
        doorAnimation.SetTrigger("OpenDoor");
        SaveData.instance.RecordState();
        Bell.PlayRandomClip(); //Rhys - Plays Telephone ring sound within the selected Phone Box on activation
        Tune.PlayRandomClip(); //Rhys - Plays checkpoint activation melody
        checkpointUI.SetActive(true);
    }

    public void DeselectCheckpoint () {
        foreach (Light light in lights) {
            light.enabled = false;
        }
        ToggleTriggerCollider(true);
    }

    void ToggleTriggerCollider(bool state) {
        if (SaveData.instance == null) {
            this.gameObject.GetComponent<Collider>().enabled = state;
        }
    }
}
