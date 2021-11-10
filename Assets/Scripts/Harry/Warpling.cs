using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warpling : MonoBehaviour
{

    public int warpmaxHealth = 80;
    public int warpcurrentHealth;
    public WarpHealthbar warpHealthbar;
    public GameObject warphealthUI;
    
    
    

    void TakeDamage(int damage)
    {
        warpcurrentHealth -= damage;

        warpHealthbar.SetwarpHealth(warpcurrentHealth);
        
    }
    


    void Update()
    {
        if (warpcurrentHealth <= 0)
        {
            
            Destroy(gameObject);

        }
        if(warpcurrentHealth < warpmaxHealth)
        {
            warphealthUI.SetActive(true);
            warpHealthbar.SetwarpHealth(warpcurrentHealth);
        }
    }
    void Start()
    {
        warpcurrentHealth = warpmaxHealth;
        
    }

  
    private void OnTriggerEnter(Collider other)
    {
        
    
    
        if (other.gameObject.tag == "PlayerBullet")
        {
            TakeDamage(20);
        }
    
    
   
    }
}
