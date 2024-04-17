using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool isLevel;
    [Header("----- Audio Source -----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource osirisSource;

    [Header("----- Audio Clip -----")]
    public AudioClip background;
    public AudioClip bossBackground; 
    public AudioClip winSound;
    public AudioClip attack;
    public AudioClip heavyAttack;
    public AudioClip soulSteal;
    public AudioClip BossHit;
    public AudioClip Hit;
    public AudioClip Gates;
    public AudioClip GatesUp;
    public AudioClip OsirisSpawn;
    public AudioClip OsirisMusic;
    public AudioClip TrapSounds;
    public AudioClip Progress;
    public AudioClip Extra;


    private void Start() 
    {
        musicSource.clip = background;
        musicSource.Play();
        if(isLevel)
        {
            osirisSource.clip = OsirisMusic;
            osirisSource.loop = true;
        }
    }



    public void playSFX(AudioClip clip) 
    {
        SFXSource.PlayOneShot(clip);
    }

    public void newBGM(AudioClip clip) 
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
}
