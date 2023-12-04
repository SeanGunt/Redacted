using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public bool shopActive;
    [SerializeField] private GameObject[] items;
    private InventoryPage inventoryPage;

    private void Start()
    {
        inventoryPage = GameManager.Instance.player.GetComponentInChildren<InventoryPage>();
    }

    public void HandleShopUI(bool isActive, int timeScale)
    {
        gameObject.SetActive(isActive);
        shopActive = isActive;
        Time.timeScale = timeScale;
    }

    public void PurchaseItem(int index)
    {
        InventoryItem inventoryItem = inventoryPage.PurchaseInventoryItem();
        if (inventoryItem != null)
        {
            Image image = inventoryItem.gameObject.GetComponent<Image>();
            GameObject itemToPurchase = Instantiate(items[index], inventoryItem.transform);
            image.sprite = itemToPurchase.GetComponent<ItemBase>().imageSprite;
            image.color = Color.white;
        }
    }
}
