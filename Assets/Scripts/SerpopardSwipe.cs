using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpopardSwipe : MonoBehaviour
{
    private GameObject player;
    private bool playerHit = false;
    private bool hitboxActive = false;
    private float dmg = 34.0f;
    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Disable()
    {
        hitboxActive = false;
        anim.SetBool("Swipe", false);
        playerHit = false;
    }

    public void Enable()
    {
        //SR on and play animation
        //Activate hitbox
        hitboxActive = true;
        anim.SetBool("Swipe", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player enters hitbox, and has not already been hit by this swipe.
        if (other.CompareTag("Player") && !playerHit && hitboxActive)
        {
            //Take away player HP
            PlayerHealth playerhealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerhealth != null)
            {
                playerhealth.updatePlayerHealth(-dmg);
            }
            //Mark player as hit for this iteration.
            playerHit =true;
        }
    }
}
