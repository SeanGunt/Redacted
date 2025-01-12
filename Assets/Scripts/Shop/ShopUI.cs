using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance {get; private set;}
    private bool shopActive;
    public GameObject[] items;
    [SerializeField] private GameObject shopUI;
    private InventoryPage inventoryPage;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenUI()
    {
        shopUI.SetActive(true);
        shopActive = true;
        Time.timeScale = 0f;
    }

    public void CloseUI()
    {
        shopUI.SetActive(false);
        shopActive = false;
        Time.timeScale = 1f;
    }

    public bool IsShopOpen()
    {
        return shopActive;
    }

    public void PurchaseItem(int index)
    {
        if (items[index].GetComponent<ItemBase>().price >= MoneyManager.instance.money)
        {
            return;
        }
        inventoryPage = GameManager.Instance.player.GetComponentInChildren<InventoryPage>();
        InventoryItem inventoryItem = inventoryPage.PurchaseInventoryItem();
        if (inventoryItem != null)
        {
            Image image = inventoryItem.gameObject.GetComponent<Image>();
            GameObject itemToPurchase = Instantiate(items[index], inventoryItem.transform);
            ItemBase itemBase = itemToPurchase.GetComponent<ItemBase>();
            image.sprite = itemBase.imageSprite;
            MoneyManager.instance.money -= itemBase.price;
            image.color = Color.white;
        }
    }
}
