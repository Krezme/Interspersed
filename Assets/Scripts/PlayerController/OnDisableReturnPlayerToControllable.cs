using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableReturnPlayerToControllable : MonoBehaviour
{

    public GameObject hud;

    void OnEnable() {
        PlayerStatisticsManager.instance.ToggleIsInvincible(true);
        OnPlayerInput.instance.ToggleIsAllowedToMove(false);
        hud.SetActive(false);
    }

    // Start is called before the first frame update
    void OnDisable()
    {
        PlayerStatisticsManager.instance.ToggleIsInvincible(false);
        OnPlayerInput.instance.ToggleIsAllowedToMove(true);
        hud.SetActive(true);
    }
}
