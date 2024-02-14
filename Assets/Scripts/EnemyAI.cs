using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private GameObject player;
    public float speed;
    private Vector2 velocity;
    public float detectionRadius;
    public Enemy enemy;

    public int dmg;
    public float dmgCD;

    private float knockbackSpeed;
    private float staggeredTime;

    private float distance;
    private Rigidbody2D rb;

    private int moveDirection;  // 0 -> right, 1 -> down, 2 -> left, 3 -> up
    private float moveDuration;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }
    void ChooseRandomDestination()
    {
        moveDirection = Random.Range(0, 4);
        moveDuration = Random.Range(1, 10);
    }

    public void Knockback(float speed, float time)
    {
        knockbackSpeed = Mathf.Max(knockbackSpeed * Mathf.Exp(-staggeredTime), speed);
        staggeredTime = Mathf.Max(staggeredTime, time);
    }

    // Update is called once per frame
    void Update()
    {


        if (enemy.Dead)
        {
            GetComponent<BoxCollider2D>().enabled = false;
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
            rb.velocity = new Vector2(0.0f, 0.0f);
        }
        else
        {
            Vector3 vToPlayer = player.transform.position - transform.position;
            distance = vToPlayer.magnitude;
            Vector3 directionToPlayer = vToPlayer.normalized;

            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            Vector2 targetDirection = directionToPlayer;
            if (distance > detectionRadius)
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
                staggeredTime -= Time.deltaTime;
                if (staggeredTime <= 0.0f)
                {
                    rb.velocity = Vector2.SmoothDamp(rb.velocity, speed * targetDirection, ref velocity, 0.05f);
                    //rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
                    //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
                }
                else
                {
                    rb.velocity = Vector2.SmoothDamp(rb.velocity, -knockbackSpeed * targetDirection, ref velocity, 0.05f);
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
