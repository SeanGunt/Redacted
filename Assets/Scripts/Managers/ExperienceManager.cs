using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    private PlayerUI playerUI;
    private float exp = 0f;
    private float totalExp;
    protected float expTillNextLevel = 100f;
    protected int level = 1;

    private void Awake()
    {
        playerUI = GetComponent<PlayerUI>();
    }

    private void Update()
    {
        HandleLevel();
        HandleExpBar();
    }

    private void HandleLevel()
    {
        playerUI.levelText.text = level.ToString();
        if (exp >= expTillNextLevel)
        {
            level += 1;
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
