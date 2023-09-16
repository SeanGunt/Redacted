using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityManager : MonoBehaviour
{
    private WeaponBase weaponBase;
    private ExperienceManager experienceManager;
    private PlayerInput playerInput;
    private PlayerUI playerUI;
    public bool canLevelAbility;
    public enum AbilityType
    {
        LevelQ, LevelE, LevelW, LevelR
    }

    private void Awake()
    {
        weaponBase = GetComponentInChildren<WeaponBase>();
        playerInput = GetComponent<PlayerInput>();
        playerUI = GetComponent<PlayerUI>();
        experienceManager = GetComponent<ExperienceManager>();

        canLevelAbility = true;
    }

    private void Update()
    {

        foreach (var abilityType in Enum.GetNames(typeof(AbilityType)))
        {
            HandleAbilityLeveling(abilityType);
            HandleAbilityLevelingUI(abilityType);
        }
    }

    private void HandleAbilityLeveling(string abilityType)
    {
        if (CanLevelAbility(abilityType))
        {
            LevelUpAbility(abilityType);
        }
    }

    private void HandleAbilityLevelingUI(string abilityType)
    {
        playerUI.levelIconsDict[abilityType].SetActive(IsUIForLevelActive(abilityType));
    }

    private void LevelUpAbility(string abilityType)
    {
        int maxLevel = GetMaxAbilityLevel(abilityType);

        switch (abilityType)
        {
            case "LevelQ":
                if (weaponBase.qLevel < maxLevel)
                {
                    weaponBase.qLevel += 1;
                    playerUI.qLevelText.text = weaponBase.qLevel.ToString();
                    canLevelAbility = false;
                }
                break;
            case "LevelW":
                if (weaponBase.wLevel < maxLevel)
                {
                    weaponBase.wLevel += 1;
                    playerUI.wLevelText.text = weaponBase.wLevel.ToString();
                    canLevelAbility = false;
                }
                break;
            case "LevelE":
                if (weaponBase.eLevel < maxLevel)
                {
                    weaponBase.eLevel += 1;
                    playerUI.eLevelText.text = weaponBase.eLevel.ToString();
                    canLevelAbility = false;
                }
                break;
            case "LevelR":
                if (weaponBase.rLevel < maxLevel)
                {
                    weaponBase.rLevel += 1;
                    playerUI.rLevelText.text = weaponBase.rLevel.ToString();
                    canLevelAbility = false;
                }
                break;
        }
    }

    private bool CanLevelAbility(string abilityToLevel)
    {
        int maxLevel = GetMaxAbilityLevel(abilityToLevel);

        return playerInput.actions[abilityToLevel].triggered && canLevelAbility &&
               GetAbilityLevel(abilityToLevel) < maxLevel &&
               (abilityToLevel != "LevelR" || experienceManager.level >= maxLevel * 5 - 4);
    }

    private bool IsUIForLevelActive(string abilityToLevel)
    {
        int maxLevel = GetMaxAbilityLevel(abilityToLevel);

        return canLevelAbility &&
               GetAbilityLevel(abilityToLevel) < maxLevel &&
               (abilityToLevel != "LevelR" || experienceManager.level >= maxLevel * 5 - 4);
    }

    private int GetMaxAbilityLevel(string abilityType)
    {
        if (abilityType == "LevelR")
        {
            if (experienceManager.level >= 16) return 3;
            if (experienceManager.level >= 11) return 2;
            if (experienceManager.level >= 6) return 1;
        }
        else
        {
            return 5;
        }

        return 0;
    }

    private int GetAbilityLevel(string abilityType)
    {
        switch (abilityType)
        {
            case "LevelQ":
                return weaponBase.qLevel;
            case "LevelW":
                return weaponBase.wLevel;
            case "LevelE":
                return weaponBase.eLevel;
            case "LevelR":
                return weaponBase.rLevel;
            default:
                return 0;
        }
    }
}

