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
        ThirdHall,
        ThirdHallLocked,
        FirstPharaohPre,
        FirstPharaoh,
        SecondPharaohPre,
        SecondPharaoh,
        FinalPharaohPre,
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
    [SerializeField] private GameObject Ammit;
    [SerializeField] private GameObject Serpopard;

    //may need multiple doors for alternate routes
    [SerializeField] private GameObject startingDoor;
    [SerializeField] public GameObject hall1Door;
    [SerializeField] public GameObject hall2Door;
    [SerializeField] public GameObject hall3Door;

    [SerializeField] private GameObject pharaoh1Door;
    [SerializeField] private GameObject pharaoh2Door;
    [SerializeField] private GameObject pharaoh2Barrier;
    [SerializeField] private GameObject pharaoh3Door;

    [SerializeField] private GameObject titleCard;
    [SerializeField] public int startCount;
    [SerializeField] public int hall1Count;
    [SerializeField] public int hall2Count;
    [SerializeField] public int hall3Count;

    private bool canAdvance;
    private int currKills;
    private float killsNeeded;
    private AudioManager audioManager;
    private GameObject checkpointManager;

    public PalaceState currState;

    void Start()
    {
        DisableSpawners();
        currKills = 0;
        killsNeeded = 1.0f;
        currState = PalaceState.StartingRoom;
        Pharaoh.SetActive(false);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        checkpointManager = GameObject.FindGameObjectWithTag("Checkpoint");
        canAdvance = true;

        pharaoh1Door.SetActive(false);
        pharaoh2Door.SetActive(false);
        pharaoh3Door.SetActive(false);
        pharaoh2Barrier.SetActive(false);
    }

    void Update()
    {
        CheckKillCount();
    }
    private void CheckKillCount()
    {
        switch (currState)
        {
            case PalaceState.StartingRoomLocked:
            startCount = 0;
            killsNeeded = 2.0f; //5.0
                foreach(EnemySpawner e in startingRoomSpawners){
                    startCount += e.Kills;
                }
                if(startCount >= killsNeeded && canAdvance)
                {
                    startingDoor.GetComponent<PalaceGateController>().LowerSegments();
                    canAdvance = false;
                }
                currKills = startCount;
                break;
            case PalaceState.FirstHallLocked:
                hall1Count = 0;
                killsNeeded = 2.0f; //20.0f
                foreach (EnemySpawner e in hall1Spawners)
                {
                    hall1Count += e.Kills;
                }

                if(Ammit == null)
                {
                }

                if (hall1Count >= killsNeeded && canAdvance)
                {
                    hall1Door.GetComponent<PalaceGateController>().LowerSegments();
                    canAdvance = false;
                }
                currKills = hall1Count;
                break;
            case PalaceState.SecondHallLocked:
                hall2Count = 0;
                killsNeeded = 2.0f; //20.0f
                foreach (EnemySpawner e in hall2Spawners)
                {
                    hall2Count += e.Kills;
                }
                if (hall2Count >= killsNeeded && canAdvance && Ammit == null)
                {
                    hall2Door.GetComponent<PalaceGateController>().LowerSegments();
                    canAdvance = false;
                }
                currKills = hall2Count;
                break;
            case PalaceState.ThirdHallLocked:
                hall3Count = 0;
                killsNeeded = 5.0f; //30.0f
                foreach (EnemySpawner e in hall3Spawners)
                {
                    hall3Count += e.Kills;
                }
                if (hall3Count >= killsNeeded && canAdvance && Serpopard == null)
                {
                    hall3Door.GetComponent<PalaceGateController>().LowerSegments();
                    canAdvance = false;
                }
                currKills = hall3Count;
                break;
            case PalaceState.SecondPharaohPre:
                currKills = 1;
                killsNeeded = 1;
                break;
            case PalaceState.FinalPharaohPre:
                currKills = 1;
                killsNeeded = 1;
                break;
            case PalaceState.BossDefeated:
                currKills = 1;
                killsNeeded = 1;
                break;
        }
        killMeter.GetComponent<Image>().fillAmount = currKills / killsNeeded;
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
    public void IncrementState()
    {
        switch (currState)
        {
            case PalaceState.StartingRoom:
                currState = PalaceState.StartingRoomLocked;
                startingDoor.SetActive(true);
                startingDoor.GetComponent<PalaceGateController>().RaiseSegments();
                ActivateStartSpawners();
                break;
            case PalaceState.StartingRoomLocked:
                canAdvance = true;
                currState = PalaceState.FirstHall;
                audioManager.playSFX(audioManager.Gates);
                startingDoor.SetActive(false); //anim trigger once sprite in
                DisableSpawners();
                break;
            case PalaceState.FirstHall:
                currState = PalaceState.FirstHallLocked;
                hall1Door.SetActive(true);
                hall1Door.GetComponent<PalaceGateController>().RaiseSegments();
                ActivateH1Spawners();
                break;
            case PalaceState.FirstHallLocked:
                canAdvance = true;
                currState = PalaceState.SecondHall;
                audioManager.playSFX(audioManager.Gates);
                hall1Door.SetActive(false);
                DisableSpawners();
                break;
            case PalaceState.SecondHall:
                checkpointManager.GetComponent<CheckpointManager>().SetCheckpoint1();
                currState = PalaceState.SecondHallLocked;
                Ammit.SetActive(true);
                hall2Door.SetActive(true);
                hall2Door.GetComponent<PalaceGateController>().RaiseSegments();
                ActivateH2Spawners();
                break;
            case PalaceState.SecondHallLocked:
                canAdvance = true;
                currState = PalaceState.ThirdHall;
                audioManager.playSFX(audioManager.Gates);
                hall2Door.SetActive(false);
                DisableSpawners();
                break;
            case PalaceState.ThirdHall:
                checkpointManager.GetComponent<CheckpointManager>().SetCheckpoint2();
                currState = PalaceState.ThirdHallLocked;
                Serpopard.SetActive(true);
                hall3Door.SetActive(true);
                hall3Door.GetComponent<PalaceGateController>().RaiseSegments();
                ActivateH3Spawners();
                break;
            case PalaceState.ThirdHallLocked:
                canAdvance = true;
                currState = PalaceState.FirstPharaohPre;
                audioManager.playSFX(audioManager.Gates);
                hall3Door.SetActive(false);
                DisableSpawners();
                break;
            case PalaceState.FirstPharaohPre:
                checkpointManager.GetComponent<CheckpointManager>().SetCheckpoint3();
                currState = PalaceState.FirstPharaoh;
                ClearAllHallSpawners();
                Pharaoh.SetActive(true);
                pharaoh1Door.SetActive(true);
                pharaoh1Door.GetComponent<PalaceGateController>().RaiseSegments();
                pharaoh2Barrier.SetActive(true);
                pharaoh2Barrier.GetComponent<PalaceGateController>().RaiseSegments();
                ActivateP1Spawners();
                break;
            case PalaceState.FirstPharaoh:
                pharaoh2Barrier.SetActive(false);
                canAdvance = true;
                currState = PalaceState.SecondPharaohPre;
                foreach (EnemySpawner e in pharaoh1Spawners)
                {
                    e.ClearAllEnemies();
                }
                DisableSpawners();
                break;
            case PalaceState.SecondPharaohPre:
                currState = PalaceState.SecondPharaoh;
                pharaoh2Door.SetActive(true);
                pharaoh2Door.GetComponent<PalaceGateController>().RaiseSegments();
                pharaoh2Barrier.SetActive(true);
                pharaoh2Barrier.GetComponent<PalaceGateController>().RaiseSegments();
                ActivateP2Spawners();
                break;
            case PalaceState.SecondPharaoh://call lower segments IN PSM instead of Increment, lower segments will increment once anim done.
                canAdvance = true;
                currState = PalaceState.FinalPharaohPre;
                pharaoh2Door.SetActive(false);
                foreach (EnemySpawner e in pharaoh2Spawners)
                {
                    e.ClearAllEnemies();
                }
                DisableSpawners();
                break;
            case PalaceState.FinalPharaohPre:
                currState = PalaceState.FinalPharaoh;
                pharaoh3Door.SetActive(true);
                pharaoh3Door.GetComponent <PalaceGateController>().RaiseSegments();
                audioManager.newBGM(audioManager.bossBackground);
                titleCard.SetActive(true);
                ActivateP3Spawners();
                break;
            case PalaceState.FinalPharaoh:
                canAdvance = true;
                currState = PalaceState.BossDefeated;
                audioManager.playSFX(audioManager.winSound);
                audioManager.newBGM(audioManager.background);
                DisableSpawners();
                foreach (EnemySpawner e in pharaoh3Spawners)
                {
                    e.ClearAllEnemies();
                }
                break;
            case PalaceState.BossDefeated:
                //Play win animation
                break;
        }
    }

    private void ClearAllHallSpawners()
    {
        foreach(EnemySpawner e in startingRoomSpawners)
        {
            e.ClearAllEnemies();
        }
        foreach(EnemySpawner e in hall1Spawners)
        {
            e.ClearAllEnemies();
        }
        foreach(EnemySpawner e in hall2Spawners)
        {
            e.ClearAllEnemies();
        }
        foreach(EnemySpawner e in hall3Spawners)
        {
            e.ClearAllEnemies();
        }
    }

    public void LowerP2Barrier()
    {
        pharaoh2Barrier.GetComponent<PalaceGateController>().LowerSegments();
    }

    public void LowerP2Door()
    {
        pharaoh2Door.GetComponent<PalaceGateController>().LowerSegments();
    }
}
