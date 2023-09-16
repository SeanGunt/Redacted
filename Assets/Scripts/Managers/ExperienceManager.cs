using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    private PlayerUI playerUI;
    private PlayerBase playerBase;
    private AbilityManager abilityManager;
    private float exp = 0f;
    private float totalExp;
    private float expTillNextLevel = 100f;
    [HideInInspector] public int level = 1;
    private readonly int maxLevel = 20;

    private void Awake()
    {
        playerUI = GetComponent<PlayerUI>();
        playerBase = GetComponent<PlayerBase>();
        abilityManager = GetComponent<AbilityManager>();
    }

    private void Update()
    {
        HandleLevel();
        HandleExpBar();
    }

    private void HandleLevel()
    {
        playerUI.levelText.text = level.ToString();
        if (exp >= expTillNextLevel && level < maxLevel)
        {
            level += 1;
            playerBase.HandleOnlevel();
            abilityManager.canLevelAbility = true;
            exp = 0f;
            expTillNextLevel += 100f;
        }
    }

    private void HandleExpBar()
    {
        float ratio = 1 / expTillNextLevel;
        playerUI.expBar.fillAmount = exp * ratio;
    }

    public void IncreaseExperience(float amountToIncrease)
    {
        exp += amountToIncrease;
    }
}
