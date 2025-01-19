using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && SaveManager.instance._gameData.tutorialCompleted)
        {
            StartGame();
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Initialization");
    }
    
}
