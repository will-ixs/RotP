using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject killMeter;
    [SerializeField] private GameObject YellowSerpopard;
    [SerializeField] private GameObject PurpleSerpopard;
    [SerializeField] private AreaTrigger BossTrigger;
    [SerializeField] private GameObject tombDoor;
    [SerializeField] private GameObject hallDoor;
    [SerializeField] private int TombKillCount;
    [SerializeField] private int HallwayKillCount;
    private int currKills;
    private bool switched;

    public CryptState currState;

    void Start()
    {
        switched = false;
        currKills = 0;
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
        if (!switched)
        {
            if (currState == CryptState.BossFight)
            {
                if (YellowSerpopard == null && PurpleSerpopard == null)
                {
                    bool dontMoveToNextStage = false;
                    DisableSpawners();
                    foreach (EnemySpawner e in bossSpawners)
                    {
                        if (e.activeEnemies.Count > 0)
                        {
                            dontMoveToNextStage = true;
                        }
                    }
                    if (!dontMoveToNextStage)
                    {
                        switched = true;
                        IncrementCryptState();
                    }
                }
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
                currKills = TombKillCount;
                break;
            case CryptState.HallwayChaos:
                HallwayKillCount = 0;
                foreach(EnemySpawner e in hallwaySpawners){
                    HallwayKillCount += e.Kills;
                }
                if(HallwayKillCount > 20){
                    IncrementCryptState();
                }
                currKills = HallwayKillCount;
                break;
            case CryptState.HallwayCalm:
                currKills = 0;
                break;
            case CryptState.BossFight:
                if(PurpleSerpopard == null && YellowSerpopard == null)
                {
                    currKills = 20;
                }else if(PurpleSerpopard == null || YellowSerpopard == null)
                {
                    currKills = 10;
                }
                break;
        }

        killMeter.GetComponent<Image>().fillAmount = currKills / 20.0f;
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
                tombDoor.SetActive(false);
                currState = CryptState.HallwayChaos;
                ActivateHallwaySpawners();
                break;
            case CryptState.HallwayChaos:
                hallDoor.SetActive(false);
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
                Invoke("LoadPalace", 3.0f);
                break;
        }
    }
    
    private void LoadPalace()
    {
        GetComponent<StateManager>().ChangeSceneByName("Palace");
    }
}
