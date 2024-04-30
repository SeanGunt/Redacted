using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShardTrigger : ProjectileTrigger
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void HandleOtherOnHitLogic(Collider2D other)
    {
        Destroy(this.gameObject);
    }

    protected override void HandleDamageSelection(float abilityDamage)
    {
        base.HandleDamageSelection(weaponBase.ApplyEDamage());
    }
}
