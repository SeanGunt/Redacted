using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class ShopManager : MonoBehaviour, IDistanceInteractable
{
    private GameObject playerGO;
    private ParticleSystem eerieParticles;
    private bool eerieParticlesPlaying;
    private bool shopMusicPlaying;
    private AudioClip previousAudioClip;
    private PlayerBase playerBase;
    private float timeToRestartBackgroundClip;
    private float timeToRestartShopClip;
    private Light2D shopLight;
    [SerializeField] SpriteRenderer faceSpriteRenderer;
    [SerializeField] private Sprite[] faceSprites;
    [SerializeField] private GameObject interactCanvas;

    private void Start()
    {
        GetPlayer();
        eerieParticles = GetComponentInChildren<ParticleSystem>();
        shopLight = GetComponentInChildren<Light2D>();
        eerieParticlesPlaying = false;
        faceSpriteRenderer.sprite = faceSprites[1];
        interactCanvas.SetActive(false);
    }
    public void GetPlayer()
    {
        playerGO = GameManager.Instance.player.gameObject;
        playerBase = playerGO.GetComponent<PlayerBase>();
    }
    public void HandleDistanceInteraction()
    {
        if (!ShopUI.Instance.IsShopOpen())
        {
            ShopUI.Instance.OpenUI();
        }
        else if (ShopUI.Instance.IsShopOpen())
        {
            ShopUI.Instance.CloseUI();
        }
    }

    public void HandleDisplayInteractKey(bool value)
    {
        interactCanvas.SetActive(value);
    }

    private void Update()
    {
        HandleShopEyes();
        HandleEerieParticles();
        HandleShopLighting();
        HandleMusicFade();
    }

    private void HandleShopEyes()
    {
        if (playerGO.transform.position.x < transform.position.x && playerGO.transform.position.x < transform.position.x - 1.2f)
        {
            faceSpriteRenderer.sprite = faceSprites[0];
        }
        else if (playerGO.transform.position.x >= transform.position.x - 1.2f && playerGO.transform.position.x <= transform.position.x + 1.2f)
        {
            faceSpriteRenderer.sprite = faceSprites[1];
        }
        else if (playerGO.transform.position.x > transform.position.x && playerGO.transform.position.x > transform.position.x + 1.2f)
        {
            faceSpriteRenderer.sprite = faceSprites[2];
        }
    }

    private void HandleEerieParticles()
    {
        float distanceToPlayer = Vector2.Distance(playerGO.transform.position, transform.position);
        if (distanceToPlayer <= 2.25f && !eerieParticlesPlaying)
        {
            eerieParticles.Play();
            eerieParticlesPlaying = true;
        }
        else if (distanceToPlayer > 3 && eerieParticlesPlaying)
        {
            eerieParticles.Stop();
            eerieParticlesPlaying = false;
        }
    }

    private void HandleMusicFade()
    {
        float distanceToPlayer = Vector2.Distance(playerGO.transform.position, transform.position);
        if (distanceToPlayer <= 2.25f && !shopMusicPlaying)
        {
            GameManager.Instance.FreezeTime();
            ShopFreezeTime();
            previousAudioClip = MusicManager.instance.musicSource.clip;
            MusicManager.instance.FadeTracks(MusicManager.instance.tracks[1], timeToRestartShopClip);
            timeToRestartBackgroundClip = MusicManager.instance.musicSource.time;
            shopMusicPlaying = true;
        }
        else if (distanceToPlayer > 3f && shopMusicPlaying)
        {
            GameManager.Instance.UnFreezeTime();
            ShopUnFreezeTime();
            MusicManager.instance.FadeTracks(previousAudioClip, timeToRestartBackgroundClip);
            timeToRestartShopClip =  MusicManager.instance.musicSource.time;
            shopMusicPlaying = false;
        }
    }

    private void HandleShopLighting()
    {
        float distanceToPlayer = Vector2.Distance(playerGO.transform.position, transform.position);
        if (distanceToPlayer >= 8f) return;
        float normalizedDistance = Mathf.Clamp(distanceToPlayer / 4f, 0f, 1f);
        GameManager.Instance.globalLight.intensity = Mathf.Pow(normalizedDistance, 2);
        shopLight.intensity = Mathf.Clamp(6f - distanceToPlayer, 0f, 5f);
    }

    public void ShopFreezeTime()
    {
        var freezables = FindObjectsOfType<MonoBehaviour>().OfType<IShopFreeze>();
        foreach(var freezable in freezables)
        {
            freezable.HandleOnShopFreeze();
        }
    }

    public void ShopUnFreezeTime()
    {
        var freezables = FindObjectsOfType<MonoBehaviour>().OfType<IShopFreeze>();
        foreach(var freezable in freezables)
        {
            freezable.HandleOnShopUnFreeze();
        }
    } 
}
