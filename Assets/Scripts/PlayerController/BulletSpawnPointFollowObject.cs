using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnPointFollowObject : MonoBehaviour
{
    [Tooltip("Assign the ForBulletSpawnPointToFollow game object connected to the players cristal arm")]
    public GameObject objectToFollow; // Assign the ForBulletSpawnPointToFollow game object connected to the players cristal arm

    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.transform.position;
        transform.LookAt(PlayerAbilitiesController.instance.rayBitch);
    }
}
