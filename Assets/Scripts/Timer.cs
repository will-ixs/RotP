using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 0.0f;
    }

    public void begin(float time)
    {
        timeRemaining = Mathf.Max(timeRemaining, time);
    }
    public bool isReady()
    {
        return timeRemaining <= 0.0f;
    }
    public float getTimeRemaining()
    {
        return Mathf.Max(timeRemaining, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
    }
}
