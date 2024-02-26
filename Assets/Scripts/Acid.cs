using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Acid : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private int dmg;
    [SerializeField] private float dmgCooldown;
    [SerializeField] private float destroyTime;
    private Animator anim;
    private float useCooldown;
    private float lifeTime;
    private int splat;
    // Start is called before the first frame update
    void Start()
    {
        splat = 0;
        useCooldown = 0.0f;
        Destroy(gameObject, destroyTime);
        lifeTime = destroyTime;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime < 0.5f)
        {
            switch (splat)
            {
                case 0:

                    splat = Random.Range(1, 3);
                    break;
                case 1:
                    anim.SetTrigger("Splat");
                    break;
                case 2:
                    anim.SetTrigger("Splat2");
                    break;
            }
        }
        lifeTime -= Time.deltaTime;
        useCooldown -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if(lifeTime > 0.5f)
        {
            transform.position += -transform.up * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && useCooldown <= 0.0f)
        {
            lifeTime = 0.5f;
            //useCooldown = dmgCooldown;
            collision.GetComponent<PlayerHealth>().updatePlayerHealth(-dmg);
        }
    }
}
