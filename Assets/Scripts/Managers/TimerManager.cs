using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerManager : MonoBehaviour, IFreezable
{
    public static TimerManager instance;
    public float timer;
    public TextMeshProUGUI timerText;
    [HideInInspector] public bool wonRun;
    private bool frozen;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (frozen) return;
        timer -= Time.deltaTime;
        timerText.text = TimeSpan.FromSeconds(timer).ToString(@"m\:ss");
        if (timer <= 0f)
        {
            wonRun = true;
        }
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
