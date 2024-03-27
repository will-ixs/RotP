using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private int EnemyDamage;

    [Header("Trap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
     private Animator anim;
     private SpriteRenderer spriteRend;

     private bool triggered;
     private bool active;
     private bool dmgCD;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        dmgCD = true;
        
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(!triggered)
            {
                StartCoroutine(ActivateTrap());
            }

            if(active && dmgCD)
            {
                PlayerHealth playerhealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerhealth != null)
                {
                    playerhealth.updatePlayerHealth(-damage);
                }
                dmgCD = false;

            }
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(!triggered)
            {
                StartCoroutine(ActivateTrap());
            }

            if(active && dmgCD)
            {
                Enemy hp = collision.gameObject.GetComponent<Enemy>();
                if (hp != null)
                {
                    
                    hp.TakeDamage(EnemyDamage);
                }
                dmgCD = false;

            }

        }
        
        
    }

    private IEnumerator ActivateTrap()
    {
        triggered = true;
        anim.SetBool("activated", true);
        yield return new WaitForSeconds(activationDelay);
        //spriteRend.color = Color.white;
        active = true;
        

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        dmgCD = true;
        anim.SetBool("activated", false);
    }
}
