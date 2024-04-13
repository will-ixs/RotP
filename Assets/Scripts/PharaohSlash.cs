using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharaohSlash : MonoBehaviour
{
    private bool active;
    void Start()
    {
        active = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active)
        {
            collision.gameObject.GetComponent<PlayerHealth>().updatePlayerHealth(-15);
            active = false;
        }
    }

    public void ActivateSword()
    {
        active = true;
    }
    public void DeactivateSword() 
    {
        active = false;
    }
}