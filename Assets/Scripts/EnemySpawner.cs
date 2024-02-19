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
    [SerializeField] private List<float> enemySpawnWeights;
    private float spawnWeightTotal;

    public List<GameObject> activeEnemies;
    private bool spawningEnabled = true;
    public GameObject enemyHealthBar;
    private Transform canvasTransform;
    void Start()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
        spawnWeightTotal = 0.0f;
        for (int i = 0; i < enemySpawnWeights.Count; i++)
        {
            spawnWeightTotal += enemySpawnWeights[i];
        }
    }

    GameObject SelectEnemyToSpawn()
    {
        float rng = Random.Range(0.0f, spawnWeightTotal);
        for (int i = 0; i < enemySpawnWeights.Count; i++)
        {
            if (rng <= enemySpawnWeights[i])
            {
                return enemyPrefabs[i];
            }
            rng -= enemySpawnWeights[i];
        }
        return enemyPrefabs[0];
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
                    //Instantiate that gameobject at spawnLocation
                    GameObject spawnedEnemy = Instantiate(SelectEnemyToSpawn(), spawnLocation.position, spawnLocation.rotation);   
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
