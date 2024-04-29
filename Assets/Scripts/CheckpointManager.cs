using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public bool loaded;
    // Start is called before the first frame update
    public Animator checkpoint1;
    public Animator checkpoint2;
    public Animator checkpoint3;
    public int checkpoint = 0;

    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Checkpoint");
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        loaded = true;
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != 3)
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint1()
    {
        checkpoint = 1;
        checkpoint1.SetTrigger("Active");
        audioManager.playSFX(audioManager.Progress);
    }
    public void SetCheckpoint2()
    {
        checkpoint = 2;
        checkpoint2.SetTrigger("Active");
        audioManager.playSFX(audioManager.Progress);
    }
    public void SetCheckpoint3()
    {
        checkpoint = 3;
        checkpoint3.SetTrigger("Active");
        audioManager.playSFX(audioManager.Progress);
    }

    public void LoadCheckpoint()
    {
        Invoke("Workaround", 0.1f);
    }
    private void Workaround()
    {
        switch (checkpoint)
        {
            case 0:
                loaded = true;
                break;
            case 1:
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.transform.position = checkpoint1.transform.position;
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = player.transform.position;
                    GameObject p = GameObject.Find("PalaceStateManager");
                    p.GetComponent<PalaceProgressionManager>().currState = PalaceProgressionManager.PalaceState.FirstHallLocked;
                    p.GetComponent<PalaceProgressionManager>().IncrementState();
                    p.GetComponent<PalaceProgressionManager>().hall1Door.SetActive(true);
                    p.GetComponent<PalaceProgressionManager>().hall1Door.GetComponent<PalaceGateController>().RaiseSegments();
                    loaded = true;
                    break;
                }
            case 2:
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.transform.position = checkpoint2.transform.position;
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = player.transform.position;
                    GameObject p = GameObject.Find("PalaceStateManager");
                    p.GetComponent<PalaceProgressionManager>().currState = PalaceProgressionManager.PalaceState.SecondHallLocked;
                    p.GetComponent<PalaceProgressionManager>().IncrementState();
                    p.GetComponent<PalaceProgressionManager>().hall2Door.SetActive(true);
                    p.GetComponent<PalaceProgressionManager>().hall2Door.GetComponent<PalaceGateController>().RaiseSegments();
                    loaded = true;
                    break;
                }
            case 3:
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.transform.position = checkpoint3.transform.position;
                    GameObject.FindGameObjectWithTag("MainCamera").transform.position = player.transform.position;
                    GameObject p = GameObject.Find("PalaceStateManager");
                    p.GetComponent<PalaceProgressionManager>().currState = PalaceProgressionManager.PalaceState.ThirdHallLocked;
                    p.GetComponent<PalaceProgressionManager>().IncrementState();
                    p.GetComponent<PalaceProgressionManager>().hall3Door.SetActive(true);
                    p.GetComponent<PalaceProgressionManager>().hall3Door.GetComponent<PalaceGateController>().RaiseSegments();
                    loaded = true;
                    break;
                }
        }
    }
}
