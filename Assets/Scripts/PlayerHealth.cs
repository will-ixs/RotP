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
        elapsed += Time.deltaTime;
        if (elapsed >= 1f) 
        {
            elapsed = elapsed % 1f;
            updatePlayerHealth(-1);
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
