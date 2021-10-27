using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warpling : MonoBehaviour
{

    public int warpmaxHealth = 50;
    public int warpcurrentHealth;
    public WarpHealthbar warphealthbar;
    
    
    

    void TakeDamage(int damage)
    {
        warpcurrentHealth -= damage;

        warphealthbar.SetwarpHealth(warpcurrentHealth);
        
    }
    


    void Update()
    {
        if (warpcurrentHealth <= 0)
        {
            
            Destroy(gameObject);

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
