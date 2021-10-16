using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParenting : MonoBehaviour
{
    void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Player") {
            col.gameObject.transform.SetParent(this.gameObject.transform);
        }
    }

    void OnTriggerExit (Collider col) {
        if (col.gameObject.tag == "Player") {
            col.gameObject.transform.SetParent(null);
        }
    }
}
