using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PharaohStateManager : MonoBehaviour
{
    private float stateChangeTimer;
    private BossHealth bossHealth;
    private GameObject player;
    private Animator anim;
    private Rigidbody2D rb;


    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private float moveSpeed;
    public enum PharaohPhase
    {
       Phase1,
       Phase2,
       Phase3,
    }
    public PharaohPhase phase;
    public enum PharaohState
    {
        Idle,
        Walk,
        Sword,
        Beam,
        Hand,
        Waiting
    }
    public PharaohState state;

    void Start()
    {
        stateChangeTimer = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        bossHealth = GetComponent<BossHealth>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        stateChangeTimer -= Time.deltaTime;
        if (bossHealth.Dead)
        {
            if(phase == PharaohPhase.Phase3)
            {
                Die();
            }
            else
            {
                AdvancePhase();
            }
        }
        else
        {
            ChooseNextState();
            ActivateState();
        }
    }

    private void ChooseNextState()
    {
        //If attack timer still going skip.
        if (stateChangeTimer <= 0.0f)
        {
            //Disable active Attacks
            switch (state)
            {
                case PharaohState.Walk:
                    anim.SetTrigger("ReturnIdle");
                    break;
            }

            //Set available attacks depending upon phase
            int maxRange = 4;
            if(phase == PharaohPhase.Phase2)
            {
                maxRange = 5; 
            }else if(phase == PharaohPhase.Phase3)
            {
                maxRange = 6;
            }

            //Choose new state that is not the same as the current state
            PharaohState currState = state;
            while (currState == state)
            {
                int selection = Random.Range(1, maxRange); //[1,4)
                switch (selection)
                {
                    case 1:
                        state = PharaohState.Idle;
                        stateChangeTimer = 2.0f;
                        break;
                    case 2:
                        state = PharaohState.Walk;
                        stateChangeTimer = 1.5f;
                        break;
                    case 3:
                        state = PharaohState.Sword;
                        stateChangeTimer = 4.0f;
                        break;
                    case 4:
                        state = PharaohState.Beam;
                        stateChangeTimer = 5.0f;
                        break;
                    case 5:
                        state = PharaohState.Hand;
                        stateChangeTimer = 3.0f;
                        break;
                }
            }
        }
    }

    private void ActivateState()
    {
        //Activate new state.
        /*Idle,0
        Walk,1
        Sword,2
        Beam,3
        Hand,4
        Waiting5*/
        switch (state)
        {
            case PharaohState.Idle:
                break;
            case PharaohState.Walk:
                anim.SetTrigger("Walk");
                Walk();
                break;
            case PharaohState.Sword:
                anim.SetTrigger("Sword");
                break;
            case PharaohState.Beam:
                anim.SetTrigger("Beam");
                break;
            case PharaohState.Hand:
                anim.SetTrigger("Hand");
                break;
        }
    }
    private void AdvancePhase()
    {
        if(phase == PharaohPhase.Phase1)
        {
            //Make Pharaoh Move to next room where it waits to be activated.
            phase = PharaohPhase.Phase2;
            state = PharaohState.Waiting;
            bossHealth.health = 75;
            bossHealth.Dead = false;
        }
        if(phase == PharaohPhase.Phase2)
        {
            //Make Pharaoh Move to next room where it waits to be activated.
            phase = PharaohPhase.Phase3;
            state = PharaohState.Waiting;
            bossHealth.health = 100;
            bossHealth.Dead = false;
        }
    }
    private void Die()
    {
        state = PharaohState.Idle;
        anim.SetTrigger("Dead");
        //Death animation / fade to white & screen shake or something
        Destroy(gameObject, 1.5f);
    }

    private void Walk()
    {
        Vector2 vecToPlayer = player.transform.position - transform.position;
        if (vecToPlayer.magnitude > 2.8f)
        {
            //_movement_controller.changeVelocity(moveSpeed * vecToPlayer.normalized);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    public void SpawnSword()
    {
        Instantiate(swordPrefab);
    }
}
