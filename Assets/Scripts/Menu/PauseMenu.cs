using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputActionReference pauseRef;
    [SerializeField] private GameObject pauseMenuCanvas;
    private GameObject playerUI;
    public bool paused;

    private void Awake()
    {
        playerUI =  GameObject.FindGameObjectWithTag("PlayerUI");
    }

    private void OnEnable()
    {
        pauseRef.action.performed += Pause;
    }

    private void OnDisable()
    {
        pauseRef.action.performed -= Pause;
    }

    public void Pause(InputAction.CallbackContext obj)
    {
        Time.timeScale = 0f;
        playerUI.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
        playerUI.SetActive(true);
        pauseMenuCanvas.SetActive(false);
    }
}
