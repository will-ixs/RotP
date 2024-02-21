using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsirisAI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float damage;
    //[SerializeField] private Vector2 dir;
    [SerializeField] private float distance;
    [SerializeField] private float gravity = 100f;
    [SerializeField] private float velocity = 1000f;

    private Rigidbody2D rb;
    



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        InvokeRepeating("testt", 1f, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        //Physics2D.gravity = Vector2.up * gravity;
 
        //rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        //Debug.Log(rb.velocity);
        //Vector2 test = Vector2.zero;
        if(rb.velocity == Vector2.zero) {
       
            rb.velocity = new Vector2(0, 0 * Time.deltaTime);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
 
        //rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        
        /* else if(Input.GetKey(KeyCode.S)) {
            Physics2D.gravity = Vector2.down * gravity;
 
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        } else if(Input.GetKey(KeyCode.A)) {
            Physics2D.gravity = Vector2.left * gravity;
 
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        } else if(Input.GetKey(KeyCode.D)) {
            Physics2D.gravity = Vector2.right * gravity;
 
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        } */
    }
        
    }

    private void FixedUpdate()
    {
            distance = Vector3.Distance(transform.position, player.transform.position);

            Vector3 direction = player.transform.position - transform.position;
           // direction.Normalize();
            float directedVelocity = velocity;
            if(rb.velocity == Vector2.zero) 
            {
                if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    if(direction.x < 0)
                        directedVelocity = velocity * -1.0f;
                    else
                        directedVelocity = velocity;
                    rb.velocity = new Vector2(directedVelocity * Time.deltaTime, 0);
                }
                else
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    if(direction.y < 0)
                        directedVelocity = velocity * -1.0f;
                    else
                        directedVelocity = velocity;
                    rb.velocity = new Vector2(0, directedVelocity * Time.deltaTime);
                }
            }
            

           
    }

    private void testt()
    {
            distance = Vector3.Distance(transform.position, player.transform.position);

            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            Debug.Log(direction);

           
    }
    
}
