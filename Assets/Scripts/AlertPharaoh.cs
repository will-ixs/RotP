using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertPharaoh : MonoBehaviour
{
    [SerializeField] private GameObject pharaoh;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PharaohStateManager p = pharaoh.GetComponent<PharaohStateManager>();
            if(p.waiting)
            {
                p.StopWaiting();
            }
        }
    }

}
