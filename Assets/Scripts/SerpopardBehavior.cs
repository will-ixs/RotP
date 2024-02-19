using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpopardBehavior : MonoBehaviour
{
    private BossHealth bossHealth;
    private float stateChangeTimer;
    private GameObject player;
    private Animator anim;
    [SerializeField] private float moveSpeed;
    [SerializeField] private SerpopardSwipe serpSwipe;
    [SerializeField] private SerpopardSpit serpSpit;
    [SerializeField] private Rigidbody2D rb;
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
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (bossHealth.Dead)
        {
            Die();
        }
        //End attacks, choose new state that is not the same as the previous.
        stateChangeTimer -= Time.deltaTime;
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
    private void ActIdle()
    {
        rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, 1f * Time.deltaTime);
    }
    private void MoveAtPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
    private void SwipeAttack()
    {

        rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, 1f * Time.deltaTime);
        serpSwipe.Enable();
        if(stateChangeTimer < 1.0f)
        {
            serpSwipe.Hit();
        }
    }

    private void SpitAttack()
    {
        rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, 1f * Time.deltaTime);
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

    private void Die()
    {
        anim.SetTrigger("Dead");
        //Death animation / fade to white & screen shake or something
        Destroy(gameObject, 1.5f);
    }
}