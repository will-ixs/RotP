using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantAI : MonoBehaviour
{
    public float detectionRadius;
    private GameObject _player;

    private Vector2 velocity;

    public float walkSpeed;
    public float runSpeed;
    private int moveDirection;  // 0 -> right, 1 -> down, 2 -> left, 3 -> up
    private float moveDuration;

    public float anxietyTime;
    float anxietyLevel;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        anxietyLevel = 0.0f;
        ChooseRandomDestination();
    }

    void ChooseRandomDestination()
    {
        moveDirection = Random.Range(0, 4);
        moveDuration = Random.Range(1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        anxietyLevel -= Time.deltaTime;
        Vector3 vecToPlayer = _player.transform.position - gameObject.GetComponent<Transform>().position;
        float distanceToPlayer = vecToPlayer.magnitude;
        if (distanceToPlayer <= detectionRadius)
        {
            anxietyLevel = anxietyTime;
        }

        float moveSpeed = Mathf.Lerp(walkSpeed, runSpeed, anxietyLevel / anxietyTime);

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        Vector2 targetDirection = new Vector2(0.0f, 0.0f);
        if (anxietyLevel <= 0.0f)
        {
            moveDuration -= Time.deltaTime;
            if (moveDuration <= 0.0f)
            {
                ChooseRandomDestination();
            }
            switch (moveDirection)
            {
                case 0:
                    targetDirection = new Vector2(1.0f, 0.0f);
                    break;
                case 1:
                    targetDirection = new Vector2(0.0f, 1.0f);
                    break;
                case 2:
                    targetDirection = new Vector2(-1.0f, 0.0f);
                    break;
                case 3:
                    targetDirection = new Vector2(0.0f, -1.0f);
                    break;
                case 4:
                    targetDirection = new Vector2(0.0f, 0.0f);
                    break;
            }
        } 
        else
        {
            targetDirection = -vecToPlayer.normalized;
        }
        rb.velocity = Vector2.SmoothDamp(rb.velocity, moveSpeed * targetDirection, ref velocity, 0.05f);
    }
}
