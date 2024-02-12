using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    private float timeUntilMovementAvailable;

    [SerializeField] private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 velocity;

    void Start()
    {
        timeUntilMovementAvailable = 0.0f;
    }

    public void DisableMovement(float duration)
    {
        timeUntilMovementAvailable = Mathf.Max(timeUntilMovementAvailable, duration);
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilMovementAvailable -= Time.deltaTime;
        if (timeUntilMovementAvailable <= 0.0f)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            rb.velocity = Vector2.SmoothDamp(rb.velocity, input.normalized * moveSpeed, ref velocity, 0.05f);
        }
    }
}
