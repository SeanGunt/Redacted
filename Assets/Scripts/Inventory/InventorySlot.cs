using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector] public bool inventorySlotFilled;
    [SerializeField] private string abilityBinding;
    private InventoryItem inventoryItem;
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.actions[abilityBinding].triggered)
        {
            ActivateItemInSlot();
        }
    }

    private void ActivateItemInSlot()
    {
        inventoryItem = GetComponentInChildren<InventoryItem>();
        IItem iItem = GetComponentInChildren<IItem>();
        if (!inventoryItem.itemInitialized || inventoryItem == null || iItem == null) return;
        iItem.ActiveAbility();
    }
}
