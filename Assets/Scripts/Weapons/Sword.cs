using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponBase
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(ApplyDamage());
        }
    }

    public void HandleSwordAnims(string abilityName)
    {
        animator.SetTrigger(abilityName);
    }
}
