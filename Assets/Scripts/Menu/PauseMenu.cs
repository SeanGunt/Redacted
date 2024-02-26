using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    [SerializeField] private GameObject pauseMenuCanvas;
    private GameObject playerUI;
    private bool paused;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerUI =  GameObject.FindGameObjectWithTag("PlayerUI");
        UnPause();
    }

    public bool IsPauseMenuOpen()
    {
        return paused;
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
