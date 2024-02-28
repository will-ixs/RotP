using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsirisSpawner : MonoBehaviour
{

    private GameObject player;

    private Transform spawnLocation;
    [SerializeField] private float healthThreshold;
    [SerializeField] private float spawningCountdown;
     [SerializeField] private float despawningCountdown;
    [SerializeField] private GameObject osirisPrefab;
    private GameObject osiris;
    private bool spawned;
    // Start is called before the first frame update
    void Start()
    {
        spawned = false;
        spawnLocation = gameObject.transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
         PlayerHealth playerhealth = player.GetComponent<PlayerHealth>();
        if(playerhealth != null)
        {
           
            if(!spawned)
            {
                if(playerhealth.curHealth <= healthThreshold)
                {
                    
                    spawningCountdown -= Time.deltaTime;
                    //Debug.Log(spawningCountdown);
                    if(spawningCountdown < 0)
                    {
                        osiris = Instantiate(osirisPrefab, spawnLocation.position, spawnLocation.rotation);
                        spawned = true;
                        spawningCountdown = 5.0f;
                    }

                }
                else
                {
                    spawningCountdown = 5.0f;
                }

            }
            else
            {
                if(playerhealth.curHealth > healthThreshold)
                {
                    despawningCountdown -= Time.deltaTime;
                    if(despawningCountdown < 0)
                    {
                        Destroy(osiris);
                        spawned = false;
                        despawningCountdown = 5.0f;
                    }
                }
                else
                {
                    despawningCountdown = 5.0f;
                }
            }
        }
        
        
    }
}
