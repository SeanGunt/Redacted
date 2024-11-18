using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class ShopManager : MonoBehaviour, IRay
{
    private GameObject playerGO;
    private ParticleSystem eerieParticles;
    private bool eerieParticlesPlaying;
    private Light2D shopLight;
    [SerializeField] SpriteRenderer faceSpriteRenderer;
    [SerializeField] private Sprite[] faceSprites;  

    private void Start()
    {
        GetPlayer();
        faceSpriteRenderer.sprite = faceSprites[1];
        eerieParticles = GetComponentInChildren<ParticleSystem>();
        eerieParticlesPlaying = false;
        shopLight = GetComponentInChildren<Light2D>();
    }
    public void GetPlayer()
    {
        playerGO = GameManager.Instance.player.gameObject;
    }
    public void HandleRaycastInteraction()
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

    private void Update()
    {
        HandleShopEyes();
        HandleEerieParticles();
        HandleShopLighting();
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

    private void HandleShopLighting()
    {
        float distanceToPlayer = Vector2.Distance(playerGO.transform.position, transform.position);
        float normalizedDistance = Mathf.Clamp01(distanceToPlayer / 4f);
        GameManager.Instance.globalLight.intensity = normalizedDistance;
        shopLight.intensity = Mathf.Clamp(6f - distanceToPlayer, 0f, 5f);
    }
}
