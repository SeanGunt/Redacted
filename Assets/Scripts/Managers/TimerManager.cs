using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerManager : MonoBehaviour, IFreezable
{
    public float timer;
    public TextMeshProUGUI timerText;
    private bool frozen;

    private void Update()
    {
        if (frozen) return;
        timer -= Time.deltaTime;
        timerText.text = TimeSpan.FromSeconds(timer).ToString(@"m\:ss");
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
