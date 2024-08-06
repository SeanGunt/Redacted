using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : PlayerBase
{
    [SerializeField] private Sword sword;
    [SerializeField] private Shield shield;

    protected override void HandleQAbility()
    {
        if (CanUseAbility("QAttack", qCooldown, weaponBase.qLevel))
        {
            sword.HandleSwordAnims("Swipe");
            StartCoroutine(HandleQCooldown(sword.clips[1].length));
        }
    }

    protected override void HandleWAbility()
    {
        if (CanUseAbility("WAttack", wCooldown, weaponBase.wLevel))
        {
            sword.HandleSwordAnims("Spin");
            StartCoroutine(HandleWCooldown(sword.clips[2].length));
        }
    }

    protected override void HandleEAbility()
    {
        if (CanUseAbility("EAttack", eCooldown, weaponBase.eLevel))
        {
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
        sword.ChangeEMultiplier(1.5f);
        canUseAbility = false;
        weaponBase.canRotate = false;
        canMove = false;
        state = State.idle;
        isInvincible = true;
        yield return new WaitForSeconds(0.5f);
        canFlipSprite = true;
        sword.DisableWeaponCollider();
        sword.ChangeEMultiplier(1.0f);
        canUseAbility = true;
        weaponBase.canRotate = true;
        canMove = true;
        isInvincible = false;
    }

    private IEnumerator ProtectAndServe()
    {
        shield.HandleShieldAnims("ShieldSpin", true);
        qCooldownAmount *= 0.5f;
        wCooldownAmount *=  0.5f;
        eCooldownAmount *=  0.5f;
        damageReduction += 0.75f;
        speed /= 2;
        animator.speed -= 0.5f;
        yield return new WaitForSeconds(10);
        shield.HandleShieldAnims("ShieldSpin", false);
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
