using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpopardSpit : MonoBehaviour
{
    private GameObject player;
    private bool spitting = false;
    private float spitAngleDelta;
    [SerializeField] private float spitCooldown;
    private float useCooldown;
    private float dmg = 12.0f;
    [SerializeField] private Animator anim;
    [SerializeField] private int iterations;
    private float angleToPlayer;
    [SerializeField] GameObject spitPrefab;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Disable()
    {
        spitting = false;
    }

    public void Enable()
    {
        spitting = true;
        //Find angle to player
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        float angleToPlayer = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
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
        /*
        for(int i = 0; i < iterations; i++){
            Instantiate(spitPrefab, transform.position + ve, Quaternion.Euler());
Declaration
        }*/
    }

}
