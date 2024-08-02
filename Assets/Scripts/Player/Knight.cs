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
            sword.abilityType = WeaponBase.AbilityType.E;
            StartCoroutine(HandleDashAbility());
            sword.HandleSwordAnims("SlashAndDash");
            StartCoroutine(HandleECooldown(0.5f + sword.clips[3].length));
        }
    }

    protected override void HandleRAbility()
    {
        if (CanUseAbility("RAttack", rCooldown, weaponBase.rLevel))
        {
           StartCoroutine(ProtectAndServe());
           StartCoroutine(HandleRCooldown(10f));
        }
    }

    private IEnumerator HandleDashAbility()
    {
        yield return new WaitForSeconds(sword.clips[3].length);
        Dash();
        canFlipSprite = false;
        sword.EnableWeaponCollider();
        canUseAbility = false;
        weaponBase.canRotate = false;
        canMove = false;
        state = State.idle;
        isInvincible = true;
        yield return new WaitForSeconds(0.5f);
        canFlipSprite = true;
        sword.DisableWeaponCollider();
        canUseAbility = true;
        weaponBase.canRotate = true;
        canMove = true;
        isInvincible = false;
    }

    private IEnumerator ProtectAndServe()
    {
        qCooldownAmount *= 0.5f;
        wCooldownAmount *=  0.5f;
        eCooldownAmount *=  0.5f;
        damageReduction += 0.75f;
        speed /= 2;
        animator.speed -= 0.5f;
        yield return new WaitForSeconds(10);
        qCooldownAmount /=  0.5f;
        wCooldownAmount /=  0.5f;
        eCooldownAmount /=  0.5f;
        damageReduction -= 0.75f;
        speed *= 2;
        animator.speed += 0.5f;
    }

    public void Dash()
    {
        Vector3 posToDash = GetMousePosition();
        Vector3 direction = (posToDash - transform.position).normalized;
        rb.AddForce(direction * 25, ForceMode2D.Impulse);
    }
}
