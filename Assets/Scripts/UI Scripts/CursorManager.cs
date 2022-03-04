using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

#region Singleton
    public static CursorManager instance;

    void Awake() {
        if (instance != null) {
            Debug.LogError("There is another instance of CursorManager!!! Please remove the second instance");
        }
        instance = this;
    }
#endregion

    public bool cursorLocked = true;
    public bool isESC;

    /// <summary>
    /// Sets the cursor state to cursorLocked variable
    /// </summary>
    void FixedUpdate () {
        SetCursorState (cursorLocked);
    }

    /// <summary>
    /// When the application is focused set cursor's state to locked
    /// </summary>
    /// <param name="isFocused">If the application is focused it is true</param>
    private void OnApplicationFocus(bool isFocused)
	{
        if (!isESC)
        {
            SetCursorState(isFocused);
        }
	}

    /// <summary>
    /// Sets the state of the curser true = locked, false = unlocked
    /// </summary>
    /// <param name="newState">The new state of the cursor. Locked or not</param>
    public void SetCursorState (bool newState) {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        cursorLocked = newState;
    }
}
