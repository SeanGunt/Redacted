using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OnHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemImageShowcase;
    [SerializeField] private int itemIndex;
    [SerializeField] private TextMeshProUGUI physicalDamageText, magicalDamageText, physicalDefenceText, magicDefenceText, critChanceText, cdrText, speedText,
    bonusHealthText, healthRegenText, pickupRangeText, itemNameText, activeAbilityText, passiveAbilityText;
    private ShopUI shopUI;
    private Image imageToShowcase;
    private void Awake()
    {
        imageToShowcase = GetComponent<Image>();
        shopUI = GetComponentInParent<ShopUI>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemImageShowcase.sprite = imageToShowcase.sprite;
        itemNameText.text = shopUI.items[itemIndex].GetComponent<ItemBase>().itemName;
        physicalDamageText.text = shopUI.items[itemIndex].GetComponent<ItemBase>().physicalDamage.ToString();
        magicalDamageText.text = shopUI.items[itemIndex].GetComponent<ItemBase>().magicalDamage.ToString();
        physicalDefenceText.text = shopUI.items[itemIndex].GetComponent<ItemBase>().physicalResistance.ToString();
        magicDefenceText.text = shopUI.items[itemIndex].GetComponent<ItemBase>().magicalResistance.ToString();
        critChanceText.text = shopUI.items[itemIndex].GetComponent<ItemBase>().critChance.ToString();
        cdrText.text = shopUI.items[itemIndex].GetComponent<ItemBase>().coolDownReduction.ToString();
        speedText.text = shopUI.items[itemIndex].GetComponent<ItemBase>().moveSpeed.ToString();
        bonusHealthText.text = shopUI.items[itemIndex].GetComponent<ItemBase>().health.ToString();
        healthRegenText.text = shopUI.items[itemIndex].GetComponent<ItemBase>().healthRegen.ToString();
        pickupRangeText.text = shopUI.items[itemIndex].GetComponent<ItemBase>().pickupRange.ToString();
        activeAbilityText.text = shopUI.items[itemIndex].GetComponent<ItemBase>().activeAbilityText;
        passiveAbilityText.text = shopUI.items[itemIndex].GetComponent<ItemBase>().passiveAbilityText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemNameText.text = null;
        physicalDamageText.text = 0f.ToString();
        magicalDamageText.text = 0f.ToString();
        physicalDefenceText.text = 0f.ToString();
        magicDefenceText.text = 0f.ToString();
        critChanceText.text = 0f.ToString();
        cdrText.text = 0f.ToString();
        speedText.text = 0f.ToString();
        bonusHealthText.text = 0f.ToString();
        healthRegenText.text = 0f.ToString();
        pickupRangeText.text = 0f.ToString();
        activeAbilityText.text = "Active Ability: ";
        passiveAbilityText.text = "Passive Ability: ";
        itemImageShowcase.sprite = null;
    }
}
