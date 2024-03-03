using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private GameObject YellowSerpopard;
    [SerializeField] private GameObject PurpleSerpopard;
    [SerializeField] private AreaTrigger ChaosTrigger;
    [SerializeField] private AreaTrigger CalmTrigger;
    [SerializeField] private AreaTrigger BossTrigger;

    public CryptState currState;

    void Start()
    {
        DisableSpawners();
        currState = CryptState.Tomb;
        YellowSerpopard.SetActive(false);
        PurpleSerpopard.SetActive(false);
        ActivateTombSpawners();
    }

    void Update()
    {
        CheckBossCompletion();
    }


    private void CheckBossCompletion()
    {
        if (currState == CryptState.BossFight)
        {
            if (YellowSerpopard.GetComponent<BossHealth>().Dead && PurpleSerpopard.GetComponent<BossHealth>().Dead)
            {
                DisableSpawners();
                //Open exit or automatically transition stage?
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

    public void IncrementCryptState()
    {
        switch (currState)
        {
            case CryptState.Tomb:
                currState = CryptState.HallwayChaos;
                ActivateHallwaySpawners();
                break;
            case CryptState.HallwayChaos:
                currState= CryptState.HallwayCalm;
                DisableSpawners();
                break;
            case CryptState.HallwayCalm:
                currState = CryptState.BossFight;
                YellowSerpopard.SetActive(true);
                PurpleSerpopard.SetActive(true);
                ActivateBossSpawners();
                break;
            case CryptState.BossFight:
                //level transition
                break;
        }
    }
}
