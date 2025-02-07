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
            SFXManager.instance.PlayOneShotAtPoint(transform.position, pickupAudioClip);
            Instantiate(healParticlesGO, player.transform.position, Quaternion.Euler(-90f, 0f, 0f), player.transform);
            playerBase.health =  Mathf.Min(playerBase.health + (playerBase.baseHealth * 0.10f), playerBase.baseHealth);
            gameObject.SetActive(false);
        }
    
    }
}
