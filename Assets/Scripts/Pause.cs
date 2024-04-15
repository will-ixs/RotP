using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public Button play;
    public Button options;
    public Button exit;

    public Button back;

    public GameObject pausePanel;
    public GameObject optionsMenu;

    private AudioManager audioManager;

    void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        play.onClick.AddListener(Play);
        options.onClick.AddListener(Options);
        exit.onClick.AddListener(Exit);
        back.onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void Play()
    {
        audioManager.playSFX(audioManager.BossHit);
        TogglePause();
    }

    void Options()
    {
        audioManager.playSFX(audioManager.BossHit);
        optionsMenu.SetActive(true);

    }

    void Exit()
    {
        audioManager.playSFX(audioManager.BossHit);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void Back()
    {
        audioManager.playSFX(audioManager.BossHit);
        optionsMenu.SetActive(false);
    }
}
