using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsirisAITest : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float speed;
    private float distance;
    private Vector2 velocity;

    private List<Vector3> playerPos;

    private Rigidbody2D rb;
    



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        playerPos = new List <Vector3>();
        playerPos.Add(player.transform.position);
        InvokeRepeating("playerPathing", 1f, 1f);
        //rb.velocity = Vector2.zero;
        

    }

    // Update is called once per frame
    void Update()
    {
             
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

    private void playerPathing()
    {
        playerPos.Add(player.transform.position);
    }

    private void OnCollisionStay2D(Collision2D collision) 
    {

        PlayerHealth playerhealth = collision.gameObject.GetComponent<PlayerHealth>();

        if(playerhealth != null)
        {
            playerhealth.updatePlayerHealth(-1);
        }


    }
    
}
