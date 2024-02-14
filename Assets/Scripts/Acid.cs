using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Acid : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private int dmg;
    [SerializeField] private float dmgCooldown;
    private float useCooldown;
    // Start is called before the first frame update
    void Start()
    {
        useCooldown = 0.0f;
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        useCooldown -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        transform.position += -transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && useCooldown <= 0.0f)
        {
            useCooldown = dmgCooldown;
            collision.GetComponent<PlayerHealth>().updatePlayerHealth(-dmg);

        }
    }
}
