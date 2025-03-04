using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    private WeaponBase weaponBase;
    private ExperienceManager experienceManager;
    private PlayerInput playerInput;
    private PlayerUI playerUI;
    public int numOfLevelsAvailable;
    [SerializeField] private AudioClip levelUpAbilityAudioClip;
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

        numOfLevelsAvailable = 1;
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

    public void LevelUpAbility(string abilityType)
    {
        int maxLevel = GetMaxAbilityLevel(abilityType);
        GameManager.Instance.audioSource.PlayOneShot(levelUpAbilityAudioClip);

        switch (abilityType)
        {
            case "LevelQ":
                if (weaponBase.qLevel < maxLevel)
                {
                    weaponBase.qLevel += 1;
                    playerUI.qLevelText.text = weaponBase.qLevel.ToString();
                    numOfLevelsAvailable -= 1;
                }
                break;
            case "LevelW":
                if (weaponBase.wLevel < maxLevel)
                {
                    weaponBase.wLevel += 1;
                    playerUI.wLevelText.text = weaponBase.wLevel.ToString();
                    numOfLevelsAvailable -= 1;
                }
                break;
            case "LevelE":
                if (weaponBase.eLevel < maxLevel)
                {
                    weaponBase.eLevel += 1;
                    playerUI.eLevelText.text = weaponBase.eLevel.ToString();
                    numOfLevelsAvailable -= 1;
                }
                break;
            case "LevelR":
                if (weaponBase.rLevel < maxLevel)
                {
                    weaponBase.rLevel += 1;
                    playerUI.rLevelText.text = weaponBase.rLevel.ToString();
                    numOfLevelsAvailable -= 1;
                }
                break;
        }
    }

    private void HandleOnLevel(int abilityLevel, TextMeshProUGUI abilityText)
    {
        abilityLevel += 1;
        abilityText.text = abilityLevel.ToString();
        numOfLevelsAvailable -= 1;
    }

    private bool CanLevelAbility(string abilityToLevel)
    {
        int maxLevel = GetMaxAbilityLevel(abilityToLevel);

        return playerInput.actions[abilityToLevel].triggered && numOfLevelsAvailable >= 1 &&
               GetAbilityLevel(abilityToLevel) < maxLevel &&
               (abilityToLevel != "LevelR" || experienceManager.level >= maxLevel * 5 - 4);
    }

    private bool IsUIForLevelActive(string abilityToLevel)
    {
        int maxLevel = GetMaxAbilityLevel(abilityToLevel);

        return numOfLevelsAvailable >= 1 &&
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

