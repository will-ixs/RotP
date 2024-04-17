using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunit_Walk : StateMachineBehaviour
{

    public float speed = 2.5f;
	public float attackRange = 2.6f;
    public Vector2 directionToPlayer;
    private float dashCooldown = 3f;
    private float dashDuration = 0.5f;
    private bool dash;

	Transform player;
	Rigidbody2D rb;
	AmmunitBehavior boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	    rb = animator.GetComponent<Rigidbody2D>();
		boss = animator.GetComponent<AmmunitBehavior>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
        //boss.CheckSpriteFlip();
        Vector2 vecToPlayer = player.transform.position - rb.transform.position;
        //directionToPlayer = vecToPlayer.normalized;
		rb.position = Vector3.MoveTowards(rb.position, player.position, speed * Time.deltaTime);
		//rb.MovePosition(newPos);

		if (vecToPlayer.magnitude <= attackRange)
		{  
			animator.SetTrigger("Slam");
		}
        if (vecToPlayer.magnitude >= 5 && dashCooldown < 0){
            dashCooldown = 3;
            dash = true;
            dashDuration = 0.5f;
            //animator.SetTrigger("Dash");
        }

        if(dash)
        {
            speed = 20;

        }
        if(dashDuration < 0)
        {
            speed = 2.5f;
            dash = false;
        }
        dashDuration -=  Time.deltaTime;
        dashCooldown -= Time.deltaTime;
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Slam");
        animator.ResetTrigger("Dash");
       
    }

    

}
