using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float movementForce;

    private Timer _t;
    private Timer _slowTimer;

    private Rigidbody2D _rb;

    private Vector2 _target_velocity;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _t = gameObject.AddComponent<Timer>();
        _slowTimer = gameObject.AddComponent<Timer>();
        _target_velocity = new Vector2(0, 0);
    }

    public void knockback(Vector2 direction, float impulse)
    {
        _rb.AddForce(direction * impulse, ForceMode2D.Impulse);
    }

    public void disableMovement(float duration)
    {
        _t.begin(duration);
    }
    public void slowMovement(float duration)
    {
        _slowTimer.begin(duration);
    }

    public void changeVelocity(Vector2 target_velocity)
    {
        _target_velocity = target_velocity;
    }

    void Update()
    {
        if (_t.isReady())
        {
            Vector2 force_direction = (_target_velocity - _rb.velocity).normalized;
            _rb.AddForce(movementForce * force_direction, ForceMode2D.Impulse);
            float new_speed = _rb.velocity.magnitude;
            float target_speed = _target_velocity.magnitude;
            if (new_speed > target_speed)
            {
                _rb.velocity *= target_speed / new_speed;
            }
            if (!_slowTimer.isReady())
            {
                _rb.velocity *= 0.75f;
            }
        }
    }
}
