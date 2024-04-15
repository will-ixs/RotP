using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{

    public Slider slider;
    public static float volume = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        volume = slider.value;
        foreach(Transform child in gameObject.transform)
        {
            child.transform.gameObject.GetComponent<AudioSource>().volume = volume;
        }
    }
}
