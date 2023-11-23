using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    public GameObject contentPanel;
    List<InventoryItem> listOfUIItems = new List<InventoryItem>();
    [SerializeField] private MouseFollower mouseFollower;

    public void AddItemToList(InventoryItem inventoryItem)
    {
        listOfUIItems.Add(inventoryItem);
    }

    public int CheckInventorySize()
    {
        return listOfUIItems.Count;
    }

    public void InitializeUIHandling(InventoryItem inventoryItem)
    {
        inventoryItem.OnItemClicked += HandleItemSelection;
        inventoryItem.OnItemBeginDrag += HandleBeginDrag;
        inventoryItem.OnItemDropped += HandleSwap;
        inventoryItem.OnItemEndDrag += HandleEndDrag;
    }

    public void HandleItemSelection(InventoryItem inventoryItem)
    {
        int index = listOfUIItems.IndexOf(inventoryItem);
        if (index == -1) return;
        Debug.Log(index);
    }

    public void HandleBeginDrag(InventoryItem inventoryItem)
    {
        mouseFollower.Toggle(false);
        Debug.Log("Dragging Started");
    }

    public void HandleSwap(InventoryItem inventoryItem)
    {
        Debug.Log("Swapped");
    }

    public void HandleEndDrag(InventoryItem inventoryItem)
    {
        Debug.Log("Dragging Ended");
    }
    
}
