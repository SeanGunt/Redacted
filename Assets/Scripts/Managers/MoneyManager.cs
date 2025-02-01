using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MoneyManager : MonoBehaviour, IFreezable
{
    public static MoneyManager instance;
    [HideInInspector] public float money;
    public float moneyPerSecondMultiplier;
    public TextMeshProUGUI timerText;
    Scene currentScene;
    private bool frozen;

    private void Awake()
    {
        instance = this;
        currentScene = SceneManager.GetActiveScene();
    }
    private void Update()
    {
        if (currentScene == SceneManager.GetSceneByName("MainMenu"))
        {
            money = Mathf.Infinity;
        }
        else
        {
            if (!frozen)
            {
                money += Time.deltaTime * moneyPerSecondMultiplier;
            }
        }
        timerText.text = money.ToString("n0");
    }

    public void AddMoney(int moneyToAdd)
    {
        money += moneyToAdd;
    }

    public void HandleOnFreeze()
    {
        frozen = true;
    }

    public void HandleOnUnfreeze()
    {
        frozen = false;
    }
}
