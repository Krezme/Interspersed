using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorCoumter : MonoBehaviour
{
    
    public GameObject generator1; //Rhys - Place a GameObject that gets enabled by the desired generator

    private bool gen1On = false; //Rhys - Gets set to true when GameObject is activated to prevent counting a generator more than once
    
    public GameObject generator2; 
    private bool gen2On = false; 

    public GameObject generator3; 
    private bool gen3On = false; 

    public GameObject generator4;
    private bool gen4On = false; 


    public GameObject towerLight1; //Rhys - Place the tower light point light that gets enabled as the generator count goes up

    public GameObject towerLight2;

    public GameObject towerLight3;

    public GameObject towerLight4;

    public AudioSource completedThunder; //Rhys -To play thunder sound when all generators are on

    private bool thunderPlay = true;



    private int genCount = 0; //Rhys - Represents the number of activated generators


    void Update()
    {
        if (generator1.activeSelf == true && gen1On == false)
        {
            gen1On = true;
            genCount++;
        }

        if (generator2.activeSelf == true && gen2On == false)
        {
            gen2On = true;
            genCount++;
        }

        if (generator3.activeSelf == true && gen3On == false)
        {
            gen3On = true;
            genCount++;
        }

        if (generator4.activeSelf == true && gen4On == false)
        {
            gen4On = true;
            genCount++;
        }




        if (genCount == 1)
        {
            towerLight1.SetActive(true);
        }

        if (genCount == 2)
        {
            towerLight2.SetActive(true);
        }

        if (genCount == 3)
        {
            towerLight3.SetActive(true);
        }

        if (genCount == 4)
        {
            towerLight4.SetActive(true);

            if (thunderPlay)
            {
            completedThunder.Play();
            thunderPlay = false;
            } 
        }
    }











}
