using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    [SerializeField] private float damage;
    private GameObject player;
    private PlayerBase playerBase;
    private bool damagingPlayer;

    private void Start()
    {
        player =  GameManager.Instance.player;
        playerBase = player.GetComponent<PlayerBase>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            damagingPlayer = true;
            StartCoroutine(DamagePlayer());
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            damagingPlayer = false;
        }
    }

    private IEnumerator DamagePlayer()
    {
        float damageInterval = 0f;
        while (damagingPlayer)
        {
            damageInterval -= Time.deltaTime;
            if (damageInterval <= 0)
            {
                playerBase.TakeDamage(damage, Mathf.Log(playerBase.physicalResistance, 10000));
                damageInterval = 0.2f;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
}
