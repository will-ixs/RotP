using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WalkDirection
{
    Left = 1, Right = 3, Up = 0, Down = 2,
}

public enum Attack
{
    NoAttack, LightAttack, HeavyAttack
}

public class PlayerAnimationManager : MonoBehaviour
{
    public WalkDirection facingDirection;
    public float speed;
    public Attack attackTrigger;

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetFloat("Speed", speed);
        _anim.SetBool("Siphon", false);
        _anim.SetInteger("Direction", (int)facingDirection);
        switch (attackTrigger)
        {
            case Attack.LightAttack:
                _anim.SetTrigger("LightAttack");
                break;
            case Attack.HeavyAttack:
                _anim.SetTrigger("HeavyAttack");
                break;
        }
    }
}
