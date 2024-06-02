using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : PlayerBase
{
    [SerializeField] private Bow bow;

    protected override void HandleQAbility()
    {
        if (CanUseAbility("QAttack", qCooldown, weaponBase.qLevel))
        {
            bow.abilityType = WeaponBase.AbilityType.Q;
            bow.ShootArrow();
            StartCoroutine(HandleQCooldown(0.5f));
        }
    }
}
