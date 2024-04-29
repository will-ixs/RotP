using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayCutsceneOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutscene;
    private AudioManager audioManager;
    private bool triggeronce;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        triggeronce = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cutscene.Play();
        if(!triggeronce)
        {
            audioManager.playSFX(audioManager.Progress);
            triggeronce = true;
        }
            
        
    }
}
