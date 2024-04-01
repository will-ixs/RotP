using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigScrollController : MonoBehaviour
{
    public GameObject childText;
    public void selfDisable()
    {
        gameObject.SetActive(false);
    }
    
    public void disableText()
    {
        childText.SetActive(false);
    }

    public void enableText()
    {
        childText.SetActive(true);
    }
}
