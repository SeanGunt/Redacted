using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string mainGameSceneName = "Level_1";

    void Start()
    {
        // Load the main game scene when initialization is complete.
        SceneManager.LoadScene(mainGameSceneName);
    }
}
