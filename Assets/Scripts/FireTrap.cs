using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private int EnemyDamage;

    [Header("Trap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
     private Animator anim;
     private SpriteRenderer spriteRend;

     private bool dmgCD;
     private bool isPlayer;
     private AudioManager audioManager;
    private PlayerHealth playerhealth;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        dmgCD = true;
        isPlayer = false;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        StartCoroutine(ActivateTrap());
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
            playerhealth = collision.gameObject.GetComponent<PlayerHealth>();
        }

        
        
    }
    private void doDamage()
    {
        if(dmgCD && isPlayer)
            {
                if (playerhealth != null)
                {
                    playerhealth.updatePlayerHealth(-damage);
                }
                dmgCD = false;

            }
    }

    private void fireSound() 
	{
        Vector3 vToPlayer = player.transform.position - transform.position;
        float distance = vToPlayer.magnitude;
        if(distance < 3f)
        {
            audioManager.playSFX(audioManager.Extra);
        }

    }
    private IEnumerator ActivateTrap()
    {
        while(true)
        {
        anim.SetBool("activated", true);
        yield return new WaitForSeconds(activationDelay);
        doDamage();
        //fireSound();

        yield return new WaitForSeconds(activeTime);
        dmgCD = true;
        isPlayer = false;
        anim.SetBool("activated", false);
        }
    }
}
