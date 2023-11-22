using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryItem : MonoBehaviour
{
    private Image itemImage;
    private PlayerInput playerInput;
    public event Action<InventoryItem> OnItemBeginDrag, OnItemEndDrag, OnItemDropped, OnItemClicked;

    private void Awake()
    {
        itemImage = GetComponent<Image>();
        playerInput = GameManager.Instance.player.GetComponent<PlayerInput>();
    }

    private void OnLeftClickUI()
    {
        OnItemClicked?.Invoke(this);
    }
    public void OnBeginDrag()
    {
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop()
    {
        OnItemDropped?.Invoke(this);
    }
    
    public void OnPointerClick(BaseEventData data)
    {
        if (playerInput.actions["LeftClickUI"].triggered)
        {
            OnLeftClickUI();
        }
    }
}
