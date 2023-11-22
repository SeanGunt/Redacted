using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    public GameObject contentPanel;
    List<InventoryItem> listOfUIItems = new List<InventoryItem>();

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
        Debug.Log(inventoryItem.name);
    }

    public void HandleBeginDrag(InventoryItem inventoryItem)
    {

    }

    public void HandleSwap(InventoryItem inventoryItem)
    {

    }

    public void HandleEndDrag(InventoryItem inventoryItem)
    {

    }
    
}
