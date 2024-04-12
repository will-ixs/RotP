using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertPharaoh : MonoBehaviour
{
    private GameObject pharaoh;
    void Start()
    {
        pharaoh = GameObject.FindGameObjectWithTag("Boss");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PharaohStateManager p = pharaoh.GetComponent<PharaohStateManager>();
            if(p.state == PharaohStateManager.PharaohState.Waiting)
            {
                p.StopWaiting();
            }
        }
    }

}
