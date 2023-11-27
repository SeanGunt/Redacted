using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestBuyItem : MonoBehaviour
{
    [SerializeField] private InventoryItem testItem;
    private GameObject player;
    private InventoryPage inventoryPage;

    private void Start()
    {
        player = GameManager.Instance.player;
        inventoryPage = player.GetComponentInChildren<InventoryPage>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && inventoryPage.CheckInventorySize() < 4)
        {
            Transform slotTransform = inventoryPage.CheckAvailableInventorySlots();
            InventoryItem inventoryItem = Instantiate(testItem, slotTransform.position, Quaternion.identity, slotTransform);
            Image image = inventoryItem.gameObject.GetComponent<Image>();
            float randomRValue = Random.Range(0f,1f);
            float randomRGalue = Random.Range(0f,1f);
            float randomRBalue = Random.Range(0f,1f);
            image.color = new Color(randomRValue, randomRGalue, randomRBalue, 1f);
            inventoryPage.AddItemToList(inventoryItem);
            inventoryPage.InitializeUIHandling(inventoryItem);
        }
    }
}
