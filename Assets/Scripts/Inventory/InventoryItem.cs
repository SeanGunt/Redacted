using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryItem : MonoBehaviour, IPointerClickHandler,
        IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    [HideInInspector] public Image itemImage;
    [HideInInspector] public bool itemInitialized;
    private PlayerInput playerInput;
    public event Action<InventoryItem> OnItemBeginDrag, OnItemEndDrag, OnItemDropped, OnItemClicked;

    private void Start()
    {
        itemImage = GetComponent<Image>();
        playerInput = GetComponentInParent<PlayerInput>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDropped?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (playerInput.actions["LeftClickUI"].triggered)
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
