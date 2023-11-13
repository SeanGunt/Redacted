using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardGuy : PlayerBase
{
    [SerializeField] private Staff staff;
    protected override void HandleQAbility() 
    {
        if (CanUseAbility("QAttack", qCooldown, weaponBase.qLevel))
        {
            staff.abilityType = WeaponBase.AbilityType.Q;
            staff.HandleStaffThrustAnim("Thrust");
            StartCoroutine(HandleQCooldown());
        }
    }

    protected override void HandleWAbility() 
    {

    }

    protected override void HandleEAbility() 
    {

    }

    protected override void HandleRAbility() 
    {

    }
}
