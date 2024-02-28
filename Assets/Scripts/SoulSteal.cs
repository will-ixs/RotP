using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSteal : MonoBehaviour
{
    public float movementCooldown;
    private Animator anim;
    private bool siphoning;
    private float siphonCountdown = 0.5f;
    private float useCountdown = 0.0f;

    public List<GameObject> nearby_ka_fragments = new List<GameObject>();
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("KaFragment"))
        {
            nearby_ka_fragments.Add(col.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        nearby_ka_fragments.Remove(col.gameObject);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (siphoning)
        {
            useCountdown += Time.deltaTime;
            if(useCountdown > siphonCountdown)
            {
                anim.SetBool("Siphon", false);
                useCountdown = 0.0f;
                siphoning = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(nearby_ka_fragments.Count > 0)
            {
                anim.SetBool("Siphon", true);
                siphoning = true;
                for (int i = 0; i < nearby_ka_fragments.Count; i++)
                {
                    gameObject.GetComponent<PlayerHealth>().updatePlayerHealth(nearby_ka_fragments[i].GetComponent<KaFragment>().amount);
                    Destroy(nearby_ka_fragments[i]);
                }
                gameObject.GetComponent<PlayerMovement>().DisableMovement(movementCooldown);
            }
        }
    }
}
