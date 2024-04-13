using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderworldDoorCall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increment()
    {
        Debug.Log("test");
        GameObject.Find("UnderworldStateManager").GetComponent<UnderworldProgressionManager>().IncrementCryptState();
    }
}
