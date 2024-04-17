using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalaceAreaTrigger : MonoBehaviour
{
    private bool triggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            triggered = true;
            GetComponentInParent<PalaceProgressionManager>().IncrementState();
            gameObject.SetActive(false);
        }
    }
}
