using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : PlayerBase
{
    [SerializeField] private Staff staff;
    protected override void HandleQAbility() 
    {
        if (CanUseAbility("QAttack", qCooldown, weaponBase.qLevel))
        {
            staff.abilityType = WeaponBase.AbilityType.Q;
            HandleRotation(GetMousePosition(), transform);
            staff.HandleStaffAnims("Fireball");
            StartCoroutine(HandleQCooldown(staff.clips[1].length));
        }
    }

    protected override void HandleWAbility() 
    {
        if (CanUseAbility("WAttack", wCooldown, weaponBase.wLevel))
        {
            staff.abilityType = WeaponBase.AbilityType.W;
            HandleRotation(GetMousePosition(), transform);
            staff.HandleStaffAnims("Lightning");
            StartCoroutine(HandleWCooldown(staff.clips[2].length));
        }
    }

    protected override void HandleEAbility() 
    {
        if (CanUseAbility("EAttack", eCooldown, weaponBase.eLevel))
        {
            staff.abilityType = WeaponBase.AbilityType.E;
            HandleRotation(GetMousePosition(), transform);
            staff.Iceball();
            StartCoroutine(HandleECooldown(9f));
        }
    }

    protected override void HandleRAbility() 
    {
        if (CanUseAbility("RAttack", rCooldown, weaponBase.rLevel))
        {
            staff.abilityType = WeaponBase.AbilityType.R;
            HandleRotation(GetMousePosition(), transform);
            staff.BlackHole();
            StartCoroutine(HandleRCooldown(5f));
        }
    }
}
