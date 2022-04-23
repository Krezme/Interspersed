using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EventState{
    public string name;
    public bool eventTackled = false;
    public bool eventComplete = false;
    public GameObject[] eventGameObjects;

}

public class EventUnlocks {
    public bool doesUnlockArm = false;
    public bool doesUnlockAbility = false;
    
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
    }
    #endregion

    [SerializeField]
    public EventState[] events;
    public static EventState[] staticEvents;
    public static int lastCheckpoint;
    public static bool needsLoading = false;

    // Start is called before the first frame update
    void Start()
    {
        if (needsLoading) {
            needsLoading = false;
        }
    }

    public void RecordState() {
        staticEvents = events.DeepClone();
        lastCheckpoint = CheckpointManager.instance.currentCheckpointIndex;
        needsLoading = true;
    }

    void LoadWorldState() {
        CheckpointManager.instance.currentCheckpointIndex = lastCheckpoint;
        foreach (EventState es in staticEvents) {
            if (es.eventComplete) {
                foreach (GameObject go in es.eventGameObjects) {
                    go.SetActive(false);
                }
            }
        }
    }

    [ContextMenu("ReloadScene")]
    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
