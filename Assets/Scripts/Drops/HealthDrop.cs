using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : DropsBase
{
    [SerializeField] private float amountToHeal;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
            playerBase.health =  Mathf.Min(playerBase.health + amountToHeal, playerBase.baseHealth);
            gameObject.SetActive(false);
        }
    
    }
}
