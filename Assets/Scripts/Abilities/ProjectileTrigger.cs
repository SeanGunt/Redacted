using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
    protected GameObject player;
    protected WeaponBase weaponBase;
    protected float damageToApply;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            player = GameManager.Instance.player;
            weaponBase = player.GetComponentInChildren<WeaponBase>();
            HandleDamageSelection(damageToApply);
            damagable.TakeDamage(damageToApply);
            HandleOtherOnHitLogic(other);
        }
    }

    protected virtual void HandleOtherOnHitLogic(Collider2D other)
    {

    }

    protected virtual void HandleDamageSelection(float abilityDamage)
    {
        damageToApply = abilityDamage;
    }
}
