using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private GameObject pauseMenuCanvas;
    private GameObject playerUI;
    private bool paused;

    private void Start()
    {
        playerUI =  GameObject.FindGameObjectWithTag("PlayerUI");
        playerInput = GameManager.Instance.player.GetComponent<PlayerInput>();
        HandlePause(1f, true, false, false);
    }

    private void Update()
    {
        if (!paused && playerInput.actions["Pause"].triggered)
        {
            Pause();
        }
        else if (paused && playerInput.actions["Pause"].triggered)
        {
            UnPause();
        }
    }

    public void Pause()
    {
        HandlePause(0f, false, true, true);
    }

    public void UnPause()
    {
        HandlePause(1f, true, false, false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void HandlePause(float timeScale, bool playerUIActive, bool pauseMenuCanvasActive, bool isPaused)
    {
        paused = isPaused;
        Time.timeScale = timeScale;
        playerUI.SetActive(playerUIActive);
        pauseMenuCanvas.SetActive(pauseMenuCanvasActive);
    }
}
