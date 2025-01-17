using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICooldownInformation : MonoBehaviour
{
    private TextMeshProUGUI cooldownText;
    private PlayerBase playerBase;
    [SerializeField] private bool qSelected, wSelected, eSelected, rSelected;
    private void Awake()
    {
        cooldownText = GetComponent<TextMeshProUGUI>();
        playerBase = GetComponentInParent<PlayerBase>();
    }

    private void OnEnable()
    {
        if (qSelected)
        {
            cooldownText.text = playerBase.qCooldownAmount.ToString() + "s";
        }

        if (wSelected)
        {
            cooldownText.text = playerBase.wCooldownAmount.ToString() + "s";
        }

        if (eSelected)
        {
            cooldownText.text = playerBase.eCooldownAmount.ToString() + "s";
        }

        if (rSelected)
        {
            cooldownText.text = playerBase.rCooldownAmount.ToString() + "s";
        }
    }
}
