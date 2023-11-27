using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    List<InventoryItem> listOfUIItems = new List<InventoryItem>();
    private InventoryItem currentlyDraggedItem;
    [SerializeField] private MouseFollower mouseFollower;
    [SerializeField] private GameObject ghostImagePrefab;
    [SerializeField] public InventorySlot[] inventorySlots;
    private GameObject ghostImage;
    private int currentlyDraggedItemIndex;
    private int slotIndexToAdd;
    private Transform parentToSwap;

    public void AddItemToList(InventoryItem inventoryItem)
    {
        listOfUIItems.Add(inventoryItem);
        inventoryItem.currentInventorySlot = slotIndexToAdd;
    }

    public int CheckInventorySize()
    {
        return listOfUIItems.Count;
    }

    public void SwapItems(int itemIndex_1, int itemIndex_2, InventoryItem inventoryItem)
    {
        InventoryItem item1 = listOfUIItems[itemIndex_1];
        listOfUIItems[itemIndex_1] = listOfUIItems[itemIndex_2];
        listOfUIItems[itemIndex_2] = item1;

        currentlyDraggedItem.transform.SetParent(inventoryItem.transform.parent, false);
        inventoryItem.transform.SetParent(parentToSwap, false);
    }

    public Transform CheckAvailableInventorySlots()
    {
        for (int i = 0; i < 4; i ++)
        {
            if (inventorySlots[i].inventorySlotFilled)
            {
                continue;
            }
            else
            {
                slotIndexToAdd = i;
                inventorySlots[i].inventorySlotFilled = true;
                return inventorySlots[i].transform;
            }
        }
        return null;
    }

    public void CreateOrDestroyGhostItem(bool val, InventoryItem inventoryItem)
    {
        if (val)
        {
            ghostImage = Instantiate(ghostImagePrefab, mouseFollower.gameObject.transform);
            Image image =  ghostImage.GetComponent<Image>();
            image.sprite = inventoryItem.itemImage.sprite;
            image.color = new Color(inventoryItem.itemImage.color.r, inventoryItem.itemImage.color.g, inventoryItem.itemImage.color.b, 0.6f);
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
        Debug.Log("Index of inventory item in list: " + listOfUIItems.IndexOf(inventoryItem));
        Debug.Log("Index of inventory item in inventory slots: " + inventoryItem.currentInventorySlot);
    }

    public void HandleBeginDrag(InventoryItem inventoryItem)
    {
        currentlyDraggedItemIndex = listOfUIItems.IndexOf(inventoryItem);
        currentlyDraggedItem = inventoryItem;
        parentToSwap = inventoryItem.transform.parent;
        CreateOrDestroyGhostItem(true, inventoryItem);
        mouseFollower.Toggle(true);
    }

    public void HandleSwap(InventoryItem inventoryItem)
    {
        int itemToSwapIndex = listOfUIItems.IndexOf(inventoryItem);
        SwapItems(currentlyDraggedItemIndex, itemToSwapIndex, inventoryItem);

        Debug.Log("Swapped");
    }

    public void HandleEndDrag(InventoryItem inventoryItem)
    {
        mouseFollower.Toggle(false);
        CreateOrDestroyGhostItem(false, inventoryItem);
    }
    
}
