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
    [SerializeField] private int TombKillCount;
    [SerializeField] private int HallwayKillCount;

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
        CheckKillCount();
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

    private void CheckKillCount()
    {
        switch (currState)
        {
            case CryptState.Tomb:
            TombKillCount = 0;
                foreach(EnemySpawner e in tombSpawners){
                    TombKillCount += e.Kills;
                }
                if(TombKillCount > 20){
                    IncrementCryptState();
                }
                break;
            case CryptState.HallwayChaos:
                HallwayKillCount = 0;
                foreach(EnemySpawner e in hallwaySpawners){
                    HallwayKillCount += e.Kills;
                }
                if(HallwayKillCount > 20){
                    IncrementCryptState();
                }
                break;
            case CryptState.HallwayCalm:
                break;
            case CryptState.BossFight:
                break;
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
