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
        playerInput = GameManager.Instance.player.GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.actions[abilityBinding].triggered)
        {
            inventoryItem = GetComponentInChildren<InventoryItem>();
            if (!inventoryItem.itemInitialized || inventoryItem == null) return;
            
        }
    }
}
