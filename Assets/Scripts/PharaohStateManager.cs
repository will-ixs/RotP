using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PharaohStateManager : MonoBehaviour
{
    private float stateChangeTimer;
    private BossHealth bossHealth;
    private GameObject player;
    private Animator anim;

    [SerializeField] private Vector3 WaitingRoom1;
    [SerializeField] private Vector3 WaitingRoom2;


    [SerializeField] private GameObject kaFragmentPrefab;
    [SerializeField] private GameObject beamPrefab;
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private float moveSpeed;

    public bool waiting;
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
        Throw,
        Beam,
        Slash,
    }
    public PharaohState state;

    void Start()
    {
        waiting = false;
        stateChangeTimer = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        bossHealth = GetComponent<BossHealth>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(state == PharaohState.Walk)
        {
            Walk();
        }
    }

    // Update is called once per frame
    void Update()
    {
        stateChangeTimer -= Time.deltaTime;

        //move to next waiting area if necessary
        if(waiting)
        {
            stateChangeTimer = 5.0f;
            anim.SetTrigger("Walk");

            Vector3 destination = Vector3.zero;
            if (phase == PharaohPhase.Phase2)
            {
                destination = WaitingRoom1;
            }
            if(phase == PharaohPhase.Phase3)
            {
                destination = WaitingRoom2;
            }
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        }

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
        if (stateChangeTimer <= 0.0f && !waiting)
        {
            //Disable active Attacks
            switch (state)
            {
                case PharaohState.Idle:
                    break;
                case PharaohState.Walk:
                    anim.SetTrigger("Idle");
                    break;
                case PharaohState.Throw:
                    anim.SetTrigger("Idle");
                    break;
                case PharaohState.Slash:
                    anim.SetTrigger("Idle");
                    break;
                case PharaohState.Beam:
                    anim.SetTrigger("Idle");
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
                int selection = Random.Range(2, maxRange); //[1,4)
                switch (selection)
                {
                    case 1:
                        state = PharaohState.Idle;
                        stateChangeTimer = 2.0f;
                        break;
                    case 2:
                        state = PharaohState.Walk;
                        stateChangeTimer = 3.75f;
                        break;
                    case 3:
                        state = PharaohState.Throw;
                        stateChangeTimer = 3.0f;
                        break;
                    case 4:
                        state = PharaohState.Slash;
                        stateChangeTimer = 2.0f;
                        break;
                    case 5:
                        state = PharaohState.Beam;
                        stateChangeTimer = 5.0f;
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
        Throw,2
        Beam,3
        Slash,4*/
        if (!waiting)
        {
            switch (state)
            {
                case PharaohState.Idle:
                    anim.SetTrigger("Idle");
                    break;
                case PharaohState.Walk:
                    anim.SetTrigger("Walk");
                    break;
                case PharaohState.Slash:
                    anim.SetTrigger("Slash");
                    break;
                case PharaohState.Beam:
                    anim.SetTrigger("Beam");
                    break;
                case PharaohState.Throw:
                    anim.SetTrigger("Throw");
                    break;
            }
        }
    }
    private void AdvancePhase()
    {
        if(phase == PharaohPhase.Phase1)
        {
            anim.SetTrigger("Idle");
            //Make Pharaoh Move to next room where it waits to be activated.
            state = PharaohState.Idle;
            phase = PharaohPhase.Phase2;
            waiting = true;
            bossHealth.BossHealthBar.GetComponent<BossHealthUI>().slider.maxValue = 150;
            bossHealth.health = 150;
            bossHealth.Dead = false; 
            KaFragment kaFragment = Instantiate(kaFragmentPrefab, gameObject.transform.position, Quaternion.identity).GetComponent<KaFragment>();
            kaFragment.initialAmount = 50;
            kaFragment.decayRate = 1.0f;
        }
        else if(phase == PharaohPhase.Phase2)
        {
            state = PharaohState.Idle;
            anim.SetTrigger("Idle");
            //Make Pharaoh Move to next room where it waits to be activated.
            phase = PharaohPhase.Phase3;
            waiting = true;
            bossHealth.BossHealthBar.GetComponent<BossHealthUI>().slider.maxValue = 250;
            bossHealth.health = 250;
            bossHealth.Dead = false; 
            KaFragment kaFragment = Instantiate(kaFragmentPrefab, gameObject.transform.position, Quaternion.identity).GetComponent<KaFragment>();
            kaFragment.initialAmount = 150;
            kaFragment.decayRate = 1.0f;
        }
    }
    private void Die()
    {
        state = PharaohState.Idle;
        anim.SetTrigger("Die");
        //Death animation / fade to white & screen shake or something
        Destroy(gameObject, 1.5f);
    }
    private void Walk()
    {
        Vector2 vecToPlayer = player.transform.position - transform.position;
        if (vecToPlayer.magnitude > 2.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0.0f, 1.5f, 0.0f), moveSpeed * Time.deltaTime);
        }
        else if(stateChangeTimer < 3.0f && phase != PharaohPhase.Phase1)
        {
            state = PharaohState.Slash;
            stateChangeTimer = 2.0f;
            anim.SetTrigger("Idle");
            ActivateState();
        }
    }
    public void SpawnSword()
    {
        Instantiate(swordPrefab);
    }
    public void SpawnSlash()
    {
        GameObject slash = Instantiate(slashPrefab);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x);

        Vector3 hitbox_position = transform.position;
        hitbox_position.x += Mathf.Cos(angle);
        hitbox_position.y += Mathf.Sin(angle);

        float hitbox_rotation = angle * Mathf.Rad2Deg + 90.0f;

        slash.transform.position = hitbox_position;
        slash.transform.rotation = Quaternion.Euler(0, 0, hitbox_rotation);
        Destroy(slash, 2.0f);
    }
    public void SpawnBeam()
    {
        GameObject beams = Instantiate(beamPrefab);
        beams.transform.position = transform.position;
        Destroy(beams, 4.01f);
    }
    public void ShortIdle() //call at end of each animation to give .5s idle buffer
    {
        stateChangeTimer = 0.5f;
        anim.SetTrigger("Idle");
        state = PharaohState.Idle;
    }
    public void StopWaiting()
    {
        waiting = false;
        anim.SetTrigger("Idle");
        state = PharaohState.Idle;
        stateChangeTimer = 0.5f;
    } //call from room triggers for phase 2 and 3
}
