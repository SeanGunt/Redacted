using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class AbilityManager : MonoBehaviour
{
    private WeaponBase weaponBase;
    private PlayerInput playerInput;
    private PlayerUI playerUI;
    public bool canLevelAbility;
    private Dictionary<string, Action<int>> abilityLevelMap = new Dictionary<string, Action<int>>();
    private List<string> abilityTypes = new List<string>();
    private void Awake()
    {
        weaponBase = GetComponentInChildren<WeaponBase>();
        playerInput = GetComponent<PlayerInput>();
        playerUI = GetComponent<PlayerUI>();

        canLevelAbility = true;

        abilityLevelMap["LevelQ"] = (level) => { weaponBase.qLevel += level; };
        abilityLevelMap["LevelW"] = (level) => { weaponBase.wLevel += level; };
        abilityLevelMap["LevelE"] = (level) => { weaponBase.eLevel += level; };
        abilityLevelMap["LevelR"] = (level) => { weaponBase.rLevel += level; };

        abilityTypes.AddRange(abilityLevelMap.Keys);
    }

    private void Update()
    {
        HandleAbilityLevelUI();

        foreach (var abilityType in abilityTypes)
        {
            HandleAbilityLeveling(abilityType);
        }
    }

    private void HandleAbilityLeveling(string abilityType)
    {
        if (CanLevelAbility(abilityType))
        {
            LevelUpAbility(abilityType);
        }
    }

    private void LevelUpAbility(string abilityType)
    {
        abilityLevelMap[abilityType](1);
        canLevelAbility = false;
    }    

    private bool CanLevelAbility(string abilityToLevel)
    {
        return playerInput.actions[abilityToLevel].triggered && canLevelAbility;
    }

    private void HandleAbilityLevelUI()
    {
        playerUI.qLevelText.text = weaponBase.qLevel.ToString();
        playerUI.wLevelText.text = weaponBase.wLevel.ToString();
        playerUI.eLevelText.text = weaponBase.eLevel.ToString();
        playerUI.rLevelText.text = weaponBase.rLevel.ToString();
    }
}
