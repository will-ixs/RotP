using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharaohSwordController : MonoBehaviour
{
    private GameObject player;
    private GameObject pharaoh;
    [SerializeField] private GameObject swordSprite;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pharaoh = GameObject.FindGameObjectWithTag("Pharaoh");
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, 120.0f * Time.deltaTime);
    }

    private void Setup()
    {
        Destroy(gameObject, 3.0f);
        transform.position = (pharaoh.transform.position + player.transform.position)/2.0f;
        swordSprite.transform.position = pharaoh.transform.position;
    }
}
