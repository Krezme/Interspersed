using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CristalArm : PlayerAbility
{
    public Transform spawnBulletPosition;
    public GameObject pfBulletProjectile;
    public AudioSource audioSource;
    public AudioClip abilitySound;

    public override void AimingAbility ()
    {
        Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
        Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
        OnPlayerInput.instance.onFire1 = false;
        audioSource.PlayOneShot(abilitySound);
    }
}
