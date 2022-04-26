using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EventState{

    public string name;
    public bool eventTackled = false;
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
    private EventManager[] eventManagers = new EventManager[] {};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RecordState() {
        savedEventsState = currentEventsState.DeepClone();
        lastCheckpoint = CheckpointManager.instance.currentCheckpointIndex;
        needsLoading = true;
    }

    public void LoadState() {
        currentEventsState = savedEventsState.DeepClone();
        CheckpointManager.instance.currentCheckpointIndex = lastCheckpoint;
        needsLoading = false;
    }

    void TriggerEventManagerAwakes() {
        foreach (EventManager em in eventManagers) {
            em.TriggerMyAwake();
        }
    }

    /* void LoadWorldState() {
        CheckpointManager.instance.currentCheckpointIndex = lastCheckpoint;
        foreach (EventState es in savedEventsState) {
            if (es.eventComplete) {
                foreach (GameObject go in es.eventGameObjects) {
                    go.SetActive(false);
                }
            }
        }
    } */

    [ContextMenu("ReloadScene")]
    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
