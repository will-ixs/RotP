using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    private bool triggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            triggered = true;
            GetComponentInParent<CryptProgressionManager>().IncrementCryptState();
        }
    }
}
