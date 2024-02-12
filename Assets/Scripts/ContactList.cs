using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactList : MonoBehaviour
{
    public List<GameObject> contactList;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss") || collision.CompareTag("Enemy"))
        {
            contactList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss") || collision.CompareTag("Enemy"))
        {
            contactList.Remove(collision.gameObject);
        }
    }
}
