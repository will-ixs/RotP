using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DoorCall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increment(string test)
    {
        UnityEngine.Debug.Log(test);
        GameObject.Find("CryptStateManager").GetComponent<CryptProgressionManager>().IncrementCryptState();
    }
}
