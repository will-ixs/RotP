using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health;
    public float moveSpeed;

    public GameObject player;
    public float speed;
    public float distanceBW;

    private float distance;
        
    [SerializeField] private float damage;
    private float maxHealth;
    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        


    }
}
