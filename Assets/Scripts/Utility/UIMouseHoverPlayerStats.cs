using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UIMouseHoverPlayerStats : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject statInformationGO;
    [SerializeField] private GameObject extraInfoGO;
    [SerializeField] private string abilityInformationText;
    [SerializeField] private TextMeshProUGUI infoTextShadow;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI extraInfoText;
    [SerializeField] private TextMeshProUGUI extraInfoTextShadow;
    [SerializeField] private bool isPhysicalResistance;
    [SerializeField] private  bool isMagicalResistance;
    private PlayerBase playerBase;

    private void Awake()
    {
        statInformationGO.SetActive(false);
        playerBase = GetComponentInParent<PlayerBase>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isPhysicalResistance)
        {
            float physicalResistance = Mathf.Log(playerBase.physicalResistance, 10000) * 100f;
            extraInfoText.text = "Curent percentage decreased " + physicalResistance.ToString("n2") + "%";
            extraInfoTextShadow.text = "Curent percentage decreased " + physicalResistance.ToString("n2") + "%";
            extraInfoGO.SetActive(true);
        }
        
        if (isMagicalResistance)
        {
            float magicalResistance = Mathf.Log(playerBase.magicalResistance, 10000) * 100f;
            extraInfoText.text = "Curent percentage decreased " + magicalResistance.ToString("n2") + "%";
            extraInfoTextShadow.text = "Curent percentage decreased " + magicalResistance.ToString("n2") + "%";
            extraInfoGO.SetActive(true);
        }
        infoTextShadow.text = abilityInformationText;
        infoText.text = abilityInformationText;
        statInformationGO.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPhysicalResistance)
        {
            extraInfoGO.SetActive(false);
        }

        if (isMagicalResistance)
        {
            extraInfoGO.SetActive(false);
        }
        infoTextShadow.text = null;
        infoText.text = null;
        statInformationGO.SetActive(false);
    }
}
