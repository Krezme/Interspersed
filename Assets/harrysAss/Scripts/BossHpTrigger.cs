using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpTrigger : MonoBehaviour
{

    public GameObject BossHpBar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            BossHpBar.SetActive(true);
        }
    }
}
