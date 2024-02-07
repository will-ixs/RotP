using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    [SerializeField] private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 velocity;

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        rb.velocity = Vector2.SmoothDamp(rb.velocity, input.normalized * moveSpeed, ref velocity, 0.05f);
    }
}