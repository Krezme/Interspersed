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
        if (needsLoading && !isInMenu) {
            LoadState();
            TriggerEventManagerAwakes();
            questMarkerController.targetTransform = FindFirstEnabledQuestMarkerTransform();
            checkpointManager.GetCheckPointFromSaveData();
            MovePlayerToLastCheckPoint();
        }
    }

    #endregion

    [SerializeField]
    public EventState[] currentEventsState; // ! DON'T FORGET TO PASTE THE CURRENT STATISTICS INTO THE MAIN MENU
    public static EventState[] savedEventsState; 
    public static PlayerStatistics savedPlayerStatistics;
    public static int lastCheckpoint;
    public static bool needsLoading = false;
    public static bool hasLoaded = false;
    public static bool saveAvailable = false;
    public bool inTheMiddleOfAnEvent;

    public bool saveChanged = false;
    
    public EventManager[] eventManagers;
    public CheckpointManager checkpointManager;
    public QuestMarkerController questMarkerController; //! This is because of script execution order Do not use the Singleton in this script
    public Transform[] questMarkerTransformsInOrder;

    private float timeToUpdateQuestMarker = 0.25f;
    private float currentTimePassed;

    public bool isInMenu = false;

    [HideInInspector]
    public bool needsToMovePlayer;

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
        savedPlayerStatistics = PlayerStatisticsManager.instance.maxStatistics.DeepClone();
        lastCheckpoint = checkpointManager.currentCheckpointIndex;
        saveChanged = false;
        saveAvailable = true;
    }

    [ContextMenu("LoadState")]
    public void LoadState() {
        currentEventsState = savedEventsState.DeepClone();
        PlayerStatisticsManager.instance.maxStatistics = savedPlayerStatistics.DeepClone();
        checkpointManager.currentCheckpointIndex = lastCheckpoint;
        needsLoading = false;
        hasLoaded = true;
    }

    void TriggerEventManagerAwakes() {
        foreach (EventManager em in eventManagers) {
            em.TriggerMyAwake();
        }
    }
    
    Transform FindFirstEnabledQuestMarkerTransform () {
        foreach (Transform questMarkerTransform in questMarkerTransformsInOrder) {
            if (questMarkerTransform != null) {
                if (questMarkerTransform.gameObject.activeSelf) {
                    return questMarkerTransform;
                }
            }
        }
        return null;
    }

    public void MovePlayerToLastCheckPoint () {
        if (needsToMovePlayer) {
            Debug.Log("needsToMovePlayer");
            ThirdPersonPlayerController.instance.gameObject.transform.position = CheckpointManager.instance.checkpoints[lastCheckpoint].playerSpawnPos.position + CheckpointManager.instance.checkpoints[lastCheckpoint].offset;
            needsToMovePlayer = false;
        }
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
    
    public bool CheckIfSaveStateIsNotDef() {
        if (saveAvailable) {
            return true;
        }
        return false;
    }

    public void NeedsToLoad(bool state) {
        needsLoading = state;
        needsToMovePlayer = state;
    }

    public void RestartSaveState() {
        savedEventsState = currentEventsState.DeepClone();
        savedPlayerStatistics = new PlayerStatistics();
        lastCheckpoint = 0;
        needsLoading = false;
        hasLoaded = false;
        saveAvailable = false;
    }

    void Update () {
        if (!isInMenu) {
            currentTimePassed += Time.deltaTime;
            if (currentTimePassed >= timeToUpdateQuestMarker) {
                questMarkerController.targetTransform = FindFirstEnabledQuestMarkerTransform();
                currentTimePassed = 0;
            }
            //MovePlayerToLastCheckPoint();
        }
    }

//! This will make the quest system more user error secure IMPLEMENT IF THERE IS TIME AT THE END OF THE PROJECT

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