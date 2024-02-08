using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public float distanceBW;
    public Enemy enemy;

    public int dmg;
    public float dmgCD;

    private float distance;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        

        if(enemy.Dead)
            Destroy(gameObject, 1.0f);
        //Move towards player
        //Attack when in range
        //Check health and enter death state if below 1
        //  In death state: Spawn Soul, Change animator state to dead, wait X seconds before fading away, desotry self and soul, if soul collected also destroy self
        dmgCD -= Time.deltaTime;

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        

        if(distance < distanceBW) {

            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) 
    {

        PlayerHealth playerhealth = collision.gameObject.GetComponent<PlayerHealth>();
        if(dmgCD <= 0) {
            if(playerhealth != null)
            {
                playerhealth.TakeDamage(dmg);
            }
        }
        dmgCD = 1f;


    }
}
