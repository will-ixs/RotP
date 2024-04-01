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

    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(Play);
        levels.onClick.AddListener(Levels);
        options.onClick.AddListener(Options);
        exit.onClick.AddListener(Exit);
    }

    void Play()
    {
        SceneManager.LoadScene(1);//underworld
    }

    void Levels()
    {

    }

    void Options()
    {

    }

    void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
