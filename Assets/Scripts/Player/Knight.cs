using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : PlayerBase
{
    [SerializeField] private Sword sword;

    protected override void HandleQAbility()
    {
        if (CanUseAbility("QAttack", qCooldown, weaponBase.qLevel))
        {
            sword.abilityType = WeaponBase.AbilityType.Q;
            sword.HandleSwordAnims("Swipe");
            StartCoroutine(HandleQCooldown(sword.clips[1].length));
        }
    }

    protected override void HandleWAbility()
    {
        if (CanUseAbility("WAttack", wCooldown, weaponBase.wLevel))
        {
            sword.abilityType = WeaponBase.AbilityType.W;
            sword.HandleSwordAnims("Spin");
            StartCoroutine(HandleWCooldown(sword.clips[2].length));
        }
    }

    protected override void HandleEAbility()
    {
        if (CanUseAbility("EAttack", eCooldown, weaponBase.eLevel))
        {
            
        }
    }

    protected override void HandleRAbility()
    {
        if (CanUseAbility("RAttack", rCooldown, weaponBase.rLevel))
        {
           
        }
    }

    public void Dash()
    {
        Vector3 posToDash = GetMousePosition();
        Vector3 direction = (posToDash - transform.position).normalized;
        HandleRotation(posToDash, transform);
        rb.AddForce(direction * 25, ForceMode2D.Impulse);
    }
}
