using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int health;
    public bool Dead;

    public GameObject BossHealthBar;

    private void Start()
    {
        BossHealthBar = Instantiate(BossHealthBar);
        BossHealthBar.GetComponent<BossHealthUI>().target = gameObject;
        Dead = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Dead = true;
        }
    }
}
