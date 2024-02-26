using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance {get; private set;}
    private bool shopActive;
    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject shopUI;
    private InventoryPage inventoryPage;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        inventoryPage = GameManager.Instance.player.GetComponentInChildren<InventoryPage>();
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
