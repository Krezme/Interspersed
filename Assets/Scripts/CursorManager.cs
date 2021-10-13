using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    public bool cursorLocked = true;

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
		SetCursorState (isFocused);
	}

    /// <summary>
    /// Sets the state of the curser true = locked, false = unlocked
    /// </summary>
    /// <param name="newState">The new state of the cursor. Locked or not</param>
    void SetCursorState (bool newState) {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
