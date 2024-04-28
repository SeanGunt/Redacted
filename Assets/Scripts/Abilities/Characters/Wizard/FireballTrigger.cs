using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballTrigger : ProjectileTrigger
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            player = GameManager.Instance.player;
            weaponBase = player.GetComponentInChildren<WeaponBase>();
            damagable.TakeDamage(weaponBase.ApplyQDamage());
        }
    }
}
