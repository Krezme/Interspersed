using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EventState{

    public string name;
    public bool eventComplete = false;

}

public class SaveData : MonoBehaviour
{

    #region Singleton

    public static SaveData instance;

    void Awake() {
        if (instance != null) {
            Debug.LogError("THERE ARE TWO OR MORE SaveData INSTANCES! PLEASE KEEP ONLY ONE instance OF THIS SCRIPT");
        }
        else {
            instance = this;
        }
        eventManagers = FindObjectsOfType<EventManager>();
        if (needsLoading) {
            LoadState();
            TriggerEventManagerAwakes();
        }
    }

    #endregion

    [SerializeField]
    public EventState[] currentEventsState;
    public static EventState[] savedEventsState;
    public static int lastCheckpoint;
    public static bool needsLoading = false;
    [HideInInspector]
    public bool inTheMiddleOfAnEvent;
    

    public bool saveChanged = false;
    private EventManager[] eventManagers = new EventManager[] {};

    // Start is called before the first frame update
    void Start()
    {

    }

    [ContextMenu("RecordState")]
    public void RecordState() {
        savedEventsState = currentEventsState.DeepClone();
        lastCheckpoint = CheckpointManager.instance.currentCheckpointIndex;
        saveChanged = false;
    }

    [ContextMenu("LoadState")]
    public void LoadState() {
        currentEventsState = savedEventsState.DeepClone();
        CheckpointManager.instance.currentCheckpointIndex = lastCheckpoint;
        MovePlayerToLastCheckPoint();
        needsLoading = false;
    }

    void TriggerEventManagerAwakes() {
        foreach (EventManager em in eventManagers) {
            em.TriggerMyAwake();
        }
    }

    void MovePlayerToLastCheckPoint () {
        ThirdPersonPlayerController.instance.gameObject.transform.position = CheckpointManager.instance.checkpoints[lastCheckpoint].playerSpawnPos.position + CheckpointManager.instance.checkpoints[lastCheckpoint].offset;
    }

    [ContextMenu("ReloadScene")]
    public void ReloadScene() {
        needsLoading = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    [ContextMenu("DebugTheEventStates")]
    public void DebugTheEventStates() {
        Debug.Log(currentEventsState == savedEventsState);
    }

    public void ToggleInMiddleOfAnEvent(bool state) {
        inTheMiddleOfAnEvent = state;
    }

    public void SetEventToComplete(int index) {
        currentEventsState[index].eventComplete = true;
        saveChanged = true;
    }

}
