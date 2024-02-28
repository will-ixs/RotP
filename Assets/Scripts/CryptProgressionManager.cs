using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryptProgressionManager : MonoBehaviour
{
    public enum CryptState
    {
        Enemies,
        EnemiesAndBoss,
        CleanupEnemies
    }
    public List<Transform> stageOneSpawners = new List<Transform>();
    public GameObject PurpleSerpopard;
    public GameObject YellowSerpopard;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
