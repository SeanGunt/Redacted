using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IShopFreeze
{
    [HideInInspector] public bool inventorySlotFilled;
    [SerializeField] private string abilityBinding;
    private bool frozen;
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
        if (frozen) return;
        inventoryItem = GetComponentInChildren<InventoryItem>();
        IItem iItem = GetComponentInChildren<IItem>();
        if (!inventoryItem.itemInitialized || inventoryItem == null || iItem == null) return;
        iItem.ActiveAbility();
    }

    public void HandleOnShopFreeze()
    {
        frozen = true;
    }

    public void HandleOnShopUnFreeze()
    {
        frozen = false;
    }
}
