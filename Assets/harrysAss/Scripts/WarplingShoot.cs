using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarplingShoot : MonoBehaviour
{
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    WaitForSeconds waitForSeconds = new WaitForSeconds(1f);

    IEnumerator Start() {
        while (true) {
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, spawnBulletPosition.rotation);
            yield return waitForSeconds;
        } 
    }
}
