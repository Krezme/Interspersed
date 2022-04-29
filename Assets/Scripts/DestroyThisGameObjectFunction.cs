using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisGameObjectFunction : MonoBehaviour
{
    public void DestroyGameObject () {
        Destroy(this.gameObject);
    }
}
