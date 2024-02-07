using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public Text healthText;
    public float maxHealth;
    private float curHealth;
    public GameObject indicator;

    public void updatePlayerHealth(float amount)
    {
        curHealth += amount;

        if (curHealth < 0)
        {
            curHealth = 0;
        }

        // Update UI to reflect health
        healthBar.value = curHealth;
        healthText.text = curHealth + "/" + maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        healthText.text = curHealth + "/" + maxHealth;
    }

    // Update is called once per frame
    float elapsed = 0f;
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f) 
        {
            elapsed = elapsed % 1f;
            updatePlayerHealth(-1);
        }
    }
}
