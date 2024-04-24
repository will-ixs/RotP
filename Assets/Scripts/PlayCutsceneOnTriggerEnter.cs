using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayCutsceneOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutscene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cutscene.Play();
    }
}
