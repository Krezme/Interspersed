using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusPlayerRestricter : MonoBehaviour
{
    public void RestrictPlayerForFungus() {
        CursorManager.instance.SetCursorState(false);
        OnPlayerInput.instance.isAllowedToMove = false;
    }

    public void ReleasePlayerAfterFungus() {
        CursorManager.instance.SetCursorState(true);
        OnPlayerInput.instance.isAllowedToMove = true;
    }
}
