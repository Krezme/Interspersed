using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableReturnPlayerToControllable : MonoBehaviour
{
    // Start is called before the first frame update
    void OnDisable()
    {
        Debug.Log("OnDisableReturnPlayerToControllable");
        PlayerStatisticsManager.instance.ToggleIsInvincible(false);
        OnPlayerInput.instance.ToggleIsAllowedToMove(true);
    }
}
