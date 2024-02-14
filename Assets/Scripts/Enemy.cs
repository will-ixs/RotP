using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float health;
    public bool Dead;
    [SerializeField] private GameObject player;
    private float maxHealth;
    private Animator anim;

    private EnemySpawner spawner;

    private float damage_color_cooldown;

    [SerializeField] private GameObject _prefab_ka_fragment;

    [SerializeField] private Vector2 dir;
    [SerializeField] private float damage;
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();
        player = GameObject.FindGameObjectWithTag("Player");
        maxHealth = health;
        Dead = false;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        damage_color_cooldown -= Time.deltaTime;
        if (damage_color_cooldown <= 0.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(207, 192, 171);
        }
        SetDirection();
    }

    public void TakeDamage(int damage) 
    {
        health -= damage;

        if (health <= 0)
        {
            Dead = true;
            KaFragment kaFragment = Instantiate(_prefab_ka_fragment, gameObject.transform.position, Quaternion.identity).GetComponent<KaFragment>();
            kaFragment.initialAmount = maxHealth;
            kaFragment.decayRate = 1.0f;
        } else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            damage_color_cooldown = 0.5f;
        }
    }

    private void SetDirection()
    {
        //0 = UP, 1 = LEFT, 2 = DOWN, 3 = RIGHT
        dir = player.transform.position - transform.position;
        dir.Normalize();
        Vector2 mag = new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));

        if (mag.y > mag.x)
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

    private void OnDestroy(){
        spawner.activeEnemies.Remove(gameObject);
    }
}
