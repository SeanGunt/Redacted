using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarDamageField : MonoBehaviour
{
    [SerializeField] private float damage;
    private GameObject player;
    private PlayerBase playerBase;
    private bool damagingPlayer;
    private void Awake()
    {
        player = GameManager.Instance.player;
        playerBase = player.GetComponent<PlayerBase>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            damagingPlayer = true;
            StartCoroutine(DoDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            damagingPlayer = false;
        }
    }

    private IEnumerator DoDamage()
    {
        float damageInterval = 0f;
        while (damagingPlayer)
        {
            damageInterval -= Time.deltaTime;
            if (damageInterval <= 0)
            {
                playerBase.TakeDamage(damage, Mathf.Log(playerBase.magicalResistance, 10000));
                damageInterval = 0.1f;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
}
