using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShardTrigger : ProjectileTrigger
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(weaponBase.ApplyEDamage());
            Destroy(gameObject);
        }
    }
}
