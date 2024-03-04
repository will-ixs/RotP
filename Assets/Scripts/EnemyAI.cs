using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    private GameObject player;
    public float speed;
    private Vector2 velocity;
    public float detectionRadius;
    public Enemy enemy;

    public int dmg;
    public float dmgCD;

    [SerializeField] private NavMeshAgent agent;

    private Vector3 knockbackDirection;
    private float knockbackSpeed;
    private float staggeredTime;

    private float distance;
    private MovementController _movement_controller;

    private int moveDirection;  // 0 -> right, 1 -> down, 2 -> left, 3 -> up
    private float moveDuration;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _movement_controller = gameObject.GetComponent<MovementController>();

        agent.updatePosition = false;
        agent.speed = speed;
        agent.angularSpeed = float.MaxValue;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void ChooseRandomDestination()
    {
        moveDirection = Random.Range(0, 4);
        moveDuration = Random.Range(1, 10);
    }

    public void Knockback(Vector3 direction, float speed, float time)
    {
        knockbackDirection = direction;
        knockbackSpeed = Mathf.Max(knockbackSpeed * Mathf.Exp(-staggeredTime), speed);
        staggeredTime = Mathf.Max(staggeredTime, time);
    }

    // Update is called once per frame
    void Update()
    {


        if (enemy.Dead)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            agent.ResetPath();
            Destroy(gameObject, 1.0f);
        }
        //Move towards player
        //Attack when in range
        //Check health and enter death state if below 1
        //  In death state: Spawn Soul, Change animator state to dead, wait X seconds before fading away, desotry self and soul, if soul collected also destroy self
        dmgCD -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (enemy.Dead)
        {
            _movement_controller.disableMovement(1.0f);
            _movement_controller.changeVelocity(new Vector2(0.0f, 0.0f));
        }
        else
        {
            Vector3 vToPlayer = player.transform.position - transform.position;
            distance = vToPlayer.magnitude;
            Vector3 directionToPlayer = vToPlayer.normalized;

            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            Vector2 targetDirection;
            bool wander = false;
            if (wander && distance > detectionRadius)
            {
                moveDuration -= Time.deltaTime;
                if (moveDuration <= 0.0f)
                {
                    ChooseRandomDestination();
                }
                switch (moveDirection)
                {
                    case 0:
                        targetDirection = new Vector2(1.0f, 0.0f);
                        break;
                    case 1:
                        targetDirection = new Vector2(0.0f, 1.0f);
                        break;
                    case 2:
                        targetDirection = new Vector2(-1.0f, 0.0f);
                        break;
                    case 3:
                        targetDirection = new Vector2(0.0f, -1.0f);
                        break;
                    default:
                        targetDirection = new Vector2(0.0f, 0.0f);
                        break;
                }
            }
            else
            {
                agent.destination = player.GetComponent<CircleCollider2D>().transform.position;
                targetDirection = (agent.steeringTarget - transform.position).normalized;
                staggeredTime -= Time.deltaTime;
                if (staggeredTime <= 0.0f)
                {
                    _movement_controller.changeVelocity(speed * targetDirection);
                }
                else
                {
                    
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision) 
    {

        PlayerHealth playerhealth = collision.gameObject.GetComponent<PlayerHealth>();
        if(dmgCD <= 0) {
            if(playerhealth != null)
            {
                playerhealth.updatePlayerHealth(-dmg);
            }
        }
        dmgCD = 1f;


    }
}
