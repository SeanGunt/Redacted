using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuyItem : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;
    private GameObject player;

    private void Start()
    {
        player = GameManager.Instance.player;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && !shopManager.shopActive)
        {
            shopManager.HandleShopUI(true, 0);
            
        }
        else if (Input.GetKeyDown(KeyCode.L) && shopManager.shopActive)
        {
            shopManager.HandleShopUI(false, 1);
        }
    }
}
