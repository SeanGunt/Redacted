using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
    private GameObject player;
    protected WeaponBase weaponBase;

    private void OnEnable()
    {
        player = GameManager.Instance.player;
        weaponBase = player.GetComponentInChildren<WeaponBase>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(weaponBase.ApplyDamage());
        }
    }
}
