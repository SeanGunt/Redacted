using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileTrigger : MonoBehaviour
{
    protected GameObject player;
    protected PlayerBase playerBase;
    public float damage;

    private void Start()
    {
        player =  GameManager.Instance.player;
        playerBase =  player.GetComponent<PlayerBase>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerBase.health -= damage - (damage * Mathf.Log(playerBase.magicalResistance, 10000));
        }
    }
}
