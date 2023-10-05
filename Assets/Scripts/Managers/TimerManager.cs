using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerManager : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI timerText;

    private void Update()
    {
        timer -= Time.deltaTime;
         timerText.text = TimeSpan.FromSeconds(timer).ToString(@"m\:ss");
    }
}
