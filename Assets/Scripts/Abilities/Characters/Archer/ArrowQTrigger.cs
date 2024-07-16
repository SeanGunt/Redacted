using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowQTrigger : ArrowTrigger
{
    protected override void HandleDamageSelection(float abilityDamage)
    {
        base.HandleDamageSelection(weaponBase.ApplyQDamage());
    }
}
