using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            InventoryItem inventoryItem = Instantiate(testItem, Vector3.zero, Quaternion.identity, inventoryPage.contentPanel.transform);
            inventoryPage.AddItemToList(inventoryItem);
            inventoryPage.InitializeUIHandling(inventoryItem);
        }
    }
}
