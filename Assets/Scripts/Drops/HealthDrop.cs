using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HealthDrop : DropsBase
{
    [SerializeField] private GameObject healParticlesGO;
    [SerializeField] private float amountToHeal;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
            Instantiate(healParticlesGO, player.transform.position, Quaternion.Euler(-90f, 0f, 0f), player.transform);
            playerBase.health =  Mathf.Min(playerBase.health + amountToHeal, playerBase.baseHealth);
            gameObject.SetActive(false);
        }
    
    }
}
