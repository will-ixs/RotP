using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float curhealth;

    private float timeUntilMovementAvailable;

    private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 velocity;
    public Vector2 dir;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        timeUntilMovementAvailable = 0.0f;
    }

    public void DisableMovement(float duration)
    {
        timeUntilMovementAvailable = Mathf.Max(timeUntilMovementAvailable, duration);
    }

    // Update is called once per frame
    void Update()
    {
        SetDirection();
        curhealth = gameObject.GetComponent<PlayerHealth>().curHealth;
        
        if(curhealth / 34f >= 2)
            moveSpeed = 10.0f;
        else if(curhealth / 34f >= 1)
            moveSpeed = 6.0f;
        else
            moveSpeed = 3.0f;

        timeUntilMovementAvailable -= Time.deltaTime;
        if (timeUntilMovementAvailable <= 0.0f)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            rb.velocity = Vector2.SmoothDamp(rb.velocity, input.normalized * moveSpeed, ref velocity, 0.05f);
        }
    }

    private void SetDirection()
    {
        //0 = UP, 1 = LEFT, 2 = DOWN, 3 = RIGHT
        dir = rb.velocity;
        dir.Normalize();
        Vector2 mag = new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
        anim.SetFloat("Speed", mag.magnitude);
        if(mag.magnitude > 0.1f)
        {
            anim.SetBool("Siphon", false);
        }
        if (mag.y < mag.x)
        {
            //LookRight/Left
            if (dir.x < 0.0f)
            {
                anim.SetInteger("Direction", 1);
            }
            else
            {
                anim.SetInteger("Direction", 3);
            }
        }
        else
        {
            //LookUp/Down
            if (dir.y < 0.0f)
            {
                anim.SetInteger("Direction", 2);
            }
            else
            {
                anim.SetInteger("Direction", 0);
            }

        }
    }
}
