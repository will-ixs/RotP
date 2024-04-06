using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CryptProgressionManager : MonoBehaviour
{
    public enum CryptState
    {
        TombOpen,
        TombLocked,
        HallwayChaosOpen,
        HallwayChaosLocked,
        HallwayCalm,
        BossFight,
        Waiting
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
    [SerializeField] private GameObject bossDoor;
    [SerializeField] private GameObject afterBossDoor;
    [SerializeField] private AreaTrigger levelAdvanceTrigger;
    [SerializeField] private GameObject titleCard;
    [SerializeField] public int TombKillCount;
    [SerializeField] public int HallwayKillCount;
    private int currKills;
    private bool switched;
    private AudioManager audioManager;

    public CryptState currState;

    void Start()
    {
        switched = false;
        currKills = 0;
        DisableSpawners();
        currState = CryptState.TombOpen;
        YellowSerpopard.SetActive(false);
        PurpleSerpopard.SetActive(false);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
                    DisableSpawners();
                    foreach (EnemySpawner e in bossSpawners)
                    {
                        e.ClearAllEnemies();
                    }
                    audioManager.playSFX(audioManager.winSound);
                    switched = true;
                    afterBossDoor.GetComponentInChildren<Animator>().SetTrigger("Collapse");
                }
            }
        }
    }

    private void CheckKillCount()
    {
        switch (currState)
        {
            case CryptState.TombLocked:
            TombKillCount = 0;
                foreach(EnemySpawner e in tombSpawners){
                    TombKillCount += e.Kills;
                }
                if(TombKillCount > 20)
                {
                    tombDoor.GetComponentInChildren<Animator>().SetTrigger("Collapse");
                }
                currKills = TombKillCount;
                break;
            case CryptState.HallwayChaosLocked:
                HallwayKillCount = 0;
                foreach(EnemySpawner e in hallwaySpawners){
                    HallwayKillCount += e.Kills;
                }
                if(HallwayKillCount > 20){
                    hallDoor.GetComponentInChildren<Animator>().SetTrigger("Collapse");
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
            case CryptState.TombOpen:
                tombDoor.SetActive(true);
                tombDoor.GetComponentInChildren<Animator>().SetTrigger("Rise");
                currState = CryptState.TombLocked;
                ActivateTombSpawners();
                break;
            case CryptState.TombLocked:
                audioManager.playSFX(audioManager.Gates);
                tombDoor.SetActive(false); //anim trigger once sprite in
                currState = CryptState.HallwayChaosOpen;
                DisableSpawners();
                break;
            case CryptState.HallwayChaosOpen:
                hallDoor.SetActive(true);
                hallDoor.GetComponentInChildren<Animator>().SetTrigger("Rise");
                currState = CryptState.HallwayChaosLocked;
                ActivateHallwaySpawners();
                break;
            case CryptState.HallwayChaosLocked:
                audioManager.playSFX(audioManager.Gates);
                hallDoor.SetActive(false);
                currState = CryptState.HallwayCalm;
                DisableSpawners();
                break;
            case CryptState.HallwayCalm:
                audioManager.newBGM(audioManager.bossBackground);
                currState = CryptState.BossFight;
                YellowSerpopard.SetActive(true);
                PurpleSerpopard.SetActive(true);
                ActivateBossSpawners();
                bossDoor.SetActive(true);
                afterBossDoor.GetComponentInChildren<Animator>().SetTrigger("Rise");
                bossDoor.GetComponentInChildren<Animator>().SetTrigger("Rise");
                titleCard.SetActive(true);
                break;
            case CryptState.BossFight:
                currState = CryptState.Waiting;
                afterBossDoor.SetActive(false);
                break;
            case CryptState.Waiting:
                Invoke("LoadPalace", 1.0f);
                break;
        }
    }
    
    private void LoadPalace()
    {
        GetComponent<StateManager>().ChangeSceneByName("Palace");
    }
}
