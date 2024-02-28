using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsirisSpawner : MonoBehaviour
{

    private GameObject player;

    private Transform spawnLocation;
    [SerializeField] private float healthThreshold;
    [SerializeField] private float spawningCountdown;
    [SerializeField] private GameObject osirisPrefab;
    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = gameObject.transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealth playerhealth = player.GetComponent<PlayerHealth>();
        if(playerhealth != null)
        {
            if(playerhealth.curHealth <= 33)
            {
                spawningCountdown -= Time.deltaTime;
                if(spawningCountdown < 0)
                {
                    GameObject osiris = Instantiate(osirisPrefab, spawnLocation.position, spawnLocation.rotation);
                }
                spawningCountdown = 5.0f;

            }
        }
        
    }
}
