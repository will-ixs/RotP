using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float curhealth;

    private Animator anim;
    private MovementController _movement_controller;
    private Vector2 movementDirection;
    private Vector2 velocity;
    public Vector2 dir;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        _movement_controller = gameObject.GetComponent<MovementController>();
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

        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
        movementDirection = movementDirection.normalized;
        _movement_controller.changeVelocity(movementDirection * moveSpeed);
    }

    private void SetDirection()
    {
        //0 = UP, 1 = LEFT, 2 = DOWN, 3 = RIGHT
        dir = movementDirection;
        Vector2 mag = new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
        anim.SetFloat("Speed", 1.0f);
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
