using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryptProgressionManager : MonoBehaviour
{
    public enum CryptState
    {
        Tomb,
        HallwayChaos,
        HallwayCalm,
        BossFight
    }
    public List<EnemySpawner> tombSpawners = new List<EnemySpawner>();
    public List<EnemySpawner> hallwaySpawners = new List<EnemySpawner>();
    public List<EnemySpawner> bossSpawners = new List<EnemySpawner>();
    public GameObject PurpleSerpopard;
    public GameObject YellowSerpopard;

    private CryptState currState;
    private CryptState nextState;

    void Start()
    {
        
    }

    void Update()
    {
        if(nextState != currState){
            switch(currState)
            {
                case CryptState.Tomb:
                    ActivateTombSpawners();
                    break;
                case CryptState.HallwayChaos:
                    ActivateHallwaySpawners();
                    break;
                case CryptState.HallwayCalm:
                    DisableSpawners();
                    break;
                case CryptState.BossFight:
                    ActivateBossSpawners();
                    break;
            }
        }
    }

    private void ActivateTombSpawners(){
        foreach(EnemySpawner e in hallwaySpawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in bossSpawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in tombSpawners){
            e.EnableSpawning();
        }
    }

    private void ActivateHallwaySpawners(){
        foreach(EnemySpawner e in tombSpawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in bossSpawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in hallwaySpawners){
            e.EnableSpawning();
        }
    }

    private void ActivateBossSpawners(){
        foreach(EnemySpawner e in tombSpawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in hallwaySpawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in bossSpawners){
            e.EnableSpawning();
        }
    }

    private void DisableSpawners(){
        foreach(EnemySpawner e in tombSpawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in hallwaySpawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in bossSpawners){
            e.DisableSpawning();
        }
    }
}
