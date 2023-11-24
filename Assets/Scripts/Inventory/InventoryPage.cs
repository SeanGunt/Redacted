using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    public GameObject contentPanel;
    List<InventoryItem> listOfUIItems = new List<InventoryItem>();
    [SerializeField] private MouseFollower mouseFollower;
    [SerializeField] private GameObject ghostImagePrefab;
    private GameObject ghostImage;
    private int currentlyDraggedItemIndex;

    public void AddItemToList(InventoryItem inventoryItem)
    {
        listOfUIItems.Add(inventoryItem);
    }

    public int CheckInventorySize()
    {
        return listOfUIItems.Count;
    }

    public void CreateOrDestroyGhostItem(bool val, InventoryItem inventoryItem)
    {
        if (val)
        {
            ghostImage = Instantiate(ghostImagePrefab, mouseFollower.gameObject.transform);
            Image image =  ghostImage.GetComponent<Image>();
            image.sprite = inventoryItem.itemImage.sprite;
            image.color = new Color(inventoryItem.itemImage.color.r, inventoryItem.itemImage.color.g, inventoryItem.itemImage.color.b, 0.4f);
        }
        else
        {
            Destroy(ghostImage);
        }
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
    }

    public void HandleBeginDrag(InventoryItem inventoryItem)
    {
        int index = listOfUIItems.IndexOf(inventoryItem);
        if (index == -1) return;
        currentlyDraggedItemIndex = index;
        CreateOrDestroyGhostItem(true, inventoryItem);
        mouseFollower.Toggle(true);
    }

    public void HandleSwap(InventoryItem inventoryItem)
    {
        int index = listOfUIItems.IndexOf(inventoryItem);
        if (index == -1) return;
        Debug.Log("Swapped");
    }

    public void HandleEndDrag(InventoryItem inventoryItem)
    {
        mouseFollower.Toggle(false);
        CreateOrDestroyGhostItem(false, inventoryItem);
    }
    
}
