using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public GameObject target;
    Slider slider;
    float health;
    // Start is called before the first frame update
    void Start()
    {
        health = target.GetComponent<Enemy>().health;
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = health;
        slider.value = health;
        slider.minValue = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = Camera.main.WorldToScreenPoint(target.transform.position);
        gameObject.transform.position = new Vector3(cameraPos.x - 25, cameraPos.y + 50, cameraPos.z);

        health = target.GetComponent<Enemy>().health;
        
        slider.value = health;


        if (target.GetComponent<Enemy>().Dead)
        {
            Destroy(gameObject);
        }
    }
}
