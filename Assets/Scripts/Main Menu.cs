using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour { 
    public Button play;
    public Button levels;
    public Button options;
    public Button exit;
    private AudioManager audioManager;
    

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        play.onClick.AddListener(Play);
        levels.onClick.AddListener(Levels);
        options.onClick.AddListener(Options);
        exit.onClick.AddListener(Exit);
    }

    void Play()
    {
        audioManager.playSFX(audioManager.BossHit);
        SceneManager.LoadScene(1);//underworld
    }

    void Levels()
    {
        audioManager.playSFX(audioManager.BossHit);

    }

    void Options()
    {
        audioManager.playSFX(audioManager.BossHit);

    }

    void Exit()
    {
        audioManager.playSFX(audioManager.BossHit);
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
