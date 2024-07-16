using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosionCircleTrigger : ExplosionCircleTrigger
{
    protected override void HandleDamageSelection(float abilityDamage)
    {
        base.HandleDamageSelection(weaponBase.ApplyEDamage());
    }
}
