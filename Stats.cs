using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    //Basic Health Script 

    public int maxHealth = 100;
    public float health = 0f;
    public float healthRegen = 0.1f;


    void Start()
    {
        health = maxHealth;
        TakeDamage(67f);
    }

    void Update()
    {
        if (health < maxHealth)
        {
            health = health + healthRegen;
        }else
        {
            health = maxHealth;
        }
    }


    void TakeDamage(float dmg)
    {
        health = health - dmg;


    }

}
