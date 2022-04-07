using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusPlayerRestricter : MonoBehaviour
{
    public void RestrictPlayerForFungus() {
        CursorManager.instance.SetCursorState(false);
        OnPlayerInput.instance.isAllowedToMove = false; // Causing the player to stop
        OnPlayerInput.instance.ResetInput(); // Restarting the inputs so the player character does not keep on moving
    }

    public void ReleasePlayerAfterFungus() {
        CursorManager.instance.SetCursorState(true);
        OnPlayerInput.instance.isAllowedToMove = true;
    }
}
