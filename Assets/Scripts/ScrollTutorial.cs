using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollTutorial : MonoBehaviour
{
    private Animator anim;
    private bool inRange;
    [SerializeField] private GameObject readText;
    [SerializeField] private GameObject bigScroll;
    [SerializeField] private Text bigScrollText;
    [TextArea(5, 10)]
    [SerializeField] private string scrollText;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(inRange) {

            if (Input.GetKeyDown(KeyCode.E))
            {
                bool scrollActive = bigScroll.activeInHierarchy;
                if (!scrollActive)
                {
                    bigScroll.SetActive(true);
                    bigScrollText.text = scrollText;
                }
                else
                {
                    bigScroll.GetComponent<Animator>().SetTrigger("Close");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Display Text Showing Press E
            readText.SetActive(true);
            inRange = true;
            anim.SetTrigger("Open");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(bigScroll.activeInHierarchy)
            {
                bigScroll.GetComponent<Animator>().SetTrigger("Close");
            }
            readText.SetActive(false);
            inRange = false;
            anim.SetTrigger("Close");
        }
    }
}
