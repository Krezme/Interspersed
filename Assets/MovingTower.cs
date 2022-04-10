using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTower : MonoBehaviour
{
    public float towerSpeed;
    private bool updateon;
    // Update is called once per frame

    
    void Update()
    {
        if (updateon == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * towerSpeed);
        }
    }

    private void Start()
    {
        updateon = true;        
        StartCoroutine(updateSetOff());
    }


    private void addTowerRigidBody()
    {
        this.gameObject.AddComponent<Rigidbody>();
    }
       
    
    IEnumerator updateSetOff()
    {
        yield return new WaitForSeconds(4f);
        updateon = false;
        addTowerRigidBody();
    }
}
