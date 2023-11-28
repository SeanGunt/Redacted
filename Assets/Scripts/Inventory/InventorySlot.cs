using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector] public bool inventorySlotFilled;
    private InventoryItem inventoryItem;

    private void Awake()
    {
        inventoryItem = GetComponentInChildren<InventoryItem>();
    }
}
