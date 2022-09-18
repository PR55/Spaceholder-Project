using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{


    public float maxHealth = 100f;
    
    public float health;
    public bool decreaseHealth;
    public float waitTime = 30.0f;
    void Start()
    {
        decreaseHealth = false;
        health = maxHealth;
    }

    
    void Update()
    {
        //These are controls for testing helath increase and decrease
        if (decreaseHealth == true)
        {
            
            health -= 1.0f / waitTime * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            health += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            health -= 0.1f;
        }
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public float CurrentHealth()
    {
        //sets up health when called to calculate the current exposure weight of the blocks in the tubes 
        return (1f - (health / maxHealth));
    }
    public float CurrentHealthCanvas()
    {
        //calculates the fill of an image based on current player health
        return ((health / maxHealth));
    }
}
