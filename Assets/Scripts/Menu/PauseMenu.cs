using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    [HideInInspector] public AudioSource audioSource;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject deathMenuCanvas;
    [SerializeField] private GameObject winMenuCanvas;
    [SerializeField] private Volume globalVolume;
    public AudioClip menuHoverAudioClip;
    private GameObject playerUI;
    private bool paused;
    private bool deathScreenActive;
    private bool winScreenActive;

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

    private void Update()
    {
        if (GameManager.Instance.player.GetComponent<PlayerBase>().dead && !deathScreenActive)
        {
            MusicManager.instance.SwapTracks(MusicManager.instance.tracks[4]);
            Time.timeScale = 0f;
            deathMenuCanvas.SetActive(true);
            playerUI =  GameObject.FindGameObjectWithTag("PlayerUI");
            playerUI.SetActive(false);
            deathScreenActive = true;
        }

        if (TimerManager.instance != null && TimerManager.instance.wonRun && !winScreenActive)
        {
            MusicManager.instance.SwapTracks(MusicManager.instance.tracks[5]);
            Time.timeScale = 0f;
            winMenuCanvas.SetActive(true);
            playerUI =  GameObject.FindGameObjectWithTag("PlayerUI");
            playerUI.SetActive(false);
            winScreenActive = true;
        }
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

    private void HandlePause(float timeScale, bool playerUIActive, bool pauseMenuCanvasActive, bool isPaused)
    {
        if (GameManager.Instance.player.GetComponent<PlayerBase>().dead) return;
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
