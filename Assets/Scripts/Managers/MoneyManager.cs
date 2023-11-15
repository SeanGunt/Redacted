using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public float money;
    public float moneyPerSecondMultiplier;
    public TextMeshProUGUI timerText;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        money += Time.deltaTime * moneyPerSecondMultiplier;
        timerText.text = money.ToString("n0");
    }

    public void AddMoney(int moneyToAdd)
    {
        money += moneyToAdd;
    }
}
