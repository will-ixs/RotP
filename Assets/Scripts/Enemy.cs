using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public bool Dead;
        
    [SerializeField] private float damage;
    private float maxHealth;
    void Start()
    {
        health = maxHealth;
        Dead = false;
    }

    void Update()
    {
        


    }

    public void TakeDamage(int damage) 
    {
        health -= damage;

        if(health <= 0)
        {
            Dead = true;
        }
    }
}
