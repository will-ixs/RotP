using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float health;
    public bool Dead;
    private GameObject player;
    private float maxHealth;
    private Animator anim;

    [SerializeField] private Vector2 dir;
    [SerializeField] private float damage;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
        Dead = false;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        SetDirection();
    }

    public void TakeDamage(int damage) 
    {
        health -= damage;

        if(health <= 0)
        {
            Dead = true;
        }
    }

    private void SetDirection()
    {
        //0 = UP, 1 = LEFT, 2 = DOWN, 3 = RIGHT
        dir = player.transform.position - transform.position ;
        dir.Normalize();
        Vector2 mag = dir.Abs();

        if(mag.y > mag.x)
        {
            //LookUp/Down
            if(dir.y > 0.0f)
            {
                anim.SetInteger("Direction", 0);
            }
            else
            {
                anim.SetInteger("Direction", 2);
            }
        }
        else
        {
            //LookRight/Left
            if (dir.x < 0.0f)
            {
                anim.SetInteger("Direction", 1);
            }
            else
            {
                anim.SetInteger("Direction", 3);
            }
        }
    }
}
