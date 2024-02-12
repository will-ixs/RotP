using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaFragment : MonoBehaviour
{
    public float initialAmount;
    public float amount;
    public float decayRate;

    private float timeSpawned;

    // Start is called before the first frame update
    void Start()
    {
        timeSpawned = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        amount = initialAmount - Mathf.Floor(decayRate * (Time.time - timeSpawned));
        if (amount < 1.0f)
        {
            amount = 0.0f;
            Destroy(this.gameObject);
        }
    }
}
