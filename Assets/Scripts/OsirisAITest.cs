using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsirisAITest : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float damage;
    public float speed;
    private Vector2 velocity;
    //[SerializeField] private Vector2 dir;
    [SerializeField] private float distance;

    private List<Vector3> playerPos;

    private Rigidbody2D rb;
    



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        playerPos = new List <Vector3>();
        playerPos.Add(player.transform.position);
        InvokeRepeating("testt", 1f, 1f);
        //rb.velocity = Vector2.zero;
        

    }

    // Update is called once per frame
    void Update()
    {
        //Physics2D.gravity = Vector2.up * gravity;
 
        //rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        //Debug.Log(rb.velocity);
        //Vector2 test = Vector2.zero;
       
        
    }

    private void FixedUpdate()
    {

            Vector3 vToPlayer = playerPos[0] - transform.position;
            distance = vToPlayer.magnitude;

            while(distance < 1)
            {
                playerPos.RemoveAt(0);
                vToPlayer = playerPos[0] - transform.position;
                distance = vToPlayer.magnitude;
            }
            


            Vector3 directionToPlayer = vToPlayer.normalized;

            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            Vector2 targetDirection = directionToPlayer;

            rb.velocity = Vector2.SmoothDamp(rb.velocity, speed * targetDirection, ref velocity, 0.05f);
            

           
    }

    private void testt()
    {
        playerPos.Add(player.transform.position);
        Debug.Log(playerPos.Count);
    }
    private void OnCollisionStay2D(Collision2D collision) 
    {

        PlayerHealth playerhealth = collision.gameObject.GetComponent<PlayerHealth>();

        if(playerhealth != null)
        {
            playerhealth.updatePlayerHealth(-100);
        }



    }
    
}
