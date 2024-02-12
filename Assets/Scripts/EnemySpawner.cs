using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach to empty GameObject, set necessary values in Inspector
//Create Emptys in scene to act as spawnLocations
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnLocations;
    [SerializeField] private float spawningCountdown;
    [SerializeField] private int enemyCap;
    [SerializeField] private List<GameObject> enemyPrefabs;

    public List<GameObject> activeEnemies;
    private bool spawningEnabled = true;
    public GameObject enemyHealthBar;
    private Transform canvasTransform;
    void Start()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
    }

    void Update()
    {
        if(activeEnemies.Count >= enemyCap)
        {
            spawningEnabled = false;
        }
        else
        {
            spawningEnabled = true; 
        }


        if (spawningEnabled)
        {
            spawningCountdown -= Time.deltaTime;
            if(spawningCountdown < 0)
            {
                //Spawn Enemies
                foreach (Transform spawnLocation in spawnLocations)
                {
                    //Randomly select index from enemyPrefabs list
                    int index = Random.Range(0, enemyPrefabs.Count);

                    //Instantiate that gameobject at spawnLocation
                    GameObject spawnedEnemy = Instantiate(enemyPrefabs[index], spawnLocation.position, spawnLocation.rotation);   
                    GameObject enemyHealth = Instantiate(enemyHealthBar, spawnLocation.position, spawnLocation.rotation);
                    enemyHealth.transform.SetParent(canvasTransform);
                    enemyHealth.GetComponent<EnemyHealth>().target = spawnedEnemy;
                    
                    //save reference to gameObject in activeEnemies list
                    activeEnemies.Add(spawnedEnemy);
                
                }

                //Reset Timer
                spawningCountdown = 10.0f;
            }
        }
    }


    public void EnableSpawning()
    {
        spawningEnabled = true;
    }

    public void DisableSpawning()
    {
        spawningEnabled = false;
    }
}
