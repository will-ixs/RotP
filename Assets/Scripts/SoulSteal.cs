using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSteal : MonoBehaviour
{
    public float movementCooldown;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < nearby_ka_fragments.Count; i++)
            {
                gameObject.GetComponent<PlayerHealth>().updatePlayerHealth(nearby_ka_fragments[i].GetComponent<KaFragment>().amount);
                Destroy(nearby_ka_fragments[i]);
            }
            gameObject.GetComponent<PlayerMovement>().DisableMovement(movementCooldown);
        }
    }
}
