using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] private string scene;

    void Update()
    {
        SceneManager.LoadScene(scene);
    }
}
