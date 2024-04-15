using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsirisAITest : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float speed;
    private float distance;
    private Vector2 direction;
    private Vector2 velocity;

    private List<Vector3> playerPos;

    private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    private float timer;
    public bool spawned;
    private float savedspeed;
    



    // Start is called before the first frame update
    void Start()
    {
        //savedspeed = speed;
        spawned = false;
        
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        playerPos = new List <Vector3>();
        playerPos.Add(player.transform.position);
        InvokeRepeating("playerPathing", 1f, 1f);
        anim = GetComponent<Animator>();
        timer = 2;
        Spawn();
        //rb.velocity = Vector2.zero;
        

    }

    // Update is called once per frame
    void Update()
    {
  
        SetDirection();
        timer -= Time.deltaTime;
        
    }

    private void FixedUpdate()
    {
        if(spawned) {
            Vector3 vToPlayer = playerPos[0] - transform.position;
            distance = vToPlayer.magnitude;

            if(distance < 1 || timer <= 0)
            {
                playerPos.RemoveAt(0);
                timer = 2f;
                if (playerPos.Count < 1)
                {
                    playerPos.Add(player.transform.position);
                }

                vToPlayer = playerPos[0] - transform.position;
                distance = vToPlayer.magnitude;
            }
            

            Vector3 directionToPlayer = vToPlayer.normalized;

            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            Vector2 targetDirection = directionToPlayer;

            rb.velocity = Vector2.SmoothDamp(rb.velocity, speed * targetDirection, ref velocity, 0.05f);
        }

        

           
    }
    public void Spawn()
    {

        anim.SetTrigger("Spawn");
       
    }
    public void Spawned()
    {
        anim.SetTrigger("Spawned");
        spawned = true;
        //speed = savedspeed;
    }

    public void Despawn()
    {
        spawned = false;
        anim.SetTrigger("Despawn");
    }

    private void SetDirection()
    {
        //0 = UP, 1 = LEFT, 2 = DOWN, 3 = RIGHT
        direction = playerPos[0] - transform.position;
        direction.Normalize();
        Vector2 mag = new Vector2(Mathf.Abs(direction.x), Mathf.Abs(direction.y));

        if (mag.y > mag.x)
        {
            //LookUp/Down
            if(direction.y > 0.0f)
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
            if (direction.x < 0.0f)
            {
                anim.SetInteger("Direction", 1);
            }
            else
            {
                anim.SetInteger("Direction", 3);
            }
        }
    }

    private void playerPathing()
    {
        playerPos.Add(player.transform.position);
    }


    private void OnCollisionStay2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("8");
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            if(health != null) {
                health.Death();
                
            }
        }
        else if (collision.gameObject.CompareTag("Boss"))
            {
                BossHealth hp = collision.gameObject.GetComponent<BossHealth>();
                hp.TakeDamage(20);
            }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy hp = collision.gameObject.GetComponent<Enemy>();
            hp.TakeDamage(10);
        }
    }

    public void increment()
    {

    }


    
}
