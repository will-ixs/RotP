using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int health;
    public bool Dead;
    public Vector3 uiOffset;

    public GameObject BossHealthBar;
    private AudioManager audioManager;

    private void Start()
    {
        BossHealthBar = Instantiate(BossHealthBar);
        BossHealthBar.GetComponent<BossHealthUI>().target = gameObject;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        Dead = false;
    }
    public void TakeDamage(int damage)
    {
        audioManager.playSFX(audioManager.BossHit);
        health -= damage;
        if (health <= 0)
        {
            Dead = true;
        }
    }
}
