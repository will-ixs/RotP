using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsirisSpawner : MonoBehaviour
{

    private GameObject player;

    private Vector3 spawnLocation;
    private  Quaternion spawnRotation;

    [SerializeField] private float healthThreshold;
    [SerializeField] private float spawningCountdown;
     [SerializeField] private float despawningCountdown;
    [SerializeField] private GameObject osirisPrefab;
    private GameObject osiris;
    private bool spawned;
    private Animator osirisIndicator;
    // Start is called before the first frame update
    void Start()
    {
        spawned = false;
        spawnLocation = gameObject.transform.position;
        spawnRotation = gameObject.transform.rotation;
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("playerPathing", 3f, 3f);
        osirisIndicator = GameObject.Find("Osiris Indicator").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealth playerhealth = player.GetComponent<PlayerHealth>();
        //Debug.Log("playerhealth != null: " + (playerhealth != null).ToString() + " !spawned: " + (!spawned).ToString() + " playerhealth.curhealth: " + (playerhealth.curHealth <= healthThreshold).ToString() + " playerhealth: " + playerhealth.curHealth.ToString());
        if(playerhealth != null)
        {
            if(!spawned)
            {
                if(playerhealth.curHealth <= healthThreshold)
                {
                    spawningCountdown -= Time.deltaTime;
                    //Debug.Log(spawningCountdown + " " +  spawned);
                    if(spawningCountdown < 0)
                    {
                        Vector3 vToPlayer = player.transform.position - transform.position;
                        float distance = vToPlayer.magnitude;
                        //if(distance < 3)
                        //    spawnLocation.x += 2;
                        osirisIndicator.SetBool("OsirisSpawn", true);
                        osiris = Instantiate(osirisPrefab, spawnLocation, spawnRotation);
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
                        osirisIndicator.SetBool("OsirisSpawn", false);
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


    private void playerPathing()
    {
        if((player.transform.position - spawnLocation).magnitude > 3.0f)
        {
            spawnLocation = player.transform.position;
        }       
    }
}
