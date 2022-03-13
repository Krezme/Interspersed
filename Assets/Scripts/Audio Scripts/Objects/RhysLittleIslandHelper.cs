using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhysLittleIslandHelper : MonoBehaviour
{
    public Animator animator;

    public AudioSource AudioSource;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet" && other.GetComponent<BulletProjectile>().statistics.isElectric)
        {
            animator.enabled = true;
            AudioSource.Play();
        }
    }
}
