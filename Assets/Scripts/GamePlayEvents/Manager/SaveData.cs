using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EventState{

    
    public string name;
    
    /* public Events eventName; */
    public bool eventComplete = false;
/* 
    public void Validate (int index) {
        eventName = (Events)index;
        name = eventName.ToString();
    } */

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
    public static bool hasLoaded = false;
    public bool inTheMiddleOfAnEvent;
    

    public bool saveChanged = false;
    public EventManager[] eventManagers;

    // Start is called before the first frame update
    void Start()
    {
        /* if (!hasLoaded) { 
            RecordState();
        } */
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
        needsLoading = false;
        hasLoaded = true;
    }

    void TriggerEventManagerAwakes() {
        foreach (EventManager em in eventManagers) {
            em.TriggerMyAwake();
        }
    }

    public void MovePlayerToLastCheckPoint () {
        ThirdPersonPlayerController.instance.gameObject.transform.position = CheckpointManager.instance.checkpoints[lastCheckpoint].playerSpawnPos.position + CheckpointManager.instance.checkpoints[lastCheckpoint].offset;
    }

    [ContextMenu("ReloadScene")]
    public void ReloadScene() {
        needsLoading = true;
        Debug.Log(lastCheckpoint);
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

/*     void FixedUpdate() {
#if !UNITY_EDITOR
        for (int i = 0; i < currentEventsState.Length; i++) {
            currentEventsState[i].Validate(i);
        }
#endif
    }

#if UNITY_EDITOR
    void OnValidate() {
        for (int i = 0; i < currentEventsState.Length; i++) {
            currentEventsState[i].Validate(i);
        }
    }
#endif */


}

/* public enum Events {
    ThunderRockEvent,
    TheTipEvent,
    ShotgunEvent,
    MeetTheSlugKing,
    BakeryBreadBankEvent,
    TheFeeding,
    StrangerInDanger,
    Toolbox,
    BaguetteMode
} */