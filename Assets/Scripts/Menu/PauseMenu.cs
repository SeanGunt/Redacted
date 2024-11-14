using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private Volume globalVolume;
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
        playerUI =  GameObject.FindGameObjectWithTag("PlayerUI");
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

    public void ExitToDesktop()
    {
        Application.Quit();
    }

    private void HandlePause(float timeScale, bool playerUIActive, bool pauseMenuCanvasActive, bool isPaused)
    {
        paused = isPaused;
        Time.timeScale = timeScale;
        playerUI.SetActive(playerUIActive);
        pauseMenuCanvas.SetActive(pauseMenuCanvasActive);
        if (globalVolume.profile.TryGet(out DepthOfField depthOfField))
        {
            depthOfField.active = isPaused;
        }
    }
}
