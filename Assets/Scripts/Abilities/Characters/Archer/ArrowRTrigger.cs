using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRTrigger : ArrowTrigger
{
    protected override void HandleDamageSelection(float abilityDamage)
    {
        base.HandleDamageSelection(weaponBase.ApplyRDamage());
    }
}
