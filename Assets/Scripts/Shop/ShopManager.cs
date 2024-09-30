using System;
using UnityEngine;
public class ShopManager : MonoBehaviour, IRay
{
    private GameObject playerGO;
    [SerializeField] SpriteRenderer faceSpriteRenderer;
    [SerializeField] private Sprite[] faceSprites;  

    private void Start()
    {
        GetPlayer();
        faceSpriteRenderer.sprite = faceSprites[1];
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
}
