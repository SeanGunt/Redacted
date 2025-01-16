using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIAbilityInformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject abilityInformationToDisplayGO;
    public void OnPointerEnter(PointerEventData eventData)
    {
        abilityInformationToDisplayGO.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        abilityInformationToDisplayGO.SetActive(false);
    }
}
