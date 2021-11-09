using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warpling : MonoBehaviour
{

    public int warpmaxHealth = 80;
    public int warpcurrentHealth;
    public WarpHealthbar warphealthbar;
   
    
    public GameObject warphealthbarUI;

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
        if (warpcurrentHealth < warpmaxHealth)
        {
            warphealthbarUI.SetActive(true);
            warphealthbar.SetwarpHealth(warpcurrentHealth);
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
