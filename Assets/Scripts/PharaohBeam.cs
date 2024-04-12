using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharaohBeam : MonoBehaviour
{
    private bool active;
    private float cooldown;
    void Start()
    {
        active = false;
    }
    private void Update()
    {
        cooldown -= Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active)
        {
            collision.gameObject.GetComponent<PlayerHealth>().updatePlayerHealth(-3);
            cooldown = 0.45f;
        }
    }

    public void ActivateBeam()
    {
        active = true;
        cooldown = 0;
    }
    public void DeactivateBeam()
    {
        active = false;
    }
}
