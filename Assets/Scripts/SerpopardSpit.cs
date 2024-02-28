using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SerpopardSpit : MonoBehaviour
{
    private GameObject player;
    private bool spitting = false;
    public float spitAngleDelta;
    public int spitCounter;
    private float useCooldown;
    public float angleToPlayer;


    [SerializeField] private Animator anim;
    [SerializeField] private int iterations;
    [SerializeField] private float spitCooldown;
    [SerializeField] GameObject spitPrefab;
    void Start()
    {
        spitCounter = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Disable()
    {
        spitting = false;
    }

    public void Enable()
    {
        //Find angle to player
        if (!spitting)
        {
            spitCounter = 0;
            Vector3 vectorToPlayer = player.transform.position - transform.position;
            angleToPlayer = 90.0f + (Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg);
            spitting = true;
        }
    }

    void Update()
    {
        useCooldown -= Time.deltaTime;
        if(spitting)
        {
            if(useCooldown <= 0.0f){
                Spit();
                useCooldown = spitCooldown;
            }
        }
    }

    private void Spit()
    {
        float startingAngle = angleToPlayer - 35.0f;
        float endingAngle = angleToPlayer + 35.0f;
        spitAngleDelta = (endingAngle - startingAngle) / (float)iterations;
        Instantiate(spitPrefab, transform.position + new Vector3(0.0f, 0.75f, 0.0f), Quaternion.Euler(0, 0, startingAngle + (spitAngleDelta * spitCounter)));
        Instantiate(spitPrefab, transform.position + new Vector3(0.0f, 0.75f, 0.0f), Quaternion.Euler(0, 0, endingAngle - (spitAngleDelta * spitCounter)));
        spitCounter++;
    }
}
