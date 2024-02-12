using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private GameObject player;
    public float speed;
    public float distanceBW;
    public Enemy enemy;


    public int dmg;
    public float dmgCD;

    private float distance;
    private Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        if (enemy.Dead)
        {
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
        if (!enemy.Dead) { 

            distance = Vector3.Distance(transform.position, player.transform.position);

            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (distance < distanceBW)
            {

                rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
                //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
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
