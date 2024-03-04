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
    private Animator anim;
    
    



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        playerPos = new List <Vector3>();
        playerPos.Add(player.transform.position);
        InvokeRepeating("playerPathing", 1f, 1f);
        anim = GetComponent<Animator>();
        //rb.velocity = Vector2.zero;
        

    }

    // Update is called once per frame
    void Update()
    {
        SetDirection();
    }

    private void FixedUpdate()
    {

        Vector3 vToPlayer = playerPos[0] - transform.position;
        distance = vToPlayer.magnitude;

        if(distance < 1)
        {
            playerPos.RemoveAt(0);
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
                Destroy(this.gameObject);
                Destroy(player);
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


    
}
