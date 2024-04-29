using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnderworldProgressionManager : MonoBehaviour
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
    public List<EnemySpawner> room1Spawners = new List<EnemySpawner>();
    public List<EnemySpawner> room2Spawners = new List<EnemySpawner>();
    public List<EnemySpawner> bossSpawners = new List<EnemySpawner>();
    [SerializeField] private GameObject killMeter;
    [SerializeField] private GameObject Ammunit;
    [SerializeField] private AreaTrigger BossTrigger;
    [SerializeField] private GameObject room1Door;
    [SerializeField] private GameObject room2Door;
    [SerializeField] private GameObject bossDoor;
    [SerializeField] private GameObject afterBossDoor;
    [SerializeField] private AreaTrigger levelAdvanceTrigger;
    [SerializeField] private GameObject titleCard;
    [SerializeField] public int room1KillCount;
    [SerializeField] public int room2KillCount;
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
        Ammunit.SetActive(false);
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
                if (Ammunit == null)
                {
                    DisableSpawners();
                    foreach (EnemySpawner e in bossSpawners)
                    {
                        e.ClearAllEnemies();
                    }
                    audioManager.stopBGM();
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
            room1KillCount = 0;
                foreach(EnemySpawner e in room1Spawners)
                {
                    room1KillCount += e.Kills;
                }
                if(room1KillCount > 20)
                {
                    room1Door.GetComponentInChildren<Animator>().SetTrigger("Collapse");
                }
                currKills = room1KillCount;
                break;
            case CryptState.HallwayChaosLocked:
                room2KillCount = 0;
                foreach(EnemySpawner e in room2Spawners)
                {
                    room2KillCount += e.Kills;
                }
                if(room2KillCount > 20){
                    room2Door.GetComponentInChildren<Animator>().SetTrigger("Collapse");
                }
                currKills = room2KillCount;
                break;
            case CryptState.HallwayCalm:
                currKills = 0;
                break;
            case CryptState.BossFight:
                if(Ammunit == null)
                {
                    currKills = 20;
                }
                break;
        }

        killMeter.GetComponent<Image>().fillAmount = currKills / 20.0f;
    }

    private void ActivateTombSpawners(){
        foreach(EnemySpawner e in room2Spawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in bossSpawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in room1Spawners){
            e.EnableSpawning();
        }
    }

    private void ActivateHallwaySpawners(){
        foreach(EnemySpawner e in room1Spawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in bossSpawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in room2Spawners){
            e.EnableSpawning();
        }
    }

    private void ActivateBossSpawners(){
        foreach(EnemySpawner e in room1Spawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in room2Spawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in bossSpawners){
            e.EnableSpawning();
        }
    }

    private void DisableSpawners(){
        foreach(EnemySpawner e in room1Spawners){
            e.DisableSpawning();
        }
        foreach(EnemySpawner e in room2Spawners){
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
                audioManager.playSFX(audioManager.GatesUp);
                room1Door.SetActive(true);
                room1Door.GetComponentInChildren<Animator>().SetTrigger("Rise");
                currState = CryptState.TombLocked;
                ActivateTombSpawners();
                break;
            case CryptState.TombLocked:
                audioManager.playSFX(audioManager.Gates);
                room1Door.SetActive(false); //anim trigger once sprite in
                currState = CryptState.HallwayChaosOpen;
                DisableSpawners();
                break;
            case CryptState.HallwayChaosOpen:
                audioManager.playSFX(audioManager.GatesUp);
                room2Door.SetActive(true);
                room2Door.GetComponentInChildren<Animator>().SetTrigger("Rise");
                currState = CryptState.HallwayChaosLocked;
                ActivateHallwaySpawners();
                break;
            case CryptState.HallwayChaosLocked:
                audioManager.playSFX(audioManager.Gates);
                room2Door.SetActive(false);
                currState = CryptState.HallwayCalm;
                DisableSpawners();
                break;
            case CryptState.HallwayCalm:
                audioManager.newBGM(audioManager.bossBackground);
                currState = CryptState.BossFight;
                Ammunit.SetActive(true);
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
