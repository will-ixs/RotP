using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PalaceProgressionManager : MonoBehaviour
{
    public enum PalaceState
    {
        StartingRoom,
        StartingRoomLocked,
        FirstHall,
        FirstHallLocked,
        SecondHall,
        SecondHallLocked,
        FirstPharaoh,
        ThirdHall,
        ThirdHallLocked,
        SecondPharaoh,
        Rest,
        FinalPharaoh,
        BossDefeated
    }
    public List<EnemySpawner> startingRoomSpawners = new List<EnemySpawner>();
    public List<EnemySpawner> hall1Spawners = new List<EnemySpawner>();
    public List<EnemySpawner> hall2Spawners = new List<EnemySpawner>();
    public List<EnemySpawner> hall3Spawners = new List<EnemySpawner>();
    public List<EnemySpawner> pharaoh1Spawners = new List<EnemySpawner>();
    public List<EnemySpawner> pharaoh2Spawners = new List<EnemySpawner>();
    public List<EnemySpawner> pharaoh3Spawners = new List<EnemySpawner>();

    [SerializeField] private GameObject killMeter;
    [SerializeField] private GameObject Pharaoh;

    //may need multiple doors for alternate routes
    [SerializeField] private GameObject startingDoor;
    [SerializeField] private GameObject hall1Door;
    [SerializeField] private GameObject hall2Door;
    [SerializeField] private GameObject hall3Door;

    [SerializeField] private GameObject pharaoh1Door;
    [SerializeField] private AreaTrigger pharaoh1Trigger;
    [SerializeField] private GameObject pharaoh2Door;
    [SerializeField] private AreaTrigger pharaoh2Trigger;
    [SerializeField] private GameObject pharaoh3Door;
    [SerializeField] private AreaTrigger pharaoh3Trigger;

    [SerializeField] private GameObject titleCard;
    [SerializeField] public int startCount;
    [SerializeField] public int hall1Count;
    [SerializeField] public int hall2Count;
    [SerializeField] public int hall3Count;

    private int currKills;
    private bool switched;
    private AudioManager audioManager;

    public PalaceState currState;

    void Start()
    {
        DisableSpawners();
        switched = false;
        currKills = 0;
        currState = PalaceState.StartingRoom;
        Pharaoh.SetActive(false);
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
            if (currState == PalaceState.FinalPharaoh)
            {
                if (Pharaoh == null)
                {
                    IncrementCryptState();
                    switched = true;
                }
            }
        }
    }

    private void CheckKillCount()
    {
        switch (currState)
        {
            case PalaceState.StartingRoomLocked:
            startCount = 0;
                foreach(EnemySpawner e in startingRoomSpawners){
                    startCount += e.Kills;
                }
                if(startCount > 20)
                {
                    startingDoor.GetComponentInChildren<Animator>().SetTrigger("Collapse");
                }
                currKills = startCount;
                break;
            case PalaceState.Rest:
                currKills = 20;
                break;
            case PalaceState.FinalPharaoh:
                if(Pharaoh == null)
                {
                    currKills = 20;
                }
                break;
        }
        killMeter.GetComponent<Image>().fillAmount = currKills / 20.0f;
    }

    /*all spawners
     * startingRoomSpawners 
     * hall1Spawners 
     * hall2Spawners 
     * hall3Spawners 
     * pharaoh1Spawners 
     * pharaoh2Spawners 
     * pharaoh3Spawners*/
    private void DisableSpawners()
    {
        foreach (EnemySpawner e in startingRoomSpawners)
        {
            e.DisableSpawning();
        }
        foreach (EnemySpawner e in hall1Spawners)
        {
            e.DisableSpawning();
        }
        foreach (EnemySpawner e in hall2Spawners)
        {
            e.DisableSpawning();
        }
        foreach (EnemySpawner e in hall3Spawners)
        {
            e.DisableSpawning();
        }
        foreach (EnemySpawner e in pharaoh1Spawners)
        {
            e.DisableSpawning();
        }
        foreach (EnemySpawner e in pharaoh2Spawners)
        {
            e.DisableSpawning();
        }
        foreach (EnemySpawner e in pharaoh3Spawners)
        {
            e.DisableSpawning();
        }
    }
    private void ActivateStartSpawners()
    {
        DisableSpawners();

        foreach (EnemySpawner e in startingRoomSpawners)
        {
            e.EnableSpawning();
        }
    }
    private void ActivateH1Spawners()
    {
        DisableSpawners();

        foreach (EnemySpawner e in hall1Spawners)
        {
            e.EnableSpawning();
        }
    }
    private void ActivateH2Spawners()
    {
        DisableSpawners();

        foreach (EnemySpawner e in hall2Spawners)
        {
            e.EnableSpawning();
        }
    }
    private void ActivateH3Spawners()
    {
        DisableSpawners();

        foreach (EnemySpawner e in hall3Spawners)
        {
            e.EnableSpawning();
        }
    }
    private void ActivateP1Spawners()
    {
        DisableSpawners();

        foreach (EnemySpawner e in pharaoh1Spawners)
        {
            e.EnableSpawning();
        }
    }
    private void ActivateP2Spawners()
    {
        DisableSpawners();

        foreach (EnemySpawner e in pharaoh2Spawners)
        {
            e.EnableSpawning();
        }
    }
    private void ActivateP3Spawners(){
        DisableSpawners();

        foreach(EnemySpawner e in pharaoh3Spawners){
            e.EnableSpawning();
        }
    }


    /*StartingRoom,
        StartingRoomLocked,
        FirstHall,
        FirstHallLocked,
        SecondHall,
        SecondHallLocked,
        FirstPharaoh,
        ThirdHall,
        ThirdHallLocked,
        SecondPharaoh,
        Rest,
        FinalPharaoh,
        BossDefeated*/
    public void IncrementCryptState()
    {
        switch (currState)
        {
            case PalaceState.StartingRoom:
                currState = PalaceState.StartingRoomLocked;
                startingDoor.SetActive(true);
                startingDoor.GetComponentInChildren<Animator>().SetTrigger("Rise");
                ActivateStartSpawners();
                break;
            case PalaceState.StartingRoomLocked:
                currState = PalaceState.FirstHall;
                audioManager.playSFX(audioManager.Gates);
                startingDoor.SetActive(false); //anim trigger once sprite in
                DisableSpawners();
                break;
            case PalaceState.FirstHall:
                currState = PalaceState.FirstHallLocked;
                hall1Door.SetActive(true);
                hall1Door.GetComponentInChildren<Animator>().SetTrigger("Rise");
                ActivateH1Spawners();
                break;
            case PalaceState.FirstHallLocked:
                currState = PalaceState.SecondHall;
                audioManager.playSFX(audioManager.Gates);
                hall1Door.SetActive(false);
                DisableSpawners();
                break;
            case PalaceState.SecondHall:
                currState = PalaceState.SecondHallLocked;
                hall2Door.SetActive(true);
                hall2Door.GetComponentInChildren<Animator>().SetTrigger("Rise");
                ActivateH2Spawners();
                break;
            case PalaceState.SecondHallLocked:
                currState = PalaceState.FirstPharaoh;///work here
                hall2Door.SetActive(false);
                break;
            case PalaceState.Rest:
                currState = PalaceState.FinalPharaoh;
                audioManager.newBGM(audioManager.bossBackground);
                pharaoh3Door.SetActive(true);
                pharaoh3Door.GetComponentInChildren<Animator>().SetTrigger("Rise");
                titleCard.SetActive(true);
                ActivateP3Spawners();
                break;
            case PalaceState.FinalPharaoh:
                currState = PalaceState.BossDefeated;
                audioManager.playSFX(audioManager.winSound);
                DisableSpawners();
                foreach (EnemySpawner e in pharaoh3Spawners)
                {
                    e.ClearAllEnemies();
                }
                break;
            case PalaceState.BossDefeated:
                //credits? main menu?
                break;
        }
    }
}
