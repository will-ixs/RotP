using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpopardRangedBehavior : MonoBehaviour
{
    private BossHealth bossHealth;
    private float stateChangeTimer;
    private GameObject player;
    [SerializeField] private Animator anim;
    [SerializeField] private float moveSpeed;
    [SerializeField] private SerpopardSwipe serpSwipe;
    [SerializeField] private SerpopardSpit serpSpit;
    private Rigidbody2D rb;
    private MovementController _movement_controller;
    public enum SerpopardState
    {
        Idle,
        MoveAtPlayer,
        SwipeAttack,
        SpitAttack,
    }
    public SerpopardState state;
    void Start()
    {
        stateChangeTimer = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        bossHealth = GetComponent<BossHealth>();
        _movement_controller = GetComponent<MovementController>();
        rb = GetComponent<Rigidbody2D>();
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
        //_movement_controller.changeVelocity(Vector2.zero);
    }
    private void MoveAtPlayer()
    {
        Vector3 offset = (transform.position - player.transform.position).normalized * 5;
        transform.position = Vector3.MoveTowards(transform.position, offset, moveSpeed * Time.deltaTime);
        // Vector3 vecFromPlayer = transform.position - player.transform.position;
        // _movement_controller.changeVelocity(moveSpeed * vecFromPlayer.normalized);
    }
    private void SwipeAttack()
    {

        rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, 1f * Time.deltaTime);
        //_movement_controller.changeVelocity(Vector2.zero);
        serpSwipe.Enable();
        if(stateChangeTimer < 1.0f)
        {
            serpSwipe.Hit();
        }
    }

    private void SpitAttack()
    {
        rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, 1f * Time.deltaTime);
        //_movement_controller.changeVelocity(Vector2.zero);
        serpSpit.Enable();
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
                case SerpopardState.SwipeAttack: //Swipe Attack
                    serpSwipe.Disable();
                    break;
                case SerpopardState.SpitAttack: //Spit Attack
                    serpSpit.Disable();
                    break;
            }
            SerpopardState currState = state;
            while (currState == state) { 
                int selection = Random.Range(0, 4); //CHANGE TO (0, 4) when Spit attack done.
                switch (selection)
                {
                    case 1:
                        state = SerpopardState.MoveAtPlayer;
                        stateChangeTimer = 3.75f;
                        break;
                    case 2:
                        state = SerpopardState.SwipeAttack;
                        stateChangeTimer = 1.5f;
                        break;
                    case 3:
                        state = SerpopardState.SpitAttack;
                        stateChangeTimer = 5.0f;
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
            case SerpopardState.Idle:
                anim.SetInteger("State", 0);
                CheckSpriteFlip();
                ActIdle();
                break;
            case SerpopardState.MoveAtPlayer:
                anim.SetInteger("State", 1);
                CheckSpriteFlip();
                MoveAtPlayer();
                break;
            case SerpopardState.SwipeAttack: //Swipe Attack
                anim.SetInteger("State", 2);
                SwipeAttack();
                break;
            case SerpopardState.SpitAttack: //Spit Attack
                anim.SetInteger("State", 3);
                SpitAttack();
                break;
        }
    }

    private void Die()
    {
        state = SerpopardState.Idle;
        anim.SetTrigger("Dead");
        //Death animation / fade to white & screen shake or something
        Destroy(gameObject, 1.5f);
    }
}