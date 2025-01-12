using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemImageShowcase;
    private Image imageToShowcase;
    private void Awake()
    {
        imageToShowcase = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemImageShowcase.sprite = imageToShowcase.sprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemImageShowcase.sprite = null;
    }
}
