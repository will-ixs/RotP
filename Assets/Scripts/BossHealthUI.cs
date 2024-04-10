using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public GameObject target;
    public Transform healthCanvasTransform;
    private Slider slider;
    private float health;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();

        healthCanvasTransform = GameObject.Find("Health Canvas").transform;
        gameObject.transform.SetParent(healthCanvasTransform);

        health = target.GetComponent<BossHealth>().health;
        slider.maxValue = health;
        slider.value = health;
        text.text = target.name;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = Camera.main.WorldToScreenPoint(target.transform.position);
        Vector3 uiOffset = target.GetComponent<BossHealth>().uiOffset;
        gameObject.transform.position = new Vector3(cameraPos.x + uiOffset.x, cameraPos.y + uiOffset.y, cameraPos.z + uiOffset.z);

        health = target.GetComponent<BossHealth>().health;
        slider.value = health;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
