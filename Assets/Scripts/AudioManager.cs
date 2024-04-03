using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Source -----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource osirisSource;

    [Header("----- Audio Clip -----")]
    public AudioClip background;
    public AudioClip bossBackground; 
    public AudioClip death;
    public AudioClip attack;
    public AudioClip heavyAttack;
    public AudioClip soulSteal;
    public AudioClip BossHit;
    public AudioClip Hit;
    public AudioClip Gates;
    public AudioClip OsirisSpawn;
    public AudioClip OsirisMusic;


    private void Start() 
    {
        musicSource.clip = background;
        osirisSource.clip = OsirisMusic;
        osirisSource.loop = true;
        musicSource.Play();
    }



    public void playSFX(AudioClip clip) 
    {
        SFXSource.PlayOneShot(clip);
    }
}
