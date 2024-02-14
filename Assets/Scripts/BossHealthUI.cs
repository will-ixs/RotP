using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public GameObject target;
    public Transform scrollContentTransform;
    private Slider slider;
    private float health;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();

        scrollContentTransform = GameObject.Find("Boss Health Content").transform;
        gameObject.transform.SetParent(scrollContentTransform);
        // gameObject.transform.position = new Vector3(scrollContentTransform.position.x, scrollContentTransform.position.y - 55.0f, scrollContentTransform.position.z);

        health = target.GetComponent<BossHealth>().health;
        slider.maxValue = health;
        slider.value = health;
        text.text = target.name;
    }

    // Update is called once per frame
    void Update()
    {
        health = target.GetComponent<BossHealth>().health;
        slider.value = health;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
