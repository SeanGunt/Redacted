using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryItem : MonoBehaviour, IPointerClickHandler,
        IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler, IShopFreeze
{
    [HideInInspector] public Image itemImage;
    [HideInInspector] public bool itemInitialized;
    private PlayerInput playerInput;
    private Color initialColor;
    private bool frozen;
    public event Action<InventoryItem> OnItemBeginDrag, OnItemEndDrag, OnItemDropped, OnItemClicked;

    private void Start()
    {
        itemImage = GetComponent<Image>();
        initialColor = itemImage.color;
        playerInput = GetComponentInParent<PlayerInput>();
    }

    private void Update()
    {
        if (frozen)
        {
            itemImage.color = Color.red;
        }
        else
        {
            if (itemInitialized)
            {
                itemImage.color = Color.white;
            }
            else
            {
                itemImage.color = initialColor;
            }
        }
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

    public void HandleOnShopFreeze()
    {
        frozen = true;
    }

    public void HandleOnShopUnFreeze()
    {
        frozen = false;
    }
}
