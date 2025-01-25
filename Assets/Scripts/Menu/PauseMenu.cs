using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    [HideInInspector] public AudioSource audioSource;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private Volume globalVolume;
    public AudioClip menuHoverAudioClip;
    private GameObject playerUI;
    private bool paused;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (playerUI == null) return;
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
