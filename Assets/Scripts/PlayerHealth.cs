using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth;
    private float curHealth;

    public void updatePlayerHealth(float amount)
    {
        curHealth += amount;
        healthBar.value = curHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
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
