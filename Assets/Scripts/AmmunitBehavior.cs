using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class AmmunitBehavior : MonoBehaviour
{
    private BossHealth bossHealth;
    private float stateChangeTimer;
    private GameObject player;
    [SerializeField] private Animator anim;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Ammunit_Slam ammSlam;
    [SerializeField] private Ammunit_Bite ammBite;
    private float speed;
    private Rigidbody2D rb;
    public enum AmmunitState
    {
        Idle,
        MoveAtPlayer,
        SlamAttack,
        BiteAttack,
    }
    public AmmunitState state;
    void Start()
    {
        stateChangeTimer = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        bossHealth = GetComponent<BossHealth>();
        //_movement_controller = GetComponent<MovementController>();
        rb = GetComponent<Rigidbody2D>();
        speed = moveSpeed;
    }

    void Update()
    {
        stateChangeTimer -= Time.deltaTime;
        if (bossHealth.Dead)
        {
            Die();
        }
        else
        {
            ChooseNextState();
            ActivateNextState();
        }
    }

    private void ActIdle()
    {
        rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, 1f * Time.deltaTime);
    }
    private void MoveAtPlayer()
    {
        Vector2 vecToPlayer = player.transform.position - transform.position;
        if (vecToPlayer.magnitude > 2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }
    private void SlamAttack()
    {
        rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, 1f * Time.deltaTime);
        ammSlam.Enable();
        if(stateChangeTimer < 0.5f)
        {
            ammSlam.Hit();
        }
    }

    private void BiteAttack()
    {
        moveSpeed = 4.5f;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        ammSlam.Enable();
        if(stateChangeTimer < 0.5f)
        {
            ammSlam.Hit();
        }
    }

    private void CheckSpriteFlip()
    {
        if (transform.position.x < player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
    }

    private void ChooseNextState()
    {
        //End attacks, choose new state that is not the same as the previous.
        if (stateChangeTimer <= 0.0f) {
            switch (state)
            {
                case AmmunitState.SlamAttack: //Swipe Attack
                    ammSlam.Disable();
                    break;
                case AmmunitState.BiteAttack: //Spit Attack
                    ammBite.Disable();
                    moveSpeed = speed;
                    break;
            }
            AmmunitState currState = state;
            while (currState == state) { 
                int selection = Random.Range(0, 4); //CHANGE TO (0, 4) when Spit attack done.
                switch (selection)
                {
                    case 0:
                        state = AmmunitState.MoveAtPlayer;
                        stateChangeTimer = 3.75f;
                        break;
                    case 1:
                        state = AmmunitState.MoveAtPlayer;
                        stateChangeTimer = 3.75f;
                        break;
                    case 2:
                        state = AmmunitState.SlamAttack;
                        stateChangeTimer = 1.2f;
                        break;
                    case 3:
                        state = AmmunitState.BiteAttack;
                        stateChangeTimer = 0.8f;
                        break;
                    case 4:
                        state = AmmunitState.MoveAtPlayer;
                        stateChangeTimer = 3.75f;
                        break;
                }
            }
        }
    }

    private void ActivateNextState()
    {
        //Activate new state.
        //Idle = 0, Move = 1, Swipe = 2, Spit = 3
        switch (state) {
            case AmmunitState.Idle:
                anim.SetInteger("State", 0);
                CheckSpriteFlip();
                ActIdle();
                break;
            case AmmunitState.MoveAtPlayer:
                anim.SetInteger("State", 1);
                CheckSpriteFlip();
                MoveAtPlayer();
                break;
            case AmmunitState.SlamAttack: //Swipe Attack
                anim.SetInteger("State", 2);
                SlamAttack();
                break;
            case AmmunitState.BiteAttack: //Spit Attack
                anim.SetInteger("State", 3);
                BiteAttack();
                break;
        }
    }

    private void Die()
    {
        state = AmmunitState.Idle;
        anim.SetTrigger("Dead");
        //Death animation / fade to white & screen shake or something
        Destroy(gameObject, 1.5f);
    }
}