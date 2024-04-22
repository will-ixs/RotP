using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoints : MonoBehaviour
{
    void Start()
    {
        CheckpointManager c = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointManager>();
        
        if(c.checkpoint > 0)
        {
            GameObject.Find("IntroCutscene").SetActive(false);
        }

        c.LoadCheckpoint();
    }
}
