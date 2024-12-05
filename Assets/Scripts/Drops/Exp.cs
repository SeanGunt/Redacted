using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : DropsBase
{
    [SerializeField] private float expAmount;
    private ExperienceManager experienceManager;
    private void Start()
    {
        experienceManager = player.GetComponent<ExperienceManager>();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
            experienceManager.IncreaseExperience(expAmount);
            gameObject.SetActive(false);
        }
    }
}
