using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsirisSpawner : MonoBehaviour
{

    private GameObject player;

    [SerializeField] private Transform spawnLocation;
    [SerializeField] private float healthThreshold;
    [SerializeField] private GameObject enemyPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealth playerhealth = player.GetComponent<PlayerHealth>();
        
    }
}
