using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;
    public GameObject indicator;
    public Image portrait;
    public Image hud;
    public Image osirisIndicator;
    public List<Sprite> portraitSprites;
    public List<Sprite> hudSprites;
    private Animator anim;
    public bool Dead;

    private float damage_color_cooldown;

    private Transform canvasTransform;

    // Start is called before the first frame update
    void Start()
    {
        Dead = false;
        // Initialize default values
        curHealth = maxHealth;

        canvasTransform = GameObject.Find("Canvas").transform;
        anim = osirisIndicator.GetComponent<Animator>();
}

    // Updates player health and UI by amount
    public void updatePlayerHealth(float amount, bool popup = true)
    {
        if(curHealth + amount > 100){
            curHealth = 100;
        }else{
            curHealth += amount;
        }
        // Prevent negative health
        if (curHealth < 0)
        {
            curHealth = 0;
        }
        

        if (popup)
        {
            GameObject healthIndicator = Instantiate(indicator);

            // Set text to display above player
            Text healthIndicatorText = healthIndicator.GetComponent<Text>();

            healthIndicatorText.text = amount.ToString();
            if (amount > 0)
            {
                healthIndicatorText.text = "+" + amount.ToString();
            }
            else
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
        int sprite_index = (int)Mathf.Ceil(curHealth/maxHealth * portraitSprites.Count) - 1;

        if (sprite_index < 0) {sprite_index = 0;}
        if (sprite_index >= portraitSprites.Count) {sprite_index = portraitSprites.Count-1;}

        portrait.sprite = portraitSprites[sprite_index];

        int hud_index = (int)Mathf.Ceil(curHealth / maxHealth * hudSprites.Count) - 1;

        if (hud_index < 0) { hud_index = 0; }
        if (hud_index >= hudSprites.Count) { hud_index = hudSprites.Count - 1; }

        hud.sprite = hudSprites[hud_index];

        anim.SetInteger("State", hud_index);
//        Debug.Log(anim.GetInteger("State"));

    }

    // Update is called once per frame
    void Update()
    {
        damage_color_cooldown -= Time.deltaTime;
        if (damage_color_cooldown <= 0.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(179, 245, 227);
        }
        updatePlayerHealth(-Time.deltaTime, false);
        
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

    public void Death() 
    {
        Dead = true;
        LevelManager.instance.GameOver();
        gameObject.SetActive(false);
    }
}
