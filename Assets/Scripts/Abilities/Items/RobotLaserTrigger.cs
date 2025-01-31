using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLaserTrigger : ProjectileTrigger
{
    protected override void HandleDamageSelection(float abilityDamage)
    {
        damageToApply = 200f;
    }
}
