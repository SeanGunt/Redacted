using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeExplosionCircleTrigger : ExplosionCircleTrigger
{
    protected override void HandleDamageSelection(float abilityDamage)
    {
        base.HandleDamageSelection(weaponBase.ApplyRDamage());
    }
}
