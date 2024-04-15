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
    public Button underworld;
    public Button crypt;
    public Button palace;
    public Button back;

    public GameObject start;
    public GameObject levelSelect;
    public GameObject optionsMenu;

    private AudioManager audioManager;
    

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        play.onClick.AddListener(Play);
        levels.onClick.AddListener(Levels);
        options.onClick.AddListener(Options);
        exit.onClick.AddListener(Exit);
        underworld.onClick.AddListener(Underworld);
        crypt.onClick.AddListener(Crypt);  
        palace.onClick.AddListener(Palace);
        back.onClick.AddListener(Back);
    }

    void Play()
    {
        audioManager.playSFX(audioManager.BossHit);
        SceneManager.LoadScene(1);//underworld
    }

    void Levels()
    {
        audioManager.playSFX(audioManager.BossHit);
        start.SetActive(false);
        levelSelect.SetActive(true);
        back.gameObject.SetActive(true);
    }

    void Options()
    {
        audioManager.playSFX(audioManager.BossHit);
        start.SetActive(false);
        optionsMenu.SetActive(true);
        back.gameObject.SetActive(true);

    }

    void Exit()
    {
        audioManager.playSFX(audioManager.BossHit);
        Application.Quit();
    }

    void Underworld()
    {
        audioManager.playSFX(audioManager.BossHit);
        SceneManager.LoadScene(1);//underworld
    }
    void Crypt()
    {
        audioManager.playSFX(audioManager.BossHit);
        SceneManager.LoadScene(2);//crypt
    }
    void Palace()
    {
        audioManager.playSFX(audioManager.BossHit);
        SceneManager.LoadScene(3);//palace
    }
    void Back()
    {
        levelSelect.SetActive(false);
        optionsMenu.SetActive(false);
        start.SetActive(true);
        back.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
