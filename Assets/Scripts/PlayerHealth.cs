using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public Text healthText;
    public float maxHealth;
    public float curHealth;
    public GameObject indicator;

    private float damage_color_cooldown;

    private Transform canvasTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize default values
        curHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        healthText.text = curHealth + "/" + maxHealth;

        canvasTransform = GameObject.Find("Canvas").transform;
    }

    // Updates player health and UI by amount
    public void updatePlayerHealth(float amount)
    {
        curHealth += amount;

        // Prevent negative health
        if (curHealth < 0)
        {
            curHealth = 0;
        }

        // Update UI to reflect health
        healthBar.value = curHealth;
        healthText.text = curHealth + "/" + maxHealth;

        GameObject healthIndicator = Instantiate(indicator);

        // Set text to display above player
        Text healthIndicatorText = healthIndicator.GetComponent<Text>();

        healthIndicatorText.text = amount.ToString();
        if (amount > 0)
        {
            healthIndicatorText.text = "+" + amount.ToString();
        } else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            damage_color_cooldown = 0.5f;

        }

        // Display text above player
        healthIndicator.transform.SetParent(canvasTransform, false);
        healthIndicator.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        healthIndicator.transform.position = new Vector3(healthIndicator.transform.position.x + 15f + Random.Range(-25f, 25f), healthIndicator.transform.position.y + 100f + Random.Range(-20f, 10f), healthIndicator.transform.position.z);

        // Remove text after delay
        Destroy(healthIndicator, 1.0f);

    }

    // Update is called once per frame
    float elapsed = 0f;
    void Update()
    {
        damage_color_cooldown -= Time.deltaTime;
        if (damage_color_cooldown <= 0.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(179, 245, 227);
        }
    }

    public void TakeDamage(int damage) 
    {
        curHealth -= damage;

        if(curHealth <= 0)
        {
            Debug.Log(curHealth);
            //Dead = true;
        }
    }
}
